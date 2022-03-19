using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Device.Location;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TreePorts.DTO;
using TreePorts.DTO.Records;

namespace TreePorts.Controllers
{
    //[Authorize]
    //[Route("[controller]")]
    [Route("/Orders/")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            
            _orderService = orderService;
        }


        // GET: Order
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Order>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetOrders(CancellationToken cancellationToken)
        {

            try {
                
                return Ok(await _orderService.GetOrdersAsync(cancellationToken));
            } catch (Exception e) {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        //[HttpPost("/Orders")]
        [HttpGet("Paging")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> OrdersPaging([FromQuery] FilterParameters parameters, CancellationToken cancellationToken)
        {
            try
            {

                return Ok(await _orderService.GetOrdersPaginationAsync(parameters,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        //[HttpPost("UserOrders")]
        [HttpGet("Agents/{id}/Paging")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UserOrdersPaging( string? id , [FromQuery] FilterParameters parameters, CancellationToken cancellationToken)
        {
            try
            {
                
                return Ok(await _orderService.UserOrdersPagingByAgentIdAsync(id ?? "",parameters,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        //[HttpGet("OrderStatusHistory/{id}")]
        [HttpGet("{id}/StatusHistories")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrderStatusHistory>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> OrderStatusHistory(long id, CancellationToken cancellationToken)
        {
            try
            {
                 return Ok(await _orderService.GetOrdersStatusHistoriesByOrderIdAsync(id, cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }




        // GET: Order/5
        //[HttpGet("ById/{id}")]
        [HttpGet("{id}/Info")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderDetails))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetOrderDetailsByOrderId(long id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _orderService.GetOrderDetailsByOrderIdAsync(id,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }



        // GET: Order/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Order))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetOrder(long id, CancellationToken cancellationToken)
        {
            try
            {
                 return Ok(await _orderService.GetOrderByIdAsync(id,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        // Post: Order/GetOrderDetails
        //[HttpPost("GetOrderDetails")]
        [HttpGet("{id}/Details/{captainAccountId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetOrderDetails([FromRoute(Name ="id")] long orderId, [FromRoute(Name = "captainAccountId")] string captainId, CancellationToken cancellationToken)//[FromBody] OrderRequest orderRequest)
        {
            try
            {
                return Ok(await _orderService.GetOrderDetailsAsync(orderId,captainId,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }






        // GET: Order/GetRunningOrderByDriverID
        //[HttpGet("GetRunningOrderByDriverID/{id}")]
        [HttpGet("Running/Captains/{captainAccountId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetRunningOrderByCaptainId( [FromRoute(Name = "captainAccountId")] string? captainId, CancellationToken cancellationToken)
        {
            try
            {

                return Ok(await _orderService.GetRunningOrderByCaptainUserAccountIdAsync(captainId ?? "",cancellationToken));
                

            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }



        // POST: Order/IgnorOrder
        //[AllowAnonymous]
        //[HttpPost("IgnoreOrder")]
        [HttpPost("Ignore")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> IgnoreOrder([FromBody] OrderRequest orderRequest, CancellationToken cancellationToken)
        {
            try
            {

                return Ok(await _orderService.IgnoreOrderAsync(orderRequest,cancellationToken));


            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }




        [HttpGet("FakeCancel/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> FakeCancel(string? id, CancellationToken cancellationToken)
        {
            try
            {

                return Ok(await _orderService.FakeCancelAsync(id,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        // POST: Order/AssignToCaptain
        //[AllowAnonymous]
        [HttpPost("FakeAssignToCaptain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> FakeAssignToCaptain([FromBody] OrderRequest orderRequest, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _orderService.FakeAssignToCaptainAsync(orderRequest,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }






        // POST: Order/AssignToCaptain
        //[AllowAnonymous]
        //[HttpPost("AssignToCaptain")]
        [HttpPost("Assign")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AssignToCaptain([FromBody] OrderRequest orderRequest, CancellationToken cancellationToken)
        {
            try
            {

                return Ok(await _orderService.AssignToCaptainAsync(orderRequest,cancellationToken));


            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }








        // POST: Order/AcceptOrder
        //[AllowAnonymous]
        //[HttpPost("AcceptOrder")]
        [HttpPost("Accept")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AcceptOrder([FromBody] OrderRequest orderRequest, CancellationToken cancellationToken)
        {
            try
            {

                return Ok(await _orderService.AcceptOrderAsync(orderRequest,cancellationToken));


            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


       /* // POST: Order/IgnorOrder
        //[AllowAnonymous]
        //[HttpPost("RejectOrder")]
        [HttpPost("Reject")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RejectOrder([FromBody] OrderRequest orderRequest)
        {
            try
            {

                return Ok(await _orderService.RejectOrderAsync(orderRequest));
                

            }
            catch (Exception e)
            {
                return NoContent(); // new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }
*/

        //[HttpGet("OrderPickedUp/{id}")]
        [HttpGet("{id}/PickedUp")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> OrderPickedUp(long id, CancellationToken cancellationToken)
        {
            try
            {

                return Ok(await _orderService.OrderPickedUpAsync(id,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        //[HttpGet("OrderDropped/{id}")]
        [HttpGet("{id}/Dropped")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> OrderDropped(long id, CancellationToken cancellationToken)
        {
            try
            {


                return Ok(await _orderService.OrderDroppedAsync(id,cancellationToken));
               
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }
        
        
        
        
        //[HttpGet("Cancel/{id}")]
        [HttpGet("{id}/Cancel")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Cancel(long id, CancellationToken cancellationToken)
        {
            try
            {

                return Ok(await _orderService.CancelOrderAsync(id,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }



        //[AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddOrder([FromBody] Order order, [FromQuery] string CouponCode, CancellationToken cancellationToken)
        {
            try
            {

                
                return Ok(await _orderService.AddOrderAsync(order,HttpContext,CouponCode,cancellationToken));


            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Order))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _orderService.DeleteOrderAsync(id,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        //[HttpPost("AddOrderInvoice")]
        [HttpPost("Invoices")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderInvoice))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddOrderInvoice([FromBody] OrderInvoice orderInvoice, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _orderService.AddOrderInvoiceAsync(orderInvoice,cancellationToken));

            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        //[HttpPost("AddPaidOrder")]
        [HttpPost("Paid")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaidOrder))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddPaidOrder([FromBody] PaidOrder paidOrder, CancellationToken cancellationTokenr)
        {
            try
            {
                return Ok(await _orderService.AddPaidOrderAsync(paidOrder,cancellationTokenr));

            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }



        // GET: Order/{id}/Locations/Current
        [HttpGet("{id}/Locations/Current")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetOrderCurrentLocationByOrderId(long id, CancellationToken cancellationToken) // order id
        {
            try
            {


                
                return Ok(await _orderService.GetOrderCurrentLocationByOrderIdAsync(id,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }


        }




        // [HttpGet("GetOrderItems/{id}")]
        [HttpGet("{id}/Items")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrderItem>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetOrderItems(long id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _orderService.GetOrderItemsAsync(id,cancellationToken));

            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }

        // Get QRCode By Order Id
        //[HttpGet("GetQRCodeByOrder/{id}")]
        [HttpGet("{id}/QRCode")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetQRCodeByOrder(long id, CancellationToken cancellationToken)
		{
            try
			{
                
                return Ok(await _orderService.GetQRCodeByOrderIdAsync(id,cancellationToken));
			}
            catch (Exception e)
			{
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
		}

        /* Get Orders  Report*//*
        [HttpGet("Report")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Report([FromQuery] FilterParameters reportParameters)
        {
            try
            {

                
                return Ok(await _orderService.ReportAsync(reportParameters,HttpContext));

            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }*/
        
        /* SearchDetails */
        [HttpGet("SearchDetails")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> SearchDetails([FromQuery] FilterParameters parameters, CancellationToken cancellationToken)
        {
            try
            {


                
                return Ok(await _orderService.SearchDetailsAsync(parameters,cancellationToken));

                
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }
        /**/








        /*Charts*/
        [HttpGet("Charts")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Chart(CancellationToken cancellationToken)
		{
            try
            {
                return Ok(await _orderService.ChartAsync(cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }
        
        
        
        /*Charts*/
        [HttpPost("Search")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrderFilterResponse>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Search([FromBody] OrderFilter orderFilter, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _orderService.SearchAsync(orderFilter,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }
        
        
        
        
        
        
    }
}
