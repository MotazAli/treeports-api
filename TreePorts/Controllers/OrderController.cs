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
using TreePorts.Infrastructure;
using TreePorts.Infrastructure.Services;
using TreePorts.Maoun;
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

        private IUnitOfWork _unitOfWork;
        //private IHubContext<MessageHub> _HubContext;
        private IWebHostEnvironment _hostingEnvironment;
        private readonly IMapper mapper;
        private IHubContext<MessageHub> _HubContext;
        private readonly INotifyService _notify;
        private readonly IOrderService _orderService;
        private readonly IServiceProvider _serviceProvider;
        public OrderController(
            IUnitOfWork unitOfWork, 
            IWebHostEnvironment hostingEnvironment, 
            IMapper mapper, 
            IHubContext<MessageHub> hubcontext,
            INotifyService notify,
            IOrderService orderService,
            IServiceProvider serviceProvider
            )//, IHubContext<MessageHub> hubcontext)
        {
            _unitOfWork = unitOfWork;
            _HubContext = hubcontext;
            _hostingEnvironment = hostingEnvironment;
            this.mapper = mapper;
            _notify = notify;
            _orderService = orderService;
            _serviceProvider = serviceProvider;
        }


        // GET: Order
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Order>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetOrders()
        {

            try {
                var resullt = await _unitOfWork.OrderRepository.GetOrdersAsync();
                return Ok(resullt);
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

                var skip = (parameters.NumberOfObjectsPerPage * (parameters.Page - 1));
                var take = parameters.NumberOfObjectsPerPage;


                var result =await  _unitOfWork.OrderRepository.GetOrdersPaginationAsync(skip,take);

                return Ok(result);
                

               /* var total = query.Count();            
                var result = Utility.Pagination(query, parameters.NumberOfObjectsPerPage, parameters.Page).ToList();
                var totalPages =(int)Math.Ceiling(total / (double)parameters.NumberOfObjectsPerPage);
               
                return Ok(new { Orders = result, Total = total, Page = parameters.Page, TotaPages = totalPages });*/
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
                var users =  _unitOfWork.OrderRepository.GetByQuerable(o => o.IsDeleted == false && o.AgentId == id);
                //var total = query.Count();
                //var result = Utility.Pagination(query.ToList(), pagination.NumberOfObjectsPerPage, pagination.Page).ToList();
                //var totalPages = (int)Math.Ceiling(total / (double)pagination.NumberOfObjectsPerPage);


                var totalResult = 0;

                var total =  Math.Ceiling(( ((decimal)users.Count()) / ((decimal)parameters.NumberOfObjectsPerPage) ));
				var skip = (parameters.NumberOfObjectsPerPage * (parameters.Page - 1));
				var take = parameters.NumberOfObjectsPerPage;

                var result = Utility.GetFilter2<Order>(parameters, users, skip, take, out totalResult);

                var usersResult = this.mapper.Map<List<OrderResponse>>(result.ToList());
                var totalPages = (int)Math.Ceiling(totalResult / (double)parameters.NumberOfObjectsPerPage);

                foreach (var order in usersResult)
                {
                    if (order.Code.Length != 0)
                    {
                        order.QRCodeUrl = Utility.ConvertImgToString(order.Code);
                    }


                }

                return Ok(new { Orders = usersResult, TotalResult = totalResult, Total = total, Page = parameters.Page, TotalPages = totalPages });
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
                var result = await _unitOfWork.OrderRepository.GetOrdersStatusHistoriesByAsync( o => o.OrderId == id);
                if (result == null) return NoContent();
                
                return Ok(result);
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
                var result = await _unitOfWork.OrderRepository.GetOrderDetailsByIdAsync(id);
                if (result?.QrCode != null && result?.QrCode.Code.Length != 0)
                {
                    result.QrCode.QrCodeUrl = Utility.ConvertImgToString(result.QrCode.Code);
                }

                return Ok(result);
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
                var resullt = await _unitOfWork.OrderRepository.GetOrderById_oldBehaviourAsync(id);

                var userPayments = await _unitOfWork.UserRepository.GetUsersPaymentsByAsync(p => p.OrderId == resullt.Id);
                var userPayment = userPayments.FirstOrDefault();
                if (userPayment != null)
                    resullt.OrderDeliveryPaymentAmount = userPayment.Value;
                var qrCodes = await _unitOfWork.UserRepository.GetQRCodeByAsync(p => p.OrderId == resullt.Id);
                var qrCode = qrCodes.FirstOrDefault();
                if (qrCode != null && qrCode.Code.Length != 0)
                {
                    qrCode.QrCodeUrl = Utility.ConvertImgToString(qrCode.Code);
                    resullt.Qrcodes.Add(qrCode);
                }
                var userAcceptedRequests = await _unitOfWork.UserRepository.GetUsersAcceptedRequestsByAsync(u => u.OrderId == resullt.Id);

                if (userAcceptedRequests != null)
                {
                    resullt.UserAcceptedRequests = userAcceptedRequests;

                }


                //var requests = await _unitOfWork.UserRepository.GetUserNewRequestBy(r => r.OrderId == id);
                //var userRequest = re
                return Ok(resullt);
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
                var resullt = await _unitOfWork.OrderRepository.GetOrderById_oldBehaviourAsync(orderId);
                var agent = await _unitOfWork.AgentRepository.GetAgentByIdAsync((long)resullt.AgentId);
                resullt.Agent = agent;

                var items = await _unitOfWork.OrderRepository.GetOrdersItemsByAsync(i => i.OrderId == orderId);
                resullt.OrderItems = items.ToList();

                var locations = await _unitOfWork.UserRepository.GetUsersCurrentLocationsByAsync(l => l.UserId == captainId);
                var userLocation = locations.FirstOrDefault();

                var system = await _unitOfWork.SystemRepository.GetCurrentSystemSettingAsync();
                var user = await _unitOfWork.UserRepository.GetUserByIdAsync(captainId);

                

                  /* Customer Distance*/
                var customerOrigin = resullt.PickupLocationLat + "," + resullt.PickupLocationLong;
                var customerDestination = resullt.DropLocationLat + "," + resullt.DropLocationLong;
             
                var customerResponse = await Utility.getDirectionsFromGoogleMap(customerOrigin, customerDestination, "driving");
                var customerResponseResult = JsonConvert.DeserializeObject<GoogleMapsResponse>(customerResponse);
                double customerDistance = 0.0;
                var customerResponseDistance = customerResponseResult.Routes.FirstOrDefault().Legs.FirstOrDefault().distance;
                var customerResponseDuration = customerResponseResult.Routes.FirstOrDefault().Legs.FirstOrDefault().duration;
				if (customerResponseDistance.text.Contains("km"))
				{
                  customerDistance = double.Parse( customerResponseDistance.value) /1000.0;
				} else
				{
                    customerDistance = double.Parse(customerResponseDistance.value);

                }
                /* Customer Distance*/

                /* Agent Distance*/
                var agentOrigin = userLocation.Lat + "," + userLocation.Long;
                var agentDestination = resullt.PickupLocationLat + "," + resullt.PickupLocationLong;
                var agentResponse = await Utility.getDirectionsFromGoogleMap(agentOrigin, agentDestination, "driving");

                var agentResponseResult = JsonConvert.DeserializeObject<GoogleMapsResponse>(agentResponse);
                var agentResponseDistance = agentResponseResult.Routes.FirstOrDefault().Legs.FirstOrDefault().distance;
                var agentResponseDuration = agentResponseResult.Routes.FirstOrDefault().Legs.FirstOrDefault().duration;
                double agentDistance = 0.0;
				if (agentResponseDistance.text.Contains("km"))
				{
                    agentDistance = double.Parse(agentResponseDistance.value) / 1000.0;

				} else
				{
                    agentDistance = double.Parse(agentResponseDistance.value);

                }
                /* Agent Distance*/


                CountryPrice countryPrice = null; // just using that object as a template to hold the prices for calcutaions
                var agentDeliveryPrices =
                    await _unitOfWork.AgentRepository.GetAgentDeliveryPriceByAsync(a =>
                        a.AgentId == resullt.AgentId && a.IsCurrent == true);
                var agentDeliveryPrice = agentDeliveryPrices.FirstOrDefault();
                if (agentDeliveryPrice != null && agentDeliveryPrice?.Id > 0)
                {
                    countryPrice = new CountryPrice()
                    {
                        Kilometers = agentDeliveryPrice.Kilometers,
                        Price = agentDeliveryPrice.Price,
                        ExtraKilometers = agentDeliveryPrice.ExtraKilometers,
                        ExtraKiloPrice = agentDeliveryPrice.ExtraKiloPrice
                    };
                }
                else
                {
                    
                    var cityPrices = await _unitOfWork.CountryRepository.GetCitiesPricesByAsync(c => c.CityId == agent.CityId);
                    var cityPrice = cityPrices.FirstOrDefault();
                    if (cityPrice != null && cityPrice?.Id > 0)
                    {
                        countryPrice = new CountryPrice()
                        {
                            Kilometers = cityPrice.Kilometers,
                            Price = cityPrice.Price,
                            ExtraKilometers = cityPrice.ExtraKilometers,
                            ExtraKiloPrice = cityPrice.ExtraKiloPrice
                        };
                    }
                    else
                    {
                        var countriesPrices = await _unitOfWork.CountryRepository.GetCountriesPricesByAsync(c => c.CountryId == agent.CountryId);
                        countryPrice = countriesPrices.FirstOrDefault(); 
                    }
                }



                

                agentDistance = Math.Round(agentDistance);
                customerDistance = Math.Round(customerDistance); // for example customer distance is 28km

                decimal? amount = 0;
                decimal? remainingAmount = 0;
                if (customerDistance <= countryPrice.Kilometers) // 28km is less or equal 5km country kilometers
                {
                    amount = ((decimal)countryPrice.Kilometers) * countryPrice.Price;
                   // amount = countryPrice.Price;
                }
                else
                {
                    
                    //remainingKilometers is 3km = 28km customerDistance % 5km country kilometers
                    var remainingKilometers = customerDistance % countryPrice.Kilometers;

                    //realKilometer is 25km = 28km customerDistance - 3km remainingKilometers
                    var realKilometer = customerDistance - remainingKilometers;
                    
                    //amount is 50 Reyal = ( 25km realKilometer / 5km country kilometers) * 10 Reyal Country Price
                    amount = ((decimal)(realKilometer / countryPrice.Kilometers)) * countryPrice.Price;

                    if (remainingKilometers > countryPrice.ExtraKilometers)// 3km is greater 1km country extra kilometers
                    {
                        // var extraRemainingKilometers = remainingKilometers % countryPrice.ExtraKilometers;
                        // var realExtraRemainingKilometers = remainingKilometers - extraRemainingKilometers;
                        //
                        // remainingAmount = ((decimal)(realExtraRemainingKilometers/ countryPrice.ExtraKilometers)) * countryPrice.ExtraKiloPrice;
                        // amount = amount + remainingAmount;
                        
                        remainingAmount = ((decimal)(remainingKilometers)) * countryPrice.ExtraKiloPrice;
                        amount = amount + remainingAmount;
                    }
                    else if (remainingKilometers > 0 && remainingKilometers <= countryPrice.ExtraKilometers)                                                                                                                 
                    {
                        remainingAmount = countryPrice.ExtraKiloPrice;
                        amount = amount + remainingAmount;
                    }
                    var agentCoupon =  _unitOfWork.AgentRepository.GetAssignedCoupon((long)resullt.AgentId, orderId);
                   
                    if(agentCoupon != null)
					{
                        amount = amount - ((amount * (decimal)agentCoupon.DiscountPercent) /100);
					}
                    
                }


                var userPayments = await _unitOfWork.UserRepository.GetUsersPaymentsByAsync(p => p.OrderId == resullt.Id);
                var userPayment = userPayments.FirstOrDefault();

                if (userPayment != null && userPayment.Id > 0)
                {
                    userPayment.Value = amount;
                    var updatePaymentResult = await _unitOfWork.UserRepository.UpdateUserPaymentAsync(userPayment);
                }
                else
                {
                    CaptainUserPayment payment = new CaptainUserPayment()
                    {
                        UserId = captainId,
                        OrderId = orderId,
                        PaymentTypeId = resullt.PaymentTypeId,
                        SystemSettingId = system.Id,
                        StatusId = (long)PaymentStatusTypes.New,
                        Value = amount,
                        CreationDate = DateTime.Now
                    };
                    var insertPaymentResult = await _unitOfWork.UserRepository.InsertUserPaymentAsync(payment);
                }
                
                
                var result = await _unitOfWork.Save();
                if (result == 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };


                return Ok(new { Order = resullt, AgentDistance = agentResponseDistance.text, CustomerDistance = customerResponseDistance.text, 
                    CustomerDuration = customerResponseDuration.text, AgentDuration = agentResponseDuration.text, DeliveryAmount = amount, });
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

                var running_orders = await _unitOfWork.OrderRepository.GetRunningOrdersByAsync(r => r.UserId == captainId);
                var running_order = running_orders.FirstOrDefault();
                if (running_order == null || running_order.Id <= 0) return Ok("No Running Order");

                return await this.GetOrderDetails((long)running_order.OrderId, captainId);
                /*OrderRequest orderRequest = new OrderRequest()
                {
                    UserId = captainId,
                    OrderId = (long)running_order.OrderId
                };*/
                
                //return await this.GetOrderDetails(orderRequest);

                //var acceptedRequsts = await _unitOfWork.UserRepository.GetUserAcceptedRequestBy(a => a.UserId == id);

                //if (acceptedRequsts == null || acceptedRequsts.Count <= 0)
                //    return new ObjectResult("No running requests") { StatusCode = 998 };


                //var ordersId = acceptedRequsts.OrderByDescending(s => s.CreationDate).FirstOrDefault().OrderId;

                //var ordersCurrentStatus = await _unitOfWork.OrderRepository.GetOrderStatusBy( s => s.OrderId == ordersId &&
                //(s.StatusTypeId == (long) OrderStatusTypes.New  ||
                //s.StatusTypeId == (long)OrderStatusTypes.Progress ||
                //s.StatusTypeId == (long)OrderStatusTypes.PickedUp ||
                //s.StatusTypeId == (long)OrderStatusTypes.AssginedToDriver));



                //var orderCurrentStatus = ordersCurrentStatus.FirstOrDefault();
                //if(orderCurrentStatus == null )
                //    return new ObjectResult("No running requests") { StatusCode = 998 };




                //var resullt = await _unitOfWork.OrderRepository.GetOrderByID((long)orderCurrentStatus.OrderId);
                //var agent = await _unitOfWork.AgentRepository.GetByID((long)resullt.AgentId);
                //resullt.Agent = agent;

                //var items = await _unitOfWork.OrderRepository.GetOrderItemBy(i => i.OrderId == orderCurrentStatus.OrderId);
                //resullt.OrderItems = items.ToList();


                //var paidOrders = await _unitOfWork.OrderRepository.GetPaidOrderBy(p => p.OrderId == resullt.Id);
                //var paidOrder = paidOrders.FirstOrDefault();
                //var isDriverPaidOrder = (paidOrder != null && paidOrder.Id > 0);


                //var userInvoices = await _unitOfWork.OrderRepository.GetOrderInvoiceBy(i => i.OrderId == resullt.Id);
                //var userInvoice = userInvoices.FirstOrDefault();
                //var isDriverAttachInvoice = (userInvoice != null && userInvoice.Id > 0);


                //resullt.OrderCurrentStatus = ordersCurrentStatus.ToList();

                //var locations = await _unitOfWork.UserRepository.GetUserCurrentLocationBy(l => l.UserId == id);
                //var userLocation = locations.FirstOrDefault();

                //var system = await _unitOfWork.SystemRepository.GetCurrent();
                //var user = await _unitOfWork.UserRepository.GetUserByID(id);


                //double agentDistance = Utility.distance(double.Parse(userLocation.Lat), double.Parse(userLocation.Long),
                //            double.Parse(resullt.PickupLocationLat), double.Parse(resullt.PickupLocationLong));
                //double customerDistance = Utility.distance(double.Parse(resullt.PickupLocationLat), double.Parse(resullt.PickupLocationLong),
                //              double.Parse(resullt.DropLocationLat), double.Parse(resullt.DropLocationLong));


                //var payments = await _unitOfWork.UserRepository.GetUserPaymentBy(p => p.OrderId == orderCurrentStatus.OrderId);
                //var payment = payments.FirstOrDefault();


                //return Ok(new { Order = resullt, AgentDistance = agentDistance, CustomerDistance = customerDistance, DeliveryAmount = payment.Value , IsDriverAttachInvoice = isDriverAttachInvoice , IsDriverPaidOrder = isDriverPaidOrder });


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

                var userNewRequests = await _unitOfWork.UserRepository.DeleteUserNewRequestByOrderIdAsync(orderRequest.OrderId);
                var deleteResult = await _unitOfWork.UserRepository.DeleteUserPaymentByOrderIdAsync(orderRequest.OrderId);

                //var userNewRequests = await _unitOfWork.UserRepository.DeleteUserNewRequestByUserID(orderRequest.UserId);
                //var oldOrderPayment = await _unitOfWork.UserRepository.GetUserPaymentBy(p => p.OrderId == orderRequest.OrderId);
                //var deleteResult = await _unitOfWork.UserRepository.DeleteUserPayment(oldOrderPayment.FirstOrDefault().Id);


                CaptainUserIgnoredRequest driverRequest = new CaptainUserIgnoredRequest()
                {
                    OrderId = orderRequest.OrderId,
                    UserId = orderRequest.UserId,
                    AgentId = userNewRequests.AgentId,
                    CreatedBy = 1,
                    CreationDate = DateTime.Now };

                var insertNewRequestResult = await _unitOfWork.UserRepository.InsertUserIgnoredRequestAsync(driverRequest);
                var result = await _unitOfWork.Save();
                if (result == 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                return Ok(true);


                //////////// the new behavior of the penalties//////////////
                //var systemSetting = await _unitOfWork.SystemRepository.GetCurrent();
                //if (systemSetting != null)
                //{
                //    if (systemSetting.IgnorPerTypeId == (long)IgnorPer.Days)
                //    {
                //        var userIgnoredRequests = await _unitOfWork.UserRepository.GetUserIgnoredRequestBy(u => u.UserId == orderRequest.UserId &&
                //       u.CreationDate.Value.Day == DateTime.Now.Day &&
                //       u.CreationDate.Value.Month == DateTime.Now.Month &&
                //       u.CreationDate.Value.Year == DateTime.Now.Year);

                //        if (userIgnoredRequests != null && userIgnoredRequests.Count >= systemSetting.IgnorRequestsNumbers)
                //        {
                //            UserIgnoredPenalty userIgnoredPenalty = new UserIgnoredPenalty()
                //            {
                //                UserId = orderRequest.UserId,
                //                SystemSettingId = systemSetting.Id,
                //                PenaltyStatusTypeId = (long)PenaltyStatusTypes.New,
                //                CreatedBy = 1,
                //                CreationDate = DateTime.Now

                //            };

                //            var insertUserIgnoredPenalty_result = await _unitOfWork.UserRepository.InsertUserIgnoredPenalty(userIgnoredPenalty);
                //            result = await _unitOfWork.Save();
                //            if (result == 0) return new ObjectResult("Server not available") { StatusCode = 707 };
                //        }

                //    }
                //    else if (systemSetting.IgnorPerTypeId == (long)IgnorPer.Months)
                //    {
                //        var userIgnoredRequests = await _unitOfWork.UserRepository.GetUserIgnoredRequestBy(u => u.UserId == orderRequest.UserId &&

                //      u.CreationDate.Value.Month == DateTime.Now.Month &&
                //      u.CreationDate.Value.Year == DateTime.Now.Year);

                //        if (userIgnoredRequests != null && userIgnoredRequests.Count >= systemSetting.IgnorRequestsNumbers)
                //        {
                //            UserIgnoredPenalty userIgnoredPenalty = new UserIgnoredPenalty()
                //            {
                //                UserId = orderRequest.UserId,
                //                SystemSettingId = systemSetting.Id,
                //                PenaltyStatusTypeId = (long)PenaltyStatusTypes.New,
                //                CreatedBy = 1,
                //                CreationDate = DateTime.Now

                //            };

                //            var insertUserIgnoredPenalty_result = await _unitOfWork.UserRepository.InsertUserIgnoredPenalty(userIgnoredPenalty);
                //            result = await _unitOfWork.Save();
                //            if (result == 0) return new ObjectResult("Server not available") { StatusCode = 707 };
                //        }
                //    }
                //    else if (systemSetting.IgnorPerTypeId == (long)IgnorPer.Years)
                //    {
                //        var userIgnoredRequests = await _unitOfWork.UserRepository.GetUserIgnoredRequestBy(u => u.UserId == orderRequest.UserId &&

                //      u.CreationDate.Value.Year == DateTime.Now.Year);

                //        if (userIgnoredRequests != null && userIgnoredRequests.Count >= systemSetting.IgnorRequestsNumbers)
                //        {
                //            UserIgnoredPenalty userIgnoredPenalty = new UserIgnoredPenalty()
                //            {
                //                UserId = orderRequest.UserId,
                //                SystemSettingId = systemSetting.Id,
                //                PenaltyStatusTypeId = (long)PenaltyStatusTypes.New,
                //                CreatedBy = 1,
                //                CreationDate = DateTime.Now

                //            };

                //            var insertUserIgnoredPenalty_result = await _unitOfWork.UserRepository.InsertUserIgnoredPenalty(userIgnoredPenalty);
                //            result = await _unitOfWork.Save();
                //            if (result == 0) return new ObjectResult("Server not available") { StatusCode = 707 };
                //        }
                //    }
                //}
                //////////// end - the new behavior of the penalties//////////////



                //return Ok(true);


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

                var usersMessageHub =
                         await _unitOfWork.UserRepository.GetUsersMessageHubsByAsync(u => u.UserId == id);
                var userMessageHub = usersMessageHub.FirstOrDefault();
                if (userMessageHub != null && userMessageHub.Id > 0)
                {

                    FirebaseNotificationResponse responseResult = null;
                    var result = FirebaseNotification.SendNotification(userMessageHub.ConnectionId, "cancelRequest", "151");

                    if (result != "")
                        responseResult = JsonConvert.DeserializeObject<FirebaseNotificationResponse>(result);

                    if (responseResult == null || responseResult.messageId == "")
                        return new ObjectResult("Failed to send notification to captain") { StatusCode = 406 };

                }
                return Ok(true);
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

                
                var order = await _unitOfWork.OrderRepository.GetOnlyOrderByIdAsync(orderRequest.OrderId);
                if (order == null) return Ok(false);
               
                var usersMessageHub =
                         await _unitOfWork.UserRepository.GetUsersMessageHubsByAsync(u => u.UserId == orderRequest.UserId);
                var userMessageHub = usersMessageHub.FirstOrDefault();
                if (userMessageHub != null && userMessageHub.Id > 0)
                {

                    FirebaseNotificationResponse responseResult = null;
                    var result = FirebaseNotification.SendNotification(userMessageHub.ConnectionId, "assignedRequest", order.Id.ToString());

                    if (result != "")
                        responseResult = JsonConvert.DeserializeObject<FirebaseNotificationResponse>(result);

                    if (responseResult == null || responseResult.messageId == "")
                        return new ObjectResult("Failed to send notification to captain") { StatusCode = 406 };
                    
                }


                return Ok(true);


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

                //check if the orderAssigen not saved before and there is no failure happend that cause the orderAssigned to saving it again
                var ordersAssigned = await _unitOfWork.OrderRepository.GetOrdersAssignmentsByAsync(o => o.OrderId == orderRequest.OrderId);
                var oldOrderAssigned = ordersAssigned?.FirstOrDefault();
                if (oldOrderAssigned != null && oldOrderAssigned.Id > 0) return Ok(true);


                var order = await _unitOfWork.OrderRepository.GetOnlyOrderByIdAsync(orderRequest.OrderId);
                var locations = await _unitOfWork.UserRepository.GetUsersCurrentLocationsByAsync(l => l.UserId == orderRequest.UserId);
                var userLocation = locations.FirstOrDefault();



                CaptainUserAcceptedRequest driverRequest = new CaptainUserAcceptedRequest() { OrderId = orderRequest.OrderId, UserId = orderRequest.UserId, CreatedBy = 1, CreationDate = DateTime.Now };

                /* Customer Distance*/
                var customerOrigin = order.PickupLocationLat + "," + order.PickupLocationLong;
                var customerDestination = order.DropLocationLat + "," + order.DropLocationLong;

                var customerResponse = await Utility.getDirectionsFromGoogleMap(customerOrigin, customerDestination, "driving");
                var customerResponseResult = JsonConvert.DeserializeObject<GoogleMapsResponse>(customerResponse);
                double customerDistance = 0.0;
                var customerResponseDistance = customerResponseResult.Routes.FirstOrDefault().Legs.FirstOrDefault().distance;
                var customerResponseDuration = customerResponseResult.Routes.FirstOrDefault().Legs.FirstOrDefault().duration;
                if (customerResponseDistance.text.Contains("km"))
                {
                    customerDistance = double.Parse(customerResponseDistance.value) / 1000.0;
                }
                else
                {
                    customerDistance = double.Parse(customerResponseDistance.value);

                }
                /* Customer Distance*/

                /* Agent Distance*/
                var agentOrigin = userLocation.Lat + "," + userLocation.Long;
                var agentDestination = order.PickupLocationLat + "," + order.PickupLocationLong;
                var agentResponse = await Utility.getDirectionsFromGoogleMap(agentOrigin, agentDestination, "driving");

                var agentResponseResult = JsonConvert.DeserializeObject<GoogleMapsResponse>(agentResponse);
                var agentResponseDistance = agentResponseResult.Routes.FirstOrDefault().Legs.FirstOrDefault().distance;
                var agentResponseDuration = agentResponseResult.Routes.FirstOrDefault().Legs.FirstOrDefault().duration;
                double agentDistance = 0.0;
                if (agentResponseDistance.text.Contains("km"))
                {
                    agentDistance = double.Parse(agentResponseDistance.value) / 1000.0;

                }
                else
                {
                    agentDistance = double.Parse(agentResponseDistance.value);

                }
                /* Agent Distance*/



                var agentDeliveryPrices =
                    await _unitOfWork.AgentRepository.GetAgentDeliveryPriceByAsync(a =>
                        a.AgentId == order.AgentId && a.IsCurrent == true);
                var agentDeliveryPrice = agentDeliveryPrices.FirstOrDefault();
                if (agentDeliveryPrice != null && agentDeliveryPrice?.Id > 0)
                {

                    AgentOrderDeliveryPrice agentOrderDeliveryPrice = new AgentOrderDeliveryPrice()
                    {
                        OrderId = order.Id,
                        AgentDeliveryPriceId = agentDeliveryPrice.Id,
                        CreationDate = DateTime.Now
                    };
                    var insertAgentOrderDeliveryPriceResult =
                        await _unitOfWork.AgentRepository.InsertAgentOrderDeliveryPriceAsync(agentOrderDeliveryPrice);
                }
                else
                {

                    var agent = await _unitOfWork.AgentRepository.GetAgentByIdAsync((long)order.AgentId);
                    var cityPrices =
                        await _unitOfWork.CountryRepository.GetCitiesPricesByAsync(a =>
                            a.CityId == agent.CityId && a.IsCurrent == true);
                    var cityPrice = cityPrices.FirstOrDefault();

                    if (cityPrice != null && cityPrice?.Id > 0)
                    {
                        CityOrderPrice cityOrderPrice = new CityOrderPrice()
                        {
                            OrderId = order.Id,
                            CityPriceId = cityPrice.Id,
                            CreationDate = DateTime.Now
                        };
                        var insertCityOrderPriceResult =
                            await _unitOfWork.CountryRepository.InsertCityOrderPriceAsync(cityOrderPrice);
                    }
                    else
                    {
                        var countriesPrices = await _unitOfWork.CountryRepository.GetCountriesPricesByAsync(c => c.CountryId == agent.CountryId);
                        var countryPrice = countriesPrices.FirstOrDefault();
                        if (countryPrice != null && countryPrice?.Id > 0)
                        {
                            CountryOrderPrice countryOrderPrice = new CountryOrderPrice()
                            {
                                OrderId = order.Id,
                                CountryPriceId = countryPrice.Id,
                                CreationDate = DateTime.Now
                            };
                            var insertCountryOrderPriceResult =
                                await _unitOfWork.CountryRepository.InsertCountryOrderPriceAsync(countryOrderPrice);
                        }
                    }
                }



                agentDistance = Math.Round(agentDistance);
                customerDistance = Math.Round(customerDistance);



                OrderAssignment orderAssignment = new OrderAssignment()
                {
                    OrderId = orderRequest.OrderId,
                    UserId = orderRequest.UserId,
                    ToAgentKilometer = agentDistance.ToString(),
                    ToAgentTime = agentResponseDuration.text,
                    ToCustomerKilometer = customerDistance.ToString(),
                    ToCustomerTime = customerResponseDuration.text,
                    CreationDate = DateTime.Now

                };

                OrderCurrentStatus orderCurrentStatus = new OrderCurrentStatus()
                {
                    OrderId = orderRequest.OrderId,
                    StatusTypeId = (long)OrderStatusTypes.AssignedToCaptain,
                    IsCurrent = true,
                    CreationDate = DateTime.Now
                    //Type = "AssginedToDriver"
                };

                CaptainUserCurrentStatus userCurrentStatus = new CaptainUserCurrentStatus()
                {
                    UserId = orderRequest.UserId,
                    StatusTypeId = (long)StatusTypes.Progress,
                    IsCurrent = true
                };


                RunningOrder runningOrder = new RunningOrder()
                {
                    OrderId = orderRequest.OrderId,
                    UserId = orderRequest.UserId,
                    CreationDate = DateTime.Now
                };


                //check if there no QrCode inserted incase if the order assigned to captain throught the Admin or Support and didn't create QrCode for the order from request new order
                var oldQrCode = await _unitOfWork.OrderRepository.GetQrcodeByOrderIdAsync(order.Id);
                if (oldQrCode == null || oldQrCode.Id <= 0)
                {
                    /* Create QrCode and Insert*/
                    var qRCode = Utility.CreateQRCode(orderRequest.UserId, order.Id);
                    var qRCodeResult = await _unitOfWork.OrderRepository.InsertQrCodeAsync(qRCode);
                    /* Create QrCode and Insert*/
                }


                var updatedOrder = await _unitOfWork.OrderRepository.UpdateOrderCurrentStatusAsync(order.Id, (long)OrderStatusTypes.AssignedToCaptain);
                var userNewRequests = await _unitOfWork.UserRepository.DeleteUserNewRequestByUserIdAsync(orderRequest.UserId);
                var insertRunningOrderResult = await _unitOfWork.OrderRepository.InsertRunningOrderAsync(runningOrder);
                var insertOrderStatusResult = await _unitOfWork.OrderRepository.InsertOrderStatusAsync(orderCurrentStatus);
                var orderAssignmentResult = await _unitOfWork.OrderRepository.InsertOrderAssignmentAsync(orderAssignment);
                var insertNewRequestResult = await _unitOfWork.UserRepository.InsertUserAcceptedRequestAsync(driverRequest);
                var insertUserStatusResult = await _unitOfWork.UserRepository.InsertUserCurrentStatusAsync(userCurrentStatus);
                var result = await _unitOfWork.Save();
                if (result == 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                _ = _notify.NotifyOrderStatusChanged(OrderStatusTypes.AssignedToCaptain, order.Id, (long)order.AgentId);

                _ = _notify.SendGoogleCloudMessageToCaptain(orderRequest.UserId, "assignedRequest", order.Id.ToString());

                _ = Task.Run(() => {
                    Task.Delay(TimeSpan.FromSeconds(10));
                    _ = _notify.ChangeOrderStatusAndNotify(OrderStatusTypes.Progress, order.Id, (long)order.AgentId);
                });


                return Ok(true);


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


                //var maounOrders =
                //    await _unitOfWork.MaounRepository.GetMaounOrderBy(o => o.SenderOrderId == orderRequest.OrderId);
                //var maounOrder = maounOrders.FirstOrDefault();
                //if (maounOrder?.Id > 0)
                //{
                //    User driverUser = await _unitOfWork.UserRepository.GetUserByID(orderRequest.UserId);
                //    Country country = await _unitOfWork.CountryRepository.GetByID((long)driverUser.CountryId);
                //    MaounUser maounUser =
                //        MaounUtility.ConvertDriverUserToMaounDriverUser(driverUser, country.Code.ToString());
                //    MaounUser maounUserResult = await MaounUtility.CheckAndCreateDriverInMaoun(maounUser);
                //    if (maounUserResult == null) return new ObjectResult("Maoun ordering server not available") { StatusCode = 707 };


                //    MaounRequestOrder maounRequestOrder =
                //        await MaounUtility.UpdateOrderInMaounAddAssinedDriver(maounOrder, maounUserResult);

                //    if(maounRequestOrder == null) return new ObjectResult("Maoun ordering server not available") { StatusCode = 707 };

                //}


                //check if the orderAssigen not saved before and there is no failure happend that cause the orderAssigned to saving it again
                var ordersAssigned = await _unitOfWork.OrderRepository.GetOrdersAssignmentsByAsync( o=> o.OrderId == orderRequest.OrderId);
                var oldOrderAssigned = ordersAssigned?.FirstOrDefault();
                if (oldOrderAssigned != null && oldOrderAssigned.Id > 0) return Ok(true);


                var order = await _unitOfWork.OrderRepository.GetOnlyOrderByIdAsync(orderRequest.OrderId);
                var locations = await _unitOfWork.UserRepository.GetUsersCurrentLocationsByAsync(l => l.UserId == orderRequest.UserId);
                var userLocation = locations.FirstOrDefault();



                CaptainUserAcceptedRequest driverRequest = new CaptainUserAcceptedRequest() { OrderId = orderRequest.OrderId, UserId = orderRequest.UserId, CreatedBy = 1, CreationDate = DateTime.Now };

                //GeoCoordinate agentLocation = new GeoCoordinate(double.Parse(resullt.PickupLocationLat), double.Parse(resullt.PickupLocationLong));
                //GeoCoordinate customerLocation = new GeoCoordinate(double.Parse(resullt.DropLocationLat), double.Parse(resullt.DropLocationLong));
                //GeoCoordinate driverLocation = new GeoCoordinate(double.Parse(userLocation.Lat), double.Parse(userLocation.Long));
                // double agentDistance = Utility.distance(double.Parse(userLocation.Lat), double.Parse(userLocation.Long),
                //             double.Parse(order.PickupLocationLat), double.Parse(order.PickupLocationLong));
                // double customerDistance = Utility.distance(double.Parse(order.PickupLocationLat), double.Parse(order.PickupLocationLong),
                //               double.Parse(order.DropLocationLat), double.Parse(order.DropLocationLong));

                  /* Customer Distance*/
                var customerOrigin = order.PickupLocationLat + "," + order.PickupLocationLong;
                var customerDestination = order.DropLocationLat + "," + order.DropLocationLong;
             
                var customerResponse = await Utility.getDirectionsFromGoogleMap(customerOrigin, customerDestination, "driving");
                var customerResponseResult = JsonConvert.DeserializeObject<GoogleMapsResponse>(customerResponse);
                double customerDistance = 0.0;
                var customerResponseDistance = customerResponseResult.Routes.FirstOrDefault().Legs.FirstOrDefault().distance;
                var customerResponseDuration = customerResponseResult.Routes.FirstOrDefault().Legs.FirstOrDefault().duration;
				if (customerResponseDistance.text.Contains("km"))
				{
                  customerDistance = double.Parse( customerResponseDistance.value) /1000.0;
				} else
				{
                    customerDistance = double.Parse(customerResponseDistance.value);

                }
                /* Customer Distance*/

                /* Agent Distance*/
                var agentOrigin = userLocation.Lat + "," + userLocation.Long;
                var agentDestination = order.PickupLocationLat + "," + order.PickupLocationLong;
                var agentResponse = await Utility.getDirectionsFromGoogleMap(agentOrigin, agentDestination, "driving");

                var agentResponseResult = JsonConvert.DeserializeObject<GoogleMapsResponse>(agentResponse);
                var agentResponseDistance = agentResponseResult.Routes.FirstOrDefault().Legs.FirstOrDefault().distance;
                var agentResponseDuration = agentResponseResult.Routes.FirstOrDefault().Legs.FirstOrDefault().duration;
                double agentDistance = 0.0;
				if (agentResponseDistance.text.Contains("km"))
				{
                    agentDistance = double.Parse(agentResponseDistance.value) / 1000.0;

				} else
				{
                    agentDistance = double.Parse(agentResponseDistance.value);

                }
                /* Agent Distance*/


                
                var agentDeliveryPrices =
                    await _unitOfWork.AgentRepository.GetAgentDeliveryPriceByAsync(a =>
                        a.AgentId == order.AgentId && a.IsCurrent == true);
                var agentDeliveryPrice = agentDeliveryPrices.FirstOrDefault();
                if (agentDeliveryPrice != null && agentDeliveryPrice?.Id > 0)
                {

                    AgentOrderDeliveryPrice agentOrderDeliveryPrice = new AgentOrderDeliveryPrice()
                    {
                        OrderId = order.Id,
                        AgentDeliveryPriceId = agentDeliveryPrice.Id,
                        CreationDate = DateTime.Now
                    };
                    var insertAgentOrderDeliveryPriceResult =
                        await _unitOfWork.AgentRepository.InsertAgentOrderDeliveryPriceAsync(agentOrderDeliveryPrice);
                }
                else
                {
                    
                    var agent = await _unitOfWork.AgentRepository.GetAgentByIdAsync((long)order.AgentId);
                    var cityPrices =
                        await _unitOfWork.CountryRepository.GetCitiesPricesByAsync(a =>
                            a.CityId == agent.CityId && a.IsCurrent == true);
                    var cityPrice = cityPrices.FirstOrDefault();

                    if (cityPrice != null && cityPrice?.Id > 0)
                    {
                        CityOrderPrice cityOrderPrice = new CityOrderPrice()
                        {
                            OrderId = order.Id,
                            CityPriceId = cityPrice.Id,
                            CreationDate = DateTime.Now
                        };
                        var insertCityOrderPriceResult =
                            await _unitOfWork.CountryRepository.InsertCityOrderPriceAsync(cityOrderPrice);
                    }
                    else
                    {
                        var countriesPrices = await _unitOfWork.CountryRepository.GetCountriesPricesByAsync(c => c.CountryId == agent.CountryId);
                        var countryPrice = countriesPrices.FirstOrDefault();
                        if (countryPrice != null && countryPrice?.Id > 0)
                        {
                            CountryOrderPrice countryOrderPrice = new CountryOrderPrice()
                            {
                                OrderId = order.Id,
                                CountryPriceId = countryPrice.Id,
                                CreationDate = DateTime.Now
                            };
                            var insertCountryOrderPriceResult =
                                await _unitOfWork.CountryRepository.InsertCountryOrderPriceAsync(countryOrderPrice);
                        }  
                    }
                }

                
                
                agentDistance = Math.Round(agentDistance);
                customerDistance = Math.Round(customerDistance);



                OrderAssignment orderAssignment = new OrderAssignment()
                {
                    OrderId = orderRequest.OrderId,
                    UserId = orderRequest.UserId,
                    ToAgentKilometer = agentDistance.ToString(),
                    ToAgentTime = agentResponseDuration.text,
                    ToCustomerKilometer = customerDistance.ToString(),
                    ToCustomerTime = customerResponseDuration.text,
                    CreationDate = DateTime.Now

                };

                OrderCurrentStatus orderCurrentStatus = new OrderCurrentStatus()
                {
                    OrderId = orderRequest.OrderId,
                    StatusTypeId = (long)OrderStatusTypes.AssignedToCaptain,
                    IsCurrent = false,
                    //Type = "AssginedToDriver"
                };

                CaptainUserCurrentStatus userCurrentStatus = new CaptainUserCurrentStatus()
                {
                    UserId = orderRequest.UserId,
                    StatusTypeId = (long)StatusTypes.Progress,
                    IsCurrent = true
                };


                RunningOrder runningOrder = new RunningOrder()
                {
                    OrderId = orderRequest.OrderId,
                    UserId = orderRequest.UserId,
                    CreationDate = DateTime.Now
                };


                //check if there no QrCode inserted incase if the order assigned to captain throught the Admin or Support and didn't create QrCode for the order from request new order
                var oldQrCode = await _unitOfWork.OrderRepository.GetQrcodeByOrderIdAsync(order.Id);
                if (oldQrCode == null || oldQrCode.Id <= 0  )
                {
                    /* Create QrCode and Insert*/
                    var qRCode = Utility.CreateQRCode(orderRequest.UserId, order.Id);
                    var qRCodeResult = await _unitOfWork.OrderRepository.InsertQrCodeAsync(qRCode);
                    /* Create QrCode and Insert*/
                }


                var updatedOrder = await _unitOfWork.OrderRepository.UpdateOrderCurrentStatusAsync(order.Id, (long)OrderStatusTypes.AssignedToCaptain);
                var userNewRequests = await _unitOfWork.UserRepository.DeleteUserNewRequestByUserIdAsync(orderRequest.UserId);
                var insertRunningOrderResult = await _unitOfWork.OrderRepository.InsertRunningOrderAsync(runningOrder);
                var insertOrderStatusResult = await _unitOfWork.OrderRepository.InsertOrderStatusAsync(orderCurrentStatus);
                var orderAssignmentResult = await _unitOfWork.OrderRepository.InsertOrderAssignmentAsync(orderAssignment);
                var insertNewRequestResult = await _unitOfWork.UserRepository.InsertUserAcceptedRequestAsync(driverRequest);
                var insertUserStatusResult = await _unitOfWork.UserRepository.InsertUserCurrentStatusAsync(userCurrentStatus);
                var result = await _unitOfWork.Save();
                if (result == 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                _ = _notify.NotifyOrderStatusChanged(OrderStatusTypes.AssignedToCaptain, order.Id,(long) order.AgentId);

                var _newnotify = new NotifyService(_serviceProvider);
                _ = Task.Run(async () => {
                    Task.Delay(TimeSpan.FromSeconds(10));
                    _ = await _newnotify.ChangeOrderStatusAndNotify(OrderStatusTypes.Progress, order.Id, (long)order.AgentId);
                });
                
                return Ok(true);


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

                
                var userNewRequests = await _unitOfWork.UserRepository.DeleteUserNewRequestByOrderIdAsync(orderRequest.OrderId);
                var deleteResult = await _unitOfWork.UserRepository.DeleteUserPaymentByOrderIdAsync(orderRequest.OrderId);
                //var oldOrderPayment = await _unitOfWork.UserRepository.GetUserPaymentBy(p => p.OrderId == orderRequest.OrderId);



                CaptainUserRejectedRequest driverRequest = new CaptainUserRejectedRequest()
                {
                    OrderId = orderRequest.OrderId,
                    UserId = orderRequest.UserId,
                    AgentId = userNewRequests.AgentId,
                    CreatedBy = 1,
                    CreationDate = DateTime.Now
                };

                var insertNewRequestResult = await _unitOfWork.UserRepository.InsertUserRejectedRequestAsync(driverRequest);
                var result = await _unitOfWork.Save();
                if (result == 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                return Ok(true);
                //////////// the new behavior of the penalties//////////////
                //var systemSetting = await _unitOfWork.SystemRepository.GetCurrent();
                //if (systemSetting != null)
                //{
                //    if (systemSetting.RejectPerTypeId == (long)RejectPer.Days)
                //    {
                //        var userRejectedRequests = await _unitOfWork.UserRepository.GetUserRejectedRequestBy(u => u.UserId == orderRequest.UserId &&
                //       u.CreationDate.Value.Day == DateTime.Now.Day &&
                //       u.CreationDate.Value.Month == DateTime.Now.Month &&
                //       u.CreationDate.Value.Year == DateTime.Now.Year);

                //        if (userRejectedRequests != null && userRejectedRequests.Count >= systemSetting.RejectRequestsNumbers)
                //        {
                //            UserRejectPenalty userRejectPenalty = new UserRejectPenalty()
                //            {
                //                UserId = orderRequest.UserId,
                //                SystemSettingId = systemSetting.Id,
                //                PenaltyStatusTypeId = (long)PenaltyStatusTypes.New,
                //                CreatedBy = 1,
                //                CreationDate = DateTime.Now

                //            };

                //            var insertUserRejectPenalty_result = await _unitOfWork.UserRepository.InsertUserRejectPenalty(userRejectPenalty);
                //            result = await _unitOfWork.Save();
                //            if (result == 0) return new ObjectResult("Server not available") { StatusCode = 707 };
                //        }

                //    }
                //    else if (systemSetting.RejectPerTypeId == (long)RejectPer.Months)
                //    {
                //       var userRejectedRequests = await _unitOfWork.UserRepository.GetUserRejectedRequestBy(u => u.UserId == orderRequest.UserId &&
                //      u.CreationDate.Value.Month == DateTime.Now.Month &&
                //      u.CreationDate.Value.Year == DateTime.Now.Year);

                //        if (userRejectedRequests != null && userRejectedRequests.Count >= systemSetting.RejectRequestsNumbers)
                //        {
                //            UserRejectPenalty userRejectPenalty = new UserRejectPenalty()
                //            {
                //                UserId = orderRequest.UserId,
                //                SystemSettingId = systemSetting.Id,
                //                PenaltyStatusTypeId = (long)PenaltyStatusTypes.New,
                //                CreatedBy = 1,
                //                CreationDate = DateTime.Now

                //            };

                //            var insertUserRejectPenalty_result = await _unitOfWork.UserRepository.InsertUserRejectPenalty(userRejectPenalty);
                //            result = await _unitOfWork.Save();
                //            if (result == 0) return new ObjectResult("Server not available") { StatusCode = 707 };
                //        }
                //    }
                //    else if (systemSetting.RejectPerTypeId == (long)RejectPer.Years)
                //    {
                //        var userRejectedRequests = await _unitOfWork.UserRepository.GetUserRejectedRequestBy(u => u.UserId == orderRequest.UserId &&

                //     u.CreationDate.Value.Year == DateTime.Now.Year);

                //        if (userRejectedRequests != null && userRejectedRequests.Count >= systemSetting.RejectRequestsNumbers)
                //        {
                //            UserRejectPenalty userRejectPenalty = new UserRejectPenalty()
                //            {
                //                UserId = orderRequest.UserId,
                //                SystemSettingId = systemSetting.Id,
                //                PenaltyStatusTypeId = (long)PenaltyStatusTypes.New,
                //                CreatedBy = 1,
                //                CreationDate = DateTime.Now

                //            };

                //            var insertUserRejectPenalty_result = await _unitOfWork.UserRepository.InsertUserRejectPenalty(userRejectPenalty);
                //            result = await _unitOfWork.Save();
                //            if (result == 0) return new ObjectResult("Server not available") { StatusCode = 707 };
                //        }
                //    }
                //}
                //////////// end - the new behavior of the penalties//////////////

                //return Ok(true);


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


                //var maounOrders =
                //    await _unitOfWork.MaounRepository.GetMaounOrderBy(o => o.SenderOrderId == id);
                //var maounOrder = maounOrders.FirstOrDefault();
                //if (maounOrder?.Id > 0)
                //{

                //    MaounRequestOrder maounRequestOrder =
                //        await MaounUtility.UpdateMaounOrderStatus((long)maounOrder.MaounOrderId, MaounStatus.Pickup_Completed_By_Driver);

                //    if (maounRequestOrder == null) return new ObjectResult("Maoun ordering server not available") { StatusCode = 707 };

                //}




                OrderCurrentStatus orderCurrentStatus = new OrderCurrentStatus()
                {
                    OrderId = id,
                    StatusTypeId = (long)OrderStatusTypes.PickedUp,
                    IsCurrent = true,

                };
                var order = await _unitOfWork.OrderRepository.GetOnlyOrderByIdAsync(id);
                var orderAssgined = await _unitOfWork.OrderRepository.GetOrdersAssignmentsByAsync(a => a.OrderId == id);



                if (order?.OrderItems == null || order?.OrderItems.Count <= 0)
                {
                    order.OrderItems = await _unitOfWork.OrderRepository.GetOrdersItemsByAsync(o => o.OrderId == order.Id);
                }

                decimal order_amount = order.OrderItems.Select(i => i.Price).Sum().GetValueOrDefault();
                // decimal order_amount = 0;
                // foreach (OrderItem item in order.OrderItems)
                // {
                //     order_amount = +(decimal)item.Price;
                // }

                Bookkeeping bookkeeping = new Bookkeeping()
                {
                    OrderId = order.Id,
                    UserId = orderAssgined.FirstOrDefault().UserId,
                    DepositTypeId = (long)DepositTypes.Order_Items_Amount,
                    CreationDate = DateTime.Now

                };

                // chechk if the customer already paid the order items or not
                if (order?.PaymentTypeId == (long)PaymentTypes.Paid)
                {
                    bookkeeping.Value = order_amount * -1;
                }
                else if (order?.PaymentTypeId == (long)PaymentTypes.Cash)
                {
                    bookkeeping.Value = order_amount;
                }

                var userId = orderAssgined.FirstOrDefault().UserId;
                var userCurrentLocation = await _unitOfWork.UserRepository.GetUsersCurrentLocationsByAsync(l => l.UserId == userId);
                var currentLocation = userCurrentLocation.FirstOrDefault();

                OrderStartLocation orderStartLocation = new OrderStartLocation()
                {
                    OrderId = id,
                    OrderAssignId = (long)orderAssgined.FirstOrDefault().Id,
                    PickedupLat = currentLocation.Lat,
                    PickedupLong = currentLocation.Long,
                    CreationDate = DateTime.Now

                };

                var updatedOrder = await _unitOfWork.OrderRepository.UpdateOrderCurrentStatusAsync(order.Id, (long)OrderStatusTypes.PickedUp);
                var insertBookkeepingResult = await _unitOfWork.PaymentRepository.InsertBookkeepingAsync(bookkeeping);
                var insertOrderStartLocationResult = await _unitOfWork.OrderRepository.InsertOrderStartLocationAsync(orderStartLocation);
                var insertOrderStatusResult = await _unitOfWork.OrderRepository.InsertOrderStatusAsync(orderCurrentStatus);
                var result = await _unitOfWork.Save();
                if (result == 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };
                
                _ = _notify.NotifyOrderStatusChanged(OrderStatusTypes.PickedUp, order.Id,(long) order.AgentId);

                return Ok(true);
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


                //var maounOrders =
                //    await _unitOfWork.MaounRepository.GetMaounOrderBy(o => o.SenderOrderId == id);
                //var maounOrder = maounOrders.FirstOrDefault();
                //if (maounOrder?.Id > 0)
                //{

                //    MaounRequestOrder maounRequestOrder =
                //        await MaounUtility.UpdateMaounOrderStatus((long)maounOrder.MaounOrderId, MaounStatus.Delivery_Completed_By_Driver);

                //    if (maounRequestOrder == null) return new ObjectResult("Maoun ordering server not available") { StatusCode = 707 };

                //}




                var orderAssgined = await _unitOfWork.OrderRepository.GetOrdersAssignmentsByAsync(a => a.OrderId == id);
                var userId = orderAssgined.FirstOrDefault().UserId;
                var userCurrentLocation = await _unitOfWork.UserRepository.GetUsersCurrentLocationsByAsync(l => l.UserId == userId);
                var currentLocation = userCurrentLocation.FirstOrDefault();
                var userPayments = await _unitOfWork.UserRepository.GetUsersPaymentsByAsync(p => p.OrderId == id);
                var userPayment = userPayments.FirstOrDefault();
                userPayment.StatusId = (long)PaymentStatusTypes.Complete;
               
                OrderEndLocation orderEndLocation = new OrderEndLocation()
                {
                    OrderId = id,
                    OrderAssignId = orderAssgined.FirstOrDefault().Id,
                    DroppedLat = currentLocation.Lat,
                    DroppedLong = currentLocation.Long,
                    CreationDate = DateTime.Now

                };
                //var order = await _unitOfWork.OrderRepository.GetOrderByID(id);


                OrderCurrentStatus orderCurrentStatus = new OrderCurrentStatus()
                {
                    OrderId = id,
                    StatusTypeId = (long)OrderStatusTypes.Dropped,
                    IsCurrent = true

                };

                OrderCurrentStatus orderDeliveredCurrentStatus = new OrderCurrentStatus()
                {
                    OrderId = id,
                    StatusTypeId = (long)OrderStatusTypes.Delivered,
                    IsCurrent = true

                };

                CaptainUserCurrentStatus userCurrentStatus = new CaptainUserCurrentStatus()
                {
                    UserId = userId,
                    StatusTypeId = (long)StatusTypes.Ready,
                    IsCurrent = true
                };


                //if (order.PaymentTypeId == (long)PaymentTypes.Paid) {

                //}

                //delete the order items amount from captain wallet
                var order_items_amount_bookkeeping = await _unitOfWork.PaymentRepository.GetBookkeepingByAsync(b => b.OrderId == id && b.DepositTypeId == (long)DepositTypes.Order_Items_Amount);
                if (order_items_amount_bookkeeping?.FirstOrDefault()?.Id > 0)
                {
                    var bookkeeping_id = order_items_amount_bookkeeping?.FirstOrDefault()?.Id;
                    var deleteBookkeepingResult = await _unitOfWork.PaymentRepository.DeleteBookkeepingAsync((long)bookkeeping_id);
                }

                // add the delivery amount to the captain wallet
                Bookkeeping delivery_bookkeeping = new Bookkeeping()
                {
                    OrderId = id,
                    UserId = userId,
                    DepositTypeId = (long)DepositTypes.Delivery_Amount,
                    Value = userPayment.Value,
                    CreationDate = DateTime.Now
                };

                var updatedOrder = await _unitOfWork.OrderRepository.UpdateOrderCurrentStatusAsync(id, (long)OrderStatusTypes.Delivered);
                var insertedDeliveryBookkeeping = await _unitOfWork.PaymentRepository.InsertBookkeepingAsync(delivery_bookkeeping);
                var insertPaymentResult = await _unitOfWork.UserRepository.UpdateUserPaymentAsync(userPayment);
                var insertOrderEndLocationResult = await _unitOfWork.OrderRepository.InsertOrderEndLocationAsync(orderEndLocation);
                var insertUserStatusResult = await _unitOfWork.UserRepository.InsertUserCurrentStatusAsync(userCurrentStatus);
                var insertOrderStatusResult = await _unitOfWork.OrderRepository.InsertOrderStatusAsync(orderCurrentStatus);
                var oldRunningOrder = await _unitOfWork.OrderRepository.DeleteRunningOrderByOrderIdAsync(id);
                var insertOrderDeliveredStatusResult = await _unitOfWork.OrderRepository.InsertOrderStatusAsync(orderDeliveredCurrentStatus);
                var result = await _unitOfWork.Save();
				if (result == 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                var order = await _unitOfWork.OrderRepository.GetOnlyOrderByIdAsync(id);
                _ = _notify.NotifyOrderStatusChanged(OrderStatusTypes.Dropped, order.Id, (long)order.AgentId);

                var _newnotify = new NotifyService(_serviceProvider);
                _ = Task.Run(async () => {
                    Task.Delay(TimeSpan.FromSeconds(10));
                    _ = await _newnotify.ChangeOrderStatusAndNotify(OrderStatusTypes.Delivered, order.Id, (long)order.AgentId);
                });


                return Ok(true);
                // /// Check Bonus
                // var userOrdersAssignedPerDay = await _unitOfWork.OrderRepository.GetOrderAssignmentBy(a => a.UserId == userId &&
                //                          a.CreationDate.Value.Date == DateTime.Now.Date);

                // var orderIds = userOrdersAssignedPerDay.Select(a => a.OrderId).ToList();
                // var ordersStatus = await _unitOfWork.OrderRepository.GetOrderStatusBy(s => orderIds.Contains(s.OrderId) &&
                //(s.StatusTypeId == (long)OrderStatusTypes.Dropped));
                // var ordersCount = ordersStatus.Count();
                // var userCountryId = orderAssgined.FirstOrDefault().User.ResidenceCountryId;
                // var bonusPerCountry = await _unitOfWork.UserRepository.GetBonusByCountry(userCountryId);
                // if (ordersCount >= bonusPerCountry.OrdersPerDay)
                // {
                //     var userBonus = new UserBonus
                //     {
                //         UserId = userId,
                //         BonusTypeId = (long)BonusTypes.BonusPerDay,
                //         CreationDate = DateTime.Now,
                //         Amount = bonusPerCountry.BonusPerDay
                //     };
                //     var insertedBonus = await _unitOfWork.UserRepository.InsertBonus(userBonus);
                // }
                // // add the delivery amount to the captain wallet
                // Bookkeeping deliveryBonus_bookkeeping = new Bookkeeping()
                // {
                //     OrderId = id,
                //     UserId = userId,
                //     DepositTypeId = (long)DepositTypes.Bonus_Amount,
                //     Value = bonusPerCountry.BonusPerDay,
                //     CreationDate = DateTime.Now
                // };


                // var insertedDeliveryBonusBookkeeping = await _unitOfWork.PaymentRepository.InsertBookkeeping(deliveryBonus_bookkeeping);
                // var result2 = await _unitOfWork.Save();
                // if (result2 == 0) return new ObjectResult("Server not available") { StatusCode = 707 };

                // /// Check Bonus

                //return Ok(true);
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


                //var maounOrders =
                //    await _unitOfWork.MaounRepository.GetMaounOrderBy(o => o.SenderOrderId == id);
                //var maounOrder = maounOrders.FirstOrDefault();
                //if (maounOrder?.Id > 0)
                //{

                //    MaounRequestOrder maounRequestOrder =
                //        await MaounUtility.UpdateMaounOrderStatus((long)maounOrder.MaounOrderId, MaounStatus.Delivery_Completed_By_Driver);

                //    if (maounRequestOrder == null) return new ObjectResult("Maoun ordering server not available") { StatusCode = 707 };

                //}


                var order = await _unitOfWork.OrderRepository.GetOnlyOrderByIdAsync(id);
                if(order == null || order.Id <= 0) return NoContent();

                var ordersAssgined = await _unitOfWork.OrderRepository.GetOrdersAssignmentsByAsync(a => a.OrderId == id);
                var orderAssgined = ordersAssgined?.FirstOrDefault();

                if (orderAssgined != null && orderAssgined.Id > 0)
                {
                    var userId = orderAssgined.UserId;
                    var userCurrentLocation = await _unitOfWork.UserRepository.GetUsersCurrentLocationsByAsync(l => l.UserId == userId);
                    var currentLocation = userCurrentLocation.FirstOrDefault();
                    var userPayments = await _unitOfWork.UserRepository.GetUsersPaymentsByAsync(p => p.OrderId == id);
                    var userPayment = userPayments.FirstOrDefault();
                    if (userPayment != null) 
                    {
                        userPayment.StatusId = (long)PaymentStatusTypes.Canclled;
                        // add the delivery amount to the captain wallet
                        Bookkeeping delivery_bookkeeping = new Bookkeeping()
                        {
                            OrderId = id,
                            UserId = userId,
                            DepositTypeId = (long)DepositTypes.Delivery_Amount,
                            Value = userPayment.Value,
                            CreationDate = DateTime.Now
                        };

                        var insertedDeliveryBookkeeping = await _unitOfWork.PaymentRepository.InsertBookkeepingAsync(delivery_bookkeeping);
                        var insertPaymentResult = await _unitOfWork.UserRepository.UpdateUserPaymentAsync(userPayment);
                    }
               
                    OrderEndLocation orderEndLocation = new OrderEndLocation()
                    {
                        OrderId = id,
                        OrderAssignId = orderAssgined.Id,
                        DroppedLat = currentLocation.Lat,
                        DroppedLong = currentLocation.Long,
                        CreationDate = DateTime.Now

                    };
                    
                    CaptainUserCurrentStatus userCurrentStatus = new CaptainUserCurrentStatus()
                    {
                        UserId = userId,
                        StatusTypeId = (long)StatusTypes.Ready,
                        IsCurrent = true
                    };
                    
                    
                    //delete the order items amount from captain wallet
                    var order_items_amount_bookkeeping = await _unitOfWork.PaymentRepository.GetBookkeepingByAsync(b => b.OrderId == id && b.DepositTypeId == (long)DepositTypes.Order_Items_Amount);
                    if (order_items_amount_bookkeeping?.FirstOrDefault()?.Id > 0)
                    {
                        var bookkeeping_id = order_items_amount_bookkeeping?.FirstOrDefault()?.Id;
                        var deleteBookkeepingResult = await _unitOfWork.PaymentRepository.DeleteBookkeepingAsync((long)bookkeeping_id);
                    }

                    
                    var insertOrderEndLocationResult = await _unitOfWork.OrderRepository.InsertOrderEndLocationAsync(orderEndLocation);
                    var insertUserStatusResult = await _unitOfWork.UserRepository.InsertUserCurrentStatusAsync(userCurrentStatus);
                }

                
                OrderCurrentStatus orderCurrentStatus = new OrderCurrentStatus()
                {
                    OrderId = id,
                    StatusTypeId = (long)OrderStatusTypes.Canceled,
                    IsCurrent = true

                };
                

                var updatedOrder = await _unitOfWork.OrderRepository.UpdateOrderCurrentStatusAsync(id, (long)OrderStatusTypes.Canceled);
                var insertOrderStatusResult = await _unitOfWork.OrderRepository.InsertOrderStatusAsync(orderCurrentStatus);
                var oldRunningOrder = await _unitOfWork.OrderRepository.DeleteRunningOrderByOrderIdAsync(id);
                var result = await _unitOfWork.Save();
				if (result == 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };
                
                _ = _notify.NotifyOrderStatusChanged(OrderStatusTypes.Canceled, order.Id,(long) order.AgentId);

                if (orderAssgined != null && orderAssgined.Id > 0)
                {
                    _ = _notify.SendGoogleCloudMessageToCaptain((long)orderAssgined.UserId, "cancelRequest", order.Id.ToString());   
                }
                    

                return Ok(true);
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

                /* var result = await _orderService.AddNewOrder(order);
                 if (result == null) return NoContent();

                 return Ok(result);*/


                if (order == null || order.PickupLocationLat == null || order.PickupLocationLong == null ||
                    order.PickupLocationLat == "" || order.PickupLocationLong == "")
                    return NoContent(); // new ObjectResult("Your request has no data") { StatusCode = 406 };


                if (order.AgentId is null || order.AgentId <= 0)
                {
                    long agentId = -1;
                    string userType = "";
                    Utility.getRequestUserIdFromToken(HttpContext, out agentId, out userType);
                    order.AgentId = agentId;
                }

                order.CurrentStatus = (long)OrderStatusTypes.New;
                var orderInsertResult = await _unitOfWork.OrderRepository.InsertOrderAsync(order);
                var result = await _unitOfWork.Save();
                if (result <= 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };


                OrderCurrentStatus orderCurrentStatus = new OrderCurrentStatus()
                {
                    OrderId = orderInsertResult.Id,
                    StatusTypeId = (long)OrderStatusTypes.New,
                    IsCurrent = true
                };
                var insertResult = await _unitOfWork.OrderRepository.InsertOrderStatusAsync(orderCurrentStatus);
                var resultSecondeOperation = await _unitOfWork.Save();
                if (resultSecondeOperation <= 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };


                _ = await _notify.NotifyOrderStatusChanged(OrderStatusTypes.New, orderInsertResult.Id,
                    (long)orderInsertResult.AgentId);


                _ = _orderService.SearchForCaptainAndNotifyOrder(orderInsertResult);

                return Ok(new
                {
                    OrderNumber = orderInsertResult.Id,
                    OrderStatus = "New",
                    Message = "Order saved and starting search for near captain"
                });


            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }





        /*//[AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Order order, [FromQuery] string CouponCode)
        {
            try
            {

                if (order == null || order.PickupLocationLat == null || order.PickupLocationLong == null ||
                    order.PickupLocationLat == "" || order.PickupLocationLong == "")
                    return new ObjectResult("Your request has no data") {StatusCode = 406};


                if (order.AgentId is null || order.AgentId <= 0)
                {
                    long agentId = -1;
                    string userType = "";
                    Utility.getRequestUserIdFromToken(HttpContext, out agentId, out userType);
                    order.AgentId = agentId;
                }

                var orderInsertResult = await _unitOfWork.OrderRepository.InsertOrder(order);
                var result = await _unitOfWork.Save();
                if (result <= 0) return new ObjectResult("Server not available") {StatusCode = 503};

                _ = _notify.ChangeOrderStatusAndNotify(OrderStatusTypes.New, orderInsertResult.Id,
                    (long) orderInsertResult.AgentId);


                var captain =
                    await _unitOfWork.UserRepository.GetUserNearestLocation(order.PickupLocationLat,
                        order.PickupLocationLong);
                if (captain is null)
                {
                    _ = _notify.ChangeOrderStatusAndNotify(OrderStatusTypes.NotAssignedToCaptain,
                        orderInsertResult.Id,
                        (long) orderInsertResult.AgentId);

                    return Ok(new
                    {
                        OrderNumber = orderInsertResult.Id,
                        OrderStatus = "Not Assigned To captain",
                        Message = "No Captains available"
                    });
                }





                UserNewRequest driverRequest = new UserNewRequest()
                {
                    OrderId = orderInsertResult.Id,
                    UserId = captain.Id,
                    AgentId = orderInsertResult.AgentId,
                    CreatedBy = 1,
                    CreationDate = DateTime.Now
                };

                var insertNewRequestResult = await _unitOfWork.UserRepository.InsertUserNewRequest(driverRequest);
                result = await _unitOfWork.Save();
                if (result <= 0) return new ObjectResult("Server not available") {StatusCode = 503};

                var usersMessageHub =
                    await _unitOfWork.UserRepository.GetUserMessageHubBy(u => u.UserId == captain.Id);
                var userMessageHub = usersMessageHub.FirstOrDefault();
                if (userMessageHub != null && userMessageHub.Id > 0)
                {
                    var notificationReuslt = Utility.SendFirebaseNotification(_hostingEnvironment, "newRequest",
                        orderInsertResult.Id.ToString(), userMessageHub.ConnectionId);
                    if (notificationReuslt == "")
                        return new ObjectResult("No Captains available, Please try again") {StatusCode = 406};
                }


                int time = 1000;
                Timer timer1 = new Timer
                {
                    Interval = 1000 // one second
                };
                timer1.Enabled = true;
                timer1.Elapsed += new ElapsedEventHandler((sender, elapsedEventArgs) => { time = time + 1000; });



                bool isDriverAcceptOrder = false;
                bool isDriverRejectOrder = false;
                bool isDriverIgnoredOrder = false;

                while (isDriverAcceptOrder == false && isDriverRejectOrder == false &&
                       isDriverIgnoredOrder == false && time <= 40000)
                {
                    var orderAssigns =
                        await _unitOfWork.OrderRepository.GetOrderAssignmentBy(r =>
                            r.OrderId == orderInsertResult.Id);
                    var orderAssign = orderAssigns.FirstOrDefault();
                    if (orderAssign != null)
                        isDriverAcceptOrder = true;

                    var ordersReject =
                        await _unitOfWork.UserRepository.GetUserRejectedRequestBy(r =>
                            r.OrderId == orderInsertResult.Id);
                    var rejectedOrder = ordersReject.FirstOrDefault();
                    if (rejectedOrder != null)
                        isDriverRejectOrder = true;


                    var ordersIgnored =
                        await _unitOfWork.UserRepository.GetUserIgnoredRequestBy(r =>
                            r.OrderId == orderInsertResult.Id);
                    var ignoredOrder = ordersIgnored.FirstOrDefault();
                    if (ignoredOrder != null)
                        isDriverIgnoredOrder = true;

                }

                timer1.Enabled = false;


                if (isDriverAcceptOrder) // case captain accept the request
                {
                    *//* Create QrCode and Insert*//*
                    var qRCode = Utility.CreateQRCode(captain.Id, orderInsertResult.Id);
                    var qRCodeResult = await _unitOfWork.OrderRepository.InsertQrCode(qRCode);
                    *//* Create QrCode and Insert*//*
                    result = await _unitOfWork.Save();
                    if (result <= 0) return new ObjectResult("Server not available") {StatusCode = 503};


                    _ = _notify.ChangeOrderStatusAndNotify(OrderStatusTypes.AssignedToCaptain,
                        orderInsertResult.Id, (long) orderInsertResult.AgentId);
                    return Ok(new
                    {
                        OrderNumber = orderInsertResult.Id,
                        OrderStatus = "Assigned To Captain",
                        CaptainName = captain.FirstName + " " + captain.FamilyName,
                        CaptainMobile = captain.Mobile,
                        QRCodeInBytes = qRCodeResult.Code,
                        QRCodeUrl = qRCodeResult.QrCodeUrl
                    });
                }

                // case captain reject or ignored the request, or didn't received the request because of firebase failure and the timeout passed
                _ = _notify.ChangeOrderStatusAndNotify(OrderStatusTypes.NotAssignedToCaptain, orderInsertResult.Id,
                    (long) orderInsertResult.AgentId);
                return Ok(new
                {
                    OrderNumber = orderInsertResult.Id,
                    OrderStatus = "Not Assigned To captain",
                    Message = "No Captains available"
                });

            }
            catch (Exception e)
            {
                return new ObjectResult(e.Message) {StatusCode = 666};
            }
        }

*/










        // //[AllowAnonymous]
        // [HttpPost]
        // public async Task<IActionResult> Post([FromBody] Order order, [FromQuery] string CouponCode)
        // {
        //     try
        //     {
        //
        //
        //         //if (User.FindFirstValue(ClaimTypes.NameIdentifier) != "")
        //         //{
        //         //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //         //    order.AgentId = long.Parse(userId);
        //         //}
        //
        //         //order.AgentId = 1;
        //         if (order == null || order.PickupLocationLat == null || order.PickupLocationLong == null ||
        //             order.PickupLocationLat == "" || order.PickupLocationLong == "")
        //             return new ObjectResult("Your request has no data") { StatusCode = 406 };
        //
        //
        //         var driver = await _unitOfWork.UserRepository.GetUserNearestLocation(order.PickupLocationLat, order.PickupLocationLong);
        //
        //         if (driver == null) return new ObjectResult("No Captains available, Plase try again") { StatusCode = 707 };
        //
        //
        //         if (order.AgentId == null || order.AgentId <= 0)
        //         {
        //             long agentId = -1;
        //             string userType = "";
        //             Utility.getRequestUserIdFromToken(HttpContext, out agentId, out userType);
        //             order.AgentId = agentId;
        //         }
        //
        //
        //         OrderCurrentStatus orderStatus = new OrderCurrentStatus()
        //         {
        //             StatusTypeId = (long)OrderStatusTypes.New,
        //
        //             IsCurrent = true,
        //             CreatedBy = 1,
        //             CreationDate = DateTime.Now
        //         };
        //
        //         var insertResullt = await _unitOfWork.OrderRepository.InsertOrder(order);
        //
        //         order.OrderCurrentStatus.Add(orderStatus);
        //         var result = await _unitOfWork.Save();
        //         if (result == 0) return new ObjectResult("Server not available") { StatusCode = 707 };
        //
        //
        //         UserNewRequest driverRequest = new UserNewRequest()
        //         {
        //             OrderId = insertResullt.Id,
        //             UserId = driver.Id,
        //             AgentId = insertResullt.AgentId,
        //             CreatedBy = 1,
        //             CreationDate = DateTime.Now
        //         };
        //
        //         /* Create QrCode and Insert*/
        //         var qRCode = Utility.CreateQRCode(driver.Id, order.Id);
        //         var qRCodeResult = await _unitOfWork.OrderRepository.InsertQrCode(qRCode);
        //         /* Create QrCode and Insert*/
        //         var insertNewRequestResult = await _unitOfWork.UserRepository.InsertUserNewRequest(driverRequest);
        //         result = await _unitOfWork.Save();
        //         if (result == 0) return new ObjectResult("Server not available") { StatusCode = 707 };
        //
        //         var usersMessageHub = await _unitOfWork.UserRepository.GetUserMessageHubBy(u => u.UserId == driver.Id);
        //         var userMessageHub = usersMessageHub.FirstOrDefault();
        //         if (userMessageHub != null && userMessageHub.Id > 0)
        //         {
        //             //OneSignalAPI.SendMessage("newRequest", insertResullt.Id.ToString(),new string[] { userMessageHub.ConnectionId });
        //             //PushyAPI.SendPushMessage(userMessageHub.ConnectionId, "newRequest", insertResullt.Id.ToString(), false);
        //             var notificationReuslt = Utility.SendFirebaseNotification(_hostingEnvironment, "newRequest", insertResullt.Id.ToString(), userMessageHub.ConnectionId);
        //             //await _HubContext.Clients.Client(userMessageHub.ConnectionId).SendAsync("newRequest", insertResullt.Id.ToString());
        //
        //             if (notificationReuslt == "") return new ObjectResult("Drivers not available, Plase try again") { StatusCode = 707 };
        //
        //         }
        //
        //
        //         await Task.Delay(TimeSpan.FromSeconds(30)).ConfigureAwait(false);
        //
        //         var orderAssigns = await _unitOfWork.OrderRepository.GetOrderAssignmentBy(r => r.OrderId == insertResullt.Id);
        //         var orderAssign = orderAssigns.FirstOrDefault();
        //         if (orderAssign == null)
        //         {
        //             var deletedOrderStatuse = await _unitOfWork.OrderRepository.DeleteOrderStatus(order.OrderCurrentStatus.FirstOrDefault().Id);
        //             var deletedResullt = await _unitOfWork.OrderRepository.DeleteOrderPermenet(insertResullt.Id);
        //             var deleteQRCode = await _unitOfWork.OrderRepository.DeleteQrCode(qRCodeResult.Id);
        //             result = await _unitOfWork.Save();
        //             if (result == 0) return new ObjectResult("Server not available") { StatusCode = 707 };
        //
        //             return new ObjectResult("Captain not available, Plase try again") { StatusCode = 707 };
        //         }
        //
        //         var user = await _unitOfWork.UserRepository.GetUserByID((long)orderAssign.UserId);
        //
        //         //var user = await _unitOfWork.UserRepository.GetUserByID((long)driver.Id);
        //         //double customerDistance = Utility.distance(double.Parse(order.PickupLocationLat), double.Parse(order.PickupLocationLong),
        //         //             double.Parse(order.DropLocationLat), double.Parse(order.DropLocationLong));
        //         //var countriesPricies = await _unitOfWork.CountryRepository.GetCountryPriceBy(c => c.CountryId == user.ResidenceCountryId);
        //         //var countryPrice = countriesPricies.FirstOrDefault();
        //
        //         var statusType = await _unitOfWork.OrderRepository.GetOrderStatusTypeByID((long)OrderStatusTypes.New);
        //         var notifyStatus = new
        //         {
        //             order_status = new { Status = statusType.Status, ArabicStatus = statusType.ArabicStatus },
        //             order_id = order.Id
        //         };
        //         var isRegistered = await _unitOfWork.HookRepository.GetHookByAgent(order.AgentId, (long)WebHookTypes.OrderStatus);
        //         if (isRegistered != null)
        //         {
        //
        //             Utility.ExecuteWebHook(isRegistered.Url, Utility.ConvertToJson(notifyStatus));
        //
        //         }
        //         var message = insertResullt.Id.ToString();
        //         await _HubContext.Clients.All.SendAsync("NewOrderNotify", message);
        //
        //         //var agent = await _unitOfWork.AgentRepository.GetByID((long)insertResullt.AgentId);
        //         //var countryId = agent.CountryId;
        //         ////var coupon = _unitOfWork.AgentRepository.GetAgentCoupon((long)order.AgentId, insertResullt.Id);
        //         //var coupon = await _unitOfWork.AgentRepository.GetCouponByCode(CouponCode);
        //         //var isValid = await _unitOfWork.AgentRepository.IsValidCoupon(CouponCode, order.AgentId, countryId);
        //         //if (isValid == true)
        //         //{
        //
        //         //    var couponUsage = new CouponUsage
        //         //    {
        //         //        AgentId = order.AgentId,
        //         //        OrderId = insertResullt.Id,
        //         //        CouponId = coupon.Id,
        //         //        UsageDate = DateTime.Now
        //
        //         //    };
        //         //    var insertedAppliedCoupon = await _unitOfWork.AgentRepository.InsertCouponUsage(couponUsage);
        //         //    var appliedCoupon = await _unitOfWork.Save();
        //         //    if (appliedCoupon == 0) return new ObjectResult("Server not available") { StatusCode = 707 };
        //
        //
        //         //}
        //
        //
        //
        //         return Ok(new
        //         {
        //             OrderNumber = insertResullt.Id,
        //             CaptainName = user.FirstName + " " + user.FamilyName,
        //             CaptainMobile = user.Mobile,
        //             QRCodeInBytes = qRCodeResult.Code,
        //             QRCodeUrl = qRCodeResult.QrCodeUrl
        //         });
        //
        //
        //         //return Ok("done");
        //
        //
        //         //var DriversIgnored = await _unitOfWork.UserRepository.GetUserIgnoredRequestBy( u => u.OrderId == order.Id );
        //         //var list = DriversIgnored.Select(u => u.Id).ToList();
        //         //var agentCoordinate = new Geo(0, order.PickupLocationLat, order.PickupLocationLong);
        //
        //         //var drivers = await _unitOfWork.UserRepository.GetAllUsersCurrentLocations();
        //         //var nearst = drivers.Select(u => new Geo((long)u.UserId, u.Lat, u.Long)).Where( u => !list.Contains(u.UserID)  ).OrderBy(g => g.GetDistanceTo(agentCoordinate)).First();
        //
        //
        //
        //         //var agentCoordinate = new GeoCoordinate(double.Parse(order.PickupLocationLat), double.Parse(order.PickupLocationLong));
        //
        //         //var drivers = await _unitOfWork.UserRepository.GetAllUsersCurrentLocations();
        //         //var nearst = drivers.Select(u => new GeoCoordinate(double.Parse(u.Lat), double.Parse(u.Long))).OrderBy(g => g.GetDistanceTo(agentCoordinate)).First();
        //
        //
        //
        //
        //
        //         //var nearest = locations.Select(x => new GeoCoordinate(x.Latitude, x.Longitude))
        //         //                       .OrderBy(x => x.GetDistanceTo(coord))
        //         //                       .First();
        //
        //     }
        //     catch (Exception e)
        //     {
        //         return new ObjectResult(e.Message) { StatusCode = 666 };
        //     }
        // }

        // PUT: Order/5
        
        
        
        
       /* [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }*/

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Order))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var resullt = await _unitOfWork.OrderRepository.DeleteOrderAsync(id);
                return Ok(resullt);
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
                var insertResullt = await _unitOfWork.OrderRepository.InsertOrderInvoiceAsync(orderInvoice);
                var result = await _unitOfWork.Save();
                if (result == 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                return Ok(insertResullt);

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
                var insertResullt = await _unitOfWork.OrderRepository.InsertPaidOrderAsync(paidOrder);
                var result = await _unitOfWork.Save();
                if (result == 0) return new ObjectResult("Service Unavailable") { StatusCode = 707 };

                return Ok(insertResullt);

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


                var orderStatuses =
                    await _unitOfWork.OrderRepository.GetOrderCurrentStatusesByAsync(s => s.OrderId == id && s.IsCurrent == true);
                var orderStatus = orderStatuses.FirstOrDefault();
                if (orderStatus == null || orderStatus?.StatusTypeId == (long)OrderStatusTypes.Dropped)
                    return NotFound();

                var orderAssigns = await _unitOfWork.OrderRepository.GetOrdersAssignmentsByAsync(a => a.OrderId == id);
                var orderAssign = orderAssigns.FirstOrDefault();
                if (orderAssign == null) return NoContent();

                var usersLocations = await _unitOfWork.UserRepository.GetUsersCurrentLocationsByAsync(u => u.UserId == orderAssign.UserId);
                var userLocation = usersLocations.FirstOrDefault();
                if (userLocation == null) return NoContent();

                return Ok(new { Latitude = userLocation.Lat, Longitude = userLocation.Long });
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
                var orderItems = await _unitOfWork.OrderRepository.GetOrdersItemsByAsync(i => i.OrderId == id);
                if (orderItems == null) return NoContent();

                return Ok(orderItems);

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
                var qRCode = await _unitOfWork.OrderRepository.GetQrcodeByOrderIdAsync(id);
                if(qRCode == null)
				{
                    return NotFound();
				}
                qRCode.QrCodeUrl = Utility.ConvertImgToString(qRCode.Code);
              
                return Ok(new { CodeInBytes = qRCode.Code, QrCodeUrl = qRCode.QrCodeUrl });
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

                var taskResult = await Task.Run(() => {
                    // 1-Check Role and Id
                    var userType = "";
                    var userId = long.Parse("0");
                    Utility.getRequestUserIdFromToken(HttpContext, out userId, out userType);

                    IQueryable<Order> query;

                    if (userType == "Driver")
                    {
                        query = _unitOfWork.OrderRepository.GetUserAcceptedRequestByQuerable(u => u.UserId == userId).Select(o => o.Order);
                    }
                    else if (userType == "Agent")
                    {
                        query = _unitOfWork.OrderRepository.GetByQuerable(o => o.AgentId == userId);
                    }
                    else
                    {
                        query = _unitOfWork.OrderRepository.GetAllOrdersQuerable();
                    }

                    var ordersResult = Utility.GetFilter(reportParameters, query);
                    var orders = this.mapper.Map<List<OrderResponse>>(ordersResult);
                    var total = orders.Count();

                    return new { Orders = orders, Total = total };
                });

                return Ok(taskResult);

            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }
        ///* Get Orders Reports */
        ///* Search */
        //[HttpGet("Search")]
        //public async Task<IActionResult> Search([FromQuery] FilterParameters parameters)
        //{
        //    try
        //    {
        //        var query = _unitOfWork.OrderRepository.GetAllOrdersQuerable();
        //        //var query = _unitOfWork.OrderRepository.GetAllOrdersDetailsQuerable();
        //        var skip = (parameters.NumberOfObjectsPerPage * (parameters.Page - 1));
        //        var take = parameters.NumberOfObjectsPerPage;
        //        var totalResult = 0;
        //        //var ordersResult = Utility.GetFilter3<OrderDetails>(parameters, query, skip, take, out totalResult);
        //        var ordersResult = Utility.GetFilter2<Order>(parameters, query, skip, take,out totalResult);
        //        var orders = ordersResult.ToList();
        //        //var orders = this.mapper.Map<List<OrderResponse>>(ordersResult);
        //        //var orders = this.mapper.Map<List<OrderResponse>>(ordersResult.ToList());

        //        var totalPages = (int)Math.Ceiling(totalResult / (double)parameters.NumberOfObjectsPerPage);
        //        //foreach (var order in orders)
        //        //{
        //        //    if (order.Code.Length != 0)
        //        //    {
        //        //        order.QRCodeUrl = Utility.ConvertImgToString(order.Code);
        //        //    }


        //        //}

        //        //return Ok(new { Orders = orders, TotalResult = totalResult, Page = parameters.Page, TotalPages = totalPages });
        //        return Ok(new { result = orders, TotalResult = totalResult, Page = parameters.Page, TotalPages = totalPages });
        //    }
        //    catch (Exception e)
        //    {
        //        return new ObjectResult(e.Message) { StatusCode = 666 };
        //    }
        //}
        ///**/


        // /* Get Orders Reports */
        // /* Search */
        // [HttpGet("Search")]
        // public async Task<IActionResult> Search([FromQuery] FilterParameters parameters)
        // {
        //     try
        //     {
        //         var query = _unitOfWork.OrderRepository.GetAllOrdersQuerable();
        //         var skip = (parameters.NumberOfObjectsPerPage * (parameters.Page - 1));
        //         var take = parameters.NumberOfObjectsPerPage;
        //         var totalResult = 0;
        //         var ordersResult = Utility.GetFilter2<Order>(parameters, query, skip, take, out totalResult);
        //         var orders = this.mapper.Map<List<OrderResponse>>(ordersResult.ToList());
        //
        //         var totalPages = (int)Math.Ceiling(totalResult / (double)parameters.NumberOfObjectsPerPage);
        //         foreach (var order in orders)
        //         {
        //             if (order.Code.Length != 0)
        //             {
        //                 order.QRCodeUrl = Utility.ConvertImgToString(order.Code);
        //             }
        //
        //
        //         }
        //
        //         return Ok(new { Orders = orders, TotalResult = totalResult, Page = parameters.Page, TotalPages = totalPages });
        //     }
        //     catch (Exception e)
        //     {
        //         return new ObjectResult(e.Message) { StatusCode = 666 };
        //     }
        // }
        // /**/


        /* SearchDetails */
        [HttpGet("SearchDetails")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> SearchDetails([FromQuery] FilterParameters parameters)
        {
            try
            {


                var taskResult = await Task.Run(() => {

                    //var query = _unitOfWork.OrderRepository.GetAllOrdersQuerable();
                    var query = _unitOfWork.OrderRepository.GetAllOrdersDetailsQuerable();
                    var skip = (parameters.NumberOfObjectsPerPage * (parameters.Page - 1));
                    var take = parameters.NumberOfObjectsPerPage;
                    var totalResult = 0;
                    var ordersResult = Utility.GetFilter3<OrderDetails>(parameters, query, skip, take, out totalResult);
                    //var ordersResult = Utility.GetFilter2<Order>(parameters, query, skip, take,out totalResult);
                    var orders = ordersResult.ToList();
                    //var orders = this.mapper.Map<List<OrderResponse>>(ordersResult);
                    //var orders = this.mapper.Map<List<OrderResponse>>(ordersResult.ToList());

                    var totalPages = (int)Math.Ceiling(totalResult / (double)parameters.NumberOfObjectsPerPage);
                    //foreach (var order in orders)
                    //{
                    //    if (order.Code.Length != 0)
                    //    {
                    //        order.QRCodeUrl = Utility.ConvertImgToString(order.Code);
                    //    }


                    //}

                    //return Ok(new { Orders = orders, TotalResult = totalResult, Page = parameters.Page, TotalPages = totalPages });
                    return new { result = orders, TotalResult = totalResult, Page = parameters.Page, TotalPages = totalPages };
                });

                return Ok(taskResult);

                
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
                var taskResult = await Task.Run(() => {
                    return  _unitOfWork.OrderRepository.OrdersReportCount();
                });
                 
                return Ok(taskResult);
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
                if(orderFilter?.Page != null)
                    orderFilter.Page = (orderFilter?.NumberOfObjects * (orderFilter?.Page - 1));

                var result = await _unitOfWork.OrderRepository.FilterAsync(orderFilter);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }
        
        
        
        
        
        
    }
}
