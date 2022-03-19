using Microsoft.AspNetCore.Mvc;
using TreePorts.DTO;
using TreePorts.DTO.Records;

namespace TreePorts.Controllers
{
    //[Authorize]
    /*[Route("[controller]")]*/
    [Route("/Admins/")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }



        // GET: Admin
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AdminUser>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAdminsUsers(CancellationToken cancellationToken)
        {

            try
            {
                return Ok(await _adminService.GetAdminsUsersAsync(cancellationToken));
                
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
        public async Task<IActionResult> GetAdminUser(string? id, CancellationToken cancellationToken)
        {
            try
            {
                
                return Ok(await _adminService.GetAdminUserByIdAsync(id ?? "",cancellationToken));

            }
            catch (Exception e)
            {
                return NoContent(); // new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }




        [HttpGet("Paging")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AdminUser>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AdminsUsersPagination([FromQuery] FilterParameters parameters, CancellationToken cancellationToken)//[FromQuery] FilterParameters parameters)
        {
            try
            {
                return Ok(await _adminService.GetAdminsUsersPaginationAsync(parameters,cancellationToken));
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
        public async Task<IActionResult> Post([FromBody] AdminUserDto user, CancellationToken cancellationToken)
        {
            try {
                
                return Ok(await _adminService.AddAdminUserAsync(user,cancellationToken));
            } catch (Exception e) {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }



      

        // PUT: Admin/AdminUserDate
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AdminUser))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> Put(string? id , [FromBody] AdminUserDto user, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _adminService.UpdateAdminUserAsync(id,user,cancellationToken));
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
        public async Task<IActionResult> Delete(string? id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _adminService.DeleteAdminUserAsync(id, cancellationToken));
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
        public async Task<IActionResult> Login([FromBody] LoginUserDto user, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _adminService.Login(user, cancellationToken));
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
        public async Task<IActionResult> Upload(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _adminService.UploadFileAsync(HttpContext, cancellationToken));
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
        public async Task<IActionResult> Search([FromQuery] FilterParameters parameters, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _adminService.SearchAsync(parameters, cancellationToken)); 
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }
        /**/




        /* Get Orders  Report*//*
        [HttpGet("Reports")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Report([FromQuery] FilterParameters reportParameters)
        {
            try
            {

                return Ok(await _adminService.ReportAsync(reportParameters));
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
        public async Task<IActionResult> SendFirebaseNotification([FromQuery] FBNotify fbNotify, CancellationToken cancellationToken)
        {
            try
            {
                 return Ok(await _adminService.SendFirebaseNotification(fbNotify, cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }
        /**/


    }
}
