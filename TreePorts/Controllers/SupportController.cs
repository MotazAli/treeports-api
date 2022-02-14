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
        private readonly ISupportService _supportService;
        public SupportController(ISupportService supportService)
        {
            _supportService = supportService;
        }

        // GET: Support
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Support>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetTicketsAsync()
        {
            try
            { 
                return Ok(await _supportService.GetTicketsAsyncs());
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
        public async Task<IActionResult> GetTicketByIdAsync(long id)
        {
            try
            {
                return Ok(await _supportService.GetTicketByIdAsync(id));
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
                return Ok(await _supportService.GetSupportAccountUserBySupportUserAccountIdAsync(id));
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
                return Ok(await _supportService.GetSupportUsersAccountsAsync(parameters));
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
        public async Task<IActionResult> AddTicketAsync([FromBody] Support support)
        {
            try
            {
                return Ok(await _supportService.AddTicketAsync(support));
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
        public async Task<IActionResult> GetTicketAssignedByCaptainIdAsync(long id)
        {
            try
            {
                return Ok(await _supportService.GetTicketAssignedByCaptainIdAsync(id));
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
        public async Task<IActionResult> GetTicketsAssignedBySupportUserAccountIdAsync(long id)
        {
            try
            {
                return Ok(await _supportService.GetTicketsAssignedBySupportUserAccountIdAsync(id));
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
                return Ok(await _supportService.GetTicketsAssignedBySupportUserAccountIdAsync(id));
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
        public async Task<IActionResult> PutAsync( long? id ,[FromBody] Support support)
        {
            try
            {

                return Ok(await _supportService.UpdateTicketAsync((long)id,support));
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
        public async Task<IActionResult> UpdateTicketAssignmentByTicketIdAsync(long id ,[FromBody] SupportAssignment supportAssgin)
        {
            try
            {

                return Ok(await _supportService.UpdateTicketAssignmentByTicketIdAsync(id,supportAssgin));
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
        public async Task<IActionResult> GetSupportUsersAccountsAsync()
        {
            try
            {

                return Ok(await _supportService.GetSupportUsersAccountsAsync());
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
        public async Task<IActionResult> GetTicketTypesAsync()
        {
            try
            {
                return Ok(await _supportService.GetTicketTypesAsync());
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
                return Ok(await _supportService.UpdateSupportUserAccountAsync(id,user));
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
                return Ok(await _supportService.DeleteSupportUserAccountAsync(id));
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
        public async Task<IActionResult> AddSupportUser([FromBody] SupportUser user)
        {
            try
            {
                return Ok(await _supportService.AddSupportUserAccountAsync(user));
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
                return Ok(await _supportService.LoginAsync(user));
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
                return Ok(await _supportService.SendMessageAsync(supportMessage));

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
                return Ok(await _supportService.UploadFileAsync(HttpContext));
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
                return Ok(await _supportService.GetSupportUsersPagingAsync(pagination,parameters));
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
                return Ok(await _supportService.SearchAsync(parameters));
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
        /**/
        
        
        /* Get Orders  Report*/
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
        /* Get Orders Reports */


        /* SendFirebaseNotification*/
        [HttpPost("SendFirebaseNotification")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> SendFirebaseNotification([FromBody] FBNotify fbNotify)
        {
            try
            {
                return Ok(await _supportService.SendFirebaseNotificationAsync(fbNotify));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }
        /**/


    }
}
