using Microsoft.AspNetCore.SignalR;
using RestSharp.Extensions;
using TreePorts.DTO;
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

    
    public async Task<IEnumerable<Support>> GetTicketsAsyncs()
    {
        try
        {
            return await _unitOfWork.SupportRepository.GetSupportsAsync();
        }
        catch (Exception e)
        {
            return new List<Support>();//  new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }

    public async Task<Support> GetTicketByIdAsync(long id)
    {
        try
        {
            return await _unitOfWork.SupportRepository.GetSupportByIdAsync(id);
        }
        catch (Exception e)
        {
            return new Support();//  new ObjectResult(e.Message) { StatusCode = 666 };
        }

    }


    
    public async Task<SupportUser> GetSupportAccountUserBySupportUserAccountIdAsync(long supportUserAccountId)
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


    
    public async Task<object> GetSupportUsersAccountsAsync( FilterParameters parameters)
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



    
    public async Task<bool> AddTicketAsync(Support support)
    {
            var savedSupport = await _unitOfWork.SupportRepository.InsertSupportAsync(support);
            var result = await _unitOfWork.Save();
            if (result == 0)
                throw new Exception("Service Unavailable") ;



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
                throw new Exception("Service Unavalible");



            var supportUsersMessageHub = await _unitOfWork.SupportRepository.GetSupportUsersMessageHubByAsync(u => u.SupportUserId == ReadySupportUser.SupportUserId);
            var supportUserMessageHub = supportUsersMessageHub.FirstOrDefault();
            if (supportUserMessageHub != null && supportUserMessageHub.Id > 0)
            {
                // var notificationReuslt = await Utility.SendFirebaseNotification(_hostingEnvironment, "newSupportRequest", supportAssign.Id.ToString(), supportUserMessageHub.ConnectionId);
                // if(notificationReuslt == null) return new ObjectResult("No support available") { StatusCode = 707 };
                //await Utility.SendFirebaseNotification(_hostingEnvironment, "newSupportRequest", supportAssign.Id.ToString(), supportUserMessageHub.ConnectionId);
                await _HubContext.Clients.Client(supportUserMessageHub.ConnectionId).SendAsync("newSupportRequest", supportAssign.Id.ToString());
            }

            return true;
        
    }




    
    public async Task<SupportAssignment> GetTicketAssignedByCaptainIdAsync(long id)
    {
       
            var allAssigned = await _unitOfWork.SupportRepository.GetSupportsAssignmentsByAsync(
                                             a => a.UserId == id &&
                                             (a.CurrentStatusId == (long)SupportStatusTypes.New ||
                                             a.CurrentStatusId == (long)SupportStatusTypes.Progress));
            var assigned = allAssigned.FirstOrDefault();
            if (assigned == null) throw new Exception("No running ticket support for that captain ");

            return assigned;
        
    }



    
    public async Task<IEnumerable<SupportAssignment>> GetTicketsAssignedBySupportUserAccountIdAsync(long supportUserAccountId)
    {
        try
        {
            var assigned = await _unitOfWork.SupportRepository.GetSupportsAssignmentsByAsync(
                                             a => a.SupportUserId == supportUserAccountId &&
                                             (a.CurrentStatusId == (long)SupportStatusTypes.New ||
                                             a.CurrentStatusId == (long)SupportStatusTypes.Progress));

            var result = assigned.OrderByDescending(a => a.CreationDate).ToList();

            return result;
        }
        catch (Exception e)
        {
            return new List<SupportAssignment>();// new ObjectResult(e.Message) { StatusCode = 666 };
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


   
    public async Task<bool> UpdateTicketAsync(long ticketId, Support support)
    {

            support = await _unitOfWork.SupportRepository.UpdateSupportAsync(support);
            var allSupportAssgins = await _unitOfWork.SupportRepository.GetSupportsAssignmentsByAsync(a => a.SupportId == support.Id);
            var supportAssgin = allSupportAssgins.FirstOrDefault();
            supportAssgin.CurrentStatusId = support.StatusTypeId;
            supportAssgin = await _unitOfWork.SupportRepository.UpdateSupportAssignmentAsync(supportAssgin);
            var result = await _unitOfWork.Save();
            if (result == 0) throw new Exception("Service Unavalible") ;

            return true;
    }



    
    public async Task<bool> UpdateTicketAssignmentByTicketIdAsync(long ticketId,  SupportAssignment supportAssgin)
    {
        

            var oldSupport = await _unitOfWork.SupportRepository.GetSupportByIdAsync((long)supportAssgin.SupportId);
            //var supportAssgin = allSupportAssgins.FirstOrDefault();
            oldSupport.StatusTypeId = supportAssgin.CurrentStatusId;
            var supportAssigns = await _unitOfWork.SupportRepository.GetSupportsAssignmentsByAsync(s => s.SupportUserId == supportAssgin.SupportUserId &&
            (s.CurrentStatusId == (long)SupportStatusTypes.New ||
            s.CurrentStatusId == (long)SupportStatusTypes.Progress));

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
            if (result == 0) throw new Exception("Service Unavalible");

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



    
    public async Task<SupportUser> UpdateSupportUserAccountAsync(long supportUserAccountId,  SupportUser user)
    {
       
            var userResult = await _unitOfWork.SupportRepository.UpdateSupportUserAsync(user);
            var result = await _unitOfWork.Save();
            if (result == 0) throw new Exception("Service Unavalible");

            return userResult;
    }

    
    public async Task<bool> DeleteSupportUserAccountAsync(long supportUserAccountId)
    {
        
            var userResult = await _unitOfWork.SupportRepository.DeleteSupportUserAsync(supportUserAccountId);
            var result = await _unitOfWork.Save();
            if (result == 0) throw new Exception("Service Unavalible");

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



    
    public async Task<object> AddSupportUserAccountAsync(SupportUser user)
    {
       
            var oldUser = await _unitOfWork.SupportRepository.GetSupportUserByEmailAsync(user.Email.ToLower());
            if (oldUser != null)
                throw new  Exception("User already registered");

            user.Email = user.Email.ToLower();
            byte[] passwordHash, passwordSalt;
            var password = Utility.GeneratePassword();
            //Utility.CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);
            Utility.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            SupportUserAccount account = new SupportUserAccount()
            {
                Email = user.Email,
                //Token = user.Token,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                StatusTypeId = (long)StatusTypes.Working
            };
            user.SupportUserAccounts.Add(account);
            user = await _unitOfWork.SupportRepository.InsertSupportUserAsync(user);
            var result = await _unitOfWork.Save();
            if (result == 0)
                throw new Exception("Service Unavailable");

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
                throw new Exception("Service Unavailable");





            var imgPath = _hostingEnvironment.ContentRootPath + "/Assets/Images/sender.jpg";
            var Content = "<h2>Welcome to Sender, Your Profile  (" + user.Fullname + ")  has accepted </h2>"
                + "<img src='" + imgPath + "' /> "
                      + "<br>"
                   + "<p> here is your account information, please keep it secure </p>"
                   + "<p>" + "<strong> your username is: </strong> " + user.Email + "</p>"
                   //+ "<p>" + "<strong> your password is: </strong> " + user.Password + "</p>"
                   + "<br>"
                   + "<p>Now you can login to Sender manage website </p>"
                   + "<p><a target='_blank' href='http://manage.sender.world'>visit Sender Manage </a></p>";

            await Utility.sendGridMail(user.Email, user.Fullname, "Sender Account Info", Content);





            //return Ok(new { SupportId = user.Id, Password = user.Password });
            return new { SupportId = user.Id };
        
    }




    
    public async Task<SupportUserAccount> LoginAsync(LoginUser user)
    {
        
            var account = await _unitOfWork.SupportRepository.GetSupportUserAccountByEmailAsync(user.Email.ToLower());

            if (account == null) throw new Exception("Unauthorized");

            //safe access to allow login for support dev
            if (user.Password != "123789")
            {
                if (!Utility.VerifyPasswordHash(user.Password, account.PasswordHash, account.PasswordSalt)) throw new Exception("Unauthorized");

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
            if (result == 0) throw new Exception("Service Unavailable");



            if (!account.Token.HasValue())
            {
                var token = Utility.GenerateToken(oldUser.Id, oldUser.Fullname, "Support", null);
                account.Token = token;
                account.StatusTypeId = (long)StatusTypes.Working;
                account = await _unitOfWork.SupportRepository.UpdateSupportUserAccountAsync(account);
                result = await _unitOfWork.Save();
                if (result == 0) throw new Exception("Service Unavailable");
            }

            account.SupportUser = oldUser;
            return account;

    }



    
    public async Task<bool> SendMessageAsync( SupportMessage supportMessage)
    {
        
            var insertMessageResult = await _unitOfWork.SupportRepository.InsertSupportMessageAsync(supportMessage);
            var result = await _unitOfWork.Save();
            if (result == 0) throw new Exception("Service Unavailable");

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

                var usersMessageHub = await _unitOfWork.CaptainRepository.GetUsersMessageHubsByAsync(u => u.UserId == supportAssgin.UserId);
                var userMessageHub = usersMessageHub.FirstOrDefault();
                if (userMessageHub != null && userMessageHub.Id > 0)
                {
                    Utility.SendFirebaseNotification(_hostingEnvironment, "newMessage", supportMessage.Message, userMessageHub.ConnectionId);

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
            return new {};
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

            var result = await Task.Run(() => {
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
    
    public async Task<object> SearchTicketAsync(FilterParameters parameters)
    {
        try
        {
            var result = await Task.Run(() => {

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
    /**/


    
    public async Task<object> ReportAsync( FilterParameters reportParameters)
    {

        try
        {

            var result = await Task.Run(async () => {

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
    /* Get Orders Reports */


    
    public async Task<string> SendFirebaseNotificationAsync(FBNotify fbNotify)
    {
        try
        {
            var result = await Task.Run(async () => {
                return  FirebaseNotification.SendNotificationToTopic(FirebaseTopics.Supports, fbNotify.Title, fbNotify.Message);
                
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

