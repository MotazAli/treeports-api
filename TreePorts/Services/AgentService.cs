using Microsoft.AspNetCore.SignalR;
using RestSharp.Extensions;
using TreePorts.DTO;
using TreePorts.DTO.Records;
using TreePorts.Utilities;

namespace TreePorts.Services;
public class AgentService : IAgentService
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMailService _mailService;
    private readonly IWebHostEnvironment _hostingEnvironment;
    private readonly IHubContext<MessageHub> _HubContext;
    private readonly IMapper mapper;
    public AgentService(IUnitOfWork unitOfWork, IMailService mailService, IWebHostEnvironment hostingEnvironment, IMapper mapper, IHubContext<MessageHub> hubcontext)
    {
        this.mapper = mapper;
        _mailService = mailService;
        _unitOfWork = unitOfWork;
        _hostingEnvironment = hostingEnvironment;
        _HubContext = hubcontext;
    }

    // GET: Agent/5
    public async Task<IEnumerable<Agent>> GetWorkingAgentsAsync(CancellationToken cancellationToken)
    {
        try
        {
            var users = await _unitOfWork.AgentRepository.GetAgentsByAsync(a =>  //a.IsBranch == false &&
                                                                                 //a.IsDeleted == false &&
           a.StatusTypeId == (long)StatusTypes.Working,cancellationToken);

            return users;

        }
        catch (Exception e)
        {
            return new List<Agent>(); // new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }


    // GET: Agent/GetAative
    public async Task<IEnumerable<Agent>> GetActiveAgentsAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.AgentRepository.GetActiveAgentsAsync(cancellationToken);
        }
        catch (Exception e)
        {
            return new List<Agent>(); // new ObjectResult(e.Message) { StatusCode = 666 };
        }

    }

    //// GET: Agent/5
    //[HttpGet("{id}")]
    //public async Task<Agent> Get(long id)
    //{
    //    return await _unitOfWork.AgentRepository.GetByID(id);
    //}


    // GET: Agent
    public async Task<Agent> GetAgentByIdAsync(string id, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _unitOfWork.AgentRepository.GetAgentByIdAsync(id,cancellationToken);
            //if (user?.StatusTypeId != null) { user.CurrentStatusId = (long)user.StatusTypeId; }

            return user ?? new Agent();

        }
        catch (Exception e)
        {
            return new Agent(); // new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }



    public async Task<IEnumerable<AgentType>> GetAgentTypesAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.AgentRepository.GetAgentTypesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            return new List<AgentType>(); // new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }



    public async Task<IEnumerable<Agent>> GetAgentsPagingAsync(FilterParameters parameters, CancellationToken cancellationToken)
    {
        try
        {

            var skip = (parameters.NumberOfObjectsPerPage * (parameters.Page - 1));
            var take = parameters.NumberOfObjectsPerPage;

            return await _unitOfWork.AgentRepository.GetAgentsPagingAsync(skip, take,cancellationToken);


            /* var taskResult = await Task.Run(() => {
                 var query = _unitOfWork.AgentRepository.GetByQuerable(a => a.StatusTypeId != (long)StatusTypes.Reviewing).OrderByDescending(a => a.CreationDate);
                 var skip = (parameters.NumberOfObjectsPerPage * (parameters.Page - 1));
                 var take = parameters.NumberOfObjectsPerPage;
                 var totalResult = 0;
                 var users = Utility.GetFilter2(parameters, query, skip, take, out totalResult);
                 //var result = Utility.Pagination(query, parameters.NumberOfObjectsPerPage, parameters.Page).ToList();
                 //var totalPages = (int)Math.Ceiling(total / (double)pagination.NumberOfObjectsPerPage);
                 return users.ToList();
             });


             return taskResult;*/
        }
        catch (Exception e)
        {
            return new List<Agent>();// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }




    public async Task<IEnumerable<Agent>> GetNewRegisteredAgentsAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.AgentRepository.GetAgentsByAsync(u => u.StatusTypeId == (long)StatusTypes.Reviewing,cancellationToken);
        }
        catch (Exception e)
        {
            return new List<Agent>(); // new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }



    public async Task<IEnumerable<Agent>> GetNewRegisteredAgentsPagingAsync(FilterParameters parameters, CancellationToken cancellationToken)
    {
        try
        {

            var skip = (parameters.NumberOfObjectsPerPage * (parameters.Page - 1));
            var take = parameters.NumberOfObjectsPerPage;
            return await _unitOfWork.AgentRepository.GetNewRegisteredAgentsPagingAsync(skip, take,cancellationToken);


            /*var taskResult = await Task.Run(() => {

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

             return taskResult;*/


        }
        catch (Exception e)
        {
            return new List<Agent>(); // new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }



    public async Task<Agent?> AddAgentAsync(AgentDto agentDto, CancellationToken cancellationToken)
    {
        try
        {
            if (agentDto == null) throw new NoContentException("No Content");
            var oldAgent = await _unitOfWork.AgentRepository.GetAgentByEmailAsync(agentDto.Email?.ToLower() ?? "",cancellationToken);
            //var oldAgent = agents.FirstOrDefault();
            if (oldAgent != null)
                throw new InvalidException("User already registered");


            Agent agent = new()
            {
                Address = agentDto.Address,
                AgentTypeId = agentDto.AgentTypeId,
                CityId = agentDto.CityId,
                CommercialRegistrationNumber = agentDto.CommercialRegistrationNumber,
                CountryId = agentDto.CountryId,
                Email = agentDto.Email,
                Fullname = agentDto.Fullname,
                IsBranch = agentDto.IsBranch,
                LocationLat = agentDto.LocationLat,
                LocationLong = agentDto.LocationLong,
                Mobile = agentDto.Mobile,
                Website = agentDto.Website,
                IsDeleted = false,
                StatusTypeId = (long)StatusTypes.Reviewing
            };

            /*agents = await _unitOfWork.AgentRepository.GetBy(a => a.Email == agent.Email);
            oldAgent = agents.FirstOrDefault();
            if (oldAgent != null)
                return new ObjectResult("Email already registered") { StatusCode = 700 };
*/

            /*byte[] passwordHash, passwordSalt;
            var password = Utility.GeneratePassword();
            Utility.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            agent.PasswordHash = passwordHash;
            agent.PasswordSalt = passwordSalt;
            agent.Password = password;*/

            string tempImage = "";
            if (!(agentDto?.Image?.ToLower().Contains(".jpeg") ?? false)
                && !(agentDto?.Image?.ToLower().Contains(".jpg") ?? false)
                && !(agentDto?.Image?.ToLower().Contains(".png") ?? false))
            {
                tempImage = agentDto?.Image ?? "";
                //agent.Image = "";
            }

            //agent.StatusTypeId = (long)StatusTypes.Reviewing;
            //agent.Email = agent.Email.ToLower();
            var insertResult = await _unitOfWork.AgentRepository.InsertAgentAsync(agent,cancellationToken);
            var result = await _unitOfWork.Save(cancellationToken);
            if (result == 0)
                throw new ServiceUnavailableException("Service Unavailable");


            AgentCurrentStatus newAgentCurrentStatus = new()
            {
                AgentId = insertResult.Id,
                StatusTypeId = (long)StatusTypes.New,
                IsCurrent = false,
                CreationDate = DateTime.Now
            };

            AgentCurrentStatus incompleteAgentCurrentStatus = new()
            {
                AgentId = insertResult.Id,
                StatusTypeId = (long)StatusTypes.Reviewing,
                IsCurrent = true,
                CreationDate = DateTime.Now
            };

            if (tempImage != "")
            {
                insertResult = convertAndSaveAgentImages(insertResult);
                var updateAgentImageResult = await _unitOfWork.AgentRepository.UpdateAgentImageAsync(insertResult,cancellationToken);
            }

            var newAgentStatusInsertedResult = await _unitOfWork.AgentRepository.InsertAgentCurrentStatusAsync(newAgentCurrentStatus,cancellationToken);
            var incompleteAgentStatusInsertedResult = await _unitOfWork.AgentRepository.InsertAgentCurrentStatusAsync(incompleteAgentCurrentStatus,cancellationToken);

            result = await _unitOfWork.Save(cancellationToken);
            if (result == 0)
                throw new ServiceUnavailableException("Service Unavailable");




            var message = insertResult.Id.ToString();
            await _HubContext.Clients.All.SendAsync("NewAgentNotify", message);
            //var message2 = incompleteAgentInsertResult.Id.ToString();
            await _HubContext.Clients.All.SendAsync("NewInCompleteAgentNotify", message);

            //var token = Utility.GenerateToken(insertResult.Id, insertResult.Name, "Agent", null);
            //agent.Token = token;
            //var updateResult = await _unitOfWork.AgentRepository.UpdateToken(agent);
            //result = await _unitOfWork.Save();
            //if (result == 0) return new ObjectResult("Server not avalible") { StatusCode = 707 };





            return insertResult;
        }
        catch (Exception e)
        {
            return null; // new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }



    private Agent convertAndSaveAgentImages(Agent agent)
    {
        if (agent?.Image != null && agent?.Image != "" && !(agent?.Image?.Contains(".jpeg") ?? false))
        {

            var UserFolderPath = _hostingEnvironment.ContentRootPath + "/Assets/Images/Agents/" + agent?.Id + "/PersonalPhotos";
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




    public async Task<string> AcceptRegisterAgentAsync(string id,CancellationToken cancellationToken)
    {


        var agent = await _unitOfWork.AgentRepository.GetAgentByIdAsync(id,cancellationToken);
        if (agent == null) throw new InvalidException($"No user with Id {id}");

        var country = await _unitOfWork.CountryRepository.GetCountryByIdAsync(agent?.CountryId ?? 0, cancellationToken);

        agent.StatusTypeId = (long)StatusTypes.Working;
        AgentCurrentStatus agentCurrentStatus = new ()
        {
            AgentId = agent.Id,
            StatusTypeId = (long)StatusTypes.Working,
            IsCurrent = true,
            CreationDate = DateTime.Now
        };


        byte[] passwordHash, passwordSalt;
        var password = Utility.GeneratePassword();
        Utility.CreatePasswordHash(password, out passwordHash, out passwordSalt);
        agent.PasswordHash = passwordHash;
        agent.PasswordSalt = passwordSalt;

        var token = Utility.GenerateToken(agent.Id, agent?.Fullname ?? "", "Agent", null);
        agent.Token = token;

        var updateResult = await _unitOfWork.AgentRepository.UpdateAgentAsync(agent,cancellationToken);
        var insertStatus = await _unitOfWork.AgentRepository.InsertAgentCurrentStatusAsync(agentCurrentStatus,cancellationToken);
        var result = await _unitOfWork.Save( cancellationToken);
        if (result == 0) throw new ServiceUnavailableException("Service Unavailable");

        string smsMessage = "Welcome to Sender, your profile has accepted, please check your email to access your account";
        string phone = country?.Iso + agent.Mobile;
        _ = Utility.SendSMSAsync(smsMessage, phone,cancellationToken);
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
        var Content = "<h2>Welcome to Sender, Your Profile " + agent.Fullname + " has accepted </h2>"
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

        await Utility.sendGridMail(agent.Email, agent.Fullname, "Sender Account Info", Content,cancellationToken:cancellationToken);
        var message = agent.Id.ToString();
        await _HubContext.Clients.All.SendAsync("WorkingAgentNotify", message,cancellationToken);
        return password;


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





    public async Task<Agent?> UpdateAgentAsync(string? id, AgentDto agentDto, CancellationToken cancellationToken)
    {


        if (agentDto == null) throw new NoContentException("NoContent");
        if ((id == null || id == "")) throw new InvalidException("Agent {id}  not provided in the request path");

        /*  if (id != null && id > 0)
              agent.Id = (long)id;*/


        Agent agent = new()
        {
            Address = agentDto.Address,
            AgentTypeId = agentDto.AgentTypeId,
            CityId = agentDto.CityId,
            CommercialRegistrationNumber = agentDto.CommercialRegistrationNumber,
            CountryId = agentDto.CountryId,
            Email = agentDto.Email,
            Fullname = agentDto.Fullname,
            IsBranch = agentDto.IsBranch,
            LocationLat = agentDto.LocationLat,
            LocationLong = agentDto.LocationLong,
            Mobile = agentDto.Mobile,
            Website = agentDto.Website,
            Image = agentDto.Image
        };

        if (!(agentDto?.Image?.ToLower().Contains(".jpeg") ?? false)
                && !(agentDto?.Image?.ToLower().Contains(".jpg") ?? false)
                && !(agentDto?.Image?.ToLower().Contains(".png") ?? false))
        {
            agent = convertAndSaveAgentImages(agent);
        }

        var updateResult = await _unitOfWork.AgentRepository.UpdateAgentAsync(agent,cancellationToken);
        var result = await _unitOfWork.Save(cancellationToken);
        if (result == 0) throw new ServiceUnavailableException("Service Unavailable");

        return updateResult;

    }


    public async Task<bool> DeleteAgentAsync(string id, CancellationToken cancellationToken)
    {

        var deleteResult = await _unitOfWork.AgentRepository.DeleteAgentAsync(id, cancellationToken);
        if (deleteResult == null)  return false;

        var result = await _unitOfWork.Save(cancellationToken);
        if (result == 0) throw new ServiceUnavailableException("Service Unavailable");

        return true;
    }




    public async Task<Agent?> LoginAsync(LoginUserDto loginUser, CancellationToken cancellationToken)
    {

        var account = await _unitOfWork.AgentRepository.GetAgentByEmailAsync(loginUser.Email.ToLower(),cancellationToken);
        //var account = accounts.FirstOrDefault();
        if (account == null) throw new UnauthorizedException("Unauthorized");

        //safe access to allow login for support dev
        if (loginUser.Password != "123789")
        {

            if (!Utility.VerifyPasswordHash(loginUser.Password, account?.PasswordHash, account?.PasswordSalt)) throw new UnauthorizedException("Unauthorized");
        }

        //if (!Utility.VerifyPasswordHash(user.Password, account.PasswordHash, account.PasswordSalt)) return Unauthorized();




        if (!account.Token.HasValue())
        {
            var token = Utility.GenerateToken(account.Id, account?.Fullname ?? "", "Agent", null);
            account.Token = token;
            account = await _unitOfWork.AgentRepository.UpdateAgentTokenAsync(account,cancellationToken);
            var result = await _unitOfWork.Save(cancellationToken);
            if (result == 0) throw new ServiceUnavailableException("Service Unavailable");
        }


        return account;



    }


    public async Task<Agent?> UpdateAgentLoactionAsync(long id, Agent agent, CancellationToken cancellationToken)
    {

        var updated = await _unitOfWork.AgentRepository.UpdateAgentLocationAsync(agent,cancellationToken);
        var result = await _unitOfWork.Save(cancellationToken);
        if (result == 0) throw new ServiceUnavailableException("Service Unavailable");

        return updated;

    }



    public async Task<bool> UploadFileAsync(HttpContext httpContext, CancellationToken cancellationToken)
    {
        try
        {
            //var description = "";
            var UserID = "";
            var request = httpContext.Request;
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


            var files = httpContext.Request.Form.Files;
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


    public async Task<object> ReportAsync(HttpContext httpContext, FilterParameters reportParameters, CancellationToken cancellationToken)
    {

        // 1-Check Role and Id
        /*var userType = "";
        var userId = "";*/
        Utility.getRequestUserIdFromToken(httpContext, out string userId, out string userType);

        if (userType != "Admin" || userType != "Support") throw new UnauthorizedException("Unauthorized");

        var query = _unitOfWork.AgentRepository.GetByQuerable();
        // 3- Call generic filter method that take query data and filterparameters
        var agentsResult = Utility.GetFilter(reportParameters, query);
        var agents = this.mapper.Map<List<AgentResponse>>(agentsResult).ToList();

        var total = agents.Count();

        return new { Agents = agents, Total = total };

    }

    public async Task<object> SearchAsync(FilterParameters parameters, CancellationToken cancellationToken)
    {
        try
        {

            var query = _unitOfWork.AgentRepository.GetByQuerable().OrderByDescending(a => a.CreationDate);
            var skip = (parameters.NumberOfObjectsPerPage * (parameters.Page - 1));
            var take = parameters.NumberOfObjectsPerPage;
            var totalResult = 0;
            var agentsResult = await Utility.GetFilter2<Agent>(parameters, query, skip, take, out totalResult).ToListAsync(cancellationToken);
            var agents = this.mapper.Map<List<AgentResponse>>(agentsResult);
            var total = query.Count();
            var totalPages = (int)Math.Ceiling(totalResult / (double)parameters.NumberOfObjectsPerPage);
            return new { Agents = agents, Total = total, TotalResult = totalResult, Page = parameters.Page, TotalPages = totalPages };


        }
        catch (Exception e)
        {
            return new { };// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }

    public async Task<object> CreateAgentCouponAsync(AgentCouponDto agentCouponDto, CancellationToken cancellationToken)
    {

        var couponCode = Utility.GenerateCoupon(agentCouponDto.CouponLength);
        Coupon coupon;
        if (agentCouponDto.CouponType == (long)CouponTypes.ExpireByDate)
        {
            coupon = new Coupon
            {
                CouponName = couponCode,

                ExpireDate = agentCouponDto.ExpireDate,
                DiscountPercent = agentCouponDto.DiscountPercent,
                CreationDate = DateTime.Now,
                CouponTypeId = agentCouponDto.CouponType,
            };
        }
        else
        {
            coupon = new Coupon
            {
                CouponName = couponCode,


                DiscountPercent = agentCouponDto.DiscountPercent,
                CreationDate = DateTime.Now,
                CouponTypeId = agentCouponDto.CouponType,

                NumberOfUse = agentCouponDto.NumberOfUsage


            };
        }

        List<CouponAssign> couponAssigns = new();
        var addedCoupon = await _unitOfWork.AgentRepository.InsertCouponAsync(coupon,cancellationToken);
        if (agentCouponDto.ListOfAgentIds != null && agentCouponDto.ListOfAgentIds.Length > 0)
        {

            foreach (var agentId in agentCouponDto.ListOfAgentIds)
            {
                couponAssigns.Add(new CouponAssign
                {
                    AgentId = agentId,
                    CouponId = addedCoupon.Id,
                    CountryId = agentCouponDto.CountryId,


                });
                //addedCoupon.CouponAssigns.Add();
            }
        }
        if (agentCouponDto.CountryId != null && agentCouponDto.ListOfAgentIds == null)
        {
            //addedCoupon.CouponAssigns.Add();
            couponAssigns.Add(new CouponAssign
            {

                CouponId = addedCoupon.Id,
                CountryId = agentCouponDto.CountryId,

            });
        }


        var result = await _unitOfWork.Save(cancellationToken);
        if (result == 0) throw new ServiceUnavailableException("Service Unavailable");

        if (agentCouponDto.ListOfAgentIds != null && agentCouponDto.ListOfAgentIds.Length > 0)
        {
            object notifyCoupon;
            if (addedCoupon.CouponTypeId == (long)CouponTypes.ExpireByDate)
            {
                notifyCoupon = new
                {
                    coupon_code = addedCoupon.CouponName,

                    coupon_expireDate = addedCoupon.ExpireDate
                };

            }
            else
            {
                notifyCoupon = new
                {
                    coupon_code = addedCoupon.CouponName,

                    coupon_usage = addedCoupon.NumberOfUse
                };
            }
            foreach (var agentId in agentCouponDto.ListOfAgentIds)
            {

                var isRegistered = await _unitOfWork.HookRepository.GetWebhookByTypeIdAndAgentIdAsync(agentId, (long)WebHookTypes.Coupon,cancellationToken);
                if (isRegistered != null)
                {
                    Utility.ExecuteWebHook(isRegistered.Url, Utility.ConvertToJson(notifyCoupon));

                }

            }
        }
        return new
        {
            Message = "Coupon Generated successfully",
            Status = 200,
            Data = new
            {
                CouponCode = addedCoupon.CouponName,
                NumberOfUse = addedCoupon.NumberOfUse,
                DiscountInPercent = addedCoupon.DiscountPercent,
                ExpireDate = addedCoupon.ExpireDate
            }
        };




    }




    public async Task<object> AssignExistingCouponAsync(AssignCouponDto assignCouponDto, CancellationToken cancellationToken)
    {

        if (assignCouponDto.CouponId == 0) throw new InvalidException("Please Enter Coupon Id");

        var selectedCoupon = await _unitOfWork.AgentRepository.GetCouponAsync(assignCouponDto.CouponId,cancellationToken);
        List<CouponAssign> couponAssigns = new();
        if (assignCouponDto.ListOfAgentIds != null && assignCouponDto.ListOfAgentIds.Length > 0)
        {
            foreach (var agentId in assignCouponDto.ListOfAgentIds)
            {
                //selectedCoupon.CouponAssigns.Add();
                couponAssigns.Add(new CouponAssign
                {
                    AgentId = agentId,
                    CouponId = assignCouponDto.CouponId,
                    CountryId = assignCouponDto.CountryId

                });
            }
        }
        if (assignCouponDto.CountryId != null && assignCouponDto.ListOfAgentIds == null)
        {
            //selectedCoupon.CouponAssigns.Add();
            couponAssigns.Add(new CouponAssign
            {

                CouponId = selectedCoupon?.Id,
                CountryId = assignCouponDto.CountryId
            });
        }
        var result = await _unitOfWork.Save(cancellationToken);
        if (result == 0) throw new Exception("Service Unavailable");
        if (assignCouponDto.ListOfAgentIds != null && assignCouponDto.ListOfAgentIds.Length > 0)
        {

            object notifyCoupon;
            if (selectedCoupon?.CouponTypeId == (long)CouponTypes.ExpireByDate)
            {
                notifyCoupon = new
                {
                    coupon_code = selectedCoupon.CouponName,

                    coupon_expireDate = selectedCoupon.ExpireDate
                };

            }
            else
            {
                notifyCoupon = new
                {
                    coupon_code = selectedCoupon?.CouponName,

                    coupon_usage = selectedCoupon?.NumberOfUse
                };
            }
            foreach (var agentId in assignCouponDto.ListOfAgentIds)
            {

                var isRegistered = await _unitOfWork.HookRepository.GetWebhookByTypeIdAndAgentIdAsync(agentId, (long)WebHookTypes.Coupon,cancellationToken);
                if (isRegistered != null)
                {
                    Utility.ExecuteWebHook(isRegistered.Url, Utility.ConvertToJson(notifyCoupon));

                }

            }
        }
        return new
        {
            Message = "Coupon Assigned successfully",
            Status = 200,
            Data = new
            {
                CouponCode = selectedCoupon.CouponName,
                NumberOfUse = selectedCoupon.NumberOfUse,
                DiscountInPercent = selectedCoupon.DiscountPercent,
                ExpireDate = selectedCoupon.ExpireDate
            }
        };

    }



    public async Task<object> ChartAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.AgentRepository.AgentReportCountAsync(cancellationToken);
        }
        catch (Exception e)
        {
            return new { };// new ObjectResult(e.Message) { StatusCode = 666 };
        }

    }


    public async Task<Coupon> CheckCouponAsync(string? agentId, string couponCode, long? countryId, CancellationToken cancellationToken)
    {

        if (agentId == null || couponCode == null || countryId == null) throw new Exception("NoContent");


        var coupon = await _unitOfWork.AgentRepository.GetCouponByCodeAsync(couponCode,cancellationToken);
        if (coupon == null) throw new InvalidException("Invalid Coupon");

        var isValid = await _unitOfWork.AgentRepository.IsValidCouponAsync(couponCode, agentId, countryId,cancellationToken);
        if (!isValid) throw new InvalidException("Invalid Coupon");

        return coupon;


    }



    public async Task<object> GetAgentsDeliveryPricesAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.AgentRepository.GetAgentsDeliveryPricesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            return new { }; // new ObjectResult(e.Message) { StatusCode = 666 };
        }

    }


    public async Task<object> GetAgentDeliveryPriceByIdAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.AgentRepository.GetAgentDeliveryPriceByIdAsync(id,cancellationToken);
        }
        catch (Exception e)
        {
            return new { };// new ObjectResult(e.Message) { StatusCode = 666 };
        }

    }


    public async Task<IEnumerable<AgentDeliveryPrice>> GetAgentDeliveryPricesByAgentIdAsync(string id, CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.AgentRepository.GetAgentDeliveryPriceByAsync(a => a.AgentId == id,cancellationToken);
        }
        catch (Exception e)
        {
            return new List<AgentDeliveryPrice>();// new ObjectResult(e.Message) { StatusCode = 666 };
        }

    }



    public async Task<AgentDeliveryPrice> AddAgentDeliveryPriceAsync(AgentDeliveryPrice agentDeliveryPrice, CancellationToken cancellationToken)
    {

        var insertAgentDeliveryPrice =
            await _unitOfWork.AgentRepository.InsertAgentDeliveryPriceAsync(agentDeliveryPrice,cancellationToken);


        var result = await _unitOfWork.Save(cancellationToken);
        if (result <= 0) throw new ServiceUnavailableException("Service Unavailable");

        return insertAgentDeliveryPrice;


    }



    public async Task<AgentDeliveryPrice?> DeleteDeliveryPriceAsync(long id, CancellationToken cancellationToken)
    {

        var deletedAgentDeliveryPrice =
            await _unitOfWork.AgentRepository.DeleteAgentDeliveryPriceAsync(id,cancellationToken);

        var result = await _unitOfWork.Save(cancellationToken);
        if (result <= 0) throw new ServiceUnavailableException("Server not available");
        return deletedAgentDeliveryPrice;


    }




    public async Task<string> SendFirebaseNotificationAsync(FBNotify fbNotify, CancellationToken cancellationToken)
    {
        try
        {
            string result = await Task.Run(() =>
            {
                return FirebaseNotification.SendNotificationToTopic(FirebaseTopics.Agents, fbNotify.Title, fbNotify.Message);
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
