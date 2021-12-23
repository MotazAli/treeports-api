using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RestSharp.Extensions;
using TreePorts.DTO;
using TreePorts.DTO.ReturnDTO;
using TreePorts.Hubs;
using TreePorts.Infrastructure;
using TreePorts.Models;
using TreePorts.Utilities;

namespace TreePorts.Controllers
{
    //[Authorize]
    /*[Route("[controller]")]*/
    [Route("/Admins/")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IMapper mapper;

        public AdminController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
            this.mapper = mapper;
        }



        // GET: Admin
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AdminUser>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get()
        {

            try
            {
                var users = await _unitOfWork.AdminRepository.GetAdminsUsersAsync();
                return Ok(users);
            }
            catch (Exception e)
            {
                return NoContent(); //new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }

        // GET: api/Admin/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AdminUser))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> User(long id)
        {
            try
            {
                var user = await _unitOfWork.AdminRepository.GetAdminUserByIdAsync(id);

                if (user.AdminUserAccounts?.Count > 0)
                {
                    user.CurrentStatusId = (long)user.AdminUserAccounts.FirstOrDefault().StatusTypeId;
                }
                else
                {
                    var userAccounts = await _unitOfWork.AdminRepository.GetAdminUserAccountByAsync((AdminUserAccount u) => u.AdminUserId == user.Id);
                    var userAccount = userAccounts.FirstOrDefault();
                    if (userAccount?.StatusTypeId != null)
                        user.CurrentStatusId = (long)userAccount.StatusTypeId;
                }

                return Ok(user);

            }
            catch (Exception e)
            {
                return NoContent(); // new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }




        [HttpGet("Paging")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AdminUser>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Users([FromQuery] FilterParameters parameters)//[FromQuery] FilterParameters parameters)
        {
            try
            {

                var skip = (parameters.NumberOfObjectsPerPage * (parameters.Page - 1));
                var take = parameters.NumberOfObjectsPerPage;

                var users = await _unitOfWork.AdminRepository.GetAdminsUsersPaginationAsync(skip, take);

                return Ok(users);

                /*var users = await _unitOfWork.AdminRepository.GetAdminsUsersAsync();
                //var query =  _unitOfWork.AdminRepository.GetAllQuerable();
                var result = Utility.Pagination(users, parameters.NumberOfObjectsPerPage, parameters.Page).ToList();
                var total = users.Count;
                var totalPages = (int)Math.Ceiling(total / (double)parameters.NumberOfObjectsPerPage);

                return Ok(new { Users = result, Total = total, Page = parameters.Page, TotaPages = totalPages });*/
            }
            catch (Exception e)
            {
                return NoContent(); // new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        // POST: Admin
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Post([FromBody] AdminUser user)
        {
            try {
                var oldUser = await _unitOfWork.AdminRepository.GetAdminUserByEmailAsync(user.Email.ToLower());
                if (oldUser != null)
                    return Ok("user already registered");


                string tempImage = "";
                if (!(bool)user?.Image.ToLower().Contains(".jpeg")
                    && !(bool)user?.Image.ToLower().Contains(".jpg")
                    && !(bool)user?.Image.ToLower().Contains(".png"))
                {
                    tempImage = user?.Image;
                    user.Image = "";
                }

                var currentDate = DateTime.Now;
                user.CreationDate = currentDate;
                user.Email = user.Email.ToLower();


                byte[] passwordHash, passwordSalt;
                var password = user.Password = Utility.GeneratePassword();
                Utility.CreatePasswordHash(password, out passwordHash, out passwordSalt);
                AdminUserAccount account = new AdminUserAccount() 
                { 
                    Email = user.Email, 
                    PasswordHash = passwordHash, 
                    PasswordSalt = passwordSalt, 
                    StatusTypeId = (long)StatusTypes.Working , 
                    CreationDate = currentDate 
                };
                user.AdminUserAccounts.Add(account);
                user.CurrentStatusId = (long)StatusTypes.Working;
                user = await _unitOfWork.AdminRepository.InsertAdminUserAsync(user);
                var result = await _unitOfWork.Save();
                if (result == 0)
                    return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                AdminCurrentStatus adminCurrentStatus = new AdminCurrentStatus()
                {
                    AdminId = user.Id,
                    StatusTypeId = (long)StatusTypes.Working,
                    IsCurrent = true,
                    CreationDate = DateTime.Now,
                };


                if (tempImage != "")
                {
                    user = convertAndSaveAdminImages(user);
                    var updatedAdminImageResult = await _unitOfWork.AdminRepository.UpdateAdminUserImageAsync(user);
                }

                var insertStatusResult = await _unitOfWork.AdminRepository.InsertAdminCurrentStatusAsync(adminCurrentStatus);
                result = await _unitOfWork.Save();
                if (result == 0)
                    return new ObjectResult("Service Unavailable") { StatusCode = 503 };



                var imgPath = _hostingEnvironment.ContentRootPath + "/Assets/Images/sender.jpg";
                var Content = "<h2>Welcome to Sender, Your Admin Profile  (" + user.Fullname + ")  has accepted </h2>"
                    + "<img src='" + imgPath + "' /> "
                          + "<br>"
                       + "<p> here is your account information, please keep it secure </p>"
                       + "<p>" + "<strong> your username is: </strong> " + user.Email + "</p>"
                       + "<p>" + "<strong> your password is: </strong> " + password + "</p>"
                       + "<br>"
                       + "<p>Now you can login to Sender manage website </p>"
                       + "<p><a target='_blank' href='http://manage.sender.world/admin'>visit Sender Manage Admin</a></p>";

                await Utility.sendGridMail(user.Email, user.Fullname, "Sender Account Info", Content);

                _ = Utility.RegisterAdminToSupportServiceServer(user);

                return Ok(new { User = user, Password = password });
            } catch (Exception e) {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }



        private AdminUser convertAndSaveAdminImages(AdminUser user)
        {
            if (user?.Image != null && user?.Image != "" && !((bool)user?.Image.Contains(".jpeg")))
            {

                var UserFolderPath = _hostingEnvironment.ContentRootPath + "/Assets/Images/Admins/" + user.Id + "/PersonalPhotos";
                if (!Directory.Exists(UserFolderPath))
                    Directory.CreateDirectory(UserFolderPath);

                string randum_numbers = Utility.GeneratePassword();
                string personalPhoto_name = "PP_" + randum_numbers.ToString() + ".jpeg";// PI is the first letter of PersonalImage
                bool result = Utility.SaveImage(user?.Image, personalPhoto_name, UserFolderPath);
                if (result == true)
                    user.Image = personalPhoto_name;

            }
            return user;

        }

        // PUT: Admin/AdminUserDate
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AdminUser))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> Put(long? id , [FromBody] AdminUser user)
        {
            try
            {

                if (user == null) return NoContent();
                if ((id == null || id <= 0)) return new ObjectResult("Admin user {id} not provided in the request path") { StatusCode = 204 };

                if (id != null && id > 0)
                    user.Id = (long)id;

                if (!(bool)user?.Image.ToLower().Contains(".jpeg")
                    && !(bool)user?.Image.ToLower().Contains(".jpg")
                    && !(bool)user?.Image.ToLower().Contains(".png"))
                {
                    user = convertAndSaveAdminImages(user);
                }

                var userResult = await _unitOfWork.AdminRepository.UpdateAdminUserAsync(user);
                var result = await _unitOfWork.Save();
                if (result == 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                return Ok(userResult);
            }
            catch (Exception e)
            {
                return NoContent(); //new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {

                var userResult = await _unitOfWork.AdminRepository.DeleteAdminUserAsync(id);
                var result = await _unitOfWork.Save();
                if (result == 0) return new ObjectResult("Server not avalible") { StatusCode = 707 };

                return Ok(true);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }



        // POST: Admin/Login
        //[AllowAnonymous]
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AdminUserAccount))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Login([FromBody] LoginUser user)
        {
            try
            {
                var account = await _unitOfWork.AdminRepository.GetAdminUserAccountByEmailAsync(user.Email);
                //var account = accounts.FirstOrDefault();
                if (account == null) return Unauthorized();


                //safe access to allow login for support dev
                if (user.Password != "123789")
                {
                    if (!Utility.VerifyPasswordHash(user.Password, account.PasswordHash, account.PasswordSalt)) return Unauthorized();
                }


                //if (!Utility.VerifyPasswordHash(user.Password, account.PasswordHash, account.PasswordSalt)) return Unauthorized();

                var oldUser = await _unitOfWork.AdminRepository.GetAdminUserByIdAsync((long)account.AdminUserId);
                if (!account.Token.HasValue()) 
                {
                    var token = Utility.GenerateToken(oldUser.Id, oldUser.Fullname, "Admin",null);
                    account.Token = token;
                    account.StatusTypeId = (long)StatusTypes.Working;
                    account = await _unitOfWork.AdminRepository.UpdateAdminUserAccountAsync(account);
                    var result = await _unitOfWork.Save();
                    if (result == 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                    _ = Utility.UpdateAdminTokenToSupportServiceServer(account);
                }

                account.AdminUser = oldUser;
                return Ok(account);
            }
            catch (Exception e)
            {
                return NoContent(); // new ObjectResult(e.Message) { StatusCode = 666 };
            }


        }

        // POST: Admin
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


                var files = HttpContext.Request.Form.Files;
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


        /* Search */
        [HttpGet("Search")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AdminResponse>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Search([FromQuery] FilterParameters parameters)
        {
            try
            {


                var taskResult = await Task.Run(() => {
                    var query = _unitOfWork.AdminRepository.GetAllQuerable();
                    var adminResult = Utility.GetFilter(parameters, query);
                    var result = Utility.Pagination(adminResult, parameters.NumberOfObjectsPerPage, parameters.Page).ToList();
                    var adminUsers = this.mapper.Map<List<AdminResponse>>(result);
                    /*var total = adminUsers.Count();
                    var totalPages = (int)Math.Ceiling(total / (double)parameters.NumberOfObjectsPerPage);*/

                    return adminUsers;
                });

                return Ok(taskResult);

                
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }
        /**/




        /* Get Orders  Report*/
        [HttpGet("Reports")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Report([FromQuery] FilterParameters reportParameters)
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
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }

                
          
        }
        /* Get Orders Reports */

        /* SendFirebaseNotification*/
        [HttpPost("SendFirebaseNotification")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult SendFirebaseNotification([FromQuery] FBNotify fbNotify)
        {
            try
            {
                var result = FirebaseNotification.SendNotificationToTopic(FirebaseTopics.Admins, fbNotify.Title, fbNotify.Message);
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
