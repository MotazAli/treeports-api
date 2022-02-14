using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Device.Location;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TreePorts.DTO;
using TreePorts.DTO.ReturnDTO;
using TreePorts.Hubs;
using TreePorts.Models;
using TreePorts.Presentation;
using TreePorts.Utilities;

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
        public async Task<IActionResult> GetOrders()
        {

            try {
                
                return Ok(await _orderService.GetOrdersAsync());
            } catch (Exception e) {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        //[HttpPost("/Orders")]
        [HttpGet("Paging")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> OrdersPaging([FromQuery] FilterParameters parameters)
        {
            try
            {

                return Ok(await _orderService.GetOrdersPaginationAsync(parameters));
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
        public async Task<IActionResult> UserOrdersPaging( long id , [FromQuery] FilterParameters parameters)
        {
            try
            {
                
                return Ok(await _orderService.UserOrdersPagingAsync(id,parameters));
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
        public async Task<IActionResult> OrderStatusHistory(long id)
        {
            try
            {
                 return Ok(await _orderService.GetOrdersStatusHistoriesByOrderIdAsync(id));
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
        public async Task<IActionResult> GetOrderDetailsByOrderId(long id)
        {
            try
            {
                return Ok(await _orderService.GetOrderDetailsByOrderIdAsync(id));
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
        public async Task<IActionResult> GetOrder(long id)
        {
            try
            {
                 return Ok(await _orderService.GetOrderByIdAsync(id));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        // Post: Order/GetOrderDetails
        //[HttpPost("GetOrderDetails")]
        [HttpGet("{id}/Details/{captainId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetOrderDetails([FromRoute(Name ="id")] long orderId, [FromRoute(Name = "captainId")] long captainId)//[FromBody] OrderRequest orderRequest)
        {
            try
            {
                return Ok(await _orderService.GetOrderDetailsAsync(orderId,captainId));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }






        // GET: Order/GetRunningOrderByDriverID
        //[HttpGet("GetRunningOrderByDriverID/{id}")]
        [HttpGet("Running/Captains/{captainId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetRunningOrderByCaptainId( [FromRoute(Name = "captainId")] long captainId)
        {
            try
            {

                return Ok(await _orderService.GetRunningOrderByCaptainIdAsync(captainId));
                

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
        public async Task<IActionResult> IgnoreOrder([FromBody] OrderRequest orderRequest)
        {
            try
            {

                return Ok(await _orderService.IgnoreOrderAsync(orderRequest));


            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }




        [HttpGet("FakeCancel/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> FakeCancel(long id)
        {
            try
            {

                return Ok(await _orderService.FakeCancelAsync(id));
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
        public async Task<IActionResult> FakeAssignToCaptain([FromBody] OrderRequest orderRequest)
        {
            try
            {
                return Ok(await _orderService.FakeAssignToCaptainAsync(orderRequest));
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
        public async Task<IActionResult> AssignToCaptain([FromBody] OrderRequest orderRequest)
        {
            try
            {

                return Ok(await _orderService.AssignToCaptainAsync(orderRequest));


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
        public async Task<IActionResult> AcceptOrder([FromBody] OrderRequest orderRequest)
        {
            try
            {

                return Ok(await _orderService.AcceptOrderAsync(orderRequest));


            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        // POST: Order/IgnorOrder
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


        //[HttpGet("OrderPickedUp/{id}")]
        [HttpGet("{id}/PickedUp")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> OrderPickedUp(long id)
        {
            try
            {

                return Ok(await _orderService.OrderPickedUpAsync(id));
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
        public async Task<IActionResult> OrderDropped(long id)
        {
            try
            {


                return Ok(await _orderService.OrderDroppedAsync(id));
               
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
        public async Task<IActionResult> Cancel(long id)
        {
            try
            {

                return Ok(await _orderService.CancelOrderAsync(id));
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
        public async Task<IActionResult> AddOrder([FromBody] Order order, [FromQuery] string CouponCode)
        {
            try
            {

                
                return Ok(await _orderService.AddOrderAsync(order,HttpContext,CouponCode));


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
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                return Ok(await _orderService.DeleteOrderAsync(id));
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
        public async Task<IActionResult> AddOrderInvoice([FromBody] OrderInvoice orderInvoice)
        {
            try
            {
                return Ok(await _orderService.AddOrderInvoiceAsync(orderInvoice));

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
        public async Task<IActionResult> AddPaidOrder([FromBody] PaidOrder paidOrder)
        {
            try
            {
                return Ok(await _orderService.AddPaidOrderAsync(paidOrder));

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
        public async Task<IActionResult> GetOrderCurrentLocationByOrderId(long id) // order id
        {
            try
            {


                
                return Ok(await _orderService.GetOrderCurrentLocationByOrderIdAsync(id));
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
        public async Task<IActionResult> GetOrderItems(long id)
        {
            try
            {
                return Ok(await _orderService.GetOrderItemsAsync(id));

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
        public async Task<IActionResult> GetQRCodeByOrder(long id)
		{
            try
			{
                
                return Ok(await _orderService.GetQRCodeByOrderIdAsync(id));
			}
            catch (Exception e)
			{
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
		}

        /* Get Orders  Report*/
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

        }
        
        /* SearchDetails */
        [HttpGet("SearchDetails")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> SearchDetails([FromQuery] FilterParameters parameters)
        {
            try
            {


                
                return Ok(await _orderService.SearchDetailsAsync(parameters));

                
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
        public async Task<IActionResult> Chart()
		{
            try
            {
                return Ok(await _orderService.ChartAsync());
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
        public async Task<IActionResult> Search([FromBody] OrderFilter orderFilter)
        {
            try
            {
                return Ok(await _orderService.SearchAsync(orderFilter));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }
        
        
        
        
        
        
    }
}
