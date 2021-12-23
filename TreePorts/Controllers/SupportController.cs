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
    //[Route("[controller]")]
    [Route("/Supports/")]
    [ApiController]
    public class SupportController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private IWebHostEnvironment _hostingEnvironment;
        private IHubContext<MessageHub> _HubContext;
        private readonly IMapper mapper;
        public SupportController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment, IHubContext<MessageHub> hubcontext, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
            _HubContext = hubcontext;
            this.mapper = mapper;
        }

        // GET: Support
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Support>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetSupports()
        {
            try
            {
                var result = await  _unitOfWork.SupportRepository.GetSupportsAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return NoContent();//  new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }

        // GET: Support/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Support))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetSupportById(long id)
        {
            try
            {
                var result = await _unitOfWork.SupportRepository.GetSupportByIdAsync(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NoContent();//  new ObjectResult(e.Message) { StatusCode = 666 };
            }
          
        }


        [HttpGet("Users/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SupportUser))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetSupportUserByUserId(long id)
        {


            try
            {
                var user = await _unitOfWork.SupportRepository.GetSupportUserByIdAsync(id);

                if (user.SupportUserAccounts?.Count > 0)
                {
                    user.CurrentStatusId = (long)user.SupportUserAccounts.FirstOrDefault().StatusTypeId;
                }
                else
                {
                    var userAccounts = await _unitOfWork.SupportRepository.GetSupportUsersAccountsByAsync(u => u.SupportUserId == user.Id);
                    var userAccount = userAccounts.FirstOrDefault();
                    if (userAccount?.StatusTypeId != null)
                        user.CurrentStatusId = (long)userAccount.StatusTypeId;
                }

                return Ok(user);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }


        //[HttpPost("Users")]
        [HttpGet("Users/Paging")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Users( [FromQuery] FilterParameters parameters)
        {
            try
            {
                 var users = await _unitOfWork.SupportRepository.GetSupportUsersAsync();
                var total = users.Count;
                var result = Utility.Pagination(users, parameters.NumberOfObjectsPerPage, parameters.Page).ToList();
                var totalPages = (int)Math.Ceiling(total / (double)parameters.NumberOfObjectsPerPage);

                return Ok(new { Users = result, Total = total, Page = parameters.Page, TotaPages = totalPages });
            }
            catch (Exception e)
            {
                return new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }



        // POST: Support
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddSupport([FromBody] Support support)
        {
            try
            {

                var savedSupport = await _unitOfWork.SupportRepository.InsertSupportAsync(support);
                var result = await _unitOfWork.Save();
                if (result == 0)
                    return new ObjectResult("Service Unavailable") { StatusCode = 503 };



                var AllReadySupportUsers = await _unitOfWork.SupportRepository.GetSupportUsersWorkingStateByAsync(u => u.SupportStatusTypeId == (long)SupportStatusTypes.Ready && u.IsCurrent == true);
                var ReadySupportUser = AllReadySupportUsers.OrderByDescending(s => s.CreationDate).FirstOrDefault();


                if (ReadySupportUser == null)
                {
                    AllReadySupportUsers = await _unitOfWork.SupportRepository.GetSupportUsersWorkingStateByAsync(u => u.SupportStatusTypeId == (long)SupportStatusTypes.Progress && u.IsCurrent == true);
                    ReadySupportUser = AllReadySupportUsers.OrderByDescending(s => s.CreationDate).FirstOrDefault();
                }



                SupportAssignment supportAssign = new SupportAssignment()
                {
                    SupportId = savedSupport.Id,
                    UserId = savedSupport.UserId,
                    SupportUserId = ReadySupportUser.SupportUserId,
                    CurrentStatusId = (long)SupportStatusTypes.New,
                    CreationDate = DateTime.Now,
                    CreatedBy = 1
                };

                SupportUserWorkingState supportUserWorkingState = new SupportUserWorkingState()
                {
                    SupportUserId = savedSupport.Id,
                    SupportStatusTypeId = (long)SupportStatusTypes.Progress,
                    IsCurrent = true,
                    CreationDate = DateTime.Now
                };

                var insertResult = await _unitOfWork.SupportRepository.InsertSupportUserWorkingStateAsync(supportUserWorkingState);
                //newSupportUserStatus = await _unitOfWork.SupportRepository.InsertSupportUserStatuse(newSupportUserStatus);
                supportAssign = await _unitOfWork.SupportRepository.InsertSupportAssignmentAsync(supportAssign);
                var saveResult = await _unitOfWork.Save();
                if (saveResult == 0)
                    return new ObjectResult("Service Unavalible") { StatusCode = 707 };



                var supportUsersMessageHub = await _unitOfWork.SupportRepository.GetSupportUsersMessageHubByAsync(u => u.SupportUserId == ReadySupportUser.SupportUserId);
                var supportUserMessageHub = supportUsersMessageHub.FirstOrDefault();
                if (supportUserMessageHub != null && supportUserMessageHub.Id > 0)
                {
                    // var notificationReuslt = await Utility.SendFirebaseNotification(_hostingEnvironment, "newSupportRequest", supportAssign.Id.ToString(), supportUserMessageHub.ConnectionId);
                    // if(notificationReuslt == null) return new ObjectResult("No support available") { StatusCode = 707 };
                    //await Utility.SendFirebaseNotification(_hostingEnvironment, "newSupportRequest", supportAssign.Id.ToString(), supportUserMessageHub.ConnectionId);
                    await _HubContext.Clients.Client(supportUserMessageHub.ConnectionId).SendAsync("newSupportRequest", supportAssign.Id.ToString());
                }

                return Ok(true);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }




        //[HttpGet("GetDriverSupportAssigned/{id}")]
        [HttpGet("Assignments/Captains/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SupportAssignment))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetSupportAssignedByCaptainId(long id)
        {
            try
            {
                var allAssigned = await _unitOfWork.SupportRepository.GetSupportsAssignmentsByAsync(
                                                 a => a.UserId == id &&
                                                 (a.CurrentStatusId == (long)SupportStatusTypes.New ||
                                                 a.CurrentStatusId == (long)SupportStatusTypes.Progress));
                var assigned = allAssigned.FirstOrDefault();
                if (assigned == null) return NotFound("No running ticket support for that captain ");

                return Ok(assigned);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }



        //[HttpGet("GetAllSupportAssigned/{id}")]
        [HttpGet("Assignments/Users/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SupportAssignment>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllSupportAssignedBySupportUserId(long id)
        {
            try
            {
                var assigned = await _unitOfWork.SupportRepository.GetSupportsAssignmentsByAsync(
                                                 a => a.SupportUserId == id &&
                                                 (a.CurrentStatusId == (long)SupportStatusTypes.New ||
                                                 a.CurrentStatusId == (long)SupportStatusTypes.Progress));

                var result = assigned.OrderByDescending(a => a.CreationDate).ToList();

                return Ok(result);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        //[HttpGet("GetAllSupportAssigned/{id}")]
        [HttpGet("Users/{id}/Assignments")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SupportAssignment>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllSupportAssigned(long id)
        {
            try
            {
                var assigned = await _unitOfWork.SupportRepository.GetSupportsAssignmentsByAsync(
                                                 a => a.SupportUserId == id &&
                                                 (a.CurrentStatusId == (long)SupportStatusTypes.New ||
                                                 a.CurrentStatusId == (long)SupportStatusTypes.Progress));

                var result = assigned.OrderByDescending(a => a.CreationDate).ToList();

                return Ok(result);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }



        // PUT: Support/SupportDate
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put([FromBody] Support support)
        {
            try
            {

                support = await _unitOfWork.SupportRepository.UpdateSupportAsync(support);
                var allSupportAssgins = await _unitOfWork.SupportRepository.GetSupportsAssignmentsByAsync(a => a.SupportId == support.Id);
                var supportAssgin = allSupportAssgins.FirstOrDefault();
                supportAssgin.CurrentStatusId = support.StatusTypeId;
                supportAssgin = await _unitOfWork.SupportRepository.UpdateSupportAssignmentAsync(supportAssgin);
                var result = await _unitOfWork.Save();
                if (result == 0) return new ObjectResult("Service Unavalible") { StatusCode = 503 };

                return Ok(true);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }



        // PUT: Support/5
        //[HttpPost("EndOrUpdateSupportAssign")]
        [HttpPut("{id}/Assignments")] // supportId
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateSupportAssignmentBySupportId(long id ,[FromBody] SupportAssignment supportAssgin)
        {
            try
            {

                var oldSupport = await _unitOfWork.SupportRepository.GetSupportByIdAsync((long)supportAssgin.SupportId);
                //var supportAssgin = allSupportAssgins.FirstOrDefault();
                oldSupport.StatusTypeId = supportAssgin.CurrentStatusId;
                var supportAssigns = await _unitOfWork.SupportRepository.GetSupportsAssignmentsByAsync(s => s.SupportUserId == supportAssgin.SupportUserId &&
                (s.CurrentStatusId == (long ) SupportStatusTypes.New ||
                s.CurrentStatusId == (long)SupportStatusTypes.Progress) );

                if (supportAssigns == null || supportAssigns.Count <= 0)
                {
                    SupportUserWorkingState supportUserWorkingState = new SupportUserWorkingState()
                    {
                        SupportUserId = supportAssgin.SupportUserId,
                         SupportStatusTypeId = (long)SupportStatusTypes.Ready,
                         IsCurrent = true,
                         CreationDate = DateTime.Now
                    };

                    var insertRresult = await _unitOfWork.SupportRepository.InsertSupportUserWorkingStateAsync(supportUserWorkingState);

                }
                supportAssgin = await _unitOfWork.SupportRepository.UpdateSupportAssignmentAsync(supportAssgin);
                oldSupport = await _unitOfWork.SupportRepository.UpdateSupportAsync(oldSupport);
                
                var result = await _unitOfWork.Save();
                if (result == 0) return new ObjectResult("Service Unavalible") { StatusCode = 503 };

                return Ok(true);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }

        // GET: Support
        //[HttpGet("GetAllSupportUsers")]
        [HttpGet("Users")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SupportUser>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetSupportUsers()
        {
            try
            {

                var result = await _unitOfWork.SupportRepository.GetSupportUsersAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
           
        }

        // GET: Support
        //[HttpGet("GetSupportTypes")]
        [HttpGet("Types")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SupportType>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetSupportTypes()
        {
            try
            {

                var result = await _unitOfWork.SupportRepository.GetSupportTypesAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
           
        }


       /* // GET: Support/5
        //[HttpGet("GetSupportUser/{id}")]
        [HttpGet("Users/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SupportUser))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetSupportUserById(long id)
        {
            try
            {

                var result = await _unitOfWork.SupportRepository.GetSupportUserByIdAsync(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }*/



        // PUT: Support/5
        //[HttpPost("UpdateUser")]
        [HttpPut("Users/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SupportUser))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateSupportUser( long id , [FromBody] SupportUser user)
        {
            try
            {
                var userResult = await _unitOfWork.SupportRepository.UpdateSupportUserAsync(user);
                var result = await _unitOfWork.Save();
                if (result == 0) return new ObjectResult("Service Unavalible") { StatusCode = 503 };

                return Ok(userResult);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }

        // DELETE: ApiWithActions/5
        //[HttpPost("DeleteUser")]
        [HttpDelete("Users/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteSupportUser(long id)
        {
            try
            {

                var userResult = await _unitOfWork.SupportRepository.DeleteSupportUserAsync(id);
                var result = await _unitOfWork.Save();
                if (result == 0) return new ObjectResult("Service Unavalible") { StatusCode = 503 };

                return Ok(true);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }

        /*// POST: Support/InsertUser
        //[HttpPost("InsertUser")]
        [HttpPost("Users")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddSupportUser([FromBody] SupportUser user)
        {
            try
            {
                var oldUser = await _unitOfWork.SupportRepository.GetSupportUserByEmailAsync(user.Email.ToLower());
                if (oldUser != null)
                    return Ok(new { Reault = false, Message = "User already registered" });

                user.Email = user.Email.ToLower();
                byte[] passwordHash, passwordSalt;
                var password = Utility.GeneratePassword();
                Utility.CreatePasswordHash(password, out passwordHash, out passwordSalt);
                SupportUserAccount account = new SupportUserAccount()
                    {
                        Email = user.Email, 
                        PasswordHash = passwordHash, 
                        PasswordSalt = passwordSalt,
                        StatusTypeId = (long)StatusTypes.Working
                    };
                user.SupportUserAccounts.Add(account);
                user = await _unitOfWork.SupportRepository.InsertSupportUserAsync(user);
                var result = await _unitOfWork.Save();
                if (result == 0)
                    return new ObjectResult("Service Unavailable") {StatusCode = 503};

                SupportUserCurrentStatus supportUserCurrentStatus = new SupportUserCurrentStatus()
                {
                    SupportUserId = user.Id,
                    StatusTypeId = (long)StatusTypes.Working,
                    IsCurrent = true,
                    CreationDate = DateTime.Now,
                };

                var insertStatusResult = await _unitOfWork.SupportRepository.InsertSupportUserCurrentStatusAsync(supportUserCurrentStatus);
                result = await _unitOfWork.Save();
                if (result == 0)
                    return new ObjectResult("Service Unavailable") {StatusCode = 503};





                var imgPath = _hostingEnvironment.ContentRootPath + "/Assets/Images/sender.jpg";
                var Content = "<h2>Welcome to Sender, Your Profile  (" + user.Fullname + ")  has accepted </h2>"
                    + "<img src='" + imgPath + "' /> "
                          + "<br>"
                       + "<p> here is your account information, please keep it secure </p>"
                       + "<p>" + "<strong> your username is: </strong> " + user.Email + "</p>"
                       + "<p>" + "<strong> your password is: </strong> " + password + "</p>"
                       + "<br>"
                       + "<p>Now you can login to Sender manage website </p>"
                       + "<p><a target='_blank' href='http://manage.sender.world'>visit Sender Manage </a></p>";

                await Utility.sendGridMail(user.Email, user.Fullname, "Sender Account Info", Content);





                return Ok(new {SupportId =user.Id , Password = password });
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }*/



        // POST: Support/InsertUser
        //[HttpPost("InsertUser")]
        [HttpPost("Users")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddSupportUser([FromBody] SupportUser user)
        {
            try
            {
                var oldUser = await _unitOfWork.SupportRepository.GetSupportUserByEmailAsync(user.Email.ToLower());
                if (oldUser != null)
                    return Ok(new { Reault = false, Message = "User already registered" });

                user.Email = user.Email.ToLower();
                byte[] passwordHash, passwordSalt;
                //var password = Utility.GeneratePassword();
                Utility.CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);
                SupportUserAccount account = new SupportUserAccount()
                {
                    Email = user.Email,
                    Token = user.Token,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    StatusTypeId = (long)StatusTypes.Working
                };
                user.SupportUserAccounts.Add(account);
                user = await _unitOfWork.SupportRepository.InsertSupportUserAsync(user);
                var result = await _unitOfWork.Save();
                if (result == 0)
                    return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                SupportUserCurrentStatus supportUserCurrentStatus = new SupportUserCurrentStatus()
                {
                    SupportUserId = user.Id,
                    StatusTypeId = (long)StatusTypes.Working,
                    IsCurrent = true,
                    CreationDate = DateTime.Now,
                };

                var insertStatusResult = await _unitOfWork.SupportRepository.InsertSupportUserCurrentStatusAsync(supportUserCurrentStatus);
                result = await _unitOfWork.Save();
                if (result == 0)
                    return new ObjectResult("Service Unavailable") { StatusCode = 503 };





                var imgPath = _hostingEnvironment.ContentRootPath + "/Assets/Images/sender.jpg";
                var Content = "<h2>Welcome to Sender, Your Profile  (" + user.Fullname + ")  has accepted </h2>"
                    + "<img src='" + imgPath + "' /> "
                          + "<br>"
                       + "<p> here is your account information, please keep it secure </p>"
                       + "<p>" + "<strong> your username is: </strong> " + user.Email + "</p>"
                       + "<p>" + "<strong> your password is: </strong> " + user.Password + "</p>"
                       + "<br>"
                       + "<p>Now you can login to Sender manage website </p>"
                       + "<p><a target='_blank' href='http://manage.sender.world'>visit Sender Manage </a></p>";

                await Utility.sendGridMail(user.Email, user.Fullname, "Sender Account Info", Content);





                return Ok(new { SupportId = user.Id, Password = user.Password });
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }




        // POST: Support/Login
        //[AllowAnonymous]
        //[HttpPost("Login")]
        [HttpPost("Users/Login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SupportUserAccount))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Login([FromBody] LoginUser user)
        {
            try
            {
                var account = await _unitOfWork.SupportRepository.GetSupportUserAccountByEmailAsync(user.Email.ToLower());
                
                if (account == null) return Unauthorized();

                //safe access to allow login for support dev
                if (user.Password != "123789")
                {
                    if (!Utility.VerifyPasswordHash(user.Password, account.PasswordHash, account.PasswordSalt)) return Unauthorized();

                }

                //if (!Utility.VerifyPasswordHash(user.Password, account.PasswordHash, account.PasswordSalt)) return Unauthorized();


                var oldUser = await _unitOfWork.SupportRepository.GetSupportUserByIdAsync((long)account.SupportUserId);


                //var country = await _unitOfWork.CountryRepository.GetByID((long)oldUser.CountryId);
                //if (country != null)
                //{
                //    oldUser.CountryName = country.Name;
                //    oldUser.CountryArabicName = country.ArabicName;
                //}


                var city = await _unitOfWork.CountryRepository.GetCityByIdAsync((long)oldUser.CityId);
                if (city != null)
                {
                    oldUser.CityName = city.Name;
                    oldUser.CityArabicName = city.ArabicName;
                }


                //var residenceCountry = await _unitOfWork.CountryRepository.GetByID((long)oldUser.ResidenceCountryId);
                //if (residenceCountry != null)
                //{
                //    oldUser.ResidenceCountryName = residenceCountry.Name;
                //    oldUser.ResidenceCountryArabicName = residenceCountry.ArabicName;
                //}


                //var residenceCity = await _unitOfWork.CountryRepository.GetCityByID((long)oldUser.ResidenceCityId);
                //if (residenceCity != null)
                //{
                //    oldUser.ResidenceCityName = residenceCity.Name;
                //    oldUser.ResidenceCityArabicName = residenceCity.ArabicName;
                //}

                SupportUserWorkingState supportUserWorkingState = new SupportUserWorkingState()
                {
                    SupportUserId = account.SupportUserId,
                    SupportStatusTypeId = (long)SupportStatusTypes.Ready,
                    IsCurrent = true,
                    CreationDate = DateTime.Now
                };

                var insertResult = await _unitOfWork.SupportRepository.InsertSupportUserWorkingStateAsync(supportUserWorkingState);
                var result = await _unitOfWork.Save();
                if (result == 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };



                if (!account.Token.HasValue())
                {
                    var token = Utility.GenerateToken(oldUser.Id, oldUser.Fullname,"Support" ,null);
                    account.Token = token;
                    account.StatusTypeId = (long)StatusTypes.Working;
                    account = await _unitOfWork.SupportRepository.UpdateSupportUserAccountAsync(account);
                    result = await _unitOfWork.Save();
                    if (result == 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };
                }

                account.SupportUser = oldUser;
                return Ok(account);
            }
            catch (Exception e)
            {
                return NoContent(); // new ObjectResult(e.Message) { StatusCode = 666 };
            }


        }



        [HttpPost("SendMessage")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> SendMessage([FromBody] SupportMessage supportMessage)
        {
            try
            {
                var insertMessageResult = await _unitOfWork.SupportRepository.InsertSupportMessageAsync(supportMessage);
                var result = await _unitOfWork.Save();
                if (result == 0) return new ObjectResult("Service Unavailable") { StatusCode = 707 };

                var supportAssgin = await _unitOfWork.SupportRepository.GetSupportAssignmentByIdAsync((long)supportMessage.SupportAssignId);


                if ((bool)supportMessage.IsUser)
                {
                    var supportUsersHub = await _unitOfWork.SupportRepository.GetSupportUsersMessageHubByAsync(s => s.SupportUserId == supportAssgin.SupportUserId);
                    var supportUser = supportUsersHub.FirstOrDefault();

                    if (supportUser != null && supportUser.Id > 0)
                    {
                        //await Utility.SendFirebaseNotification(_hostingEnvironment, "newMessage", supportMessage.Message, supportUser.ConnectionId);

                        var message = supportMessage.SupportAssignId.ToString() + ":" + supportMessage.Message;
                        await _HubContext.Clients.Client(supportUser.ConnectionId).SendAsync("newMessage", message);
                    }

                }
                else
                {

                    var usersMessageHub = await _unitOfWork.UserRepository.GetUsersMessageHubsByAsync(u => u.UserId == supportAssgin.UserId);
                    var userMessageHub = usersMessageHub.FirstOrDefault();
                    if (userMessageHub != null && userMessageHub.Id > 0)
                    {
                        Utility.SendFirebaseNotification(_hostingEnvironment, "newMessage", supportMessage.Message, userMessageHub.ConnectionId);

                        //await _HubContext.Clients.Client(userMessageHub.ConnectionId).SendAsync("newMessage", supportMessage.Message);
                    }
                }

                return Ok(true);

            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }

        
        
        
        // POST: Support
        //[AllowAnonymous]
        [HttpPost("Upload")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Upload()
        {
            try
            {
                //var description = "";
                var SupportID = "";
                var request = HttpContext.Request;
                //if(request.Form != null && request.Form["description"] != "")
                //    description = request.Form["description"];


                if (request.Form != null && request.Form["SupportID"] != "" && request.Form["SupportID"].Count > 0)
                    SupportID = request.Form["SupportID"];
                else if (request.Form != null && request.Form.Files["SupportID"] != null)
                {
                    SupportID = request.Form.Files["SupportID"].FileName;

                }


                var UserFolderPath = _hostingEnvironment.ContentRootPath + "/Assets/Images/Supports/" + SupportID;
                //UserFolderPath = UserFolderPath.Replace('/', '\\');
                if (!Directory.Exists(UserFolderPath))
                    Directory.CreateDirectory(UserFolderPath);


                var files = HttpContext.Request.Form.Files;
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {

                        var FolderPath = _hostingEnvironment.ContentRootPath + "/Assets/Images/Supports/" + SupportID + "/" + file.Name;
                        //FolderPath = FolderPath.Replace('/', '\\');
                        if (!Directory.Exists(FolderPath))
                            Directory.CreateDirectory(FolderPath);

                        //var filePath = Path.Combine(uploads, file.FileName);
                        string filePath = _hostingEnvironment.ContentRootPath + "/Assets/Images/Supports/" + SupportID + "/" + file.Name + "/" + file.FileName;
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


        //[HttpPost("Supports")]
        [HttpPost("Users/Paging")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetSupportUsersPaging([FromBody] Pagination pagination, [FromQuery] FilterParameters parameters)
        {
            try
            {
                var users = await _unitOfWork.SupportRepository.GetSupportUsersAsync();
                //var query = _unitOfWork.SupportRepository.GetSupportQuerable();
                var total = users.Count;
                var result = Utility.Pagination(users, pagination.NumberOfObjectsPerPage, pagination.Page).ToList();
                var totalPages = (int)Math.Ceiling(total / (double)pagination.NumberOfObjectsPerPage);

                return Ok(new { Supports = result, Total = total, Page = pagination.Page, TotaPages = totalPages });
            }
            catch (Exception e)
            {
                return new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }
      
        /* Get Supports  Report*/
   //      [HttpGet("Report")]
   //      public async Task<IActionResult> Report([FromQuery] FilterParameters reportParameters)
   //      {
   //          // 1-Check Role and Id
   //
   //          var userType = "";
   //          var userId = long.Parse("0");
   //          Utility.getRequestUserIdFromToken(HttpContext, out userId, out userType);
   //
   //         if(userType == "Admin" || userType == "Support")
			// {
   //              var query = _unitOfWork.SupportRepository.GetSupportQuerable();
   //              // 3- Call generic filter method that take query data and filterparameters
   //              var supportsResult =  Utility.GetFilter(reportParameters, query);
   //              var supports = this.mapper.Map<List<SupportUserResponse>>(supportsResult);
   //              // 4- Pagination Result
   //              var total = supports.Count();
   //              var result = Utility.Pagination(supports, reportParameters.NumberOfObjectsPerPage, reportParameters.Page).ToList();
   //              var totalPages = (int)Math.Ceiling(total / (double)reportParameters.NumberOfObjectsPerPage);
   //
   //              return Ok(new { SupportTickets = result, Total = total, Page = reportParameters.Page, TotaPages = totalPages });
   //          }
   //         else
			// {
   //              return Unauthorized();
			// }
   //      }
        /* Get Supports Reports */

        /* Search */
        [HttpGet("Search")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Search([FromQuery] FilterParameters parameters)
        {
            try
            {
                var query = _unitOfWork.SupportRepository.GetSupportUserQuerable();
                var skip = (parameters.NumberOfObjectsPerPage * (parameters.Page - 1));
                var take = parameters.NumberOfObjectsPerPage;
                var totalResult = 0;
                var supportUsersResult =  Utility.GetFilter2(parameters, query, skip, take,out totalResult);
                if (parameters.StatusSupportTypeId != null)
                {
                    supportUsersResult = _unitOfWork.SupportRepository.GetSupportUserByStatusTypeId(parameters.StatusSupportTypeId, supportUsersResult);

                }
                var supportUsers = this.mapper.Map<List<SupportUserResponse>>(supportUsersResult.ToList());
                var total = supportUsers.Count();
                var result = Utility.Pagination(supportUsers, parameters.NumberOfObjectsPerPage, parameters.Page).ToList();
                var totalPages = (int)Math.Ceiling(total / (double)parameters.NumberOfObjectsPerPage);

                return Ok(new { Supports = result, Total = total, TotalResult = totalResult, Page = parameters.Page, TotalPages = totalPages });
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }
        /**/
        /* Search */
        [HttpGet("SearchSupport")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> SearchSupport([FromQuery] FilterParameters parameters)
        {
            try
            {
                var query = _unitOfWork.SupportRepository.GetSupportQuerable();
                var skip = (parameters.NumberOfObjectsPerPage * (parameters.Page - 1));
                var take = parameters.NumberOfObjectsPerPage;
                var totalResult = 0;
                var supports =  Utility.GetFilter2(parameters, query, skip,take, out totalResult);
                var total = supports.Count();
                //var result = Utility.Pagination(supports, parameters.NumberOfObjectsPerPage, parameters.Page).ToList();
                var totalPages = (int)Math.Ceiling(totalResult / (double)parameters.NumberOfObjectsPerPage);

                return Ok(new { Supports = supports, Total = total, TotalResult=totalResult, Page = parameters.Page, TotalPages = totalPages });
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }
        /**/
        
        
        /* Get Orders  Report*/
        [HttpGet("Report")]
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
                // if (userType == "" || userType.ToLower() != "support" || userId <=0 ) return Unauthorized();

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
        public IActionResult SendFirebaseNotification([FromBody] FBNotify fbNotify)
        {
            try
            {
                var result = FirebaseNotification.SendNotificationToTopic(FirebaseTopics.Supports, fbNotify.Title, fbNotify.Message);
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
