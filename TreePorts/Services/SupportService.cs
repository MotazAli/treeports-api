using Microsoft.AspNetCore.SignalR;
using RestSharp.Extensions;
using TreePorts.DTO;
using TreePorts.DTO.Records;
using TreePorts.Utilities;

namespace TreePorts.Services;
public class SupportService : ISupportService
{
    private IUnitOfWork _unitOfWork;
    private IWebHostEnvironment _hostingEnvironment;
    private IHubContext<MessageHub> _HubContext;
    private readonly IMapper mapper;
    public SupportService(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment, IHubContext<MessageHub> hubcontext, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _hostingEnvironment = hostingEnvironment;
        _HubContext = hubcontext;
        this.mapper = mapper;
    }


    public async Task<IEnumerable<Ticket>> GetTicketsAsyncs()
    {
        try
        {
            return await _unitOfWork.SupportRepository.GetTicketsAsync();
        }
        catch (Exception e)
        {
            return new List<Ticket>();//  new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }

    public async Task<Ticket?> GetTicketByIdAsync(long id)
    {
        try
        {
            return await _unitOfWork.SupportRepository.GetTicketByIdAsync(id);
        }
        catch (Exception e)
        {
            return new Ticket();//  new ObjectResult(e.Message) { StatusCode = 666 };
        }

    }



    public async Task<SupportUser> GetSupportAccountUserBySupportUserAccountIdAsync(string supportUserAccountId)
    {


        try
        {
            var user = await _unitOfWork.SupportRepository.GetSupportUserByIdAsync(supportUserAccountId);

            if (user.SupportUserAccounts?.Count > 0)
            {
                //user.CurrentStatusId = (long)user.SupportUserAccounts.FirstOrDefault().StatusTypeId;
            }
            else
            {
                var userAccounts = await _unitOfWork.SupportRepository.GetSupportUsersAccountsByAsync(u => u.SupportUserId == user.Id);
                var userAccount = userAccounts.FirstOrDefault();
                //if (userAccount?.StatusTypeId != null)
                //    user.CurrentStatusId = (long)userAccount.StatusTypeId;
            }

            return user;
        }
        catch (Exception e)
        {
            return new SupportUser();// new ObjectResult(e.Message) { StatusCode = 666 };
        }

    }



    public async Task<object> GetSupportUsersAccountsAsync(FilterParameters parameters)
    {
        try
        {
            var users = await _unitOfWork.SupportRepository.GetSupportUsersAsync();
            var total = users.Count;
            var result = Utility.Pagination(users, parameters.NumberOfObjectsPerPage, parameters.Page).ToList();
            var totalPages = (int)Math.Ceiling(total / (double)parameters.NumberOfObjectsPerPage);

            return new { Users = result, Total = total, Page = parameters.Page, TotaPages = totalPages };
        }
        catch (Exception e)
        {
            return new { };
        }
    }




    public async Task<bool> AddTicketAsync(Ticket ticket)
    {
        var savedSupport = await _unitOfWork.SupportRepository.InsertTicketAsync(ticket);
        var result = await _unitOfWork.Save();
        if (result == 0)
            throw new ServiceUnavailableException("Service Unavailable");



        var AllReadySupportUsers = await _unitOfWork.SupportRepository.GetSupportUsersWorkingStateByAsync(u => u.StatusTypeId == (long)SupportStatusTypes.Ready && u.IsCurrent == true);
        var ReadySupportUser = AllReadySupportUsers.OrderByDescending(s => s.CreationDate).FirstOrDefault();


        if (ReadySupportUser == null)
        {
            AllReadySupportUsers = await _unitOfWork.SupportRepository.GetSupportUsersWorkingStateByAsync(u => u.StatusTypeId == (long)SupportStatusTypes.Progress && u.IsCurrent == true);
            ReadySupportUser = AllReadySupportUsers.OrderByDescending(s => s.CreationDate).FirstOrDefault();
        }



        TicketAssignment ticketAssign = new()
        {
            TicketId = savedSupport.Id,
            CaptainUserAccountId = savedSupport.CaptainUserAccountId,
            SupportUserAccountId = ReadySupportUser?.SupportUserAccountId,
            TicketStatusTypeId = (long)SupportStatusTypes.New,
            CreationDate = DateTime.Now
        };

        SupportUserWorkingState supportUserWorkingState = new()
        {
            SupportUserAccountId = ReadySupportUser?.SupportUserAccountId,
            StatusTypeId = (long)SupportStatusTypes.Progress,
            IsCurrent = true,
            CreationDate = DateTime.Now
        };

        var insertResult = await _unitOfWork.SupportRepository.InsertSupportUserWorkingStateAsync(supportUserWorkingState);
        //newSupportUserStatus = await _unitOfWork.SupportRepository.InsertSupportUserStatuse(newSupportUserStatus);
        ticketAssign = await _unitOfWork.SupportRepository.InsertTicketAssignmentAsync(ticketAssign);
        var saveResult = await _unitOfWork.Save();
        if (saveResult == 0)
            throw new ServiceUnavailableException("Service Unavalible");



        var supportUsersMessageHub = await _unitOfWork.SupportRepository.GetSupportUsersMessageHubByAsync(u => u.SupportUserAccountId == ReadySupportUser.SupportUserAccountId);
        var supportUserMessageHub = supportUsersMessageHub.FirstOrDefault();
        if (supportUserMessageHub != null && supportUserMessageHub.Id > 0)
        {
            // var notificationReuslt = await Utility.SendFirebaseNotification(_hostingEnvironment, "newSupportRequest", supportAssign.Id.ToString(), supportUserMessageHub.ConnectionId);
            // if(notificationReuslt == null) return new ObjectResult("No support available") { StatusCode = 707 };
            //await Utility.SendFirebaseNotification(_hostingEnvironment, "newSupportRequest", supportAssign.Id.ToString(), supportUserMessageHub.ConnectionId);
            await _HubContext.Clients.Client(supportUserMessageHub.ConnectionId).SendAsync("newSupportRequest", ticketAssign.Id.ToString());
        }

        return true;

    }





    public async Task<TicketAssignment> GetTicketAssignedByCaptainUserAccountIdAsync(string captainUserAccountId)
    {

        


        try
        {
            var allAssigned = await _unitOfWork.SupportRepository.GetTicketsAssignmentsByAsync(
                                         a => a.CaptainUserAccountId == captainUserAccountId &&
                                         (a.TicketStatusTypeId == (long)SupportStatusTypes.New ||
                                         a.TicketStatusTypeId == (long)SupportStatusTypes.Progress));
            return  allAssigned.FirstOrDefault();
            //if (assigned == null) throw new NotFoundException("No running ticket support for that captain ");

            //return assigned;
        }
        catch (Exception e)
        {
            return new TicketAssignment();// new ObjectResult(e.Message) { StatusCode = 666 };
        }

    }




    public async Task<IEnumerable<TicketAssignment>> GetTicketsAssignedBySupportUserAccountIdAsync(string supportUserAccountId)
    {
        try
        {
            var assigned = await _unitOfWork.SupportRepository.GetTicketsAssignmentsByAsync(
                                             a => a.SupportUserAccountId == supportUserAccountId &&
                                             (a.TicketStatusTypeId == (long)SupportStatusTypes.New ||
                                             a.TicketStatusTypeId == (long)SupportStatusTypes.Progress));

            var result = assigned.OrderByDescending(a => a.CreationDate).ToList();

            return result;
        }
        catch (Exception e)
        {
            return new List<TicketAssignment>();// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }


    /*//[HttpGet("GetAllSupportAssigned/{id}")]
    [HttpGet("Users/{id}/Assignments")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SupportAssignment>))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IEnumerable<SupportAssignment>> GetTicketsAssignedAsync(long id)
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
*/



    public async Task<bool> UpdateTicketAsync(long ticketId, Ticket ticket)
    {

        ticket = await _unitOfWork.SupportRepository.UpdateTicketAsync(ticket);
        if(ticket == null) throw new ServiceUnavailableException("Service Unavalible");

        var allTicketAssgins = await _unitOfWork.SupportRepository.GetTicketsAssignmentsByAsync(a => a.TicketId == ticket.Id);
        var ticketAssgin = allTicketAssgins.FirstOrDefault();
        ticketAssgin.TicketStatusTypeId = ticket.TicketStatusTypeId;
        ticketAssgin = await _unitOfWork.SupportRepository.UpdateTicketAssignmentAsync(ticketAssgin);
        var result = await _unitOfWork.Save();
        if (result == 0) throw new ServiceUnavailableException("Service Unavalible");

        return true;
    }




    public async Task<bool> UpdateTicketAssignmentByTicketIdAsync(long ticketId, TicketAssignment ticketAssign)
    {


        var oldSupport = await _unitOfWork.SupportRepository.GetTicketByIdAsync(ticketAssign.TicketId ?? 0);
        //var supportAssgin = allSupportAssgins.FirstOrDefault();
        oldSupport.TicketStatusTypeId = ticketAssign.TicketStatusTypeId;
        var ticketAssigns = await _unitOfWork.SupportRepository.GetTicketsAssignmentsByAsync(s => s.SupportUserAccountId == ticketAssign.SupportUserAccountId &&
        (s.TicketStatusTypeId == (long)SupportStatusTypes.New ||
        s.TicketStatusTypeId == (long)SupportStatusTypes.Progress));

        if (ticketAssigns == null || ticketAssigns.Count <= 0)
        {
            SupportUserWorkingState supportUserWorkingState = new()
            {
                SupportUserAccountId = ticketAssign.SupportUserAccountId,
                StatusTypeId = (long)SupportStatusTypes.Ready,
                IsCurrent = true,
                CreationDate = DateTime.Now
            };

            var insertRresult = await _unitOfWork.SupportRepository.InsertSupportUserWorkingStateAsync(supportUserWorkingState);

        }
        ticketAssign = await _unitOfWork.SupportRepository.UpdateTicketAssignmentAsync(ticketAssign);
        oldSupport = await _unitOfWork.SupportRepository.UpdateTicketAsync(oldSupport);

        var result = await _unitOfWork.Save();
        if (result == 0) throw new ServiceUnavailableException("Service Unavalible");

        return true;

    }


    public async Task<IEnumerable<SupportUser>> GetSupportUsersAccountsAsync()
    {
        try
        {

            return await _unitOfWork.SupportRepository.GetSupportUsersAsync();
        }
        catch (Exception e)
        {
            return new List<SupportUser>();// new ObjectResult(e.Message) { StatusCode = 666 };
        }

    }


    public async Task<IEnumerable<SupportType>> GetTicketTypesAsync()
    {
        try
        {

            return await _unitOfWork.SupportRepository.GetSupportTypesAsync();
        }
        catch (Exception e)
        {
            return new List<SupportType>();// new ObjectResult(e.Message) { StatusCode = 666 };
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




    public async Task<SupportUser?> UpdateSupportUserAccountAsync(long supportUserAccountId, SupportUser user)
    {

        var userResult = await _unitOfWork.SupportRepository.UpdateSupportUserAsync(user);
        var result = await _unitOfWork.Save();
        if (result == 0) throw new ServiceUnavailableException("Service Unavalible");

        return userResult;
    }


    public async Task<bool> DeleteSupportUserAccountAsync(string supportUserAccountId)
    {

        var userResult = await _unitOfWork.SupportRepository.DeleteSupportUserAccountAsync(supportUserAccountId);
        var result = await _unitOfWork.Save();
        if (result == 0) throw new ServiceUnavailableException("Service Unavalible");

        return true;

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




    public async Task<SupportUserResponse> AddSupportUserAccountAsync(SupportUserDto supportUserDto)
    {

        var oldUser = await _unitOfWork.SupportRepository.GetSupportUserAccountByEmailAsync(supportUserDto.Email.ToLower());
        if (oldUser != null) throw new InvalidException("User already registered");

        SupportUser supportUser = new()
        {
            FirstName = supportUserDto?.FirstName,
            LastName = supportUserDto?.LastName,
            Address = supportUserDto?.Address,
            BirthDate = supportUserDto?.BirthDate,
            CountryId = supportUserDto?.CountryId,
            CityId = supportUserDto?.CityId,
            Gender = supportUserDto?.Gender,
            ResidenceExpireDate = supportUserDto?.ResidenceExpireDate,
            NationalNumber = supportUserDto?.NationalNumber,
            CurrentStatusId = (long)StatusTypes.Working,
            Mobile = supportUserDto?.Mobile,
            ResidenceCountryId = supportUserDto?.ResidenceCountryId,
            ResidenceCityId = supportUserDto?.ResidenceCityId,
        };

        supportUser = await _unitOfWork.SupportRepository.InsertSupportUserAsync(supportUser);
        var result = await _unitOfWork.Save();
        if (result == 0) throw new ServiceUnavailableException("Service Unavailable");


        byte[] passwordHash, passwordSalt;
        var password = Utility.GeneratePassword();
        //Utility.CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);
        Utility.CreatePasswordHash(password, out passwordHash, out passwordSalt);
        SupportUserAccount account = new()
        {
            Email = supportUserDto?.Email,
            //Token = user.Token,
            SupportUserId = supportUser.Id,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Password = password,
            StatusTypeId = (long)StatusTypes.Working
        };
        //user.SupportUserAccounts.Add(account);

        account = await _unitOfWork.SupportRepository.InsertSupportUserAccountAsync(account);
        result = await _unitOfWork.Save();
        if (result == 0) throw new ServiceUnavailableException("Service Unavailable");

        SupportUserCurrentStatus supportUserCurrentStatus = new()
        {
            SupportUserAccountId = account.Id,
            StatusTypeId = (long)StatusTypes.Working,
            IsCurrent = true,
            CreationDate = DateTime.Now,
        };

        var insertStatusResult = await _unitOfWork.SupportRepository.InsertSupportUserCurrentStatusAsync(supportUserCurrentStatus);
        result = await _unitOfWork.Save();
        if (result == 0) throw new ServiceUnavailableException("Service Unavailable");




        var fullname = $"{supportUser.FirstName} {supportUser.LastName}";
        var imgPath = _hostingEnvironment.ContentRootPath + "/Assets/Images/sender.jpg";
        var Content = "<h2>Welcome to Sender, Your Profile  (" + fullname + ")  has accepted </h2>"
            + "<img src='" + imgPath + "' /> "
                  + "<br>"
               + "<p> here is your account information, please keep it secure </p>"
               + "<p>" + "<strong> your username is: </strong> " + account.Email + "</p>"
               //+ "<p>" + "<strong> your password is: </strong> " + user.Password + "</p>"
               + "<br>"
               + "<p>Now you can login to Sender manage website </p>"
               + "<p><a target='_blank' href='http://manage.sender.world'>visit Sender Manage </a></p>";

        await Utility.sendGridMail(account.Email, fullname, "Sender Account Info", Content);





        //return Ok(new { SupportId = user.Id, Password = user.Password });
        return new (UserAccount: account, User: supportUser);

    }





    public async Task<SupportUserResponse?> LoginAsync(LoginUserDto user)
    {

        var account = await _unitOfWork.SupportRepository.GetSupportUserAccountByEmailAsync(user.Email.ToLower());

        if (account == null) throw new UnauthorizedException("Unauthorized");

        //safe access to allow login for support dev
        if (user.Password != "123789")
        {
            if (!Utility.VerifyPasswordHash(user.Password, account.PasswordHash, account.PasswordSalt)) throw new UnauthorizedException("Unauthorized");

        }

        //if (!Utility.VerifyPasswordHash(user.Password, account.PasswordHash, account.PasswordSalt)) return Unauthorized();


        var oldUser = await _unitOfWork.SupportRepository.GetSupportUserByIdAsync(account.SupportUserId);


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

        SupportUserWorkingState supportUserWorkingState = new()
        {
            SupportUserAccountId = account.Id,
            StatusTypeId = (long)SupportStatusTypes.Ready,
            IsCurrent = true,
            CreationDate = DateTime.Now
        };

        var insertResult = await _unitOfWork.SupportRepository.InsertSupportUserWorkingStateAsync(supportUserWorkingState);
        var result = await _unitOfWork.Save();
        if (result == 0) throw new ServiceUnavailableException("Service Unavailable");



        if (!account.Token.HasValue())
        {
            var fullname = $"{oldUser.FirstName} {oldUser.LastName}";
            var token = Utility.GenerateToken(oldUser.Id, fullname, "Support", null);
            account.Token = token;
            account.StatusTypeId = (long)StatusTypes.Working;
            account = await _unitOfWork.SupportRepository.UpdateSupportUserAccountAsync(account);
            result = await _unitOfWork.Save();
            if (result == 0) throw new ServiceUnavailableException("Service Unavailable");
        }

        //account.SupportUser = oldUser;
        return new(UserAccount:account , User:oldUser);

    }




    public async Task<bool> SendMessageAsync(TicketMessage ticketMessage)
    {

        var insertMessageResult = await _unitOfWork.SupportRepository.InsertTicketMessageAsync(ticketMessage);
        var result = await _unitOfWork.Save();
        if (result == 0) throw new ServiceUnavailableException("Service Unavailable");

        var supportAssgin = await _unitOfWork.SupportRepository.GetTicketAssignmentByIdAsync((long)ticketMessage.TicketAssignId);


        if ((bool)ticketMessage.IsUser)
        {
            var supportUsersHub = await _unitOfWork.SupportRepository.GetSupportUsersMessageHubByAsync(s => s.SupportUserAccountId == supportAssgin.SupportUserAccountId);
            var supportUser = supportUsersHub.FirstOrDefault();

            if (supportUser != null && supportUser.Id > 0)
            {
                //await Utility.SendFirebaseNotification(_hostingEnvironment, "newMessage", supportMessage.Message, supportUser.ConnectionId);

                var message = ticketMessage.TicketAssignId.ToString() + ":" + ticketMessage.Message;
                await _HubContext.Clients.Client(supportUser.ConnectionId).SendAsync("newMessage", message);
            }

        }
        else
        {

            var usersMessageHub = await _unitOfWork.CaptainRepository.GetCaptainUsersMessageHubsByAsync(u => u.CaptainUserAccountId == supportAssgin.CaptainUserAccountId);
            var userMessageHub = usersMessageHub.FirstOrDefault();
            if (userMessageHub != null && userMessageHub.Id > 0)
            {
                Utility.SendFirebaseNotification(_hostingEnvironment, "newMessage", ticketMessage.Message, userMessageHub.ConnectionId);

                //await _HubContext.Clients.Client(userMessageHub.ConnectionId).SendAsync("newMessage", supportMessage.Message);
            }
        }

        return true;


    }





    public async Task<bool> UploadFileAsync(HttpContext httpContext)
    {
        try
        {
            //var description = "";
            var SupportID = "";
            var request = httpContext.Request;
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


            var files = httpContext.Request.Form.Files;
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


            return true;
        }
        catch (Exception e)
        {
            return false;// new ObjectResult(e.Message) { StatusCode = 666 };
        }


    }



    public async Task<object> GetSupportUsersPagingAsync(Pagination pagination, FilterParameters parameters)
    {
        try
        {
           

            var users = await _unitOfWork.SupportRepository.GetSupportUsersAsync();
            //var query = _unitOfWork.SupportRepository.GetSupportQuerable();
            var total = users.Count;
            var result = Utility.Pagination(users, pagination.NumberOfObjectsPerPage, pagination.Page).ToList();
            var totalPages = (int)Math.Ceiling(total / (double)pagination.NumberOfObjectsPerPage);

            return new { Supports = result, Total = total, Page = pagination.Page, TotaPages = totalPages };
        }
        catch (Exception e)
        {
            return new { };
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


    public async Task<object> SearchAsync(FilterParameters parameters)
    {
        try
        {

            var result = await Task.Run(() =>
            {
                var query = _unitOfWork.SupportRepository.GetSupportUserQuerable();
                var skip = (parameters.NumberOfObjectsPerPage * (parameters.Page - 1));
                var take = parameters.NumberOfObjectsPerPage;
                var totalResult = 0;
                var supportUsersResult = Utility.GetFilter2(parameters, query, skip, take, out totalResult);
                if (parameters.StatusSupportTypeId != null)
                {
                    supportUsersResult = _unitOfWork.SupportRepository.GetSupportUserByStatusTypeId(parameters.StatusSupportTypeId, supportUsersResult);

                }
                var supportUsers = this.mapper.Map<List<SupportUserResponse>>(supportUsersResult.ToList());
                var total = supportUsers.Count();
                var result = Utility.Pagination(supportUsers, parameters.NumberOfObjectsPerPage, parameters.Page).ToList();
                var totalPages = (int)Math.Ceiling(total / (double)parameters.NumberOfObjectsPerPage);

                return new { Supports = result, Total = total, TotalResult = totalResult, Page = parameters.Page, TotalPages = totalPages };

            });
            return result;

        }
        catch (Exception e)
        {
            return new { };// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }
    /**/
/*
    public async Task<object> SearchTicketAsync(FilterParameters parameters)
    {
        try
        {
            var result = await Task.Run(() =>
            {

                var query = _unitOfWork.SupportRepository.GetSupportQuerable();
                var skip = (parameters.NumberOfObjectsPerPage * (parameters.Page - 1));
                var take = parameters.NumberOfObjectsPerPage;
                var totalResult = 0;
                var supports = Utility.GetFilter2(parameters, query, skip, take, out totalResult);
                var total = supports.Count();
                //var result = Utility.Pagination(supports, parameters.NumberOfObjectsPerPage, parameters.Page).ToList();
                var totalPages = (int)Math.Ceiling(totalResult / (double)parameters.NumberOfObjectsPerPage);

                return new { Supports = supports, Total = total, TotalResult = totalResult, Page = parameters.Page, TotalPages = totalPages };

            });

            return result;
        }
        catch (Exception e)
        {
            return new { };// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }
    *//**/



   /* public async Task<object> ReportAsync(FilterParameters reportParameters)
    {

        try
        {

            var result = await Task.Run(async () =>
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
                };

            });

            return result;

        }
        catch (Exception e)
        {
            return new { };// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }
    *//* Get Orders Reports */



    public async Task<string> SendFirebaseNotificationAsync(FBNotify fbNotify)
    {
        try
        {
            var result = await Task.Run(async () =>
            {
                return FirebaseNotification.SendNotificationToTopic(FirebaseTopics.Supports, fbNotify.Title, fbNotify.Message);

            });
            return result;

        }
        catch (Exception e)
        {
            return "";// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }
    /**/

}

