using Microsoft.AspNetCore.Mvc;
using TreePorts.DTO;
using TreePorts.DTO.Records;

namespace TreePorts.Controllers
{
    //[Authorize]
    //[Route("[controller]")]
    [Route("/Supports/")]
    [ApiController]
    public class SupportController : ControllerBase
    {
        private readonly ISupportService _supportService;
        public SupportController(ISupportService supportService)
        {
            _supportService = supportService;
        }

        // GET: Support
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Ticket>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetTicketsAsync(CancellationToken cancellationToken)
        {
            try
            { 
                return Ok(await _supportService.GetTicketsAsyncs(cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();//  new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }

        // GET: Support/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Ticket))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetTicketByIdAsync(long id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _supportService.GetTicketByIdAsync(id, cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();//  new ObjectResult(e.Message) { StatusCode = 666 };
            }
          
        }


        [HttpGet("Users/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SupportUser))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetSupportUserByUserId(string? id, CancellationToken cancellationToken)
        {


            try
            {
                return Ok(await _supportService.GetSupportAccountUserBySupportUserAccountIdAsync(id ?? "",cancellationToken));
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
        public async Task<IActionResult> Users( [FromQuery] FilterParameters parameters, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _supportService.GetSupportUsersAccountsAsync(parameters,cancellationToken));
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
        public async Task<IActionResult> AddTicketAsync([FromBody] Ticket ticket, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _supportService.AddTicketAsync(ticket,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }




        //[HttpGet("GetDriverSupportAssigned/{id}")]
        [HttpGet("Assignments/Captains/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TicketAssignment))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetTicketAssignedByCaptainIdAsync(string? id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _supportService.GetTicketAssignedByCaptainUserAccountIdAsync(id ?? "",cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }



        //[HttpGet("GetAllSupportAssigned/{id}")]
        [HttpGet("Assignments/Users/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TicketAssignment>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetTicketsAssignedBySupportUserAccountIdAsync(string? id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _supportService.GetTicketsAssignedBySupportUserAccountIdAsync(id??"",cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        //[HttpGet("GetAllSupportAssigned/{id}")]
        [HttpGet("Users/{id}/Assignments")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TicketAssignment>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllTicketAssignedAsync(string? id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _supportService.GetTicketsAssignedBySupportUserAccountIdAsync(id??"",cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }



        // PUT: Support/SupportDate
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateTicketAsync( long? id ,[FromBody] Ticket ticket, CancellationToken cancellationToken)
        {
            try
            {

                return Ok(await _supportService.UpdateTicketAsync((long)id, ticket,cancellationToken));
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
        public async Task<IActionResult> UpdateTicketAssignmentByTicketIdAsync(long id ,[FromBody] TicketAssignment ticketAssgin, CancellationToken cancellationToken)
        {
            try
            {

                return Ok(await _supportService.UpdateTicketAssignmentByTicketIdAsync(id, ticketAssgin,cancellationToken));
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
        public async Task<IActionResult> GetSupportUsersAccountsAsync(CancellationToken cancellationToken)
        {
            try
            {

                return Ok(await _supportService.GetSupportUsersAccountsAsync(cancellationToken));
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
        public async Task<IActionResult> GetTicketTypesAsync(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _supportService.GetTicketTypesAsync(cancellationToken));
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
        public async Task<IActionResult> UpdateSupportUser( long id , [FromBody] SupportUser user, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _supportService.UpdateSupportUserAccountAsync(id,user,cancellationToken));
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
        public async Task<IActionResult> DeleteSupportUser(string? id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _supportService.DeleteSupportUserAccountAsync(id??"",cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }

        


        // POST: Support/InsertUser
        //[HttpPost("InsertUser")]
        [HttpPost("Users")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddSupportUser([FromBody] SupportUserDto user, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _supportService.AddSupportUserAccountAsync(user,cancellationToken));
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
        public async Task<IActionResult> Login([FromBody] LoginUserDto user, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _supportService.LoginAsync(user,cancellationToken));
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
        public async Task<IActionResult> SendMessage([FromBody] TicketMessage ticketMessage, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _supportService.SendMessageAsync(ticketMessage,cancellationToken));

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
        public async Task<IActionResult> Upload(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _supportService.UploadFileAsync(HttpContext, cancellationToken));
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
        public async Task<IActionResult> GetSupportUsersPaging([FromBody] Pagination pagination, [FromQuery] FilterParameters parameters, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _supportService.GetSupportUsersPagingAsync(pagination,parameters,cancellationToken));
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
        public async Task<IActionResult> Search([FromQuery] FilterParameters parameters, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _supportService.SearchAsync(parameters, cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }
        /**/
        /* Search *//*
        [HttpGet("SearchSupport")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> SearchTicket([FromQuery] FilterParameters parameters)
        {
            try
            {
                return Ok(await _supportService.SearchTicketAsync(parameters));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }
        *//**/
        
        
        /* Get Orders  Report*//*
        [HttpGet("Report")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Report([FromQuery] FilterParameters reportParameters)
        {

            try
            {
                return Ok(await _supportService.ReportAsync(reportParameters));

            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }    
        }
        *//* Get Orders Reports */


        /* SendFirebaseNotification*/
        [HttpPost("SendFirebaseNotification")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> SendFirebaseNotification([FromBody] FBNotify fbNotify, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _supportService.SendFirebaseNotificationAsync(fbNotify,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }
        /**/


    }
}
