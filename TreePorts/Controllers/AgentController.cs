using Microsoft.AspNetCore.Mvc;
using TreePorts.DTO;
using TreePorts.DTO.Records;

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
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _agentService.GetWorkingAgentsAsync(cancellationToken));

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
        public async Task<IActionResult> Active(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _agentService.GetActiveAgentsAsync(cancellationToken));
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
        public async Task<IActionResult> Get(string? id, CancellationToken cancellationToken)
        {
            try
            {  
                return Ok(await _agentService.GetAgentByIdAsync(id?? "",cancellationToken));

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
        public async Task<IActionResult> GetAgentTypes(CancellationToken cancellationToken)
        {
            try
            {
               return Ok(await _agentService.GetAgentTypesAsync(cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent(); // new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        [HttpPost("Paging")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Agent>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAgentsPaging( [FromQuery] FilterParameters parameters, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _agentService.GetAgentsPagingAsync(parameters,cancellationToken));
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
        public async Task<IActionResult> GetNewRegisteredAgents(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _agentService.GetNewRegisteredAgentsAsync(cancellationToken));
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
        public async Task<IActionResult> GetNewRegisteredAgentsPaging( [FromQuery] FilterParameters parameters, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _agentService.GetNewRegisteredAgentsPagingAsync(parameters, cancellationToken));
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
        public async Task<IActionResult> Post([FromBody] AgentDto agent, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _agentService.AddAgentAsync(agent,cancellationToken));
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
        public async Task<IActionResult> AcceptRegisterAgent(string? id, CancellationToken cancellationToken)
         {

            try
            {
                return Ok(await _agentService.AcceptRegisterAgentAsync(id?? "", cancellationToken));
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
        public async Task<IActionResult> Put(string? id ,[FromBody] AgentDto agent, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _agentService.UpdateAgentAsync(id,agent,cancellationToken));
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
        public async Task<IActionResult> Delete(string? id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _agentService.DeleteAgentAsync(id ?? "", cancellationToken));
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
        public async Task<IActionResult> Login([FromBody] LoginUserDto user, CancellationToken cancellationToken)
        {


            try
            {

                return Ok(await _agentService.LoginAsync(user,cancellationToken));
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
        public async Task<IActionResult> UpdateLoaction(long id ,[FromBody] Agent agent, CancellationToken cancellationToken)
        {
            try
            {

                return Ok(await _agentService.UpdateAgentLoactionAsync(id,agent,cancellationToken));
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
        public async Task<IActionResult> Upload(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _agentService.UploadFileAsync(HttpContext, cancellationToken));
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
        public async Task<IActionResult> Report([FromQuery] FilterParameters reportParameters, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _agentService.ReportAsync(HttpContext,reportParameters,cancellationToken));
                
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
        public async Task<IActionResult> Search([FromQuery] FilterParameters parameters, CancellationToken cancellationToken)
        {
            try
            {

                return Ok(await _agentService.SearchAsync(parameters, cancellationToken));
                
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
        public async Task<IActionResult> CreateAgentCoupon([FromBody] AgentCouponDto agentCouponDto, CancellationToken cancellationToken)
		{
			try
			{
				
                return Ok(await _agentService.CreateAgentCouponAsync(agentCouponDto,cancellationToken));
                
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
        public async Task<IActionResult> AssignExistingCoupon([FromBody] AssignCouponDto assignCouponDto, CancellationToken cancellationToken)
		{
			
            try
            {
                
                return Ok(await _agentService.AssignExistingCouponAsync(assignCouponDto, cancellationToken));
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
        public async Task<IActionResult> Chart(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _agentService.ChartAsync(cancellationToken));
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
        public async Task<IActionResult> CheckCoupon( string? id ,[FromQuery(Name = "couponCode")]string couponCode, [FromQuery(Name = "countryId")] long? countryId, CancellationToken cancellationToken)
        {
            try
            {

                
                return Ok(await _agentService.CheckCouponAsync(id,couponCode, countryId,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }
        
        
        
        [HttpGet("DeliveryPrices")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetDeliveryPrices(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _agentService.GetAgentsDeliveryPricesAsync(cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent(); // new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }
        
        [HttpGet("DeliveryPrices/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetDeliveryPriceById(string? id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _agentService.GetAgentDeliveryPricesByAgentIdAsync(id ?? "",cancellationToken));
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
        public async Task<IActionResult> GetDeliveryPricesByAgentId(string? id, CancellationToken cancellationToken)
        {
            try
            {
                 return Ok(await _agentService.GetAgentDeliveryPricesByAgentIdAsync(id ?? "",cancellationToken));
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
        public async Task<IActionResult> AddDeliveryPrice([FromBody] AgentDeliveryPrice agentDeliveryPrice, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _agentService.AddAgentDeliveryPriceAsync(agentDeliveryPrice,cancellationToken));
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
        public async Task<IActionResult> DeleteDeliveryPrice(long id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _agentService.DeleteDeliveryPriceAsync(id,cancellationToken));
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
        public async Task<IActionResult> SendFirebaseNotification([FromBody] FBNotify fbNotify, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _agentService.SendFirebaseNotificationAsync(fbNotify, cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }
        /**/


    }
}