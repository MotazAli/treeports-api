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
using TreePorts.DTO;
using TreePorts.DTO.Records;
using TreePorts.Utilities;

namespace TreePorts.Controllers
{
    //[Authorize]
    /*[Route("[controller]")]*/
    [Route("/Captains/")]
    [ApiController]
    public class CaptainController : ControllerBase
    {

        private readonly ICaptainService _captainService;
        public CaptainController(ICaptainService captainService)
        {
            _captainService = captainService;
        }




        // GET: Driver
        //[AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CaptainUser>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                return Ok(await _captainService.GetCaptainUsersAsync());
            }
            catch (Exception e)
            {
                return NoContent();
            }
        }

        // GET: Driver/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CaptainUser))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetUserByIdAsync(string? id)
        {
            try {
                
                return Ok(await _captainService.GetCaptainUserAccountByCaptainUserAccountIdAsync(id ?? ""));

            } catch (Exception e) {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }


        [HttpGet("Paging")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CaptainUserAccount>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UsersPaging([FromQuery] FilterParameters parameters)
        {
            try
            {
                return Ok(await _captainService.GetUsersPagingAsync(parameters));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }



        //[HttpPost("GetNewDriverUsers")]
        [HttpGet("New/Paging")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CaptainUserAccount>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetNewCaptainsUsersAsync([FromQuery] FilterParameters parameters)//[FromBody] Pagination pagination, [FromQuery] FilterParameters parameters)
        {
            try
            {
                return Ok(await _captainService.GetNewCaptainsUsersAsync(parameters));
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
                return Ok(await _captainService.GetDirectionsMapAsync(origin, destination, mode));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }


        }


        // POST: Driver
        //[AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddCaptain([FromBody] CaptainUserDto user)
        {
            try
            {
                return Ok(await _captainService.AddCaptainAsync(HttpContext,user));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }



        }




        //[AllowAnonymous]
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Login([FromBody] LoginCaptainUserDto captain)
        {
            try
            {
                return Ok(await _captainService.LoginAsync(captain)) ;
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

                return Ok(await _captainService.ChangePasswordAsync(driver));

            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }


        }



        // POST: Driver
        //[AllowAnonymous]
        [HttpPost("Upload")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Upload()
        {
            try
            {
                
                return Ok(await _captainService.UploadAsync(HttpContext));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }


        }




        //[HttpGet("AcceptRegisteredDriver/{id}")]
        [HttpGet("{id}/Accept")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AcceptRegisterCaptainById(string? id)
        {

            try
            {

                return Ok(await _captainService.AcceptRegisterCaptainByIdAsync(id ?? "",HttpContext));
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
        public async Task<IActionResult> UpdateCaptain(string? id,[FromBody] CaptainUserDto user)
        {
            try
            {
                return Ok(await _captainService.UpdateCaptainUserAsync(id,user));
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
        public async Task<IActionResult> UpdateCurrentLocation([FromBody] CaptainUserCurrentLocation userCurrentLocation)
        {
            try
            {
                return Ok(await _captainService.UpdateCaptainCurrentLocationAsync(userCurrentLocation));
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
        public async Task<IActionResult> Delete(string? id)
        {

            try
            {
                return Ok(await _captainService.DeleteCaptainUserAccountAsync(id));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }



        // GET: Driver/5
        //[HttpGet("GetAllOrdersPayments/{id}")]
        [HttpGet("{id}/Payments")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetOrdersPaymentsByUserId(string? id)
        {
            try
            {
                return Ok(await _captainService.GetOrdersPaymentsByCaptainUserAccountIdAsync(id));

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
        public async Task<IActionResult> GetBookkeepingPagingByCaptainId(string? id, [FromQuery] FilterParameters parameters)// , [FromBody] Pagination pagination)
        {
            try
            {

                return Ok(await _captainService.GetBookkeepingPagingByCaptainUserAccountIdAsync(id,parameters));

            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }


        [HttpPost("{id}/Bookkeeping")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Bookkeeping>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetBookkeepingByCaptainId(string? id)
        {
            try
            {

                return Ok(await _captainService.GetBookkeepingByCaptainUserAccountIdAsync(id));

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
        public async Task<IActionResult> GetUntransferredBookkeepingByCaptainId(string? id)
        {
            try
            {
                return Ok(await _captainService.GetUntransferredBookkeepingByCaptainUserAccountIdAsync(id));

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
        public async Task<IActionResult> GetAllOrdersAssignments(string? id, [FromQuery] FilterParameters parameters)//[FromBody] Pagination pagination)
        {
            try
            {
                return Ok(await _captainService.GetAllOrdersAssignmentsByCaptainUserAccountIdAsync(id,parameters));

            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }

        //[HttpPost("AddUserShift")]
        [HttpPost("Shifts")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CaptainUserShift))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddShift([FromBody] CaptainUserShift userShift)
        {
            try
            {
                return Ok(await _captainService.AddCaptainUserShiftAsync(userShift));

            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        // DELETE: ApiWithActions/5
        //[HttpPost("DeleteUserShift")]
        [HttpDelete("{id}/Shifts/{shiftId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CaptainUserShift))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUserShift([FromRoute(Name = "id")] string? userId, [FromRoute(Name = "shiftId")] long shiftId)
        {

            try
            {
                return Ok(await _captainService.DeleteCaptainUserShiftAsync(userId,shiftId));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }


        //[HttpPost("GetUsershift")]
        [HttpGet("{id}/Shifts/{shiftId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CaptainUserShift))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetUsershift( [FromRoute(Name ="id")] string? userId , [FromRoute(Name = "shiftId")] long shiftId)//[FromBody] UserShift userShift)
        {

            try
            {
                return Ok( await _captainService.GetCaptainUsershiftAsync(userId,shiftId) );
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
        public async Task<IActionResult> GetShiftsAndUserShiftsByDate(string? id ,[FromBody] Shift shift)
        {
            try
            {
                return Ok(await _captainService.GetShiftsAndUserShiftsByDateAsync(id,shift));
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CaptainUserActivity))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> CaptainActivities([FromBody] CaptainUserActivity userActivity)
        {
            try
            {
                return Ok(await _captainService.CaptainUserActivitiesAsync(userActivity));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        /* Get Orders  Report*//*
        [HttpGet("Reports")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Reports([FromQuery] FilterParameters reportParameters)
        {
            try
            {
                return Ok(await _captainService.ReportsAsync(reportParameters,HttpContext));

            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }  
          
        }
        *//* Get Orders Reports */
        
        
        
        
        
        /* Search *//*
        [HttpGet("Search")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Search([FromQuery] FilterParameters parameters)
        {
            try
            {
                return Ok(await _captainService.SearchAsync(parameters));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }
        *//**/

        /*Charts*/
        [HttpGet("Charts")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Charts()
        {
            try
            {
                return Ok(await _captainService.ChartsAsync());
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
                return Ok( await _captainService.CheckBonusPerMonthAsync(bonusCheckDto));
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
                return Ok(await _captainService.CheckBonusPerYearAsync(bonusCheckDto));
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
        public async Task<IActionResult> SendFirebaseNotification([FromBody] FBNotify fbNotify)
        {
            try
            {
                return Ok(await _captainService.SendFirebaseNotificationAsync(fbNotify));
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CaptainUser>))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> NearToLocation([FromBody] Location location)
        {
            try
            {
                return Ok(await _captainService.GetCaptainsUsersNearToLocationAsync(location));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }
        /**/
        
        
        

    }
}
