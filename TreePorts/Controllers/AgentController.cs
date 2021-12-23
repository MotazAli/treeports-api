using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MimeKit;
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
    [Route("/Agents/")]
    [ApiController]
    public class AgentController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailService _mailService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private IHubContext<MessageHub> _HubContext;
        private readonly IMapper mapper;
        public AgentController(IUnitOfWork unitOfWork, IMailService mailService, IWebHostEnvironment hostingEnvironment, IMapper mapper, IHubContext<MessageHub> hubcontext)
        {
            this.mapper = mapper;
            _mailService = mailService;
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
            _HubContext = hubcontext;
        }

        // GET: Agent/5
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Agent>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var users = await _unitOfWork.AgentRepository.GetAgentsByAsync(a =>  //a.IsBranch == false &&
                                                                         //a.IsDeleted == false &&
               a.StatusTypeId == (long)StatusTypes.Working);

                return Ok(users);

            }
            catch (Exception e)
            {
                return NoContent(); // new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        // GET: Agent/GetAative
        [HttpGet("Active")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Agent>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Active()
        {
            try
            {
                var result=  await _unitOfWork.AgentRepository.GetActiveAgentsAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return NoContent(); // new ObjectResult(e.Message) { StatusCode = 666 };
            }
            
        }

        //// GET: Agent/5
        //[HttpGet("{id}")]
        //public async Task<Agent> Get(long id)
        //{
        //    return await _unitOfWork.AgentRepository.GetByID(id);
        //}


        // GET: Agent/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Agent))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var user = await _unitOfWork.AgentRepository.GetAgentByIdAsync(id);
                if (user?.StatusTypeId != null) { user.CurrentStatusId = (long)user.StatusTypeId; }
                    
                return Ok(user);

            }
            catch (Exception e)
            {
                return NoContent(); // new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        // GET: Agent/5
        [HttpGet("Types")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AgentType>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAgentTypes()
        {
            try
            {
                var types = await _unitOfWork.AgentRepository.GetAgentTypesAsync();
                return Ok(types);
            }
            catch (Exception e)
            {
                return NoContent(); // new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        [HttpPost("Paging")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Agent>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAgentsPaging( [FromQuery] FilterParameters parameters)
        {
            try
            {

                var taskResult = await Task.Run(() => {
                    var query = _unitOfWork.AgentRepository.GetByQuerable(a => a.StatusTypeId != (long)StatusTypes.Reviewing).OrderByDescending(a => a.CreationDate);
                    var skip = (parameters.NumberOfObjectsPerPage * (parameters.Page - 1));
                    var take = parameters.NumberOfObjectsPerPage;
                    var totalResult = 0;
                    var users = Utility.GetFilter2(parameters, query, skip, take, out totalResult);
                    //var result = Utility.Pagination(query, parameters.NumberOfObjectsPerPage, parameters.Page).ToList();
                    //var totalPages = (int)Math.Ceiling(total / (double)pagination.NumberOfObjectsPerPage);
                    return users.ToList();
                });

                
                return Ok(taskResult);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }



        //[HttpPost("GetNewRegisteredAgents")]
        [HttpPost("New")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Agent>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetNewRegisteredAgents()
        {
            try
            {
                var result = await _unitOfWork.AgentRepository.GetAgentsByAsync(u => u.StatusTypeId == (long)StatusTypes.Reviewing);

                return Ok(result);
            }
            catch (Exception e)
            {
                return NoContent(); // new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        //[HttpPost("GetNewRegisteredAgents")]
        [HttpPost("New/paging")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetNewRegisteredAgentsPaging( [FromQuery] FilterParameters parameters)
        {
            try
            {


                var taskResult = await Task.Run(() => {

                    var query = _unitOfWork.AgentRepository.GetByQuerable(u => u.StatusTypeId == (long)StatusTypes.Reviewing).OrderByDescending(a => a.CreationDate);

                    //   var users = await _unitOfWork.AgentRepository.GetFilterAgent(parameters, query);
                    var skip = (parameters.NumberOfObjectsPerPage * (parameters.Page - 1));
                    var take = parameters.NumberOfObjectsPerPage;
                    var totalResult = 0;
                    var users = Utility.GetFilter2(parameters, query, skip, take, out totalResult);
                    //var total = users.Count();
                    //var totalPages = (int)Math.Ceiling(totalResult / (double)parameters.NumberOfObjectsPerPage);

                    return users.ToList();

                });

                return Ok(taskResult);

                
            }
            catch (Exception e)
            {
                return NoContent(); // new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        // POST: Agent
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Post([FromBody] Agent agent)
        {
            try
            {
                var oldAgent = await _unitOfWork.AgentRepository.GetAgentByEmailAsync(agent.Email.ToLower());
                //var oldAgent = agents.FirstOrDefault();
                if (oldAgent != null)
                    return Ok("User already registered");


                /*agents = await _unitOfWork.AgentRepository.GetBy(a => a.Email == agent.Email);
                oldAgent = agents.FirstOrDefault();
                if (oldAgent != null)
                    return new ObjectResult("Email already registered") { StatusCode = 700 };
*/

                //byte[] passwordHash, passwordSalt;
                //var password = Utility.GeneratePassword();
                //Utility.CreatePasswordHash(password, out passwordHash, out passwordSalt);
                //agent.PasswordHash = passwordHash;
                //agent.PasswordSalt = passwordSalt;
                string tempImage = "";
                if (!(bool)agent?.Image.ToLower().Contains(".jpeg") 
                    && !(bool)agent?.Image.ToLower().Contains(".jpg")
                    && !(bool)agent?.Image.ToLower().Contains(".png")) 
                {
                    tempImage = agent?.Image;
                    agent.Image = "";
                }

                agent.StatusTypeId = (long)StatusTypes.Reviewing;
                agent.Email = agent.Email.ToLower();
                var insertResult = await _unitOfWork.AgentRepository.InsertAgentAsync(agent);
                var result = await _unitOfWork.Save();

                if (result == 0)
                    return new ObjectResult("Service Unavailable") { StatusCode = 503 };


                AgentCurrentStatus newAgentCurrentStatus = new AgentCurrentStatus()
                {
                    AgentId = insertResult.Id,
                    StatusId = (long)StatusTypes.New,
                    IsCurrent = false,
                    CreationDate = DateTime.Now
                };

                AgentCurrentStatus incompleteAgentCurrentStatus = new AgentCurrentStatus()
                {
                    AgentId = insertResult.Id,
                    StatusId = (long)StatusTypes.Reviewing,
                    IsCurrent = true,
                    CreationDate = DateTime.Now
                };

                if (tempImage != "")
                {
                    insertResult = convertAndSaveAgentImages(insertResult);
                    var updateAgentImageResult = await _unitOfWork.AgentRepository.UpdateAgentImageAsync(insertResult);
                }

                var newAgentStatusInsertedResult = await _unitOfWork.AgentRepository.InsertAgentCurrentStatusAsync(newAgentCurrentStatus);
                var incompleteAgentStatusInsertedResult = await _unitOfWork.AgentRepository.InsertAgentCurrentStatusAsync(incompleteAgentCurrentStatus);

                result = await _unitOfWork.Save();
                if (result == 0)
                    return new ObjectResult("Service Unavailable") { StatusCode = 503 };


                

                var message = insertResult.Id.ToString();
                await _HubContext.Clients.All.SendAsync("NewAgentNotify", message);
                //var message2 = incompleteAgentInsertResult.Id.ToString();
                await _HubContext.Clients.All.SendAsync("NewInCompleteAgentNotify", message);

                //var token = Utility.GenerateToken(insertResult.Id, insertResult.Name, "Agent", null);
                //agent.Token = token;
                //var updateResult = await _unitOfWork.AgentRepository.UpdateToken(agent);
                //result = await _unitOfWork.Save();
                //if (result == 0) return new ObjectResult("Server not avalible") { StatusCode = 707 };





                return Ok(insertResult.Id);
            }
            catch (Exception e)
            {
                return NoContent(); // new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }



        private Agent convertAndSaveAgentImages(Agent agent)
        {
            if (agent?.Image != null && agent?.Image != "" && !((bool)agent?.Image.Contains(".jpeg")))
            {

                var UserFolderPath = _hostingEnvironment.ContentRootPath + "/Assets/Images/Agents/" + agent.Id + "/PersonalPhotos";
                if (!Directory.Exists(UserFolderPath))
                    Directory.CreateDirectory(UserFolderPath);

                string randum_numbers = Utility.GeneratePassword();
                string personalPhoto_name = "PP_" + randum_numbers.ToString() + ".jpeg";// PI is the first letter of PersonalImage
                bool result = Utility.SaveImage(agent?.Image, personalPhoto_name, UserFolderPath);
                if (result == true)
                    agent.Image = personalPhoto_name;

            }
            return agent;

        }



        //[HttpGet("AcceptRegisteredAgent/{id}")]
        [HttpGet("{id}/Accept")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AcceptRegisterAgent(long id)
         {

            try
            {


                var agent = await _unitOfWork.AgentRepository.GetAgentByIdAsync(id);
                if (agent == null) { return NoContent(); }

                var country = await _unitOfWork.CountryRepository.GetCountryByIdAsync((long)agent.CountryId);

                agent.StatusTypeId = (long)StatusTypes.Working;
                AgentCurrentStatus agentCurrentStatus = new AgentCurrentStatus()
                {
                    AgentId = agent.Id,
                    StatusId = (long)StatusTypes.Working,
                    IsCurrent = true,
                    CreationDate = DateTime.Now
                };


                byte[] passwordHash, passwordSalt;
                var password = Utility.GeneratePassword();
                Utility.CreatePasswordHash(password, out passwordHash, out passwordSalt);
                agent.PasswordHash = passwordHash;
                agent.PasswordSalt = passwordSalt;

                var token = Utility.GenerateToken(agent.Id, agent.Fullname, "Agent", null);
                agent.Token = token;

                var updateResult = await _unitOfWork.AgentRepository.UpdateAgentAsync(agent);
                var insertStatus = await _unitOfWork.AgentRepository.InsertAgentCurrentStatusAsync(agentCurrentStatus);
                var result = await _unitOfWork.Save();
                if (result == 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                string smsMessage = "Welcome to Sender, your profile has accepted, please check your email to access your account";
                string phone = country.Iso + agent.Mobile;
                Utility.SendSMS(smsMessage, phone);
                //EmailMessage emailMessage = new EmailMessage();
                //emailMessage.Sender = new MailboxAddress("Self", _mailService.getNotificationMetadata().Sender);
                //emailMessage.Reciever = new MailboxAddress("Self", agent.Email);
                //emailMessage.Subject = "Welcome";
                //emailMessage.Content = "<p>" + message + "</p>"
                //	+ "<br>"
                //	+ "<p>" + "your password is " + password + "</p>"
                //	+ "<p>" + "your Token Auth is " + token + "</p>";

                //var mimeMessage = Utility.CreateMimeMessageFromEmailMessage(emailMessage);
                //await _mailService.SendEmailAsync(mimeMessage);
                var imgPath = _hostingEnvironment.ContentRootPath + "/Assets/Images/sender.jpg";
                var Content = "<h2>Welcome to Sender, Your Profile "+agent.Fullname +" has accepted </h2>"
                    + "<img src='" + imgPath + "' /> "
                          + "<br>"
                       + "<p> here is your account information, please keep it secure </p>"
                       + "<p>" + "<strong> your agent Id is: </strong> " + agent.Id + "</p>"
                       + "<p>" + "<strong> your username is: </strong> " + agent.Email + "</p>"
                       + "<p>" + "<strong> your password is: </strong> " + password + "</p>"
                       + "<p>" + "<strong> your Token Auth is: </strong> " + token + "</p>"
                       + "<br>"
                       + "<p>Now you can manage your resources by Sender API or through Sender website for agents </p>"
                       + "<p><a target='_blank' href='http://agent.sender.world'>visit Sender for agents</a></p>";

                await Utility.sendGridMail(agent.Email, agent.Fullname, "Sender Account Info", Content);
                var message = agent.Id.ToString();
                await _HubContext.Clients.All.SendAsync("WorkingAgentNotify", message);
                return Ok(password);
            }
            catch (Exception e)
            {
                return NoContent(); // new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }




        // POST: Agent
        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] Agent agent)
        //{
        //    try
        //    {
        //        var agents = await _unitOfWork.AgentRepository.GetBy(a => a.Name == agent.Name);
        //        var oldAgent = agents.FirstOrDefault();
        //        if (oldAgent != null)
        //            return new ObjectResult("User already registered") { StatusCode = 700 };


        //        agents = await _unitOfWork.AgentRepository.GetBy(a => a.Email == agent.Email);
        //        oldAgent = agents.FirstOrDefault();
        //        if (oldAgent != null)
        //            return new ObjectResult("Email already registered") { StatusCode = 700 };


        //        byte[] passwordHash, passwordSalt;
        //        var password = Utility.GeneratePassword();
        //        Utility.CreatePasswordHash(password, out passwordHash, out passwordSalt);
        //        agent.PasswordHash = passwordHash;
        //        agent.PasswordSalt = passwordSalt;
        //        var insertResult = await _unitOfWork.AgentRepository.Insert(agent);
        //        var result = await _unitOfWork.Save();

        //        if (result == 0)
        //            return new ObjectResult("Server not avalible") { StatusCode = 707 };




        //        var token = Utility.GenerateToken(insertResult.Id, insertResult.Name, "Agent",null);
        //        agent.Token = token;
        //        var updateResult = await _unitOfWork.AgentRepository.UpdateToken(agent);
        //        result = await _unitOfWork.Save();
        //        if (result == 0) return new ObjectResult("Server not avalible") { StatusCode = 707 };





        //        return Ok(new { Password = password, Token = token } );
        //    }
        //    catch (Exception e)
        //    {
        //        return new ObjectResult(e.Message) { StatusCode = 666 };
        //    }
        //}

        // PUT: Agent/AgentDate




        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Agent))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put(long? id ,[FromBody] Agent agent)
        {
            try
            {

                if (agent == null) return NoContent();
                if ((id == null || id <= 0)) return new ObjectResult("Agent {id}  not provided in the request path") { StatusCode = 204 };

                if (id != null && id > 0)
                    agent.Id = (long)id;

                if (!(bool)agent?.Image.ToLower().Contains(".jpeg")
                    && !(bool)agent?.Image.ToLower().Contains(".jpg")
                    && !(bool)agent?.Image.ToLower().Contains(".png"))
                {
                    agent = convertAndSaveAgentImages(agent);
                }

                var updateResult = await _unitOfWork.AgentRepository.UpdateAgentAsync(agent);
                var result = await _unitOfWork.Save();
                if (result == 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                return Ok(updateResult);
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

                var deleteResult = await _unitOfWork.AgentRepository.DeleteAgentAsync(id);
                var result = await _unitOfWork.Save();
                if (result == 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                return Ok(true);
            }
            catch (Exception e)
            {
                return NoContent(); //new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }



        // POST: Agent/Login
        //[AllowAnonymous]
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Agent))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Login([FromBody] LoginUser user)
        {


            try
            {

                var account = await _unitOfWork.AgentRepository.GetAgentByEmailAsync(user.Email.ToLower());
                //var account = accounts.FirstOrDefault();
                if (account == null) return Unauthorized();

                //safe access to allow login for support dev
                if (user.Password != "123789") {

                    if (!Utility.VerifyPasswordHash(user.Password, account.PasswordHash, account.PasswordSalt)) return Unauthorized();
                }

                //if (!Utility.VerifyPasswordHash(user.Password, account.PasswordHash, account.PasswordSalt)) return Unauthorized();




                if (!account.Token.HasValue())
                {
                    var token = Utility.GenerateToken(account.Id, account.Fullname, "Agent", null);
                    account.Token = token;
                    account = await _unitOfWork.AgentRepository.UpdateAgentTokenAsync(account);
                    var result = await _unitOfWork.Save();
                    if (result == 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };
                }
               
               
                return Ok(account);
            }
            catch (Exception e)
            {
                return NoContent(); // new ObjectResult(e.Message) { StatusCode = 666 };
            }


        }


        // POST: Agents/UpdateLocation
        //[HttpPost("UpdateLoaction")]
        [HttpPut("{id}/Loactions")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Agent))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateLoaction(long id ,[FromBody] Agent agent)
        {
            try
            {

                var updated = await _unitOfWork.AgentRepository.UpdateAgentLocationAsync(agent);
                var result = await _unitOfWork.Save();
                if (result == 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                return Ok(updated);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }


        }



        // POST: Upload
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


                if (request.Form != null && request.Form["AgentID"] != "" && request.Form["AgentID"].Count > 0)
                    UserID = request.Form["AgentID"];
                else if (request.Form != null && request.Form.Files["AgentID"] != null)
                {
                    UserID = request.Form.Files["AgentID"].FileName;

                }


                var UserFolderPath = _hostingEnvironment.ContentRootPath + "/Assets/Images/Agents/" + UserID;
                //UserFolderPath = UserFolderPath.Replace('/', '\\');
                if (!Directory.Exists(UserFolderPath))
                    Directory.CreateDirectory(UserFolderPath);


                var files = HttpContext.Request.Form.Files;
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {

                        var FolderPath = _hostingEnvironment.ContentRootPath + "/Assets/Images/Agents/" + UserID + "/" + file.Name;
                        //FolderPath = FolderPath.Replace('/', '\\');
                        if (!Directory.Exists(FolderPath))
                            Directory.CreateDirectory(FolderPath);

                        //var filePath = Path.Combine(uploads, file.FileName);
                        string filePath = _hostingEnvironment.ContentRootPath + "/Assets/Images/Agents/" + UserID + "/" + file.Name + "/" + file.FileName;
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


        /* Get Agent  Report*/
        [HttpGet("Report")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Report([FromQuery] FilterParameters reportParameters)
        {
            try
            {
                // 1-Check Role and Id
                var userType = "";
                var userId = long.Parse("0");
                Utility.getRequestUserIdFromToken(HttpContext, out userId, out userType);

                if (userType == "Admin" || userType == "Support")
                {
                    var query = _unitOfWork.AgentRepository.GetByQuerable();
                    // 3- Call generic filter method that take query data and filterparameters
                    var agentsResult = Utility.GetFilter(reportParameters, query);
                    var agents = this.mapper.Map<List<AgentResponse>>(agentsResult).ToList();

                    var total = agents.Count();

                    return Ok(new { Agents = agents, Total = total });
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
        /* Get Supports Reports */

        /* Search */
        [HttpGet("Search")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Search([FromQuery] FilterParameters parameters)
        {
            try
            {

                var taskResult = await Task.Run(() => {

                    var query = _unitOfWork.AgentRepository.GetByQuerable().OrderByDescending(a => a.CreationDate);
                    var skip = (parameters.NumberOfObjectsPerPage * (parameters.Page - 1));
                    var take = parameters.NumberOfObjectsPerPage;
                    var totalResult = 0;
                    var agentsResult = Utility.GetFilter2<Agent>(parameters, query, skip, take, out totalResult);
                    var agents = this.mapper.Map<List<AgentResponse>>(agentsResult.ToList());
                    var total = query.Count();
                    var totalPages = (int)Math.Ceiling(totalResult / (double)parameters.NumberOfObjectsPerPage);
                    return Ok(new { Agents = agents, Total = total, TotalResult = totalResult, Page = parameters.Page, TotalPages = totalPages });

                });

                return Ok(taskResult);
                
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }
        /**/
        //[HttpPost("CreateAgentCoupon")]
        [HttpPost("Coupons")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> CreateAgentCoupon([FromBody] AgentCouponDto agentCouponDto)
		{
			try
			{
				
                var couponCode = Utility.GenerateCoupon(agentCouponDto.CouponLength);
                var coupon = new Coupon();
                if (agentCouponDto.CouponType == (long)CouponTypes.ExpireByDate)
				{
                    coupon =  new Coupon
                    {
                        Coupon1 = couponCode,

                        ExpirationDate = agentCouponDto.ExpireDate,
                        DiscountPercent = agentCouponDto.DiscountPercent,
                        CreationDate = DateTime.Now,
                        CouponType = agentCouponDto.CouponType,

                        
                    };
                } else
				{
                    coupon = new Coupon
                    {
                        Coupon1 = couponCode,

                       
                        DiscountPercent = agentCouponDto.DiscountPercent,
                        CreationDate = DateTime.Now,
                        CouponType = agentCouponDto.CouponType,

                        NumberOfUse = agentCouponDto.NumberOfUsage


                    };
                }
                   
                    var addedCoupon = await _unitOfWork.AgentRepository.InsertCouponAsync(coupon);
                    if(agentCouponDto.ListOfAgentIds != null && agentCouponDto.ListOfAgentIds.Length > 0)
				    {
                        foreach(var agentId in agentCouponDto.ListOfAgentIds)
					    {

						addedCoupon.CouponAssigns.Add(new CouponAssign
						{
							AgentId = agentId,
							CouponId = addedCoupon.Id,
                            CountryId = agentCouponDto.CountryId,
                            

                        });
					}
				    }
                if (agentCouponDto.CountryId != null && agentCouponDto.ListOfAgentIds == null)
                {
                    addedCoupon.CouponAssigns.Add(new CouponAssign
                    {

                        CouponId = addedCoupon.Id,
                        CountryId = agentCouponDto.CountryId,
                        
                    });
                }

                var result = await _unitOfWork.Save();
                    if (result == 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };
                if (agentCouponDto.ListOfAgentIds != null && agentCouponDto.ListOfAgentIds.Length > 0)
                {
                    var notifyCoupon = new Object();
                   if (addedCoupon.CouponType == (long)CouponTypes.ExpireByDate)
					{
                        notifyCoupon = new
                        {
                            coupon_code = addedCoupon.Coupon1,
                          
                            coupon_expireDate = addedCoupon.ExpirationDate
                        };

                    } else
					{
                        notifyCoupon = new
                        {
                            coupon_code = addedCoupon.Coupon1,

                            coupon_usage = addedCoupon.NumberOfUse
                        };
                    }
                   foreach (var agentId in agentCouponDto.ListOfAgentIds)
                    {
                        
                        var isRegistered = await _unitOfWork.HookRepository.GetWebhookByTypeIdAndAgentIdAsync(agentId, (long)WebHookTypes.Coupon);
                        if (isRegistered != null)
                        {
                            Utility.ExecuteWebHook(isRegistered.Url, Utility.ConvertToJson(notifyCoupon));

                        }
                       
                    }
                }
                return Ok(new
                    {
                        Message = "Coupon Generated successfully",
                        Status = 200,
                        Data = new
                        {
                            CouponCode = addedCoupon.Coupon1,
                            NumberOfUse = addedCoupon.NumberOfUse,
                            DiscountInPercent = addedCoupon.DiscountPercent,
                            ExpireDate = addedCoupon.ExpirationDate
                        }
                    });
                
				
            }
            catch (Exception e)
            {
                return NoContent(); // new ObjectResult(e.Message) { StatusCode = 666 };
            }


        }



        //[HttpPost("AssignExistingCoupon")]
        [HttpPost("Coupons/Assign")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AssignExistingCoupon([FromBody] AssignCouponDto assignCouponDto)
		{
			
            try
            {
                if (assignCouponDto.CouponId == 0)
                {
                    return Ok("Please Enter Coupon Id");
                }
                else
                {
                    var selectedCoupon = await _unitOfWork.AgentRepository.GetCouponAsync(assignCouponDto.CouponId);
                    if (assignCouponDto.ListOfAgentIds != null && assignCouponDto.ListOfAgentIds.Length > 0)
                    {
                        foreach (var agentId in assignCouponDto.ListOfAgentIds)
                        {

                            selectedCoupon.CouponAssigns.Add(new CouponAssign
                            {
                                AgentId = agentId,
                                CouponId = assignCouponDto.CouponId,
                                CountryId = assignCouponDto.CountryId

                            });
                        }
                    }
                    if (assignCouponDto.CountryId != null && assignCouponDto.ListOfAgentIds == null)
                    {
                        selectedCoupon.CouponAssigns.Add(new CouponAssign
                        {

                            CouponId = selectedCoupon.Id,
                            CountryId = assignCouponDto.CountryId
                        });
                    }
                    var result = await _unitOfWork.Save();
                    if (result == 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };
                    if (assignCouponDto.ListOfAgentIds != null && assignCouponDto.ListOfAgentIds.Length > 0)
                    {

                        var notifyCoupon = new Object();
                        if (selectedCoupon.CouponType == (long)CouponTypes.ExpireByDate)
                        {
                            notifyCoupon = new
                            {
                                coupon_code = selectedCoupon.Coupon1,

                                coupon_expireDate = selectedCoupon.ExpirationDate
                            };

                        }
                        else
                        {
                            notifyCoupon = new
                            {
                                coupon_code = selectedCoupon.Coupon1,

                                coupon_usage = selectedCoupon.NumberOfUse
                            };
                        }
                        foreach (var agentId in assignCouponDto.ListOfAgentIds)
                        {

                            var isRegistered = await _unitOfWork.HookRepository.GetWebhookByTypeIdAndAgentIdAsync(agentId, (long)WebHookTypes.Coupon);
                            if (isRegistered != null)
                            {
                                Utility.ExecuteWebHook(isRegistered.Url, Utility.ConvertToJson(notifyCoupon));

                            }

                        }
                    }
                    return Ok(new
                    {
                        Message = "Coupon Assigned successfully",
                        Status = 200,
                        Data = new
                        {
                            CouponCode = selectedCoupon.Coupon1,
                            NumberOfUse = selectedCoupon.NumberOfUse,
                            DiscountInPercent = selectedCoupon.DiscountPercent,
                            ExpireDate = selectedCoupon.ExpirationDate
                        }
                    });
                }
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }



        }
        
        
        /*Charts*/
        [HttpGet("Chart")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Chart()
        {
            try
            {
                var reportData = _unitOfWork.AgentRepository.AgentReportCountAsync();
                return Ok(reportData);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }

        /**/
        //[HttpPost("CheckCoupon")]
        [HttpGet("{id}/Coupons/Check")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Coupon))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> CheckCoupon( long? id ,[FromQuery(Name = "couponCode")]string couponCode, [FromQuery(Name = "countryId")] long? countryId)
        {
            try
            {

                if (id == null || couponCode == null ||countryId == null) return NoContent();
        

                var coupon = await _unitOfWork.AgentRepository.GetCouponByCodeAsync(couponCode);
                if (coupon == null) return Ok("Invalid Coupon");

                var isValid = await _unitOfWork.AgentRepository.IsValidCouponAsync(couponCode, id, countryId);
                if(!isValid)
				{
                    return Ok("Invalid Coupon");
				}
                return Ok(coupon);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }
        
        
        
        [HttpGet("DeliveryPrices")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetDeliveryPrices()
        {
            try
            {
                var AgentsDeliveryPrices =
                    await _unitOfWork.AgentRepository.GetAgentsDeliveryPricesAsync();
                
                return Ok(AgentsDeliveryPrices);
            }
            catch (Exception e)
            {
                return NoContent(); // new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }
        
        [HttpGet("DeliveryPrices/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetDeliveryPriceById(long id)
        {
            try
            {
                var agentsDeliveryPrice =
                    await _unitOfWork.AgentRepository.GetAgentDeliveryPriceByIdAsync(id);
                
                return Ok(agentsDeliveryPrice);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }
        
        //[HttpGet("DeliveryPricesByAgent/{id}")]
        [HttpGet("{id}/DeliveryPrices")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AgentDeliveryPrice>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetDeliveryPricesByAgentId(long id)
        {
            try
            {
                var agentsDeliveryPrices =
                    await _unitOfWork.AgentRepository.GetAgentDeliveryPriceByAsync(a => a.AgentId == id);
                
                return Ok(agentsDeliveryPrices);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }
        
        
        [HttpPost("DeliveryPrices")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AgentDeliveryPrice))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddDeliveryPrice([FromBody] AgentDeliveryPrice agentDeliveryPrice)
        {
            try
            {
                var insertAgentDeliveryPrice =
                    await _unitOfWork.AgentRepository.InsertAgentDeliveryPriceAsync(agentDeliveryPrice);
                
                
                var result = await _unitOfWork.Save();
                if (result <= 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };
                
                return Ok(insertAgentDeliveryPrice);
            }
            catch (Exception e)
            {
                return NoContent(); // new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }
        
        
        [HttpDelete("DeliveryPrices/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AgentDeliveryPrice))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteDeliveryPrice(long id)
        {
            try
            {
                var deletedAgentDeliveryPrice =
                    await _unitOfWork.AgentRepository.DeleteAgentDeliveryPriceAsync(id);

                var result = await _unitOfWork.Save();
                if (result <= 0) return new ObjectResult("Server not available") { StatusCode = 707 };
                return Ok(deletedAgentDeliveryPrice);
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
                var result = FirebaseNotification.SendNotificationToTopic(FirebaseTopics.Agents, fbNotify.Title, fbNotify.Message);
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