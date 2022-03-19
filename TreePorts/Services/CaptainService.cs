using Microsoft.AspNetCore.SignalR;
using RestSharp.Extensions;
using TreePorts.DTO;
using TreePorts.DTO.Records;
using TreePorts.Utilities;

namespace TreePorts.Services;
public class CaptainService : ICaptainService
{
    private IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _hostingEnvironment;
    private readonly IMapper mapper;
    private IHubContext<MessageHub> _HubContext;
    public CaptainService(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment, IMapper mapper, IHubContext<MessageHub> hubcontext)
    {
        this.mapper = mapper;
        _unitOfWork = unitOfWork;
        _hostingEnvironment = hostingEnvironment;
        _HubContext = hubcontext;
    }




    public async Task<IEnumerable<CaptainUser>> GetCaptainUsersAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.CaptainRepository.GetCaptainUsersAsync(cancellationToken);
        }
        catch (Exception e)
        {
            return new List<CaptainUser>();
        }
    }


    public async Task<CaptainUserVechicleResponse> GetCaptainUserAccountByCaptainUserAccountIdAsync(string captainUserAccountId, CancellationToken cancellationToken)
    {

        var captainUserAccount = await _unitOfWork.CaptainRepository.GetCaptainUserAccountByIdAsync(captainUserAccountId,cancellationToken);
        if (captainUserAccount == null) throw new NotFoundException($"User with account id {captainUserAccountId} not found");

        var captainUser = await _unitOfWork.CaptainRepository.GetCaptainUserByIdAsync(captainUserAccount?.CaptainUserId ?? "",cancellationToken);
        if (captainUser == null) throw new NotFoundException($"User with account id {captainUserAccountId} not found");

        List<CaptainUserAccountVehicle> captainUserAccountVehicles = new();
        var captainUserVehicles = await _unitOfWork.CaptainRepository.GetCaptainUsersVehiclesByAsync(v => v.CaptainUserAccountId == captainUserAccountId,cancellationToken);

        List<CaptainUserVehicleBox> captainUserVehicleBoxs = new();
        foreach (CaptainUserVehicle captainUserVehicle in captainUserVehicles)
        {
            var userBoxs = await _unitOfWork.CaptainRepository.GetCaptainUsersBoxesByAsync(b => b.CaptainUserVehicleId == captainUserVehicle.Id,cancellationToken);
            CaptainUserVehicleBox captainUserVehicleBox = new(CaptainUserVehicle: captainUserVehicle, CaptainUserBoxs: userBoxs);
            captainUserVehicleBoxs.Add(captainUserVehicleBox);
        }

        CaptainUserAccountVehicle captainUserAccountVehicle = new(CaptainUserAccount: captainUserAccount, CaptainUserVehicleBoxs: captainUserVehicleBoxs);
        captainUserAccountVehicles.Add(captainUserAccountVehicle);


        return new(CaptainUser: captainUser, CaptainUserAccountsVehicles: captainUserAccountVehicles);


    }


    public async Task<CaptainUserVechicleResponse> GetCaptainUserByCaptainUserIdAsync(string captainUserId, CancellationToken cancellationToken)
    {

        var captainUser = await _unitOfWork.CaptainRepository.GetCaptainUserByIdAsync(captainUserId,cancellationToken);
        if (captainUser == null) throw new NotFoundException($"User with id {captainUserId} not found");


        var captainUserAccounts = await _unitOfWork.CaptainRepository.GetCaptainUsersAccountsByAsync(c => c.CaptainUserId == captainUserId,cancellationToken);
        if (captainUserAccounts == null) throw new NotFoundException($"User with id {captainUserId} not found");

        //var birthCountry = await _unitOfWork.CountryRepository.GetCountryByIdAsync(captainUser?.CountryId ?? 0);
        //driver.CountryName = birthCountry.Name;
        //driver.CountryArabicName = birthCountry.ArabicName;

        //var birthCity = await _unitOfWork.CountryRepository.GetCityByIdAsync(captainUser?.CityId ?? 0);
        //driver.CityName = birthCity.Name;
        //driver.CityArabicName = birthCity.ArabicName;


        //var residenceCountry = await _unitOfWork.CountryRepository.GetByID((long)driver.ResidenceCountryId);
        //driver.ResidenceCountryName = residenceCountry.Name;
        //driver.ResidenceCountryArabicName = residenceCountry.ArabicName;

        //var residenceCity = await _unitOfWork.CountryRepository.GetCityByID((long)driver.ResidenceCityId);
        //driver.ResidenceCityName = residenceCity.Name;
        //driver.ResidenceCityArabicName = residenceCity.ArabicName;
        List<CaptainUserAccountVehicle> captainUserAccountVehicles = new();
        foreach (CaptainUserAccount captain in captainUserAccounts)
        {
            var captainUserVehicles = await _unitOfWork.CaptainRepository.GetCaptainUsersVehiclesByAsync(v => v.CaptainUserAccountId == captain.Id,cancellationToken);

            List<CaptainUserVehicleBox> captainUserVehicleBoxs = new();
            foreach (CaptainUserVehicle captainUserVehicle in captainUserVehicles)
            {
                var userBoxs = await _unitOfWork.CaptainRepository.GetCaptainUsersBoxesByAsync(b => b.CaptainUserVehicleId == captainUserVehicle.Id,cancellationToken);
                CaptainUserVehicleBox captainUserVehicleBox = new(CaptainUserVehicle: captainUserVehicle, CaptainUserBoxs: userBoxs);
                captainUserVehicleBoxs.Add(captainUserVehicleBox);
            }

            CaptainUserAccountVehicle captainUserAccountVehicle = new(CaptainUserAccount: captain, CaptainUserVehicleBoxs: captainUserVehicleBoxs);
            captainUserAccountVehicles.Add(captainUserAccountVehicle);
        }


        return new(CaptainUser: captainUser, CaptainUserAccountsVehicles: captainUserAccountVehicles);




    }



    public async Task<IEnumerable<CaptainUserAccount>> GetUsersPagingAsync(FilterParameters parameters, CancellationToken cancellationToken)
    {
        try
        {

            var skip = (parameters.NumberOfObjectsPerPage * (parameters.Page - 1));
            var take = parameters.NumberOfObjectsPerPage;

            var result = await _unitOfWork.CaptainRepository.GetActiveCaptainUsersAccountsPaginationAsync(skip, take,cancellationToken);

            /*var total = query.Count();
            var result = Utility.Pagination(query, parameters.NumberOfObjectsPerPage, parameters.Page).ToList();
            var totalPages = (int)Math.Ceiling(total / (double)parameters.NumberOfObjectsPerPage);
*/
            return result;
        }
        catch (Exception e)
        {
            return new List<CaptainUserAccount>();// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }






    /* [HttpPost("Paging")]
     [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
     [ProducesResponseType(StatusCodes.Status204NoContent)]
     public async Task<IActionResult> Users([FromBody] Pagination pagination, [FromQuery] FilterParameters parameters)
     {
         try
         {
             var query = await _unitOfWork.UserAccountRepository.GetBy(u => u.StatusTypeId != (long)StatusTypes.Reviewing);

             var total = query.Count();
             var result = Utility.Pagination(query, pagination.NumberOfObjectsPerPage, pagination.Page).ToList();
             var totalPages = (int)Math.Ceiling(total / (double)pagination.NumberOfObjectsPerPage);       

             return Ok(new { Users = result, Total = total, Page = pagination.Page, TotaPages = totalPages });
         }
         catch (Exception e)
         {
             return new ObjectResult(e.Message) { StatusCode = 666 };
         }
     }

     */




    public async Task<IEnumerable<CaptainUserAccount>> GetNewCaptainsUsersAsync(FilterParameters parameters, CancellationToken cancellationToken)//[FromBody] Pagination pagination, [FromQuery] FilterParameters parameters)
    {
        try
        {



            /*var taskResult = await Task.Run(() => {
                var query = _unitOfWork.CaptainRepository.GetUserAccountByQuerable(u => u.StatusTypeId == (long)StatusTypes.Reviewing).OrderByDescending(c => c.CreationDate);
                //var total = query.Count();
                var result = Utility.Pagination(query.ToList(), parameters.NumberOfObjectsPerPage, parameters.Page).ToList();
                //var totalPages = (int)Math.Ceiling(total / (double)parameters.NumberOfObjectsPerPage);

                return Ok(new { Users = result, Total = total, Page = parameters.Page, TotaPages = totalPages });

            });*/

            var skip = (parameters.NumberOfObjectsPerPage * (parameters.Page - 1));
            var take = parameters.NumberOfObjectsPerPage;
            var result = await _unitOfWork.CaptainRepository.GetReviewingCaptainUsersAccountsPaginationAsync(skip, take,cancellationToken);

            return result;

            //var total =  Math.Ceiling(( ((decimal)users.Count) / ((decimal)pagination.NumberOfObjectsPerPage) ));
            //var skip = (pagination.NumberOfObjectsPerPage * (pagination.Page - 1));
            //var take = pagination.NumberOfObjectsPerPage;
            //var result = users.Skip(skip).Take(take).ToList();

            //return Ok(new { Users = result, Total = total , Page = pagination.Page });
        }
        catch (Exception e)
        {
            return new List<CaptainUserAccount>();// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }




    public async Task<object> GetDirectionsMapAsync(string origin, string destination, string mode, CancellationToken cancellationToken)
    {

        try
        {
            return await Utility.getDirectionsFromGoogleMap(origin, destination, mode,cancellationToken);
        }
        catch (Exception e)
        {
            return new { };// new ObjectResult(e.Message) { StatusCode = 666 };
        }


    }


    //// POST: Driver
    //[AllowAnonymous]
    //[HttpPost("Register")]
    //public async Task<IActionResult> Register( RegisterDriver driver)
    //{


    //    try
    //    {
    //        var accounts = await _unitOfWork.UserAccountRepository.GetBy(d => d.Mobile == driver.Mobile);
    //        var oldAccount = accounts.FirstOrDefault();
    //        if (oldAccount != null && oldAccount.Id > 0)
    //            return new ObjectResult("user already registered") { StatusCode = 700 };

    //        byte[] passwordHash, passwordSalt;
    //        var password = Utility.GeneratePassword();
    //        Utility.CreatePasswordHash(password, out passwordHash, out passwordSalt);
    //        UserAccount account = new UserAccount()
    //        {
    //            Mobile = driver.Mobile,
    //            Fullname = driver.FullName,
    //            PasswordHash = passwordHash,
    //            PasswordSalt = passwordSalt,
    //            StatusTypeId = (long)StatusTypes.Incomplete
    //        };

    //        User user = new User()
    //        {
    //            Mobile = driver.Mobile,
    //            Fullname = driver.FullName
    //        };
    //        user.UserAccounts.Add(account);


    //        UserCurrentStatus userStatus = new UserCurrentStatus() { StatusTypeId = (long)StatusTypes.New, CreationDate = DateTime.Now, IsCurrent = true };
    //        user.UserCurrentStatus.Add(userStatus);




    //        //var message = "Welcome to Sender App, your password is " + password;
    //        //var responseResult = Utility.SendSMS(message);
    //        //if (!responseResult)
    //        //    return new ObjectResult("Server not available") { StatusCode = 707 };

    //        var insertedUser = await _unitOfWork.CaptainRepository.InsertUser(user);
    //        var result = await _unitOfWork.Save();

    //        if (result == 0)
    //            return new ObjectResult("Server not available") { StatusCode = 707 };


    //        return Ok(true);
    //    }
    //    catch (Exception e)
    //     {
    //        return new ObjectResult(e.Message) { StatusCode = 666 };
    //    }



    //}




    public async Task<CaptainUserResponse> AddCaptainAsync(HttpContext httpContext, CaptainUserDto captainUserDto, CancellationToken cancellationToken)
    {

        var users = await _unitOfWork.CaptainRepository.GetCaptainUsersAccountsByAsync(u => u.Mobile == captainUserDto.Mobile,cancellationToken);
        if (users != null && users.Count() > 0) throw new InvalidException("Mobile already registered before");

        // user.UserAccounts.FirstOrDefault().StatusTypeId = (long)StatusTypes.Reviewing;               

        //UserCurrentStatus userStatus_New = new UserCurrentStatus() { StatusTypeId = (long)StatusTypes.New, CreationDate = DateTime.Now, IsCurrent = false };
        //UserCurrentStatus userStatus_Review = new UserCurrentStatus() { StatusTypeId = (long)StatusTypes.Reviewing, CreationDate = DateTime.Now, IsCurrent = true };
        //user.UserCurrentStatus.Add(userStatus_New);
        //user.UserCurrentStatus.Add(userStatus_Review);

        CaptainUserAccount account = new()
        {
            Id = Guid.NewGuid().ToString(),
            //CaptainUserId = insertedUser.Id,
            Mobile = captainUserDto?.Mobile,
            StatusTypeId = (long)StatusTypes.Reviewing,
            CreationDate = DateTime.Now
        };

        CaptainUser captainUser = new() {
            BirthDate =  captainUserDto?.BirthDate,
            CityId = captainUserDto?.CityId,
            CountryId = captainUserDto?.CountryId,
            DrivingLicenseImage = captainUserDto?.DrivingLicenseImage,
            FirstName = captainUserDto?.FirstName,
            LastName = captainUserDto?.LastName,
            Gender = captainUserDto?.Gender,
            Mobile = captainUserDto?.Mobile,
            NationalNumber = captainUserDto?.NationalNumber,
            NationalNumberExpireDate = captainUserDto?.NationalNumberExpireDate,
            NationalNumberFrontImage = captainUserDto?.NationalNumberFrontImage,
            NbsherNationalNumberImage = captainUserDto?.NbsherNationalNumberImage,
            PersonalImage = captainUserDto?.PersonalImage,
            ResidenceCityId = captainUserDto?.ResidenceCityId,
            RecidenceImage = captainUserDto?.RecidenceImage,
            ResidenceCountryId = captainUserDto?.ResidenceCountryId,
            StcPay = captainUserDto?.StcPay,
            VehiclePlateNumber = captainUserDto?.VehiclePlateNumber,
            VehicleRegistrationImage = captainUserDto?.VehicleRegistrationImage,
            CaptainUserAccounts = new List<CaptainUserAccount>() { account }
        };


        captainUser = convertAndSaveUserImages(captainUser);
        var insertedUser = await _unitOfWork.CaptainRepository.InsertCaptainUserAsync(captainUser,cancellationToken);
        var result = await _unitOfWork.Save(cancellationToken);
        if (result == 0) throw new ServiceUnavailableException("Service Unavailable");





        //var status = new StatusType { Id = (long)StatusTypes.Reviewing };

        /*CaptainUserAccount account = new()
        {
            CaptainUserId = insertedUser.Id,
            Mobile = insertedUser.Mobile,
            StatusTypeId = (long)StatusTypes.Reviewing,
            CreationDate = DateTime.Now
        }

        var userAccount = await _unitOfWork.CaptainRepository.InsertCaptainUserAccountAsync(account);
        result = await _unitOfWork.Save();
        if (result == 0) throw new ServiceUnavailableException("Service Unavailable");
        ;*/

        string modifierID = "";
        string modifierType = "";
        Utility.getRequestUserIdFromToken(httpContext, out modifierID, out modifierType);
        CaptainUserCurrentStatus userStatus_Review = new ()
        {
            CaptainUserAccountId = insertedUser.CaptainUserAccounts?.FirstOrDefault()?.Id,
            StatusTypeId = (long)StatusTypes.Reviewing,
            IsCurrent = true,
            CreatedBy = modifierID,
            CreationDate = DateTime.Now,
            ModificationDate = DateTime.Now
        };

        var insertedResult = await _unitOfWork.CaptainRepository.InsertCaptainUserCurrentStatusAsync(userStatus_Review,cancellationToken);
        result = await _unitOfWork.Save(cancellationToken); 
        if (result == 0) throw new ServiceUnavailableException("Service Unavailable");


        var message = insertedUser.Id.ToString();
        _ = _HubContext.Clients.All.SendAsync("ReviewingDriverNotify", message,cancellationToken);

        return new( CaptainUser: insertedUser, CaptainUserAccount: insertedUser.CaptainUserAccounts.FirstOrDefault());


    }



    private CaptainUser convertAndSaveUserImages(CaptainUser user)
    {
        if (user?.PersonalImage != null && user?.PersonalImage != "" && !(user?.PersonalImage?.Contains(".jpeg") ?? false))
        {

            var UserFolderPath = _hostingEnvironment.ContentRootPath + "/Assets/Images/Drivers/PersonalImages";
            if (!Directory.Exists(UserFolderPath))
                Directory.CreateDirectory(UserFolderPath);

            string randum_numbers = Utility.GeneratePassword();
            string personalImage_name = "PI_" + randum_numbers.ToString() + ".jpeg";// PI is the first letter of PersonalImage
            bool result = Utility.SaveImage(user?.PersonalImage, personalImage_name, UserFolderPath);
            if (result == true)
                user.PersonalImage = personalImage_name;

        }

        if (user?.NbsherNationalNumberImage != null && user?.NbsherNationalNumberImage != "" && !(user?.NbsherNationalNumberImage?.Contains(".jpeg") ?? false))
        {

            var UserFolderPath = _hostingEnvironment.ContentRootPath + "/Assets/Images/Drivers/NbsherNationalNumberImages";
            if (!Directory.Exists(UserFolderPath))
                Directory.CreateDirectory(UserFolderPath);

            string randum_numbers = Utility.GeneratePassword();
            string NbsherNationalNumberImage_name = "NNNI_" + randum_numbers.ToString() + ".jpeg"; // NNNI is the first letter of NbsherNationalNumberImage
            bool result = Utility.SaveImage(user?.NbsherNationalNumberImage, NbsherNationalNumberImage_name, UserFolderPath);
            if (result == true)
                user.NbsherNationalNumberImage = NbsherNationalNumberImage_name;

        }
        if (user?.NationalNumberFrontImage != null && user?.NationalNumberFrontImage != "" && !(user?.NationalNumberFrontImage?.Contains(".jpeg") ?? false))
        {

            var UserFolderPath = _hostingEnvironment.ContentRootPath + "/Assets/Images/Drivers/NationalNumberFrontImages";
            if (!Directory.Exists(UserFolderPath))
                Directory.CreateDirectory(UserFolderPath);

            string randum_numbers = Utility.GeneratePassword();
            string NationalNumberFrontImage_name = "NNFI_" + randum_numbers.ToString() + ".jpeg"; // NNFI is the first letter of NationalNumberFrontImage
            bool result = Utility.SaveImage(user?.NationalNumberFrontImage, NationalNumberFrontImage_name, UserFolderPath);
            if (result == true)
                user.NationalNumberFrontImage = NationalNumberFrontImage_name;

        }
        if (user?.VehicleRegistrationImage != null && user?.VehicleRegistrationImage != "" && !(user?.VehicleRegistrationImage?.Contains(".jpeg") ?? false))
        {

            var UserFolderPath = _hostingEnvironment.ContentRootPath + "/Assets/Images/Drivers/VehicleRegistrationImages";
            if (!Directory.Exists(UserFolderPath))
                Directory.CreateDirectory(UserFolderPath);

            string randum_numbers = Utility.GeneratePassword();
            string VehicleRegistrationImage_name = "VRI_" + randum_numbers.ToString() + ".jpeg"; // VRI is the first letter of VehicleRegistrationImage
            bool result = Utility.SaveImage(user?.VehicleRegistrationImage, VehicleRegistrationImage_name, UserFolderPath);
            if (result == true)
                user.VehicleRegistrationImage = VehicleRegistrationImage_name;

        }
        if (user?.DrivingLicenseImage != null && user?.DrivingLicenseImage != "" && !(user?.DrivingLicenseImage.Contains(".jpeg") ?? false))
        {

            var UserFolderPath = _hostingEnvironment.ContentRootPath + "/Assets/Images/Drivers/DrivingLicenseImages";
            if (!Directory.Exists(UserFolderPath))
                Directory.CreateDirectory(UserFolderPath);

            string randum_numbers = Utility.GeneratePassword();
            string DrivingLicenseImage_name = "DLI_" + randum_numbers.ToString() + ".jpeg"; // DLI is the first letter of DrivingLicenseImage
            bool result = Utility.SaveImage(user?.DrivingLicenseImage, DrivingLicenseImage_name, UserFolderPath);
            if (result == true)
                user.DrivingLicenseImage = DrivingLicenseImage_name;

        }

        return user;

    }



    public async Task<CaptainUserResponse> LoginAsync(LoginCaptainUserDto loginCaptain, CancellationToken cancellationToken)
    {


        var accounts = await _unitOfWork.CaptainRepository.GetCaptainUsersAccountsByAsync(d => d.Mobile == loginCaptain.Mobile,cancellationToken);
        var account = accounts.FirstOrDefault();
        if (account == null) throw new UnauthorizedException("Unauthorized");



        if (loginCaptain.Password != "123789")
        {
            if (!Utility.VerifyPasswordHash(loginCaptain.Password, account.PasswordHash, account.PasswordSalt))
                throw new UnauthorizedException("Unauthorized");
        }



        var user = await _unitOfWork.CaptainRepository.GetCaptainUserByIdAsync(account.CaptainUserId,cancellationToken);

        if (!account.Token.HasValue())
        {
            //var user = await _unitOfWork.CaptainRepository.GetUserByID((long)account.UserId);
            var token = Utility.GenerateToken(account.Id, $"{user.FirstName} {user.LastName}" , "Captain", null);
            account.Token = token;
            //account.StatusTypeId = (long)StatusTypes.Incomplete;
            account = await _unitOfWork.CaptainRepository.UpdateCaptainUserAccountAsync(account,cancellationToken);
            var result = await _unitOfWork.Save(cancellationToken);
            if (result == 0) throw new ServiceUnavailableException("Service Unavailable");

            //account.User = user;

        }


        return new(CaptainUser: user, CaptainUserAccount: account);


    }


    //we don't use that any more , we use the method in the system controller
    public async Task<bool> ChangePasswordAsync(DriverPhone driver, CancellationToken cancellationToken)
    {


        var accounts = await _unitOfWork.CaptainRepository.GetCaptainUsersAccountsByAsync(d => d.Mobile == driver.Mobile,cancellationToken);
        var account = accounts.FirstOrDefault();
        if (account == null) throw new UnauthorizedException("Unauthorized");

        var user = await _unitOfWork.CaptainRepository.GetCaptainUserByIdAsync(account.CaptainUserId,cancellationToken);
        var country = await _unitOfWork.CountryRepository.GetCountryByIdAsync((long)user.CountryId, cancellationToken);

        byte[] passwordHash, passwordSalt;
        var password = Utility.GeneratePassword();
        Utility.CreatePasswordHash(password, out passwordHash, out passwordSalt);

        account.PasswordHash = passwordHash;
        account.PasswordSalt = passwordSalt;
        account.Password = password;


        var message = "Welcome to Sender, your password reset, the password is " + password;
        var phone = country.Code + account.Mobile;
        _ = Utility.SendSMSAsync(message, phone);
        //if (!responseResult)
        //    return new ObjectResult("Server not available") { StatusCode = 707 };

        var updatedUser = await _unitOfWork.CaptainRepository.UpdateCaptainUserAccountAsync(account,cancellationToken);
        var result = await _unitOfWork.Save(cancellationToken);

        if (result == 0) throw new ServiceUnavailableException("Service Unavailable");

        return true;

    }




    // POST: Driver
    //[AllowAnonymous]
    //[HttpPost("SaveImage")]
    //public async Task<IActionResult> SaveImage([FromBody] ImageHelper imageHelper)
    //{
    //    try
    //    {
    //        string path = _hostingEnvironment.ContentRootPath + "/Assets/UserImages/";
    //        Utility.SaveImage(imageHelper.ImageBase64,imageHelper.ImageName, path);


    //        return Ok(true);
    //    }
    //    catch (Exception e)
    //    {
    //        return new ObjectResult(e.Message) { StatusCode = 666 };
    //    }


    //}




    public async Task<bool> UploadAsync(HttpContext httpContext, CancellationToken cancellationToken)
    {
        try
        {
            //var description = "";
            var UserID = "";
            var request = httpContext.Request;
            //if(request.Form != null && request.Form["description"] != "")
            //    description = request.Form["description"];


            if (request.Form != null && request.Form["UserID"] != "" && request.Form["UserID"].Count > 0)
                UserID = request.Form["UserID"];
            else if (request.Form != null && request.Form.Files["UserID"] != null)
            {
                UserID = request.Form.Files["UserID"].FileName;

            }


            var UserFolderPath = _hostingEnvironment.ContentRootPath + "/Assets/Images/Drivers/" + UserID;
            //UserFolderPath = UserFolderPath.Replace('/', '\\');
            if (!Directory.Exists(UserFolderPath))
                Directory.CreateDirectory(UserFolderPath);


            var files = httpContext.Request.Form.Files;
            foreach (var file in files)
            {
                if (file.Length > 0)
                {

                    var FolderPath = _hostingEnvironment.ContentRootPath + "/Assets/Images/Drivers/" + UserID + "/" + file.Name;
                    //FolderPath = FolderPath.Replace('/', '\\');
                    if (!Directory.Exists(FolderPath))
                        Directory.CreateDirectory(FolderPath);

                    //var filePath = Path.Combine(uploads, file.FileName);
                    string filePath = _hostingEnvironment.ContentRootPath + "/Assets/Images/Drivers/" + UserID + "/" + file.Name + "/" + file.FileName;
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



    /*// PUT: Driver
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] User user)
    {

        try
        {


            var usersAccounts = await _unitOfWork.UserAccountRepository.GetBy(a => a.UserId == user.Id);
            var userAccount = usersAccounts.FirstOrDefault();
            if(userAccount!= null && userAccount.StatusTypeId == (long)StatusTypes.Incomplete)
                userAccount.StatusTypeId = (long)StatusTypes.Working;


            var userStatuses = await _unitOfWork.CaptainRepository.GetUserCurrentStatusBy(u => u.UserId == user.Id && u.IsCurrent == true);
            var userCurrentStatus = userStatuses.FirstOrDefault();
            if (userCurrentStatus != null && userCurrentStatus.StatusTypeId == (long)StatusTypes.New)
            {
                userCurrentStatus.StatusTypeId = (long)StatusTypes.Working;
                userCurrentStatus = await _unitOfWork.CaptainRepository.UpdateUserCurrentStatus(userCurrentStatus);
            }
            else if(userCurrentStatus == null)
            {
                userCurrentStatus = new UserCurrentStatus() { StatusTypeId = (long)StatusTypes.Working };
                userCurrentStatus = await _unitOfWork.CaptainRepository.InsertUserCurrentStatus(userCurrentStatus);
            }


            var updateResult = await _unitOfWork.CaptainRepository.UpdateUser(user);
            userAccount = await _unitOfWork.UserAccountRepository.Update(userAccount);




            var result = await _unitOfWork.Save();
            var message = user.Id.ToString();
            await _HubContext.Clients.All.SendAsync("WorkingDriverNotify", message);
            if (result == 0) return new ObjectResult("Server not avalible") { StatusCode = 707 };

            return Ok(updateResult);
        }
        catch (Exception e)
        {
            return new ObjectResult(e.Message) { StatusCode = 666 };
        }

    }

*/



    public async Task<object> AcceptRegisterCaptainByIdAsync(string captainUserAccountId, HttpContext httpContext, CancellationToken cancellationToken)
    {


        var userAccount = await _unitOfWork.CaptainRepository.GetCaptainUserAccountByIdAsync(captainUserAccountId,cancellationToken);
        if (userAccount == null) throw new NotFoundException($"User with id {captainUserAccountId} not found");

        //var userAccount = usersAccounts.FirstOrDefault();
        if (userAccount?.StatusTypeId == (long)StatusTypes.Reviewing)
            userAccount.StatusTypeId = (long)StatusTypes.Working;

        byte[] passwordHash, passwordSalt;
        var password = Utility.GeneratePassword();
        Utility.CreatePasswordHash(password, out passwordHash, out passwordSalt);

        userAccount.PasswordHash = passwordHash;
        userAccount.PasswordSalt = passwordSalt;
        userAccount.Password = password;

        var user = await _unitOfWork.CaptainRepository.GetCaptainUserByIdAsync(userAccount.CaptainUserId,cancellationToken);



        //string modifierID = -1;
        //string modifierType = "";
        Utility.getRequestUserIdFromToken(httpContext, out string modifierID, out string modifierType);
        CaptainUserCurrentStatus userCurrentStatus = new ()
        {
            CaptainUserAccountId = userAccount.Id,
            StatusTypeId = (long)StatusTypes.Working,
            IsCurrent = true,
            CreatedBy = modifierID,
            CreationDate = DateTime.Now,
            ModifiedBy = modifierID,
            ModificationDate = DateTime.Now
        };






        //var userStatuses = await _unitOfWork.CaptainRepository.GetUserCurrentStatusBy(u => u.UserId == userAccount.UserId && u.IsCurrent == true);
        //var userCurrentStatus = userStatuses.FirstOrDefault();
        //if (userCurrentStatus != null && userCurrentStatus.StatusTypeId == (long)StatusTypes.New)
        //{
        //    userCurrentStatus.StatusTypeId = (long)StatusTypes.Working;
        //    userCurrentStatus = await _unitOfWork.CaptainRepository.UpdateUserCurrentStatus(userCurrentStatus);
        //}
        //else if (userCurrentStatus == null)
        //{
        //    userCurrentStatus = new UserCurrentStatus() { StatusTypeId = (long)StatusTypes.Working };
        //    userCurrentStatus = await _unitOfWork.CaptainRepository.InsertUserCurrentStatus(userCurrentStatus);
        //}


        //var updateResult = await _unitOfWork.CaptainRepository.UpdateUser(user);


        var userCurrentStatusInsertedResult = await _unitOfWork.CaptainRepository.InsertCaptainUserCurrentStatusAsync(userCurrentStatus,cancellationToken);
        userAccount = await _unitOfWork.CaptainRepository.UpdateCaptainUserAccountAsync(userAccount,cancellationToken);
        var result = await _unitOfWork.Save(cancellationToken);
        if (result == 0) throw new ServiceUnavailableException("Service Unavailable");


        /*var country = await _unitOfWork.CountryRepository.GetCountryByIdAsync((long)user.CountryId);
        var message = "Welcome to Sender,your registration has been approved,your password is " + password;
        var phone = country.Code + userAccount.Mobile;
        var responseResult = Utility.SendSMS(message, phone);*/


        var message2 = userAccount?.Id;
        _ = _HubContext.Clients.All.SendAsync("WorkingDriverNotify", message2,cancellationToken);
        return new { Result = true, Password = password };


    }









    public async Task<object> UpdateCaptainUserAsync(string? captainUserAccountId, CaptainUserDto captainUserDto, CancellationToken cancellationToken)
    {

        if (captainUserAccountId == null || captainUserAccountId == "") throw new InvalidException($"No User Id provided"); 
        if (captainUserDto == null) throw new NoContentException("NoContent");

        var captainUserAccount = await _unitOfWork.CaptainRepository.GetCaptainUserAccountByIdAsync(captainUserAccountId,cancellationToken);
        if (captainUserAccount == null) throw new NotFoundException($"User account with id {captainUserAccountId} not found");


        CaptainUser captainUser = new()
        {
            Id = captainUserAccount?.CaptainUserId ?? "",
            BirthDate = captainUserDto?.BirthDate,
            CityId = captainUserDto?.CityId,
            CountryId = captainUserDto?.CountryId,
            DrivingLicenseImage = captainUserDto?.DrivingLicenseImage,
            FirstName = captainUserDto?.FirstName,
            LastName = captainUserDto?.LastName,
            Gender = captainUserDto?.Gender,
            Mobile = captainUserDto?.Mobile,
            NationalNumber = captainUserDto?.NationalNumber,
            NationalNumberExpireDate = captainUserDto?.NationalNumberExpireDate,
            NationalNumberFrontImage = captainUserDto?.NationalNumberFrontImage,
            NbsherNationalNumberImage = captainUserDto?.NbsherNationalNumberImage,
            PersonalImage = captainUserDto?.PersonalImage,
            ResidenceCityId = captainUserDto?.ResidenceCityId,
            RecidenceImage = captainUserDto?.RecidenceImage,
            ResidenceCountryId = captainUserDto?.ResidenceCountryId,
            StcPay = captainUserDto?.StcPay,
            VehiclePlateNumber = captainUserDto?.VehiclePlateNumber,
            VehicleRegistrationImage = captainUserDto?.VehicleRegistrationImage
        };



        captainUser = convertAndSaveUserImages(captainUser);
        var updateResult = await _unitOfWork.CaptainRepository.UpdateCaptainUserAsync(captainUser,cancellationToken);
        var result = await _unitOfWork.Save(cancellationToken);
        if (result == 0) throw new ServiceUnavailableException("Service Unavailable");

        return new { Result = true, Message = "Updated successfuly" };

    }



    public async Task<bool> UpdateCaptainCurrentLocationAsync(CaptainUserCurrentLocation userCurrentLocation, CancellationToken cancellationToken)
    {

        var insertedResult = await _unitOfWork.CaptainRepository.InsertCaptainUserCurrentLocationAsync(userCurrentLocation,cancellationToken);

        var result = await _unitOfWork.Save(cancellationToken);
        if (result == 0) throw new ServiceUnavailableException("Service Unavailable");

        return true;

    }





    public async Task<bool> DeleteCaptainUserAccountAsync(string captainUserAccountId, CancellationToken cancellationToken)
    {


        var userAccount = await _unitOfWork.CaptainRepository.DeleteCaptainUserAccountAsync(captainUserAccountId,cancellationToken);

        var result = await _unitOfWork.Save(cancellationToken);
        if (result == 0) throw new ServiceUnavailableException("Service Unavailable");

        return true;


    }


    // moved to orders controller
    /*// POST: Driver
    [HttpGet("CurrentLocation/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> CurrentLocation(long id) // order id
    {
        try
        {


            var orderStatuses =
                await _unitOfWork.OrderRepository.GetOrderCurrentStatusesByAsync(s => s.OrderId == id && s.IsCurrent == true);
            var orderStatus = orderStatuses.FirstOrDefault();
            if (orderStatus == null || orderStatus?.StatusTypeId ==(long) OrderStatusTypes.Dropped)
                return NotFound();

            var orderAssigns = await _unitOfWork.OrderRepository.GetOrdersAssignmentsByAsync( a=> a.OrderId == id);
            var orderAssign = orderAssigns.FirstOrDefault();
            if (orderAssign == null) return NoContent();

            var usersLocations = await _unitOfWork.CaptainRepository.GetUserCurrentLocationBy(u => u.UserId == orderAssign.UserId);
            var userLocation = usersLocations.FirstOrDefault();
            if(userLocation == null) return NoContent();

            return Ok( new { Latitude = userLocation.Lat,Longitude = userLocation.Long });
        }
        catch (Exception e)
        {
            return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
        }


    }

*/


    public async Task<object> GetOrdersPaymentsByCaptainUserAccountIdAsync(string captainUserAccountId, CancellationToken cancellationToken)
    {
        try
        {
            var payments = await _unitOfWork.CaptainRepository
                .GetCaptainUsersPaymentsByAsync(p => 
                p.CaptainUserAccountId == captainUserAccountId && 
                p.PaymentStatusTypeId == (long)PaymentStatusTypes.Complete,
                cancellationToken);

            var orderIds = payments.Select(p => p.OrderId).ToList();

            var userPaidOrders = await _unitOfWork.OrderRepository.GetPaidOrdersByAsync(p => orderIds.Contains(p.OrderId) && p.Type == (long)PaymentTypes.WalletCredit,cancellationToken);


            return new { UserPayments = payments, PaidOrders = userPaidOrders };

        }
        catch (Exception e)
        {
            return new { };// new ObjectResult(e.Message) { StatusCode = 666 };
        }

    }



    public async Task<object> GetBookkeepingPagingByCaptainUserAccountIdAsync(string? captainUserAccountId, FilterParameters parameters, CancellationToken cancellationToken)// , [FromBody] Pagination pagination)
    {

        if (captainUserAccountId == null || captainUserAccountId == "") throw new InvalidException("No account Id provided");
        if (parameters == null) throw new NoContentException("NoContent");

        /*int skip = 0 , take = 0;
        if (objects != null &&  objects >= 0  &&  page != null && page > 0 ) {
            skip = (int)objects * (int)page;
            take = (int)page;
        }*/

        if (parameters.NumberOfObjectsPerPage <= 0)
            parameters.NumberOfObjectsPerPage = 10;

        if (parameters.Page <= 0)
            parameters.Page = 1;

        var skip = (parameters.NumberOfObjectsPerPage * (parameters.Page - 1));
        var take = parameters.NumberOfObjectsPerPage;
        var payments = await _unitOfWork.PaymentRepository.GetBookkeepingPaginationByAsync(p => p.CaptainUserAccountId == captainUserAccountId, skip, take,cancellationToken);

        return payments;



    }



    public async Task<IEnumerable<Bookkeeping>> GetBookkeepingByCaptainUserAccountIdAsync(string captainUserAccountId, CancellationToken cancellationToken)
    {
        try
        {

            /*var skip = (pagination.NumberOfObjectsPerPage * (pagination.Page));
            var take = pagination.NumberOfObjectsPerPage;*/
            var payments = await _unitOfWork.PaymentRepository.GetBookkeepingByAsync(p => p.CaptainUserAccountId == captainUserAccountId,cancellationToken);

            return payments;

        }
        catch (Exception e)
        {
            return new List<Bookkeeping>();// new ObjectResult(e.Message) { StatusCode = 666 };
        }

    }



    public async Task<decimal> GetUntransferredBookkeepingByCaptainUserAccountIdAsync(string captainUserAccountId, CancellationToken cancellationToken)
    {
        try
        {
            var payments = await _unitOfWork.PaymentRepository.UntransferredBookkeepingByAsync(p => p.CaptainUserAccountId == captainUserAccountId && p.Value > 0,cancellationToken);
            if (payments == null || payments?.Count <= 0) return 0;

            var total = payments?.Sum(p => p.Value);
            return total ?? 0;

        }
        catch (Exception e)
        {
            return 0;// new ObjectResult(e.Message) { StatusCode = 666 };
        }

    }



    public async Task<IEnumerable<Order>> GetAllOrdersAssignmentsByCaptainUserAccountIdAsync(string captainUserAccountId, FilterParameters parameters, CancellationToken cancellationToken)//[FromBody] Pagination pagination)
    {
        try
        {
            if (parameters.Page <= 0)
                parameters.Page = 1;

            if (parameters.NumberOfObjectsPerPage <= 0)
                parameters.NumberOfObjectsPerPage = 10;

            var skip = (parameters.NumberOfObjectsPerPage * (parameters.Page - 1));
            var take = parameters.NumberOfObjectsPerPage;
            //var result = orders.Skip(skip).Take(take).ToList();

            var orderAssignments = await _unitOfWork.OrderRepository.GetOrdersAssignmentsByAsync(p => p.CaptainUserAccountId == captainUserAccountId,cancellationToken);
            var orderIds = orderAssignments.Select(a => a.OrderId).ToList();
            var OrdersStatus = await _unitOfWork.OrderRepository
                .GetOrderCurrentStatusesByAsync(s => 
                orderIds.Contains(s.OrderId) &&
            ( s.OrderStatusTypeId == (long)OrderStatusTypes.Dropped || 
            s.OrderStatusTypeId == (long)OrderStatusTypes.Delivered ),
            cancellationToken);

            var realOrdersIDs = OrdersStatus.Skip(skip).Take(take).Select(o => o.OrderId);

            var orders = await _unitOfWork.OrderRepository.GetOrdersByAsync(o => realOrdersIDs.Contains(o.Id),cancellationToken);

            return orders;

        }
        catch (Exception e)
        {
            return new List<Order>();// new ObjectResult(e.Message) { StatusCode = 666 };
        }

    }


    public async Task<CaptainUserShift> AddCaptainUserShiftAsync(CaptainUserShift userShift, CancellationToken cancellationToken)
    {
        try
        {
            var insertResult = await _unitOfWork.CaptainRepository.InsertCaptainUserShiftAsync(userShift,cancellationToken);
            var result = await _unitOfWork.Save(cancellationToken);
            if (result == 0) throw new ServiceUnavailableException("Service Unavailable");

            return insertResult;

        }
        catch (Exception e)
        {
            return new CaptainUserShift();// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }


    public async Task<CaptainUserShift> DeleteCaptainUserShiftAsync(string captainUserAccountId, long shiftId, CancellationToken cancellationToken)
    {


        var userShifts = await _unitOfWork.CaptainRepository.GetCaptainUsersShiftsByAsync(s => s.CaptainUserAccountId == captainUserAccountId && s.ShiftId == shiftId,cancellationToken); //&& s.CreationDate >= DateTime.Now);
        var oldUserShift = userShifts.FirstOrDefault();
        if (oldUserShift == null) throw new NotFoundException("the target shift not found");

        var deleteResult = await _unitOfWork.CaptainRepository.DeleteCaptainUserShiftAsync(oldUserShift.Id,cancellationToken);
        var result = await _unitOfWork.Save(cancellationToken);
        if (result == 0) throw new ServiceUnavailableException("Service Unavailable");

        return deleteResult;

    }



    public async Task<CaptainUserShift?> GetCaptainUsershiftAsync(string captainUserAccountId, long shiftId, CancellationToken cancellationToken)//[FromBody] UserShift userShift)
    {

        try
        {
            var userShifts = await _unitOfWork.CaptainRepository.GetCaptainUsersShiftsByAsync(s => s.CaptainUserAccountId == captainUserAccountId && s.ShiftId == shiftId,cancellationToken); //&& s.CreationDate >= DateTime.Now);
            var result = userShifts.FirstOrDefault();

            return result;
        }
        catch (Exception e)
        {
            return new CaptainUserShift();// new ObjectResult(e.Message) { StatusCode = 666 };
        }

    }



    public async Task<object> GetShiftsAndUserShiftsByDateAsync(string captainUserAccountId, Shift shift, CancellationToken cancellationToken)
    {
        try
        {
            var resultShiftsOftheDay = await _unitOfWork.SystemRepository.GetShiftsByShiftDateAsync(shift,cancellationToken);
            var shiftsOftheDay_IDs = resultShiftsOftheDay.Select(s => s.Id).ToList();
            var userShifts = await _unitOfWork.CaptainRepository.GetCaptainUsersShiftsByAsync(s => s.CaptainUserAccountId == captainUserAccountId && shiftsOftheDay_IDs.Contains(s.ShiftId ?? 0),cancellationToken);

            return new { shifts = resultShiftsOftheDay, userShifts = userShifts };
        }
        catch (Exception e)
        {
            return new { };// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }


    //set active or incative to captain 
    //Active is if captain is currently working and ready to take orders
    //Inactive is if captain is don't want to take orders and stopped using the app 
    public async Task<CaptainUserActivity> CaptainUserActivitiesAsync(CaptainUserActivity userActivity, CancellationToken cancellationToken)
    {


        if (userActivity.StatusTypeId == (long)StatusTypes.Inactive)
        {
            var updateDriverLocationResult =
            await _unitOfWork.CaptainRepository.DeleteCaptainUserCurrentLocationByCaptainUserAccountIdAsync(userActivity.CaptainUserAccountId ?? "",cancellationToken);


        }

        var updateResult = await _unitOfWork.CaptainRepository.InsertCaptainUserActivityAsync(userActivity,cancellationToken);
        var result = await _unitOfWork.Save(cancellationToken);
        if (result == 0) throw new ServiceUnavailableException("Service Unavailable");


        return updateResult;

    }



   /* public async Task<object> ReportsAsync(FilterParameters reportParameters, HttpContext httpContext)
    {

        // 1-Check Role and Id


        //var userType = "";
        //var userId = long.Parse("0");
        Utility.getRequestUserIdFromToken(httpContext, out string userId, out string userType);

        //   var accessToken = await HttpContext.GetTokenAsync("access_token");
        // 2- Get Querable data depends on Role and Id 
        IQueryable<Order> acceptedOrders;
        //if (userType != "Admin" || userType != "Support") throw new Exception("Unauthorized");
        if (userType != "Captain" && userId == "") throw new Exception("Unauthorized");


        IQueryable<Order> rejectedOrders;

        IQueryable<Order> ignoredOrders;
        if (reportParameters.FilterByDriverId == 0)
        {
            //rejectedOrders = _unitOfWork.CaptainRepository.GetAllRejectedRequestByQuerable().Select(o => o.Order);
            acceptedOrders = _unitOfWork.CaptainRepository.GetAllAcceptedRequestByQuerable().Select(o => o.Order);
            //ignoredOrders = _unitOfWork.CaptainRepository.GetAllIgnoredRequestByQuerable().Select(o => o.Order);

        }
        else if (userId != "")
        {
            acceptedOrders = _unitOfWork.CaptainRepository.GetCaptainUserAcceptedRequestByQuerable(u => u.CaptainUserAccountId == userId).Select(o => o.Order);
        }
        else
        {

            //rejectedOrders = _unitOfWork.CaptainRepository.GetUserRejectedRequestByQuerable(u => u.UserId == reportParameters.FilterByDriverId).Select(o => o.Order);
            acceptedOrders = _unitOfWork.CaptainRepository.GetCaptainUserAcceptedRequestByQuerable(u => u.CaptainUserAccountId == reportParameters.FilterByCaptainUserAccountId);//.Select(o => o.Order);
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
        };



    }*/
    /* Get Orders Reports */






   /* public async Task<object> SearchAsync(FilterParameters parameters)
    {
        try
        {
            IQueryable<CaptainUser> query;
            if (parameters.StatusUserTypeId == null)
            {
                query = _unitOfWork.CaptainRepository.GetByQuerable();
            }
            else
            {

                query = _unitOfWork.CaptainRepository.GetByStatusQuerableS(parameters.StatusUserTypeId);
            }

            //added new
            var totalResult = 0;
            var skip = (parameters.NumberOfObjectsPerPage * (parameters.Page - 1));
            var take = parameters.NumberOfObjectsPerPage;
            var usersResult = Utility.GetFilter2<CaptainUser>(parameters, query, skip, take, out totalResult);
            var users = this.mapper.Map<List<UserResponse>>(usersResult.ToList());


            var totalPages = (int)Math.Ceiling(totalResult / (double)parameters.NumberOfObjectsPerPage);
            return new { Drivers = users, totalResult = totalResult, Page = parameters.Page, TotalPages = totalPages };
            //end -- added new

            // var users = await Utility.GetFilter<User>(parameters, query);
            //var usersResult = await Utility.GetFilter<User>(parameters, query);

            //var users = this.mapper.Map<List<UserResponse>>(usersResult);

            //var total = users.Count();
            //var result = Utility.Pagination(users, parameters.NumberOfObjectsPerPage, parameters.Page).ToList();
            //var totalPages = (int)Math.Ceiling(total / (double)parameters.NumberOfObjectsPerPage);

            //return Ok(new { Drivers = result, Total = total, Page = parameters.Page, TotalPages = totalPages });
        }
        catch (Exception e)
        {
            return new { };// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }*/
    /**/


    public async Task<object> ChartsAsync(CancellationToken cancellationToken)
    {
        try
        {
            var reportData = _unitOfWork.CaptainRepository.UserReportCount(cancellationToken);
            return reportData;
        }
        catch (Exception e)
        {
            return new { };// new ObjectResult(e.Message) { StatusCode = 666 };
        }

    }


    public async Task<object> CheckBonusPerMonthAsync(BonusCheckDto bonusCheckDto, CancellationToken cancellationToken)
    {

        /// Check Bonus
        var userOrdersAssignedPerMonth = await _unitOfWork.OrderRepository.GetOrdersAssignmentsByAsync(a =>
            a.CaptainUserAccountId == bonusCheckDto.captainUserAccountId &&
            a.CreationDate.Value.Month == bonusCheckDto.date.Month &&
            a.CreationDate.Value.Month == bonusCheckDto.date.Month,
            cancellationToken);

        var orderIds = userOrdersAssignedPerMonth.Select(a => a.OrderId).ToList();
        var ordersStatus = await _unitOfWork.OrderRepository.GetOrderCurrentStatusesByAsync(s =>
            orderIds.Contains(s.OrderId) &&
            (s.OrderStatusTypeId == (long)OrderStatusTypes.Dropped),
            cancellationToken);
        var ordersCount = ordersStatus.Count();
        // var order = await _unitOfWork.OrderRepository.GetOrderByID((long)orderIds[0]);

        var anyOrderAssign = userOrdersAssignedPerMonth.FirstOrDefault();
        var userAccount = await _unitOfWork.CaptainRepository.GetCaptainUserAccountByIdAsync(anyOrderAssign.CaptainUserAccountId,cancellationToken);
        var user = await _unitOfWork.CaptainRepository.GetCaptainUserByIdAsync(userAccount.CaptainUserId,cancellationToken);
        var bonusPerCountry = await _unitOfWork.CaptainRepository.GetBonusByCountryAsync(user.ResidenceCountryId,cancellationToken);


        var userBonus = new CaptainUserBonus();
        if (ordersCount >= bonusPerCountry.OrdersPerMonth)
        {
            userBonus = new CaptainUserBonus()
            {
                CaptainUserAccountId = bonusCheckDto.captainUserAccountId,
                BonusTypeId = (long)BonusTypes.BonusPerMonth,
                CreationDate = DateTime.Now,
                Amount = bonusPerCountry.BonusPerMonth
            };
            var insertedBonus = await _unitOfWork.CaptainRepository.InsertCaptainUserBonusAsync(userBonus,cancellationToken);
            var result = await _unitOfWork.Save(cancellationToken);
            if (result == 0) throw new ServiceUnavailableException("Service Unavailable");

            return new
            {
                Bonus = userBonus,
                Message = "User Delivered about " + ordersCount + " Orders in Month" + bonusCheckDto.date.Month
            };
        }
        else
        {
            return new
            {
                Message = "User has no bonus as he delivered about " + ordersCount + " Orders in Month " +
                          bonusCheckDto.date.Month
            };
        }

    }


    public async Task<object> CheckBonusPerYearAsync(BonusCheckDto bonusCheckDto, CancellationToken cancellationToken)
    {

        /// Check Bonus
        var userOrdersAssignedPerYear = await _unitOfWork.OrderRepository.GetOrdersAssignmentsByAsync(a => a.CaptainUserAccountId == bonusCheckDto.captainUserAccountId &&
                                 a.CreationDate.Value.Year == bonusCheckDto.date.Year,cancellationToken);

        var orderIds = userOrdersAssignedPerYear.Select(a => a.OrderId).ToList();
        var ordersStatus = await _unitOfWork.OrderRepository.GetOrderCurrentStatusesByAsync(s => orderIds.Contains(s.OrderId) &&
       (s.OrderStatusTypeId == (long)OrderStatusTypes.Dropped),cancellationToken);
        var ordersCount = ordersStatus.Count();
        //var order = await _unitOfWork.OrderRepository.GetOrderByID((long)orderIds[0]);

        var anyOrderAssign = userOrdersAssignedPerYear.FirstOrDefault();
        var userAccount = await _unitOfWork.CaptainRepository.GetCaptainUserAccountByIdAsync(anyOrderAssign.CaptainUserAccountId,cancellationToken);
        var user = await _unitOfWork.CaptainRepository.GetCaptainUserByIdAsync(userAccount.CaptainUserId,cancellationToken);
        var bonusPerCountry = await _unitOfWork.CaptainRepository.GetBonusByCountryAsync(user.ResidenceCountryId,cancellationToken);

        var userBonus = new CaptainUserBonus();
        if (ordersCount >= bonusPerCountry.OrdersPerYear)
        {
            userBonus = new CaptainUserBonus()
            {
                CaptainUserAccountId = bonusCheckDto.captainUserAccountId,
                BonusTypeId = (long)BonusTypes.BonusPerYear,
                CreationDate = DateTime.Now,
                Amount = bonusPerCountry.BonusPerYear
            };
            var insertedBonus = await _unitOfWork.CaptainRepository.InsertCaptainUserBonusAsync(userBonus, cancellationToken);
            var result = await _unitOfWork.Save(cancellationToken);
            if (result == 0) throw new ServiceUnavailableException("Service Unavailable");

            return new { Bonus = userBonus, Message = "User Delivered about " + ordersCount + " Orders in Year " + bonusCheckDto.date.Year };
        }
        else
        {
            return new { Message = "User has no bonus as he delivered about " + ordersCount + " Orders in Year " + bonusCheckDto.date.Year };
        }


    }



    public async Task<string> SendFirebaseNotificationAsync(FBNotify fbNotify, CancellationToken cancellationToken)
    {
        try
        {
            var result = await Task.Run(() => {
                return FirebaseNotification.SendNotificationToTopic(FirebaseTopics.Captains, fbNotify.Title, fbNotify.Message);
            },cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            return "";// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }
    /**/




    public async Task<IEnumerable<NearCaptainUser>> GetCaptainsUsersNearToLocationAsync(Location location, CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.CaptainRepository.GetCaptainsUsersNearToLocationAsync(location.Lat, location.Lng,cancellationToken);

        }
        catch (Exception e)
        {
            return new List<NearCaptainUser>();// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }
    /**/


}
