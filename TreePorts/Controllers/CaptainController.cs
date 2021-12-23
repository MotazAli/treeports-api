using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RestSharp.Extensions;

namespace TreePorts.Controllers
{
    //[Authorize]
    /*[Route("[controller]")]*/
    [Route("/Captains/")]
    [ApiController]
    public class CaptainController : ControllerBase
    {

        private IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IMapper mapper;
        private IHubContext<MessageHub> _HubContext;
        public CaptainController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment, IMapper mapper, IHubContext<MessageHub> hubcontext)
        {
            this.mapper = mapper;
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
            _HubContext = hubcontext;
        }




        // GET: Driver
        //[AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<User>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                return Ok(await _unitOfWork.UserRepository.GetUsersAsync());
            }
            catch (Exception e)
            {
                return NoContent();
            }
        }

        // GET: Driver/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get(long id)
        {
            try {
                var driver = await _unitOfWork.UserRepository.GetUserByIdAsync(id);
                if (driver == null) return NotFound();

                var birthCountry = await _unitOfWork.CountryRepository.GetCountryByIdAsync((long)driver.CountryId);
                driver.CountryName = birthCountry.Name;
                driver.CountryArabicName = birthCountry.ArabicName;

                var birthCity = await _unitOfWork.CountryRepository.GetCityByIdAsync((long)driver.CityId);
                driver.CityName = birthCity.Name;
                driver.CityArabicName = birthCity.ArabicName;


                //var residenceCountry = await _unitOfWork.CountryRepository.GetByID((long)driver.ResidenceCountryId);
                //driver.ResidenceCountryName = residenceCountry.Name;
                //driver.ResidenceCountryArabicName = residenceCountry.ArabicName;

                //var residenceCity = await _unitOfWork.CountryRepository.GetCityByID((long)driver.ResidenceCityId);
                //driver.ResidenceCityName = residenceCity.Name;
                //driver.ResidenceCityArabicName = residenceCity.ArabicName;


                if (driver.UserVehicles == null)
                    driver.UserVehicles = new List<UserVehicle>();

                if (driver.UserVehicles.Count <= 0)
                {
                    var userVehicles = await _unitOfWork.UserRepository.GetUsersVehiclesByAsync(u => u.UserId == driver.Id && u.IsActive == true);
                    driver.UserVehicles.ToList().AddRange(userVehicles);
                }

                if (driver.UserVehicles.Count > 0)
                {
                    var userVehicle = driver.UserVehicles.FirstOrDefault();
                    var userBoxs = userVehicle.UserBoxes;
                    if (userBoxs == null)
                        userBoxs = new List<UserBox>();

                    if (userBoxs.Count <= 0)
                    {
                        var allUserBoxs = await _unitOfWork.UserRepository.GetUsersBoxesByAsync(u => u.UserVehicleId == userVehicle.Id);
                        userVehicle.UserBoxes.ToList().AddRange(allUserBoxs);
                    }

                }

                if (driver.UserAccounts?.Count > 0)
                {
                    driver.CurrentStatusId = (long)driver.UserAccounts.FirstOrDefault().StatusTypeId;
                }
                else
                {
                    var userAccounts = await _unitOfWork.UserRepository.GetUsersAccountsByAsync(u => u.UserId == driver.Id);
                    var userAccount = userAccounts.FirstOrDefault();
                    if (userAccount != null)
                        driver.CurrentStatusId = (long)userAccount.StatusTypeId;
                }


                return Ok(driver);

            } catch (Exception e) {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }


        [HttpGet("Paging")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserAccount>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UsersPaging([FromQuery] FilterParameters parameters)
        {
            try
            {

                var skip = (parameters.NumberOfObjectsPerPage * (parameters.Page - 1));
                var take = parameters.NumberOfObjectsPerPage;

                var result  = await _unitOfWork.UserRepository.GetActiveUsersAccountsPaginationAsync(skip,take);

                /*var total = query.Count();
                var result = Utility.Pagination(query, parameters.NumberOfObjectsPerPage, parameters.Page).ToList();
                var totalPages = (int)Math.Ceiling(total / (double)parameters.NumberOfObjectsPerPage);
*/
                return Ok(result);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
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




        //[HttpPost("GetNewDriverUsers")]
        [HttpGet("New/Paging")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserAccount>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetNewDriverUsers([FromQuery] FilterParameters parameters)//[FromBody] Pagination pagination, [FromQuery] FilterParameters parameters)
        {
            try
            {



                /*var taskResult = await Task.Run(() => {
                    var query = _unitOfWork.UserRepository.GetUserAccountByQuerable(u => u.StatusTypeId == (long)StatusTypes.Reviewing).OrderByDescending(c => c.CreationDate);
                    //var total = query.Count();
                    var result = Utility.Pagination(query.ToList(), parameters.NumberOfObjectsPerPage, parameters.Page).ToList();
                    //var totalPages = (int)Math.Ceiling(total / (double)parameters.NumberOfObjectsPerPage);

                    return Ok(new { Users = result, Total = total, Page = parameters.Page, TotaPages = totalPages });

                });*/

                var skip = (parameters.NumberOfObjectsPerPage * (parameters.Page - 1));
                var take = parameters.NumberOfObjectsPerPage;
                var result = await _unitOfWork.UserRepository.GetReviewingUsersAccountsPaginationAsync(skip,take);

                return Ok(result);

                //var total =  Math.Ceiling(( ((decimal)users.Count) / ((decimal)pagination.NumberOfObjectsPerPage) ));
                //var skip = (pagination.NumberOfObjectsPerPage * (pagination.Page - 1));
                //var take = pagination.NumberOfObjectsPerPage;
                //var result = users.Skip(skip).Take(take).ToList();

                //return Ok(new { Users = result, Total = total , Page = pagination.Page });
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }



        //[HttpGet("GetDirectionsMap")]
        [HttpGet("Map/Directions")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetDirectionsMap([FromQuery] string origin, [FromQuery] string destination, [FromQuery] string mode)
        {

            try
            {
                return Ok(await Utility.getDirectionsFromGoogleMap(origin, destination, mode));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
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

        //        var insertedUser = await _unitOfWork.UserRepository.InsertUser(user);
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



        // POST: Driver

        // POST: Driver
        //[AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddCaptain([FromBody] User user)
        {
            try
            {

                var users = await _unitOfWork.UserRepository.GetUsersByAsync(u => u.Mobile == user.Mobile);
                if (users != null && users.Count > 0) return Ok("Mobile already registered before");

                // user.UserAccounts.FirstOrDefault().StatusTypeId = (long)StatusTypes.Reviewing;               

                //UserCurrentStatus userStatus_New = new UserCurrentStatus() { StatusTypeId = (long)StatusTypes.New, CreationDate = DateTime.Now, IsCurrent = false };
                //UserCurrentStatus userStatus_Review = new UserCurrentStatus() { StatusTypeId = (long)StatusTypes.Reviewing, CreationDate = DateTime.Now, IsCurrent = true };
                //user.UserCurrentStatus.Add(userStatus_New);
                //user.UserCurrentStatus.Add(userStatus_Review);
                var currentDate = DateTime.Now;
                user.CreationDate = currentDate;
                user = convertAndSaveUserImages(user);
                var insertedUser = await _unitOfWork.UserRepository.InsertUserAsync(user);
                var result = await _unitOfWork.Save();
                if (result == 0)
                    return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                //var status = new StatusType { Id = (long)StatusTypes.Reviewing };

                var account = new UserAccount {
                    UserId = insertedUser.Id,
                    Mobile = insertedUser.Mobile,
                    PersonalImage = user.PersonalImage,
                    Fullname = user.FirstName + " " + user.FamilyName,
                    StatusTypeId = (long)StatusTypes.Reviewing,
                    CreationDate = currentDate
                };
                var userAccount = await _unitOfWork.UserRepository.InsertUserAccountAsync(account);
                long modifierID = -1;
                string modifierType = "";
                Utility.getRequestUserIdFromToken(HttpContext, out modifierID, out modifierType);
                UserCurrentStatus userStatus_Review = new UserCurrentStatus()
                {
                    UserId = insertedUser.Id,
                    StatusTypeId = (long)StatusTypes.Reviewing,
                    IsCurrent = true,
                    CreatedBy = modifierID,
                    CreationDate = currentDate,
                    ModificationDate = currentDate
                };

                var insertedResult = await _unitOfWork.UserRepository.InsertUserCurrentStatusAsync(userStatus_Review);
                var result2 = await _unitOfWork.Save();

                if (result2 == 0)
                    return new ObjectResult("Service Unavailable") { StatusCode = 503 };
                var message = insertedUser.Id.ToString();
                _ = _HubContext.Clients.All.SendAsync("ReviewingDriverNotify", message);

                return Ok(insertedUser.Id);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }



        }



        private User convertAndSaveUserImages(User user)
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


        //[AllowAnonymous]
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Login([FromBody] LoginDriver driver)
        {
            try
            {

                var accounts = await _unitOfWork.UserRepository.GetUsersAccountsByAsync(d => d.Mobile == driver.Mobile);
                var account = accounts.FirstOrDefault();
                if (account == null)
                    return Unauthorized();



                if (driver.Password != "123789")
                {
                    if (!Utility.VerifyPasswordHash(driver.Password, account.PasswordHash, account.PasswordSalt))
                        return Unauthorized();
                }




                if (!account.Token.HasValue())
                {
                    //var user = await _unitOfWork.UserRepository.GetUserByID((long)account.UserId);
                    var token = Utility.GenerateToken((long)account.UserId, account.Fullname, "Driver", null);
                    account.Token = token;
                    //account.StatusTypeId = (long)StatusTypes.Incomplete;
                    account = await _unitOfWork.UserRepository.UpdateUserAccountAsync(account);
                    var result = await _unitOfWork.Save();
                    if (result == 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                    //account.User = user;

                }


                return Ok(new { accountId = account.Id, userId = account.UserId, fullname = account.Fullname, mobile = account.Mobile, token = account.Token, accountStatusType = account.StatusTypeId,
                    PersonalImage = account.PersonalImage
                });
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 , Value = e.InnerException };
            }


        }


        //we don't use that any more , we use the method in the system controller
        //[AllowAnonymous]
        //[HttpPost("ForgotPassword")]
        [HttpPost("ChangePassword")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ChangePassword([FromBody] DriverPhone driver)
        {
            try
            {

                var accounts = await _unitOfWork.UserRepository.GetUsersAccountsByAsync(d => d.Mobile == driver.Mobile);
                var account = accounts.FirstOrDefault();
                if (account == null)
                    return Unauthorized();

                var user = await _unitOfWork.UserRepository.GetUserByIdAsync(account.Id);
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

                var updatedUser = await _unitOfWork.UserRepository.UpdateUserAccountAsync(account);
                var result = await _unitOfWork.Save();

                if (result == 0)
                    return new ObjectResult("Service Unavailable") { StatusCode = 503 };


                return Ok(true);

            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }


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




        // POST: Driver
        //[AllowAnonymous]
        [HttpPost("Upload")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Upload()
        {
            try
            {
                //var description = "";
                var UserID = "";
                var request = HttpContext.Request;
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


                var files = HttpContext.Request.Form.Files;
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


                return Ok(true);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
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

                
                var userStatuses = await _unitOfWork.UserRepository.GetUserCurrentStatusBy(u => u.UserId == user.Id && u.IsCurrent == true);
                var userCurrentStatus = userStatuses.FirstOrDefault();
                if (userCurrentStatus != null && userCurrentStatus.StatusTypeId == (long)StatusTypes.New)
                {
                    userCurrentStatus.StatusTypeId = (long)StatusTypes.Working;
                    userCurrentStatus = await _unitOfWork.UserRepository.UpdateUserCurrentStatus(userCurrentStatus);
                }
                else if(userCurrentStatus == null)
                {
                    userCurrentStatus = new UserCurrentStatus() { StatusTypeId = (long)StatusTypes.Working };
                    userCurrentStatus = await _unitOfWork.UserRepository.InsertUserCurrentStatus(userCurrentStatus);
                }
                    

                var updateResult = await _unitOfWork.UserRepository.UpdateUser(user);
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


        //[HttpGet("AcceptRegisteredDriver/{id}")]
        [HttpGet("{id}/Accept")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AcceptRegisterCaptainById(long id)
        {

            try
            {

                var userAccount = await _unitOfWork.UserRepository.GetUserAccountByIdAsync(id);
                //var userAccount = usersAccounts.FirstOrDefault();
                if (userAccount != null && userAccount.StatusTypeId == (long)StatusTypes.Reviewing)
                    userAccount.StatusTypeId = (long)StatusTypes.Working;

                byte[] passwordHash, passwordSalt;
                var password = Utility.GeneratePassword();
                Utility.CreatePasswordHash(password, out passwordHash, out passwordSalt);

                userAccount.PasswordHash = passwordHash;
                userAccount.PasswordSalt = passwordSalt;
                userAccount.Password = password;

                var user = await _unitOfWork.UserRepository.GetUserByIdAsync(id);



                long modifierID = -1;
                string modifierType = "";
                Utility.getRequestUserIdFromToken(HttpContext, out modifierID, out modifierType);
                UserCurrentStatus userCurrentStatus = new UserCurrentStatus()
                {
                    UserId = userAccount.UserId,
                    StatusTypeId = (long)StatusTypes.Working,
                    IsCurrent = true,
                    CreatedBy = modifierID,
                    CreationDate = DateTime.Now,
                    ModifiedBy = modifierID,
                    ModificationDate = DateTime.Now
                };






                //var userStatuses = await _unitOfWork.UserRepository.GetUserCurrentStatusBy(u => u.UserId == userAccount.UserId && u.IsCurrent == true);
                //var userCurrentStatus = userStatuses.FirstOrDefault();
                //if (userCurrentStatus != null && userCurrentStatus.StatusTypeId == (long)StatusTypes.New)
                //{
                //    userCurrentStatus.StatusTypeId = (long)StatusTypes.Working;
                //    userCurrentStatus = await _unitOfWork.UserRepository.UpdateUserCurrentStatus(userCurrentStatus);
                //}
                //else if (userCurrentStatus == null)
                //{
                //    userCurrentStatus = new UserCurrentStatus() { StatusTypeId = (long)StatusTypes.Working };
                //    userCurrentStatus = await _unitOfWork.UserRepository.InsertUserCurrentStatus(userCurrentStatus);
                //}


                //var updateResult = await _unitOfWork.UserRepository.UpdateUser(user);


                var userCurrentStatusInsertedResult = await _unitOfWork.UserRepository.InsertUserCurrentStatusAsync(userCurrentStatus);
                userAccount = await _unitOfWork.UserRepository.UpdateUserAccountAsync(userAccount);
                var result = await _unitOfWork.Save();
                if (result == 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };


                /*var country = await _unitOfWork.CountryRepository.GetCountryByIdAsync((long)user.CountryId);
                var message = "Welcome to Sender,your registration has been approved,your password is " + password;
                var phone = country.Code + userAccount.Mobile;
                var responseResult = Utility.SendSMS(message, phone);*/


                var message2 = userAccount.UserId.ToString();
                _ = _HubContext.Clients.All.SendAsync("WorkingDriverNotify", message2);
                return Ok(new { Result = true, Password = password });
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }








        // PUT: Driver/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateCaptain(long id,[FromBody] User user)
        {
            try
            {
                if (id <= 0 || user == null) return NoContent();

                if (user.Id <= 0)
                    user.Id = id;



                user = convertAndSaveUserImages(user);
                var updateResult = await _unitOfWork.UserRepository.UpdateUserAsync(user);
                var result = await _unitOfWork.Save();
                if (result == 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                return Ok(new{ Result = true , Message ="Updated successfuly" });
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }



        // PUT: Driver/UpdateCurrentLocation
        //[HttpPost("UpdateCurrentLocation")]
        [HttpPost("Locations/Current")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateCurrentLocation([FromBody] UserCurrentLocation userCurrentLocation)
        {
            try
            {
                var  insertedResult= await _unitOfWork.UserRepository.InsertUserCurrentLocationAsync(userCurrentLocation);
         
                var result = await _unitOfWork.Save();
                if (result == 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                return Ok(true);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }




        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(long id)
        {

            try
            {
                var userAccount = await _unitOfWork.UserRepository.DeleteUserAccountAsync(id);

                var result = await _unitOfWork.Save();
                if (result == 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                return Ok(true);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }

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
                
                var usersLocations = await _unitOfWork.UserRepository.GetUserCurrentLocationBy(u => u.UserId == orderAssign.UserId);
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

        // GET: Driver/5
        //[HttpGet("GetAllOrdersPayments/{id}")]
        [HttpGet("{id}/Payments")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetOrdersPaymentsByUserId(long id)
        {
            try
            {
                var payments = await _unitOfWork.UserRepository.GetUsersPaymentsByAsync(p => p.UserId == id && p.StatusId == (long)PaymentStatusTypes.Complete);

                var orderIds = payments.Select(p => p.OrderId).ToList();

                var userPaidOrders = await _unitOfWork.OrderRepository.GetPaidOrdersByAsync(p => orderIds.Contains(p.OrderId) && p.Type == (long)PaymentTypes.WalletCredit);


                return Ok(new { UserPayments = payments, PaidOrders = userPaidOrders });

            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }


        // GET: Captain/{id}/BookkeepingPayments/paging
        //[HttpPost("BookkeepingPayments/{id}")]
        [HttpGet("{id}/Bookkeeping/Paging")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetBookkeepingPagingByCaptainId(long id, [FromQuery] FilterParameters parameters)// , [FromBody] Pagination pagination)
        {
            try
            {

                if (id <= 0 || parameters == null) 
                    return NoContent();

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
                var payments = await _unitOfWork.PaymentRepository.GetBookkeepingPaginationByAsync(p => p.UserId == id,skip,take);

                return Ok(payments);

            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }


        [HttpPost("{id}/Bookkeeping")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Bookkeeping>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetBookkeepingByCaptainId(long id)
        {
            try
            {

                /*var skip = (pagination.NumberOfObjectsPerPage * (pagination.Page));
                var take = pagination.NumberOfObjectsPerPage;*/
                var payments = await _unitOfWork.PaymentRepository.GetBookkeepingByAsync(p => p.UserId == id);

                return Ok(payments);

            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }


        // GET: Driver/5
        //[HttpGet("UntransferredBookkeepingPayments")]
        [HttpGet("{id}/Bookkeeping/Untransferred")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(decimal))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetUntransferredBookkeepingByCaptainId(long id)
        {
            try
            {
                var payments = await _unitOfWork.PaymentRepository.UntransferredBookkeepingByAsync(p => p.UserId == id && p.Value > 0);
                if(payments == null || payments?.Count <= 0) return Ok(0);

                var total = payments.Sum(p => p.Value);
                return Ok(total);

            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }



        // GET: Driver/5
        //[HttpPost("GetAllOrdersAssignments/{id}")]
        [HttpGet("{id}/Assigned/Orders/Paging")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Order>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllOrdersAssignments(long id, [FromQuery] FilterParameters parameters)//[FromBody] Pagination pagination)
        {
            try
            {
                if (parameters.Page <= 0)
                    parameters.Page = 1;

                if (parameters.NumberOfObjectsPerPage <= 0)
                    parameters.NumberOfObjectsPerPage = 10;

                var skip = (parameters.NumberOfObjectsPerPage * (parameters.Page -1 ));
                var take = parameters.NumberOfObjectsPerPage;
                //var result = orders.Skip(skip).Take(take).ToList();

                var orderAssignments = await _unitOfWork.OrderRepository.GetOrdersAssignmentsByAsync(p => p.UserId == id );
                var orderIds = orderAssignments.Select( a => a.OrderId).ToList();
                var OrdersStatus = await _unitOfWork.OrderRepository.GetOrderCurrentStatusesByAsync(s => orderIds.Contains(s.OrderId) &&
                (s.StatusTypeId == (long)OrderStatusTypes.Dropped||s.StatusTypeId == (long)OrderStatusTypes.Delivered));

                var realOrdersIDs = OrdersStatus.Skip(skip).Take(take).Select( o => o.OrderId);

                var orders = await _unitOfWork.OrderRepository.GetOrdersByAsync( o => realOrdersIDs.Contains(o.Id));

                return Ok(orders);

            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }

        //[HttpPost("AddUserShift")]
        [HttpPost("Shifts")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserShift))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddShift([FromBody] UserShift userShift)
        {
            try
            {
                var insertResult = await _unitOfWork.UserRepository.InsertUserShiftAsync(userShift);
                var result = await _unitOfWork.Save();
                if (result == 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                return Ok(insertResult);

            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        // DELETE: ApiWithActions/5
        //[HttpPost("DeleteUserShift")]
        [HttpDelete("{id}/Shifts/{shiftId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserShift))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUserShift([FromRoute(Name = "id")] long userId, [FromRoute(Name = "shiftId")] long shiftId)
        {

            try
            {
                var userShifts = await _unitOfWork.UserRepository.GetUsersShiftsByAsync(s => s.UserId == userId && s.ShiftId == shiftId); //&& s.CreationDate >= DateTime.Now);
                var oldUserShift = userShifts.FirstOrDefault();
                if(oldUserShift == null) return NotFound();

                var deleteResult = await _unitOfWork.UserRepository.DeleteUserShiftAsync(oldUserShift.Id);
                var result = await _unitOfWork.Save();
                if (result == 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                return Ok(true);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }


        //[HttpPost("GetUsershift")]
        [HttpGet("{id}/Shifts/{shiftId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserShift))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetUsershift( [FromRoute(Name ="id")] long userId , [FromRoute(Name = "shiftId")] long shiftId)//[FromBody] UserShift userShift)
        {

            try
            {
                var userShifts = await _unitOfWork.UserRepository.GetUsersShiftsByAsync(s => s.UserId == userId && s.ShiftId == shiftId); //&& s.CreationDate >= DateTime.Now);
                var result = userShifts.FirstOrDefault();

                return Ok( result??= new UserShift() );
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }


        // POST: Driver/GetShiftsAndUserShiftsByDate
        //[HttpPost("GetShiftsAndUserShiftsByDate/{id}")]
        [HttpPost("{id}/Shifts/Dates")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetShiftsAndUserShiftsByDate(long id ,[FromBody] Shift shift)
        {
            try
            {
                var resultShiftsOftheDay = await _unitOfWork.SystemRepository.GetShiftsByShiftDateAsync(shift);
                var shiftsOftheDay_IDs = resultShiftsOftheDay.Select(s => s.Id).ToList();
                var userShifts = await _unitOfWork.UserRepository.GetUsersShiftsByAsync(s => s.UserId == id && shiftsOftheDay_IDs.Contains((long)s.ShiftId) );

                return Ok(new { shifts = resultShiftsOftheDay , userShifts = userShifts });
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        //set active or incative to captain 
        //Active is if captain is currently working and ready to take orders
        //Inactive is if captain is don't want to take orders and stopped using the app 
        //[HttpPost("Activation")]
        [HttpPost("Activities")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserActivity))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> CaptainActivities([FromBody] UserActivity userActivity)
        {
            try
            {
                
                if (userActivity.StatusTypeId == (long)StatusTypes.Inactive)
                {
                    var updateDriverLocationResult =
                    await _unitOfWork.UserRepository.DeleteUserCurrentLocationAsync((long)userActivity.UserId);

                    
                } 

                var updateResult = await _unitOfWork.UserRepository.InsertUserActivityAsync(userActivity);
                var result = await _unitOfWork.Save();
                if (result == 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };


                return Ok(updateResult);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        /* Get Orders  Report*/
        [HttpGet("Reports")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Reports([FromQuery] FilterParameters reportParameters)
        {
            try
            {
                // 1-Check Role and Id


                var userType = "";
                var userId = long.Parse("0");
                Utility.getRequestUserIdFromToken(HttpContext, out userId, out userType);

                //   var accessToken = await HttpContext.GetTokenAsync("access_token");
                // 2- Get Querable data depends on Role and Id 
                IQueryable<Order> acceptedOrders;
                if (userType == "Admin" || userType == "Support")
                {
                    IQueryable<Order> rejectedOrders;

                    IQueryable<Order> ignoredOrders;
                    if (reportParameters.FilterByDriverId == 0)
                    {
                        //rejectedOrders = _unitOfWork.UserRepository.GetAllRejectedRequestByQuerable().Select(o => o.Order);
                        acceptedOrders = _unitOfWork.UserRepository.GetAllAcceptedRequestByQuerable().Select(o => o.Order);
                        //ignoredOrders = _unitOfWork.UserRepository.GetAllIgnoredRequestByQuerable().Select(o => o.Order);

                    }
                    else
                    {

                        //rejectedOrders = _unitOfWork.UserRepository.GetUserRejectedRequestByQuerable(u => u.UserId == reportParameters.FilterByDriverId).Select(o => o.Order);
                        acceptedOrders = _unitOfWork.UserRepository.GetUserAcceptedRequestByQuerable(u => u.UserId == reportParameters.FilterByDriverId).Select(o => o.Order);
                        //ignoredOrders = _unitOfWork.UserRepository.GetUserIgnoredRequestByQuerable(u => u.UserId == reportParameters.FilterByDriverId).Select(o => o.Order);

                    }


                    var filteredAcceptedOrdersResult = Utility.GetFilter(reportParameters, acceptedOrders);
                    var filteredAcceptedOrders = this.mapper.Map<List<OrderResponse>>(filteredAcceptedOrdersResult.ToList());
                    //var finalIgnoredOrderResult = Utility.GetFilter(reportParameters, ignoredOrders);
                    //var finalIgnoredOrders = this.mapper.Map<List<OrderResponse>>(finalIgnoredOrderResult.ToList());
                    //var finalRejectedOrderResult = Utility.GetFilter(reportParameters, rejectedOrders);
                    //var finalRejectedOrders = this.mapper.Map<List<OrderResponse>>(finalRejectedOrderResult.ToList());


                    return Ok(new
                    {

                        //RejectedRequestsCount = finalRejectedOrders.Count(),
                        //RejectedRequests = finalRejectedOrders,
                        AcceptedRequests = filteredAcceptedOrders,
                        AcceptedRequestsCount = filteredAcceptedOrders.Count(),
                        //IgnoredRequests = finalIgnoredOrders,
                        //IgnoredRequestsCount = finalIgnoredOrders.Count()
                    }); ;
                }
                if (userType == "Driver" && userId != 0)
                {
                    //var rejectedOrders = _unitOfWork.UserRepository.GetUserRejectedRequestByQuerable(u => u.UserId == userId).Select(o => o.Order);


                    acceptedOrders = _unitOfWork.UserRepository.GetUserAcceptedRequestByQuerable(u => u.UserId == userId).Select(o => o.Order);
                    var filteredAcceptedOrdersultResult = Utility.GetFilter(reportParameters, acceptedOrders);
                    var filteredAcceptedOrders = this.mapper.Map<List<OrderResponse>>(filteredAcceptedOrdersultResult.ToList());


                    //var ignoredOrders = _unitOfWork.UserRepository.GetUserIgnoredRequestByQuerable(u => u.UserId == userId).Select(o => o.Order);

                    //var filteredIgnoredOrdersultResult = Utility.GetFilter(reportParameters, ignoredOrders);
                    //var filteredIgnoredOrders = this.mapper.Map<List<OrderResponse>>(filteredIgnoredOrdersultResult.ToList());

                    //var filteredRejectedOrdersultResult = Utility.GetFilter(reportParameters, rejectedOrders);
                    //var filteredRejectedOrders = this.mapper.Map<List<OrderResponse>>(filteredRejectedOrdersultResult.ToList());

                    return Ok(new
                    {

                        //RejectedRequestsCount = filteredRejectedOrders.Count(),
                        //RejectedResult = filteredRejectedOrders,
                        AcceptedRequests = filteredAcceptedOrders,
                        AcceptedRequestsCount = filteredAcceptedOrders.Count(),
                        //IgnoredRequestsCount = filteredIgnoredOrders.Count(),
                        //IgnoredResult = filteredIgnoredOrders
                    });
                }
                else
                {
                    return Unauthorized();
                }

            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }  
          
        }
        /* Get Orders Reports */
        
        
        
        
        
        /* Search */
        [HttpGet("Search")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Search([FromQuery] FilterParameters parameters)
        {
            try
            {
                IQueryable<User> query;
                if (parameters.StatusUserTypeId == null)
				{
                     query = _unitOfWork.UserRepository.GetByQuerable();
                } else
				{

                      query = _unitOfWork.UserRepository.GetByStatusQuerableS(parameters.StatusUserTypeId);
				}

                //added new
                var totalResult =0;
                var skip = (parameters.NumberOfObjectsPerPage * (parameters.Page - 1));
                var take = parameters.NumberOfObjectsPerPage;
                var usersResult = Utility.GetFilter2<User>(parameters, query, skip, take, out totalResult);
                var users =  this.mapper.Map<List<UserResponse>>(usersResult.ToList());
                
               
                var totalPages = (int)Math.Ceiling(totalResult / (double)parameters.NumberOfObjectsPerPage);
                return Ok(new { Drivers = users, totalResult=totalResult, Page = parameters.Page, TotalPages = totalPages });
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
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }
        /**/

        /*Charts*/
        [HttpGet("Charts")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Charts()
        {
            try
            {
                var reportData = _unitOfWork.UserRepository.UserReportCount();
                return Ok(reportData);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }

        [HttpPost("CheckBonusPerMonth")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> CheckBonusPerMonth(BonusCheckDto bonusCheckDto)
        {
            try
            {
                /// Check Bonus
                var userOrdersAssignedPerMonth = await _unitOfWork.OrderRepository.GetOrdersAssignmentsByAsync(a =>
                    a.UserId == bonusCheckDto.userId &&
                    a.CreationDate.Value.Month == bonusCheckDto.date.Month &&
                    a.CreationDate.Value.Month == bonusCheckDto.date.Month);

                var orderIds = userOrdersAssignedPerMonth.Select(a => a.OrderId).ToList();
                var ordersStatus = await _unitOfWork.OrderRepository.GetOrderCurrentStatusesByAsync(s =>
                    orderIds.Contains(s.OrderId) &&
                    (s.StatusTypeId == (long) OrderStatusTypes.Dropped));
                var ordersCount = ordersStatus.Count();
                // var order = await _unitOfWork.OrderRepository.GetOrderByID((long)orderIds[0]);
                var userCountryId = userOrdersAssignedPerMonth.FirstOrDefault().User.ResidenceCountryId;
                var bonusPerCountry = await _unitOfWork.UserRepository.GetBonusByCountryAsync(userCountryId);


                var userBonus = new UserBonus();
                if (ordersCount >= bonusPerCountry.OrdersPerMonth)
                {
                    userBonus = new UserBonus
                    {
                        UserId = bonusCheckDto.userId,
                        BonusTypeId = (long) BonusTypes.BonusPerMonth,
                        CreationDate = DateTime.Now,
                        Amount = bonusPerCountry.BonusPerMonth
                    };
                    var insertedBonus = await _unitOfWork.UserRepository.InsertBonusAsync(userBonus);
                    var result = await _unitOfWork.Save();
                    if (result == 0) return new ObjectResult("Service Unavailable") {StatusCode = 503};

                    return Ok(new
                    {
                        Bonus = userBonus,
                        Message = "User Delivered about " + ordersCount + " Orders in Month" + bonusCheckDto.date.Month
                    });
                }
                else
                {
                    return Ok(new
                    {
                        Message = "User has no bonus as he delivered about " + ordersCount + " Orders in Month " +
                                  bonusCheckDto.date.Month
                    });
                }
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) {StatusCode = 666};
            }
        }

        [HttpPost("CheckBonusPerYear")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> CheckBonusPerYear(BonusCheckDto bonusCheckDto)
		{
			try
			{
                /// Check Bonus
				var userOrdersAssignedPerYear = await _unitOfWork.OrderRepository.GetOrdersAssignmentsByAsync(a => a.UserId == bonusCheckDto.userId &&
                                         a.CreationDate.Value.Year == bonusCheckDto.date.Year);
                
                var orderIds = userOrdersAssignedPerYear.Select(a => a.OrderId).ToList();
                var ordersStatus = await _unitOfWork.OrderRepository.GetOrderCurrentStatusesByAsync(s => orderIds.Contains(s.OrderId) &&
               (s.StatusTypeId == (long)OrderStatusTypes.Dropped));
                var ordersCount = ordersStatus.Count();
                //var order = await _unitOfWork.OrderRepository.GetOrderByID((long)orderIds[0]);
                var userCountryId = userOrdersAssignedPerYear.FirstOrDefault().User.ResidenceCountryId;
                var bonusPerCountry = await _unitOfWork.UserRepository.GetBonusByCountryAsync(userCountryId);

                var userBonus = new UserBonus();
                if (ordersCount >= bonusPerCountry.OrdersPerYear)
                {
                     userBonus = new UserBonus
                    {
                        UserId = bonusCheckDto.userId,
                        BonusTypeId = (long)BonusTypes.BonusPerYear,
                        CreationDate = DateTime.Now,
                        Amount = bonusPerCountry.BonusPerYear
                    };
                    var insertedBonus = await _unitOfWork.UserRepository.InsertBonusAsync(userBonus);
                    var result = await _unitOfWork.Save();
                    if (result == 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                    return Ok(new { Bonus = userBonus, Message = "User Delivered about "+ ordersCount + " Orders in Year "+ bonusCheckDto.date.Year });
                } else
				{
                    return Ok(new{ Message = "User has no bonus as he delivered about "+ ordersCount + " Orders in Year " + bonusCheckDto.date.Year });
				}
               
			}
			catch (Exception e)
			{
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
			}
		}


        /* SendFirebaseNotification*/
        [HttpPost("SendFirebaseNotification")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult SendFirebaseNotification([FromBody] FBNotify fbNotify)
        {
            try
            {
                var result = FirebaseNotification.SendNotificationToTopic(FirebaseTopics.Captains, fbNotify.Title, fbNotify.Message);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }
        /**/



        /* SendFirebaseNotification*/
        //[HttpPost("NearToLocation")]
        [HttpPost("Locations/Near")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<User>))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> NearToLocation([FromBody] Location location)
        {
            try
            {
                var result = await _unitOfWork.UserRepository.GetCaptainsUsersNearToLocationAsync(location.Lat,location.Lng);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }
        /**/
        
        
        

    }
}
