using Microsoft.AspNetCore.Mvc;
using TreePorts.DTO;

namespace TreePorts.Controllers
{
    //[Authorize]
    /*[Route("[controller]")]*/
    [Route("/Agents/")]
    [ApiController]
    public class AgentController : ControllerBase
    {

        private readonly IAgentService _agentService;
        public AgentController(IAgentService agentService)
        {
            _agentService = agentService;
        }

        // GET: Agent/5
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Agent>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _agentService.GetWorkingAgentsAsync());

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
                return Ok(await _agentService.GetActiveAgentsAsync());
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
                return Ok(await _agentService.GetAgentByIdAsync(id));

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
               return Ok(await _agentService.GetAgentTypesAsync());
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
                return Ok(await _agentService.GetAgentsPagingAsync(parameters));
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
                return Ok(await _agentService.GetNewRegisteredAgentsAsync());
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
                return Ok(await _agentService.GetNewRegisteredAgentsPagingAsync(parameters));
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
                return Ok(await _agentService.AddAgentAsync(agent));
            }
            catch (Exception e)
            {
                return NoContent(); // new ObjectResult(e.Message) { StatusCode = 666 };
            }
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
                return Ok(await _agentService.AcceptRegisterAgentAsync(id));
            }
            catch (Exception e)
            {
                return NoContent(); // new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }





        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Agent))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put(long? id ,[FromBody] Agent agent)
        {
            try
            {
                return Ok(await _agentService.UpdateAgentAsync(id,agent));
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
                return Ok(await _agentService.DeleteAgentAsync(id));
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

                return Ok(await _agentService.LoginAsync(user));
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

                return Ok(await _agentService.UpdateAgentLoactionAsync(id,agent));
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
                return Ok(await _agentService.UploadFileAsync(HttpContext));
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
                return Ok(await _agentService.ReportAsync(HttpContext,reportParameters));
                
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

                return Ok(await _agentService.SearchAsync(parameters));
                
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
				
                return Ok(await _agentService.CreateAgentCouponAsync(agentCouponDto));
                
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
                
                return Ok(await _agentService.AssignExistingCouponAsync(assignCouponDto));
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
                return Ok(await _agentService.ChartAsync());
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

                
                return Ok(await _agentService.CheckCouponAsync(id,couponCode, countryId));
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
                return Ok(await _agentService.GetAgentsDeliveryPricesAsync());
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
                return Ok(await _agentService.GetAgentDeliveryPriceByIdAsync(id));
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
                 return Ok(await _agentService.GetAgentDeliveryPricesByAgentId(id));
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
                return Ok(await _agentService.AddAgentDeliveryPriceAsync(agentDeliveryPrice));
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
                return Ok(await _agentService.DeleteDeliveryPriceAsync(id));
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
                return Ok(await _agentService.SendFirebaseNotificationAsync(fbNotify));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }
        /**/


    }
}