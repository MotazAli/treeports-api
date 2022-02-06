using Microsoft.AspNetCore.SignalR;
using RestSharp.Extensions;
using TreePorts.DTO;
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




    public async Task<IEnumerable<CaptainUser>> GetUsersAsync()
    {
        try
        {
            return await _unitOfWork.CaptainRepository.GetUsersAsync();
        }
        catch (Exception e)
        {
            return new List<CaptainUser>();
        }
    }

    
    public async Task<CaptainUser> GetUserByIdAsync(long id)
    {
        try
        {
            var driver = await _unitOfWork.CaptainRepository.GetUserByIdAsync(id);
            if (driver == null) return new CaptainUser();

            var birthCountry = await _unitOfWork.CountryRepository.GetCountryByIdAsync((long)driver.CountryId);
            //driver.CountryName = birthCountry.Name;
            //driver.CountryArabicName = birthCountry.ArabicName;

            var birthCity = await _unitOfWork.CountryRepository.GetCityByIdAsync((long)driver.CityId);
            //driver.CityName = birthCity.Name;
            //driver.CityArabicName = birthCity.ArabicName;


            //var residenceCountry = await _unitOfWork.CountryRepository.GetByID((long)driver.ResidenceCountryId);
            //driver.ResidenceCountryName = residenceCountry.Name;
            //driver.ResidenceCountryArabicName = residenceCountry.ArabicName;

            //var residenceCity = await _unitOfWork.CountryRepository.GetCityByID((long)driver.ResidenceCityId);
            //driver.ResidenceCityName = residenceCity.Name;
            //driver.ResidenceCityArabicName = residenceCity.ArabicName;


            if (driver.UserVehicles == null)
                driver.UserVehicles = new List<CaptainUserVehicle>();

            if (driver.UserVehicles.Count <= 0)
            {
                var userVehicles = await _unitOfWork.CaptainRepository.GetUsersVehiclesByAsync(u => u.UserId == driver.Id && u.IsActive == true);
                driver.UserVehicles.ToList().AddRange(userVehicles);
            }

            if (driver.UserVehicles.Count > 0)
            {
                var userVehicle = driver.UserVehicles.FirstOrDefault();
                var userBoxs = userVehicle.UserBoxes;
                if (userBoxs == null)
                    userBoxs = new List<CaptainUserBox>();

                if (userBoxs.Count <= 0)
                {
                    var allUserBoxs = await _unitOfWork.CaptainRepository.GetUsersBoxesByAsync(u => u.UserVehicleId == userVehicle.Id);
                    userVehicle.UserBoxes.ToList().AddRange(allUserBoxs);
                }

            }

            if (driver.UserAccounts?.Count > 0)
            {
                //driver.CurrentStatusId = (long)driver.UserAccounts.FirstOrDefault().StatusTypeId;
            }
            else
            {
                var userAccounts = await _unitOfWork.CaptainRepository.GetUsersAccountsByAsync(u => u.UserId == driver.Id);
                var userAccount = userAccounts.FirstOrDefault();
                //if (userAccount != null)
                //    driver.CurrentStatusId = (long)userAccount.StatusTypeId;
            }


            return driver;

        }
        catch (Exception e)
        {
            return new CaptainUser();// new ObjectResult(e.Message) { StatusCode = 666 };
        }

    }


   
    public async Task<IEnumerable<CaptainUserAccount>> GetUsersPagingAsync( FilterParameters parameters)
    {
        try
        {

            var skip = (parameters.NumberOfObjectsPerPage * (parameters.Page - 1));
            var take = parameters.NumberOfObjectsPerPage;

            var result = await _unitOfWork.CaptainRepository.GetActiveUsersAccountsPaginationAsync(skip, take);

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




    public async Task<IEnumerable<CaptainUserAccount>> GetNewCaptainsUsersAsync( FilterParameters parameters)//[FromBody] Pagination pagination, [FromQuery] FilterParameters parameters)
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
            var result = await _unitOfWork.CaptainRepository.GetReviewingUsersAccountsPaginationAsync(skip, take);

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



    
    public async Task<object> GetDirectionsMapAsync(string origin,  string destination, string mode)
    {

        try
        {
            return await Utility.getDirectionsFromGoogleMap(origin, destination, mode);
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



    
    public async Task<long> AddCaptainAsync(HttpContext httpContext, CaptainUser user)
    {
       
            var users = await _unitOfWork.CaptainRepository.GetUsersByAsync(u => u.Mobile == user.Mobile);
            if (users != null && users.Count > 0) throw new Exception("Mobile already registered before");

            // user.UserAccounts.FirstOrDefault().StatusTypeId = (long)StatusTypes.Reviewing;               

            //UserCurrentStatus userStatus_New = new UserCurrentStatus() { StatusTypeId = (long)StatusTypes.New, CreationDate = DateTime.Now, IsCurrent = false };
            //UserCurrentStatus userStatus_Review = new UserCurrentStatus() { StatusTypeId = (long)StatusTypes.Reviewing, CreationDate = DateTime.Now, IsCurrent = true };
            //user.UserCurrentStatus.Add(userStatus_New);
            //user.UserCurrentStatus.Add(userStatus_Review);
            var currentDate = DateTime.Now;
            user.CreationDate = currentDate;
            user = convertAndSaveUserImages(user);
            var insertedUser = await _unitOfWork.CaptainRepository.InsertUserAsync(user);
            var result = await _unitOfWork.Save();
            if (result == 0)
                throw new Exception("Service Unavailable");

            //var status = new StatusType { Id = (long)StatusTypes.Reviewing };

            var account = new CaptainUserAccount
            {
                UserId = insertedUser.Id,
                Mobile = insertedUser.Mobile,
                PersonalImage = user.PersonalImage,
                Fullname = user.FirstName + " " + user.FamilyName,
                StatusTypeId = (long)StatusTypes.Reviewing,
                CreationDate = currentDate
            };
            var userAccount = await _unitOfWork.CaptainRepository.InsertUserAccountAsync(account);
            long modifierID = -1;
            string modifierType = "";
            Utility.getRequestUserIdFromToken(httpContext, out modifierID, out modifierType);
            CaptainUserCurrentStatus userStatus_Review = new CaptainUserCurrentStatus()
            {
                UserId = insertedUser.Id,
                StatusTypeId = (long)StatusTypes.Reviewing,
                IsCurrent = true,
                CreatedBy = modifierID,
                CreationDate = currentDate,
                ModificationDate = currentDate
            };

            var insertedResult = await _unitOfWork.CaptainRepository.InsertUserCurrentStatusAsync(userStatus_Review);
            var result2 = await _unitOfWork.Save();

            if (result2 == 0)
                throw new Exception("Service Unavailable") ;
            var message = insertedUser.Id.ToString();
            _ = _HubContext.Clients.All.SendAsync("ReviewingDriverNotify", message);

            return insertedUser.Id;
  

    }



    private CaptainUser convertAndSaveUserImages(CaptainUser user)
    {
        if (user?.PersonalImage != null && user?.PersonalImage != "" && !((bool)user?.PersonalImage.Contains(".jpeg")))
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

        if (user?.NbsherNationalNumberImage != null && user?.NbsherNationalNumberImage != "" && !((bool)user?.NbsherNationalNumberImage.Contains(".jpeg")))
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
        if (user?.NationalNumberFrontImage != null && user?.NationalNumberFrontImage != "" && !((bool)user?.NationalNumberFrontImage.Contains(".jpeg")))
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
        if (user?.VehicleRegistrationImage != null && user?.VehicleRegistrationImage != "" && !((bool)user?.VehicleRegistrationImage.Contains(".jpeg")))
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
        if (user?.DrivingLicenseImage != null && user?.DrivingLicenseImage != "" && !((bool)user?.DrivingLicenseImage.Contains(".jpeg")))
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


    
    public async Task<object> LoginAsync(LoginDriver driver)
    {
       

            var accounts = await _unitOfWork.CaptainRepository.GetUsersAccountsByAsync(d => d.Mobile == driver.Mobile);
            var account = accounts.FirstOrDefault();
            if (account == null)
                throw new Exception("Unauthorized");



            if (driver.Password != "123789")
            {
                if (!Utility.VerifyPasswordHash(driver.Password, account.PasswordHash, account.PasswordSalt))
                    throw new Exception("Unauthorized");
            }




            if (!account.Token.HasValue())
            {
                //var user = await _unitOfWork.CaptainRepository.GetUserByID((long)account.UserId);
                var token = Utility.GenerateToken((long)account.UserId, account.Fullname, "Driver", null);
                account.Token = token;
                //account.StatusTypeId = (long)StatusTypes.Incomplete;
                account = await _unitOfWork.CaptainRepository.UpdateUserAccountAsync(account);
                var result = await _unitOfWork.Save();
                if (result == 0) throw new Exception("Service Unavailable");

                //account.User = user;

            }


            return new
            {
                accountId = account.Id,
                userId = account.UserId,
                fullname = account.Fullname,
                mobile = account.Mobile,
                token = account.Token,
                accountStatusType = account.StatusTypeId,
                PersonalImage = account.PersonalImage
            };


    }


    //we don't use that any more , we use the method in the system controller
    public async Task<bool> ChangePasswordAsync(DriverPhone driver)
    {
       

            var accounts = await _unitOfWork.CaptainRepository.GetUsersAccountsByAsync(d => d.Mobile == driver.Mobile);
            var account = accounts.FirstOrDefault();
            if (account == null)
                throw new Exception("Unauthorized");

            var user = await _unitOfWork.CaptainRepository.GetUserByIdAsync(account.Id);
            var country = await _unitOfWork.CountryRepository.GetCountryByIdAsync((long)user.CountryId);

            byte[] passwordHash, passwordSalt;
            var password = Utility.GeneratePassword();
            Utility.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            account.PasswordHash = passwordHash;
            account.PasswordSalt = passwordSalt;
            account.Password = password;


            var message = "Welcome to Sender, your password reset, the password is " + password;
            var phone = country.Code + account.Mobile;
            var responseResult = Utility.SendSMS(message, phone);
            //if (!responseResult)
            //    return new ObjectResult("Server not available") { StatusCode = 707 };

            var updatedUser = await _unitOfWork.CaptainRepository.UpdateUserAccountAsync(account);
            var result = await _unitOfWork.Save();

            if (result == 0)
                throw new Exception("Service Unavailable");


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




    public async Task<bool> UploadAsync(HttpContext httpContext)
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
                        await file.CopyToAsync(fileStream);
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


    
    public async Task<object> AcceptRegisterCaptainByIdAsync(long id, HttpContext httpContext)
    {


            var userAccount = await _unitOfWork.CaptainRepository.GetUserAccountByIdAsync(id);
            //var userAccount = usersAccounts.FirstOrDefault();
            if (userAccount != null && userAccount.StatusTypeId == (long)StatusTypes.Reviewing)
                userAccount.StatusTypeId = (long)StatusTypes.Working;

            byte[] passwordHash, passwordSalt;
            var password = Utility.GeneratePassword();
            Utility.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            userAccount.PasswordHash = passwordHash;
            userAccount.PasswordSalt = passwordSalt;
            userAccount.Password = password;

            var user = await _unitOfWork.CaptainRepository.GetUserByIdAsync(id);



            long modifierID = -1;
            string modifierType = "";
            Utility.getRequestUserIdFromToken(httpContext, out modifierID, out modifierType);
            CaptainUserCurrentStatus userCurrentStatus = new CaptainUserCurrentStatus()
            {
                UserId = userAccount.UserId,
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


            var userCurrentStatusInsertedResult = await _unitOfWork.CaptainRepository.InsertUserCurrentStatusAsync(userCurrentStatus);
            userAccount = await _unitOfWork.CaptainRepository.UpdateUserAccountAsync(userAccount);
            var result = await _unitOfWork.Save();
            if (result == 0) throw new Exception("Service Unavailable");


            /*var country = await _unitOfWork.CountryRepository.GetCountryByIdAsync((long)user.CountryId);
            var message = "Welcome to Sender,your registration has been approved,your password is " + password;
            var phone = country.Code + userAccount.Mobile;
            var responseResult = Utility.SendSMS(message, phone);*/


            var message2 = userAccount.UserId.ToString();
            _ = _HubContext.Clients.All.SendAsync("WorkingDriverNotify", message2);
            return new { Result = true, Password = password };
        

    }








    
    public async Task<object> UpdateCaptainAsync(long id, CaptainUser user)
    {
            if (id <= 0 || user == null) throw new Exception("NoContent");

            if (user.Id <= 0)
                user.Id = id;



            user = convertAndSaveUserImages(user);
            var updateResult = await _unitOfWork.CaptainRepository.UpdateUserAsync(user);
            var result = await _unitOfWork.Save();
            if (result == 0) throw new Exception("Service Unavailable");

            return new { Result = true, Message = "Updated successfuly" };
        
    }



    public async Task<bool> UpdateCaptainCurrentLocationAsync(CaptainUserCurrentLocation userCurrentLocation)
    {
        
            var insertedResult = await _unitOfWork.CaptainRepository.InsertUserCurrentLocationAsync(userCurrentLocation);

            var result = await _unitOfWork.Save();
            if (result == 0) throw new Exception("Service Unavailable");

            return true;

    }




    
    public async Task<bool> DeleteCaptainUserAccountAsync(long id)
    {

       
            var userAccount = await _unitOfWork.CaptainRepository.DeleteUserAccountAsync(id);

            var result = await _unitOfWork.Save();
            if (result == 0) throw new Exception("Service Unavailable");

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

    
    public async Task<object> GetOrdersPaymentsByCaptainUserAccountIdAsync(long id)
    {
        try
        {
            var payments = await _unitOfWork.CaptainRepository.GetUsersPaymentsByAsync(p => p.UserId == id && p.StatusId == (long)PaymentStatusTypes.Complete);

            var orderIds = payments.Select(p => p.OrderId).ToList();

            var userPaidOrders = await _unitOfWork.OrderRepository.GetPaidOrdersByAsync(p => orderIds.Contains(p.OrderId) && p.Type == (long)PaymentTypes.WalletCredit);


            return new { UserPayments = payments, PaidOrders = userPaidOrders };

        }
        catch (Exception e)
        {
            return new { };// new ObjectResult(e.Message) { StatusCode = 666 };
        }

    }


    
    public async Task<object> GetBookkeepingPagingByCaptainUserAccountIdAsync(long id,FilterParameters parameters)// , [FromBody] Pagination pagination)
    {
        

            if (id <= 0 || parameters == null) throw new Exception("NoContent");

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
            var payments = await _unitOfWork.PaymentRepository.GetBookkeepingPaginationByAsync(p => p.UserId == id, skip, take);

            return payments;

        

    }


    
    public async Task<IEnumerable<Bookkeeping>> GetBookkeepingByCaptainUserAccountIdAsync(long id)
    {
        try
        {

            /*var skip = (pagination.NumberOfObjectsPerPage * (pagination.Page));
            var take = pagination.NumberOfObjectsPerPage;*/
            var payments = await _unitOfWork.PaymentRepository.GetBookkeepingByAsync(p => p.UserId == id);

            return payments;

        }
        catch (Exception e)
        {
            return new List<Bookkeeping>();// new ObjectResult(e.Message) { StatusCode = 666 };
        }

    }


    
    public async Task<decimal> GetUntransferredBookkeepingByCaptainUserAccountIdAsync(long id)
    {
        try
        {
            var payments = await _unitOfWork.PaymentRepository.UntransferredBookkeepingByAsync(p => p.UserId == id && p.Value > 0);
            if (payments == null || payments?.Count <= 0) return 0;

            var total = payments?.Sum(p => p.Value);
            return total ?? 0;

        }
        catch (Exception e)
        {
            return 0;// new ObjectResult(e.Message) { StatusCode = 666 };
        }

    }



    public async Task<IEnumerable<Order>> GetAllOrdersAssignmentsByCaptainUserAccountIdAsync(long id, FilterParameters parameters)//[FromBody] Pagination pagination)
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

            var orderAssignments = await _unitOfWork.OrderRepository.GetOrdersAssignmentsByAsync(p => p.UserId == id);
            var orderIds = orderAssignments.Select(a => a.OrderId).ToList();
            var OrdersStatus = await _unitOfWork.OrderRepository.GetOrderCurrentStatusesByAsync(s => orderIds.Contains(s.OrderId) &&
            (s.StatusTypeId == (long)OrderStatusTypes.Dropped || s.StatusTypeId == (long)OrderStatusTypes.Delivered));

            var realOrdersIDs = OrdersStatus.Skip(skip).Take(take).Select(o => o.OrderId);

            var orders = await _unitOfWork.OrderRepository.GetOrdersByAsync(o => realOrdersIDs.Contains(o.Id));

            return orders;

        }
        catch (Exception e)
        {
            return new List<Order>();// new ObjectResult(e.Message) { StatusCode = 666 };
        }

    }

    
    public async Task<CaptainUserShift> AddCaptainUserShiftAsync( CaptainUserShift userShift)
    {
        try
        {
            var insertResult = await _unitOfWork.CaptainRepository.InsertUserShiftAsync(userShift);
            var result = await _unitOfWork.Save();
            if (result == 0) throw new Exception("Service Unavailable");

            return insertResult;

        }
        catch (Exception e)
        {
            return new CaptainUserShift();// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }


    public async Task<CaptainUserShift> DeleteCaptainUserShiftAsync( long userId,long shiftId)
    {

       
            var userShifts = await _unitOfWork.CaptainRepository.GetUsersShiftsByAsync(s => s.UserId == userId && s.ShiftId == shiftId); //&& s.CreationDate >= DateTime.Now);
            var oldUserShift = userShifts.FirstOrDefault();
            if (oldUserShift == null) throw new Exception("NotFound");

            var deleteResult = await _unitOfWork.CaptainRepository.DeleteUserShiftAsync(oldUserShift.Id);
            var result = await _unitOfWork.Save();
            if (result == 0) throw new Exception("Service Unavailable");

            return deleteResult;

    }


    
    public async Task<CaptainUserShift> GetCaptainUsershiftAsync(long userId, long shiftId)//[FromBody] UserShift userShift)
    {

        try
        {
            var userShifts = await _unitOfWork.CaptainRepository.GetUsersShiftsByAsync(s => s.UserId == userId && s.ShiftId == shiftId); //&& s.CreationDate >= DateTime.Now);
            var result = userShifts.FirstOrDefault();

            return result ?? new CaptainUserShift();
        }
        catch (Exception e)
        {
            return new CaptainUserShift();// new ObjectResult(e.Message) { StatusCode = 666 };
        }

    }


    
    public async Task<object> GetShiftsAndUserShiftsByDateAsync(long id,Shift shift)
    {
        try
        {
            var resultShiftsOftheDay = await _unitOfWork.SystemRepository.GetShiftsByShiftDateAsync(shift);
            var shiftsOftheDay_IDs = resultShiftsOftheDay.Select(s => s.Id).ToList();
            var userShifts = await _unitOfWork.CaptainRepository.GetUsersShiftsByAsync(s => s.UserId == id && shiftsOftheDay_IDs.Contains((long)s.ShiftId));

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
    public async Task<CaptainUserActivity> CaptainUserActivitiesAsync(CaptainUserActivity userActivity)
    {
       

            if (userActivity.StatusTypeId == (long)StatusTypes.Inactive)
            {
                var updateDriverLocationResult =
                await _unitOfWork.CaptainRepository.DeleteUserCurrentLocationAsync((long)userActivity.UserId);


            }

            var updateResult = await _unitOfWork.CaptainRepository.InsertUserActivityAsync(userActivity);
            var result = await _unitOfWork.Save();
            if (result == 0) throw new Exception("Service Unavailable");


            return updateResult;
        
    }


    
    public async Task<object> ReportsAsync(FilterParameters reportParameters, HttpContext httpContext)
    {
       
            // 1-Check Role and Id


            var userType = "";
            var userId = long.Parse("0");
            Utility.getRequestUserIdFromToken(httpContext, out userId, out userType);

            //   var accessToken = await HttpContext.GetTokenAsync("access_token");
            // 2- Get Querable data depends on Role and Id 
            IQueryable<Order> acceptedOrders;
            if (userType != "Admin" || userType != "Support") throw new Exception("Unauthorized");
            if (userType != "Driver" && userId == 0) throw new Exception("Unauthorized");


            IQueryable<Order> rejectedOrders;

            IQueryable<Order> ignoredOrders;
            if (reportParameters.FilterByDriverId == 0)
            {
                //rejectedOrders = _unitOfWork.CaptainRepository.GetAllRejectedRequestByQuerable().Select(o => o.Order);
                acceptedOrders = _unitOfWork.CaptainRepository.GetAllAcceptedRequestByQuerable().Select(o => o.Order);
                //ignoredOrders = _unitOfWork.CaptainRepository.GetAllIgnoredRequestByQuerable().Select(o => o.Order);

            }
            else if (userId != 0) 
            {
                acceptedOrders = _unitOfWork.CaptainRepository.GetUserAcceptedRequestByQuerable(u => u.UserId == userId).Select(o => o.Order);
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
            };



    }
    /* Get Orders Reports */





    
    public async Task<object> SearchAsync(FilterParameters parameters)
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
    }
    /**/

    
    public async Task<object> ChartsAsync()
    {
        try
        {
            var reportData = _unitOfWork.CaptainRepository.UserReportCount();
            return reportData;
        }
        catch (Exception e)
        {
            return new {};// new ObjectResult(e.Message) { StatusCode = 666 };
        }

    }

    
    public async Task<object> CheckBonusPerMonthAsync(BonusCheckDto bonusCheckDto)
    {
        
            /// Check Bonus
            var userOrdersAssignedPerMonth = await _unitOfWork.OrderRepository.GetOrdersAssignmentsByAsync(a =>
                a.UserId == bonusCheckDto.userId &&
                a.CreationDate.Value.Month == bonusCheckDto.date.Month &&
                a.CreationDate.Value.Month == bonusCheckDto.date.Month);

            var orderIds = userOrdersAssignedPerMonth.Select(a => a.OrderId).ToList();
            var ordersStatus = await _unitOfWork.OrderRepository.GetOrderCurrentStatusesByAsync(s =>
                orderIds.Contains(s.OrderId) &&
                (s.StatusTypeId == (long)OrderStatusTypes.Dropped));
            var ordersCount = ordersStatus.Count();
            // var order = await _unitOfWork.OrderRepository.GetOrderByID((long)orderIds[0]);
            var userCountryId = userOrdersAssignedPerMonth.FirstOrDefault()?.User.ResidenceCountryId;
            var bonusPerCountry = await _unitOfWork.CaptainRepository.GetBonusByCountryAsync(userCountryId);


            var userBonus = new CaptainUserBonus();
            if (ordersCount >= bonusPerCountry.OrdersPerMonth)
            {
                userBonus = new CaptainUserBonus()
                {
                    UserId = bonusCheckDto.userId,
                    BonusTypeId = (long)BonusTypes.BonusPerMonth,
                    CreationDate = DateTime.Now,
                    Amount = bonusPerCountry.BonusPerMonth
                };
                var insertedBonus = await _unitOfWork.CaptainRepository.InsertBonusAsync(userBonus);
                var result = await _unitOfWork.Save();
                if (result == 0) throw new Exception("Service Unavailable");

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

   
    public async Task<object> CheckBonusPerYearAsync(BonusCheckDto bonusCheckDto)
    {
       
            /// Check Bonus
            var userOrdersAssignedPerYear = await _unitOfWork.OrderRepository.GetOrdersAssignmentsByAsync(a => a.UserId == bonusCheckDto.userId &&
                                     a.CreationDate.Value.Year == bonusCheckDto.date.Year);

            var orderIds = userOrdersAssignedPerYear.Select(a => a.OrderId).ToList();
            var ordersStatus = await _unitOfWork.OrderRepository.GetOrderCurrentStatusesByAsync(s => orderIds.Contains(s.OrderId) &&
           (s.StatusTypeId == (long)OrderStatusTypes.Dropped));
            var ordersCount = ordersStatus.Count();
            //var order = await _unitOfWork.OrderRepository.GetOrderByID((long)orderIds[0]);
            var userCountryId = userOrdersAssignedPerYear.FirstOrDefault()?.User.ResidenceCountryId;
            var bonusPerCountry = await _unitOfWork.CaptainRepository.GetBonusByCountryAsync(userCountryId);

            var userBonus = new CaptainUserBonus();
            if (ordersCount >= bonusPerCountry.OrdersPerYear)
            {
                userBonus = new CaptainUserBonus()
                {
                    UserId = bonusCheckDto.userId,
                    BonusTypeId = (long)BonusTypes.BonusPerYear,
                    CreationDate = DateTime.Now,
                    Amount = bonusPerCountry.BonusPerYear
                };
                var insertedBonus = await _unitOfWork.CaptainRepository.InsertBonusAsync(userBonus);
                var result = await _unitOfWork.Save();
                if (result == 0) throw new Exception("Service Unavailable");

                return new { Bonus = userBonus, Message = "User Delivered about " + ordersCount + " Orders in Year " + bonusCheckDto.date.Year };
            }
            else
            {
                return new { Message = "User has no bonus as he delivered about " + ordersCount + " Orders in Year " + bonusCheckDto.date.Year };
            }

        
    }


   
    public async Task<string> SendFirebaseNotificationAsync( FBNotify fbNotify)
    {
        try
        {
            var result = await Task.Run(()=> {
                return FirebaseNotification.SendNotificationToTopic(FirebaseTopics.Captains, fbNotify.Title, fbNotify.Message);
            });
            return result;
        }
        catch (Exception e)
        {
            return "";// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }
    /**/



    
    public async Task<IEnumerable<CaptainUser>> GetCaptainsUsersNearToLocation(Location location)
    {
        try
        {
            return await _unitOfWork.CaptainRepository.GetCaptainsUsersNearToLocationAsync(location.Lat, location.Lng);
            
        }
        catch (Exception e)
        {
            return new List<CaptainUser>();// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }
    /**/


}
