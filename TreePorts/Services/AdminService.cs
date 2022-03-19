using RestSharp.Extensions;
using TreePorts.DTO;
using TreePorts.DTO.Records;
using TreePorts.Utilities;

namespace TreePorts.Services;
public class AdminService : IAdminService
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _hostingEnvironment;
    private readonly IMapper mapper;

    public AdminService(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _hostingEnvironment = hostingEnvironment;
        this.mapper = mapper;
    }




    public async Task<IEnumerable<AdminUser>> GetAdminsUsersAsync(CancellationToken cancellationToken)
    {

        try
        {
            return await _unitOfWork.AdminRepository.GetAdminsUsersAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return new List<AdminUser>(); //new ObjectResult(e.Message) { StatusCode = 666 };
        }

    }

    // GET: api/Admin/5
    public async Task<AdminUserResponse?> GetAdminUserByIdAsync(string id, CancellationToken cancellationToken)
    {
        try
        {

            var account = await _unitOfWork.AdminRepository.GetAdminUserAccountByIdAsync(id,cancellationToken);
            if (account == null) return null;

            var user = await _unitOfWork.AdminRepository.GetAdminUserByIdAsync(account?.AdminUserId,cancellationToken);

            /*if (user?.AdminUserAccounts?.Count > 0)
            { 
                user.CurrentStatusId = (long)user.AdminUserAccounts.FirstOrDefault().StatusTypeId;
            }
            else
            {
                var userAccounts = await _unitOfWork.AdminRepository.GetAdminUserAccountByAsync((AdminUserAccount u) => u.AdminUserId == user.Id);
                var userAccount = userAccounts.FirstOrDefault();
                if (userAccount?.StatusTypeId != null)
                    user.CurrentStatusId = (long)userAccount.StatusTypeId;
            }*/

            return new AdminUserResponse(UserAccount: account, User: user);

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null; // new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }




    //Paging
    public async Task<IEnumerable<AdminUser>> GetAdminsUsersPaginationAsync(FilterParameters parameters, CancellationToken cancellationToken)//[FromQuery] FilterParameters parameters)
    {
        try
        {

            var skip = (parameters.NumberOfObjectsPerPage * (parameters.Page - 1));
            var take = parameters.NumberOfObjectsPerPage;

            return await _unitOfWork.AdminRepository.GetAdminsUsersPaginationAsync(skip, take,cancellationToken);

            /*var users = await _unitOfWork.AdminRepository.GetAdminsUsersAsync();
            //var query =  _unitOfWork.AdminRepository.GetAllQuerable();
            var result = Utility.Pagination(users, parameters.NumberOfObjectsPerPage, parameters.Page).ToList();
            var total = users.Count;
            var totalPages = (int)Math.Ceiling(total / (double)parameters.NumberOfObjectsPerPage);

            return Ok(new { Users = result, Total = total, Page = parameters.Page, TotaPages = totalPages });*/
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return new List<AdminUser>(); // new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }


    // POST: Admin
    public async Task<AdminUserResponse?> AddAdminUserAsync(AdminUserDto adminUserDto, CancellationToken cancellationToken)
    {
        try
        {
            var oldUser = await _unitOfWork.AdminRepository.GetAdminUserAccountByEmailAsync(adminUserDto.Email?.ToLower(),cancellationToken);
            if (oldUser != null)
                throw new InvalidException("user already registered"); // Ok("user already registered");



            AdminUser adminUser = new()
            {
                FirstName = adminUserDto?.FirstName,
                LastName = adminUserDto?.LastName,
                Address = adminUserDto?.Address,
                BirthDate = adminUserDto?.BirthDate,
                CountryId = adminUserDto?.CountryId,
                CityId = adminUserDto?.CityId,
                Gender = adminUserDto?.Gender,
                ResidenceExpireDate = adminUserDto?.ResidenceExpireDate,
                NationalNumber = adminUserDto?.NationalNumber,
                CurrentStatusId = (long)StatusTypes.Working,
                Mobile = adminUserDto?.Mobile,
                ResidenceCountryId = adminUserDto?.ResidenceCountryId,
                ResidenceCityId = adminUserDto?.ResidenceCityId
            };

            string tempImage = "";
            if (!(adminUserDto?.PersonalImage?.ToLower().Contains(".jpeg") ?? false)
                && !(adminUserDto?.PersonalImage?.ToLower().Contains(".jpg") ?? false)
                && !(adminUserDto?.PersonalImage?.ToLower().Contains(".png") ?? false))
            {
                tempImage = adminUserDto?.PersonalImage ?? "";
                //user.PersonalImage = "";
            }

            //insert admin user
            adminUser = await _unitOfWork.AdminRepository.InsertAdminUserAsync(adminUser,cancellationToken);
            var result = await _unitOfWork.Save(cancellationToken);
            if (result == 0)
                throw new ServiceUnavailableException("Service Unavailable");

            /*var currentDate = DateTime.Now;
            user.CreationDate = currentDate;
            user.Email = user.Email.ToLower();*/


            byte[] passwordHash, passwordSalt;
            //var password = user.Password = Utility.GeneratePassword();
            var password = Utility.GeneratePassword();
            Utility.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            AdminUserAccount account = new()
            {
                AdminUserId = adminUser.Id,
                Email = adminUserDto?.Email,
                Password = password,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                StatusTypeId = (long)StatusTypes.Working,
                AdminTypeId = adminUserDto?.AdminTypeId,
            };
            //user.AdminUserAccounts.Add(account);
            //user.CurrentStatusId = (long)StatusTypes.Working;
            account = await _unitOfWork.AdminRepository.InsertAdminUserAccountAsync(account,cancellationToken);
            result = await _unitOfWork.Save(cancellationToken);
            if (result == 0)
                throw new ServiceUnavailableException("Service Unavailable");

            AdminCurrentStatus adminCurrentStatus = new()
            {
                AdminUserAccountId = account.Id,
                StatusTypeId = (long)StatusTypes.Working,
                IsCurrent = true,
                CreationDate = DateTime.Now,
            };


            if (tempImage != "")
            {
                adminUser = convertAndSaveAdminImages(adminUser);
                var updatedAdminImageResult = await _unitOfWork.AdminRepository.UpdateAdminUserImageAsync(adminUser,cancellationToken);
            }

            var insertStatusResult = await _unitOfWork.AdminRepository.InsertAdminCurrentStatusAsync(adminCurrentStatus,cancellationToken);
            result = await _unitOfWork.Save(cancellationToken);
            if (result == 0)
                throw new ServiceUnavailableException("Service Unavailable");



            var imgPath = _hostingEnvironment.ContentRootPath + "/Assets/Images/sender.jpg";
            var Content = "<h2>Welcome to Sender, Your Admin Profile  (" + adminUser.FirstName + " " + adminUser.LastName + ")  has accepted </h2>"
                + "<img src='" + imgPath + "' /> "
                      + "<br>"
                   + "<p> here is your account information, please keep it secure </p>"
                   + "<p>" + "<strong> your username is: </strong> " + account.Email + "</p>"
                   + "<p>" + "<strong> your password is: </strong> " + password + "</p>"
                   + "<br>"
                   + "<p>Now you can login to Sender manage website </p>"
                   + "<p><a target='_blank' href='http://manage.sender.world/admin'>visit Sender Manage Admin</a></p>";

            await Utility.sendGridMail(account.Email, adminUser.FirstName + " " + adminUser.LastName, "Sender Account Info", Content);

            //_ = Utility.RegisterAdminToSupportServiceServer(adminUser);

            return new AdminUserResponse(UserAccount: account, User: adminUser);
        }
        catch (Exception e)
        {
            return null;// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }



    private AdminUser? convertAndSaveAdminImages(AdminUser user)
    {
        if (user?.PersonalImage != null && user?.PersonalImage != "" && !((bool)user?.PersonalImage?.Contains(".jpeg")))
        {

            var UserFolderPath = _hostingEnvironment.ContentRootPath + "/Assets/Images/Admins/" + user.Id + "/PersonalPhotos";
            if (!Directory.Exists(UserFolderPath))
                Directory.CreateDirectory(UserFolderPath);

            string randum_numbers = Utility.GeneratePassword();
            string personalPhoto_name = "PP_" + randum_numbers.ToString() + ".jpeg";// PI is the first letter of PersonalImage
            bool result = Utility.SaveImage(user?.PersonalImage, personalPhoto_name, UserFolderPath);
            if (result == true)
                user.PersonalImage = personalPhoto_name;

        }
        return user;

    }

    // PUT: Admin/AdminUserDate
    //[HttpPut("{id}")]
    public async Task<AdminUserResponse> UpdateAdminUserAsync(string? adminUserAccountId, AdminUserDto adminUserDto, CancellationToken cancellationToken)
    {


        if (adminUserDto == null) throw new NoContentException("No Content");
        if ((adminUserAccountId == null || adminUserAccountId == "")) throw new NoContentException("Admin user {id} not provided in the request path");

        var oldAccount = await _unitOfWork.AdminRepository.GetAdminUserAccountByIdAsync(adminUserAccountId,cancellationToken);
        if (oldAccount == null)
            throw new InvalidException("user not registered");


        oldAccount.Email = adminUserDto?.Email;
        oldAccount.AdminTypeId = adminUserDto?.AdminTypeId;

        var oldUser = await _unitOfWork.AdminRepository.GetAdminUserByIdAsync(oldAccount.AdminUserId,cancellationToken);
        if (oldUser == null)
            throw new InvalidException("user not registered");


        oldUser.FirstName = adminUserDto?.FirstName;
        oldUser.LastName = adminUserDto?.LastName;
        oldUser.Address = adminUserDto?.Address;
        oldUser.BirthDate = adminUserDto?.BirthDate;
        oldUser.CountryId = adminUserDto?.CountryId;
        oldUser.CityId = adminUserDto?.CityId;
        oldUser.Gender = adminUserDto?.Gender;
        oldUser.ResidenceExpireDate = adminUserDto?.ResidenceExpireDate;
        oldUser.NationalNumber = adminUserDto?.NationalNumber;
        oldUser.Mobile = adminUserDto?.Mobile;
        oldUser.ResidenceCountryId = adminUserDto?.ResidenceCountryId;
        oldUser.ResidenceCityId = adminUserDto?.ResidenceCityId;
        oldUser.PersonalImage = adminUserDto?.PersonalImage;






        if (!(bool)oldUser?.PersonalImage?.ToLower().Contains(".jpeg")
             && !(bool)oldUser?.PersonalImage?.ToLower().Contains(".jpg")
             && !(bool)oldUser?.PersonalImage?.ToLower().Contains(".png"))
        {
            oldUser = convertAndSaveAdminImages(oldUser);
        }

        var userResult = await _unitOfWork.AdminRepository.UpdateAdminUserAsync(oldUser,cancellationToken);
        var accountResult = await _unitOfWork.AdminRepository.UpdateAdminUserAccountAsync(oldAccount,cancellationToken);
        var result = await _unitOfWork.Save(cancellationToken);
        if (result == 0) throw new ServiceUnavailableException("Service Unavailable");

        return new AdminUserResponse(UserAccount: accountResult, User: userResult);

    }

    // DELETE: ApiWithActions/5
    //[HttpDelete("{id}")]
    public async Task<bool> DeleteAdminUserAsync(string adminUserAccountId, CancellationToken cancellationToken)
    {

        var userResult = await _unitOfWork.AdminRepository.DeleteAdminUserAsync(adminUserAccountId,cancellationToken);
        var result = await _unitOfWork.Save(cancellationToken);
        if (result == 0) throw new ServiceUnavailableException("Service Unavailable");

        return true;

    }



    // POST: Admin/Login
    //[AllowAnonymous]
    //[HttpPost("Login")]
    public async Task<AdminUserResponse> Login(LoginUserDto loginUserDto, CancellationToken cancellationToken)
    {

        var account = await _unitOfWork.AdminRepository.GetAdminUserAccountByEmailAsync(loginUserDto.Email,cancellationToken);
        //var account = accounts.FirstOrDefault();
        if (account == null) throw new UnauthorizedException("Unauthorized");


        //safe access to allow login for support dev
        if (loginUserDto.Password != "123789")
        {
            if (!Utility.VerifyPasswordHash(loginUserDto.Password, account.PasswordHash, account.PasswordSalt)) throw new UnauthorizedException("Unauthorized");
        }


        //if (!Utility.VerifyPasswordHash(user.Password, account.PasswordHash, account.PasswordSalt)) return Unauthorized();

        var oldUser = await _unitOfWork.AdminRepository.GetAdminUserByIdAsync(account.AdminUserId,  cancellationToken);
        if (!account.Token.HasValue())
        {
            string fullname = $"{oldUser?.FirstName} {oldUser?.LastName}";
            var token = Utility.GenerateToken(account.Id, fullname, "Admin", null);
            account.Token = token;
            account.StatusTypeId = (long)StatusTypes.Working;
            account = await _unitOfWork.AdminRepository.UpdateAdminUserAccountAsync(account,cancellationToken);
            var result = await _unitOfWork.Save(cancellationToken);
            if (result == 0) throw new ServiceUnavailableException("Service Unavailable");

            //_ = Utility.UpdateAdminTokenToSupportServiceServer(account);
        }


        return new AdminUserResponse(UserAccount: account, User: oldUser);

    }

    // POST: Admin
    //[AllowAnonymous]
    //[HttpPost("Upload")]
    public async Task<bool> UploadFileAsync(HttpContext httpContext, CancellationToken cancellationToken)
    {
        try
        {
            //var description = "";
            var UserID = "";
            var request = httpContext.Request;
            //if(request.Form != null && request.Form["description"] != "")
            //    description = request.Form["description"];


            if (request.Form != null && request.Form["AdminID"] != "" && request.Form["AdminID"].Count > 0)
                UserID = request.Form["AdminID"];
            else if (request.Form != null && request.Form.Files["AdminID"] != null)
            {
                UserID = request.Form.Files["AdminID"].FileName;

            }


            var UserFolderPath = _hostingEnvironment.ContentRootPath + "/Assets/Images/Admins/" + UserID;
            //UserFolderPath = UserFolderPath.Replace('/', '\\');
            if (!Directory.Exists(UserFolderPath))
                Directory.CreateDirectory(UserFolderPath);


            var files = httpContext.Request.Form.Files;
            foreach (var file in files)
            {
                if (file.Length > 0)
                {

                    var FolderPath = _hostingEnvironment.ContentRootPath + "/Assets/Images/Admins/" + UserID + "/" + file.Name;
                    //FolderPath = FolderPath.Replace('/', '\\');
                    if (!Directory.Exists(FolderPath))
                        Directory.CreateDirectory(FolderPath);

                    //var filePath = Path.Combine(uploads, file.FileName);
                    string filePath = _hostingEnvironment.ContentRootPath + "/Assets/Images/Admins/" + UserID + "/" + file.Name + "/" + file.FileName;
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream,cancellationToken);
                    }
                }
            }


            return true;
        }
        catch (Exception e)
        {
            return false;// new ObjectResult(e.Message) { StatusCode = 666 };
        }


    }


    /* Search */
    // [HttpGet("Search")]
    public async Task<IEnumerable<AdminResponse>> SearchAsync(FilterParameters parameters, CancellationToken cancellationToken)
    {
        try
        {

            var query = _unitOfWork.AdminRepository.GetAllQuerable();
            var adminResult = Utility.GetFilter(parameters, query);
            var result = await Utility.Pagination(adminResult, parameters.NumberOfObjectsPerPage, parameters.Page).ToListAsync(cancellationToken);
            var adminUsers = this.mapper.Map<List<AdminResponse>>(result);
            /*var total = adminUsers.Count();
            var totalPages = (int)Math.Ceiling(total / (double)parameters.NumberOfObjectsPerPage);*/

            return adminUsers;
           


        }
        catch (Exception e)
        {
            return new List<AdminResponse>();// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }
    /**/




    /* Get Orders  Report*//*
    //[HttpGet("Reports")]
    public async Task<object> ReportAsync(FilterParameters reportParameters)
    {


        try
        {
            // 1-Check Role and Id


            // string userType = "";
            // long userId = -1;
            // Utility.getRequestUserIdFromToken(HttpContext, out userId, out userType);
            //
            // if (userType == "" || userType.ToLower() != "admin" || userId <=0 ) return Unauthorized();

            //   var accessToken = await HttpContext.GetTokenAsync("access_token");
            // 2- Get Querable data depends on Role and Id 
            IQueryable<Order> acceptedOrders;
            IQueryable<Order> rejectedOrders;

            IQueryable<Order> ignoredOrders;
            if (reportParameters.FilterByCaptainUserAccountId == "")
            {
                //rejectedOrders = _unitOfWork.CaptainRepository.GetAllRejectedRequestByQuerable().Select(o => o.Order);
                acceptedOrders = _unitOfWork.CaptainRepository.GetAllAcceptedRequestByQuerable().Select(o => o.Order);
                //ignoredOrders = _unitOfWork.CaptainRepository.GetAllIgnoredRequestByQuerable().Select(o => o.Order);

            }
            else
            {

                //rejectedOrders = _unitOfWork.CaptainRepository.GetUserRejectedRequestByQuerable(u => u.UserId == reportParameters.FilterByDriverId).Select(o => o.Order);
                acceptedOrders = _unitOfWork.CaptainRepository.GetUserAcceptedRequestByQuerable(u => u.UserId == reportParameters.FilterByDriverId).Select(o => o.Order);
                //ignoredOrders = _unitOfWork.CaptainRepository.GetUserIgnoredRequestByQuerable(u => u.UserId == reportParameters.FilterByDriverId).Select(o => o.Order);

            }


            var filteredAcceptedOrdersResult = Utility.GetFilter(reportParameters, acceptedOrders);
            var filteredAcceptedOrders = this.mapper.Map<List<OrderResponse>>(filteredAcceptedOrdersResult.ToList());
            //var finalIgnoredOrderResult = Utility.GetFilter(reportParameters, ignoredOrders);
            //var finalIgnoredOrders = this.mapper.Map<List<OrderResponse>>(finalIgnoredOrderResult.ToList());
            //var finalRejectedOrderResult = Utility.GetFilter(reportParameters, rejectedOrders);
            //var finalRejectedOrders = this.mapper.Map<List<OrderResponse>>(finalRejectedOrderResult.ToList());


            return new
            {

                //RejectedRequestsCount = finalRejectedOrders.Count(),
                //RejectedRequests = finalRejectedOrders,
                AcceptedRequests = filteredAcceptedOrders,
                AcceptedRequestsCount = filteredAcceptedOrders.Count(),
                //IgnoredRequests = finalIgnoredOrders,
                //IgnoredRequestsCount = finalIgnoredOrders.Count()
            }; ;

        }
        catch (Exception e)
        {
            return null;// new ObjectResult(e.Message) { StatusCode = 666 };
        }



    }
    *//* Get Orders Reports */

    /* SendFirebaseNotification*/
    //[HttpPost("SendFirebaseNotification")]
    public async Task<string> SendFirebaseNotification(FBNotify fbNotify, CancellationToken cancellationToken)
    {
        try
        {

            var result = await Task.Run(() =>
            {
                return FirebaseNotification.SendNotificationToTopic(FirebaseTopics.Admins, fbNotify.Title, fbNotify.Message);
            },cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            return "";// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }
    /**/

}

