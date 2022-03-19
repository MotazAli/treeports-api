using System.Timers;
using Newtonsoft.Json;
using TreePorts.DTO;
using TreePorts.DTO.Records;
using TreePorts.Utilities;

namespace TreePorts.Presentation
{
    public class OrderService : IOrderService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly INotifyService _notify;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IServiceProvider _sp;
        private readonly IMapper mapper;
        public OrderService(IServiceProvider sp, IUnitOfWork unitOfWork, INotifyService notify, IWebHostEnvironment hostingEnvironment, IMapper mapper)//, IMapper mapper, IWebHostEnvironment hostingEnvironment, IHubContext<MessageHub> hubcontext)
        {
            _unitOfWork = unitOfWork;
            _notify = notify;
            _hostingEnvironment = hostingEnvironment;
            _sp = sp;
            this.mapper = mapper;
        }









        public async Task<IEnumerable<Order>> GetOrdersAsync(CancellationToken cancellationToken)
        {

            try
            {
                return await _unitOfWork.OrderRepository.GetOrdersAsync(cancellationToken);
            }
            catch (Exception e)
            {
                return new List<Order>();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }



        public async Task<object> GetOrdersPaginationAsync(FilterParameters parameters, CancellationToken cancellationToken)
        {
            try
            {

                var skip = (parameters.NumberOfObjectsPerPage * (parameters.Page - 1));
                var take = parameters.NumberOfObjectsPerPage;


                return await _unitOfWork.OrderRepository.GetOrdersPaginationAsync(skip, take,cancellationToken);


                /* var total = query.Count();            
                 var result = Utility.Pagination(query, parameters.NumberOfObjectsPerPage, parameters.Page).ToList();
                 var totalPages =(int)Math.Ceiling(total / (double)parameters.NumberOfObjectsPerPage);

                 return Ok(new { Orders = result, Total = total, Page = parameters.Page, TotaPages = totalPages });*/
            }
            catch (Exception e)
            {
                return new { };// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }



        public async Task<object> UserOrdersPagingByAgentIdAsync(string agentId, FilterParameters parameters,CancellationToken cancellationToken)
        {
            try
            {


                    var users = _unitOfWork.OrderRepository.GetByQuerable(o => o.IsDeleted == false && o.AgentId == agentId);
                    //var total = query.Count();
                    //var result = Utility.Pagination(query.ToList(), pagination.NumberOfObjectsPerPage, pagination.Page).ToList();
                    //var totalPages = (int)Math.Ceiling(total / (double)pagination.NumberOfObjectsPerPage);


                    var totalResult = 0;

                    var total = Math.Ceiling((((decimal)users.Count()) / ((decimal)parameters.NumberOfObjectsPerPage)));
                    var skip = (parameters.NumberOfObjectsPerPage * (parameters.Page - 1));
                    var take = parameters.NumberOfObjectsPerPage;

                    var result = await Utility.GetFilter2<Order>(parameters, users, skip, take, out totalResult).ToListAsync(cancellationToken);

                    var usersResult = this.mapper.Map<List<OrderResponse>>(result);
                    var totalPages = (int)Math.Ceiling(totalResult / (double)parameters.NumberOfObjectsPerPage);

                    foreach (var order in usersResult)
                    {
                        if (order.Code.Length != 0)
                        {
                            order.QRCodeUrl = Utility.ConvertImgToString(order.Code);
                        }


                    }

                    return new { Orders = usersResult, TotalResult = totalResult, Total = total, Page = parameters.Page, TotalPages = totalPages };
                

            }
            catch (Exception e)
            {
                return new { };// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }



        public async Task<IEnumerable<OrderStatusHistory>> GetOrdersStatusHistoriesByOrderIdAsync(long id, CancellationToken cancellationToken)
        {
            try
            {
                return await _unitOfWork.OrderRepository.GetOrdersStatusHistoriesByAsync(o => o.OrderId == id,cancellationToken);

            }
            catch (Exception e)
            {
                return new List<OrderStatusHistory>();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }





        public async Task<OrderDetails?> GetOrderDetailsByOrderIdAsync(long id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _unitOfWork.OrderRepository.GetOrderDetailsByIdAsync(id,cancellationToken);
                if (result?.QrCode != null && result?.QrCode?.Code?.Length != 0)
                {
                    result.QrCode.QrCodeUrl = Utility.ConvertImgToString(result.QrCode.Code);
                }

                return result;
            }
            catch (Exception e)
            {
                return new OrderDetails();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }




        public async Task<Order?> GetOrderByIdAsync(long id, CancellationToken cancellationToken)
        {
            try
            {
                var resullt = await _unitOfWork.OrderRepository.GetOrderById_oldBehaviourAsync(id,cancellationToken);

                var userPayments = await _unitOfWork.CaptainRepository.GetCaptainUsersPaymentsByAsync(p => p.OrderId == resullt.Id,cancellationToken);
                var userPayment = userPayments.FirstOrDefault();
                if (userPayment != null)
                    resullt.OrderDeliveryPaymentAmount = userPayment.Value;
                var qrCodes = await _unitOfWork.CaptainRepository.GetOrderQRCodeByAsync(p => p.OrderId == resullt.Id,cancellationToken);
                var qrCode = qrCodes.FirstOrDefault();
                if (qrCode != null && qrCode.Code.Length != 0)
                {
                    qrCode.QrCodeUrl = Utility.ConvertImgToString(qrCode.Code);
                    //resullt.Qrcodes.Add(qrCode);
                }
                var userAcceptedRequests = await _unitOfWork.CaptainRepository.GetCaptainUsersAcceptedRequestsByAsync(u => u.OrderId == resullt.Id,cancellationToken);

                if (userAcceptedRequests != null)
                {
                    //resullt.UserAcceptedRequests = userAcceptedRequests;

                }


                //var requests = await _unitOfWork.CaptainRepository.GetUserNewRequestBy(r => r.OrderId == id);
                //var userRequest = re
                return resullt;
            }
            catch (Exception e)
            {
                return new Order();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }



        public async Task<object> GetOrderDetailsAsync(long orderId, string captainUserAccountId, CancellationToken cancellationToken)//[FromBody] OrderRequest orderRequest)
        {

            var resullt = await _unitOfWork.OrderRepository.GetOrderById_oldBehaviourAsync(orderId,cancellationToken);
            var agent = await _unitOfWork.AgentRepository.GetAgentByIdAsync(resullt.AgentId,cancellationToken);
            //resullt.Agent = agent;

            var items = await _unitOfWork.OrderRepository.GetOrdersItemsByAsync(i => i.OrderId == orderId,cancellationToken);
            //resullt.OrderItems = items.ToList();

            var locations = await _unitOfWork.CaptainRepository.GetCaptainUsersCurrentLocationsByAsync(l => l.CaptainUserAccountId == captainUserAccountId,cancellationToken);
            var userLocation = locations.FirstOrDefault();

            var system = await _unitOfWork.SystemRepository.GetCurrentSystemSettingAsync(cancellationToken);
            var userAccount = await _unitOfWork.CaptainRepository.GetCaptainUserAccountByIdAsync(captainUserAccountId,cancellationToken);



            /* Customer Distance*/
            var customerOrigin = resullt.PickupLocationLat + "," + resullt.PickupLocationLong;
            var customerDestination = resullt.DropLocationLat + "," + resullt.DropLocationLong;

            var customerResponse = await Utility.getDirectionsFromGoogleMap(customerOrigin, customerDestination, "driving",cancellationToken);
            var customerResponseResult = JsonConvert.DeserializeObject<GoogleMapsResponse>(customerResponse);
            double customerDistance = 0.0;
            var customerResponseDistance = customerResponseResult?.Routes?.FirstOrDefault()?.Legs?.FirstOrDefault()?.distance;
            var customerResponseDuration = customerResponseResult?.Routes?.FirstOrDefault()?.Legs?.FirstOrDefault()?.duration;
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
            var agentOrigin = userLocation?.Lat + "," + userLocation?.Long;
            var agentDestination = resullt.PickupLocationLat + "," + resullt.PickupLocationLong;
            var agentResponse = await Utility.getDirectionsFromGoogleMap(agentOrigin, agentDestination, "driving",cancellationToken);

            var agentResponseResult = JsonConvert.DeserializeObject<GoogleMapsResponse>(agentResponse);
            var agentResponseDistance = agentResponseResult?.Routes?.FirstOrDefault()?.Legs?.FirstOrDefault()?.distance;
            var agentResponseDuration = agentResponseResult?.Routes?.FirstOrDefault()?.Legs?.FirstOrDefault()?.duration;
            double agentDistance = 0.0;
            if (agentResponseDistance.text.Contains("km"))
            {
                agentDistance = double.Parse(agentResponseDistance.value) / 1000.0;

            }
            else
            {
                agentDistance = double.Parse(agentResponseDistance?.value);

            }
            /* Agent Distance*/


            CountryPrice countryPrice; // just using that object as a template to hold the prices for calcutaions
            var agentDeliveryPrices =
                await _unitOfWork.AgentRepository.GetAgentDeliveryPriceByAsync(a =>
                    a.AgentId == resullt.AgentId && a.IsCurrent == true,cancellationToken);
            var agentDeliveryPrice = agentDeliveryPrices.FirstOrDefault();
            if (agentDeliveryPrice != null && agentDeliveryPrice?.Id > 0)
            {
                countryPrice = new()
                {
                    Kilometers = agentDeliveryPrice.Kilometers,
                    Price = agentDeliveryPrice.Price,
                    ExtraKilometers = agentDeliveryPrice.ExtraKilometers,
                    ExtraKiloPrice = agentDeliveryPrice.ExtraKiloPrice
                };
            }
            else
            {

                var cityPrices = await _unitOfWork.CountryRepository.GetCitiesPricesByAsync(c => c.CityId == agent.CityId,cancellationToken);
                var cityPrice = cityPrices.FirstOrDefault();
                if (cityPrice != null && cityPrice?.Id > 0)
                {
                    countryPrice = new()
                    {
                        Kilometers = cityPrice.Kilometers,
                        Price = cityPrice.Price,
                        ExtraKilometers = cityPrice.ExtraKilometers,
                        ExtraKiloPrice = cityPrice.ExtraKiloPrice
                    };
                }
                else
                {
                    var countriesPrices = await _unitOfWork.CountryRepository.GetCountriesPricesByAsync(c => c.CountryId == agent.CountryId,cancellationToken);
                    countryPrice = countriesPrices.FirstOrDefault();
                }
            }





            agentDistance = Math.Round(agentDistance);
            customerDistance = Math.Round(customerDistance); // for example customer distance is 28km

            decimal? amount = 0;
            decimal? remainingAmount = 0;
            if (customerDistance <= countryPrice?.Kilometers) // 28km is less or equal 5km country kilometers
            {
                amount = ((decimal)countryPrice.Kilometers) * countryPrice.Price;
                // amount = countryPrice.Price;
            }
            else
            {

                //remainingKilometers is 3km = 28km customerDistance % 5km country kilometers
                var remainingKilometers = customerDistance % countryPrice?.Kilometers;

                //realKilometer is 25km = 28km customerDistance - 3km remainingKilometers
                var realKilometer = customerDistance - remainingKilometers;

                //amount is 50 Reyal = ( 25km realKilometer / 5km country kilometers) * 10 Reyal Country Price
                amount = ((decimal)(realKilometer / countryPrice?.Kilometers)) * countryPrice?.Price;

                if (remainingKilometers > countryPrice?.ExtraKilometers)// 3km is greater 1km country extra kilometers
                {
                    // var extraRemainingKilometers = remainingKilometers % countryPrice.ExtraKilometers;
                    // var realExtraRemainingKilometers = remainingKilometers - extraRemainingKilometers;
                    //
                    // remainingAmount = ((decimal)(realExtraRemainingKilometers/ countryPrice.ExtraKilometers)) * countryPrice.ExtraKiloPrice;
                    // amount = amount + remainingAmount;

                    remainingAmount = ((decimal)remainingKilometers) * countryPrice?.ExtraKiloPrice;
                    amount += remainingAmount;
                }
                else if (remainingKilometers > 0 && remainingKilometers <= countryPrice?.ExtraKilometers)
                {
                    remainingAmount = countryPrice.ExtraKiloPrice;
                    amount += remainingAmount;
                }
                var agentCoupon = await _unitOfWork.AgentRepository.GetAssignedCoupon(resullt.AgentId, orderId,cancellationToken);

                if (agentCoupon != null)
                {
                    amount -= ((amount * (decimal)agentCoupon.DiscountPercent) / 100);
                }

            }


            var userPayments = await _unitOfWork.CaptainRepository.GetCaptainUsersPaymentsByAsync(p => p.OrderId == resullt.Id,cancellationToken);
            var userPayment = userPayments.FirstOrDefault();

            if (userPayment != null && userPayment.Id > 0)
            {
                userPayment.Value = amount;
                var updatePaymentResult = await _unitOfWork.CaptainRepository.UpdateCaptainUserPaymentAsync(userPayment,cancellationToken);
            }
            else
            {
                CaptainUserPayment payment = new()
                {
                    CaptainUserAccountId = captainUserAccountId,
                    OrderId = orderId,
                    PaymentTypeId = resullt.PaymentTypeId,
                    SystemSettingId = system?.Id,
                    PaymentStatusTypeId = (long)PaymentStatusTypes.New,
                    Value = amount,
                    CreationDate = DateTime.Now
                };
                var insertPaymentResult = await _unitOfWork.CaptainRepository.InsertCaptainUserPaymentAsync(payment,cancellationToken);
            }


            var result = await _unitOfWork.Save(cancellationToken);
            if (result == 0) throw new ServiceUnavailableException("Service Unavailable");


            return new
            {
                Order = resullt,
                AgentDistance = agentResponseDistance.text,
                CustomerDistance = customerResponseDistance.text,
                CustomerDuration = customerResponseDuration?.text,
                AgentDuration = agentResponseDuration?.text,
                DeliveryAmount = amount,
            };

        }







        public async Task<object> GetRunningOrderByCaptainUserAccountIdAsync(string captainUserAccountId, CancellationToken cancellationToken)
        {
            try
            {

                var running_orders = await _unitOfWork.OrderRepository.GetRunningOrdersByAsync(r => r.CaptainUserAccountId == captainUserAccountId,cancellationToken);
                var running_order = running_orders.FirstOrDefault();
                if (running_order == null || running_order.Id <= 0) throw new NotFoundException("No Running Order");

                return await GetOrderDetailsAsync(running_order.OrderId ?? 0, captainUserAccountId,cancellationToken);
                /*OrderRequest orderRequest = new OrderRequest()
                {
                    UserId = captainId,
                    OrderId = (long)running_order.OrderId
                };*/

                //return await this.GetOrderDetails(orderRequest);

                //var acceptedRequsts = await _unitOfWork.CaptainRepository.GetUserAcceptedRequestBy(a => a.UserId == id);

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

                //var locations = await _unitOfWork.CaptainRepository.GetUserCurrentLocationBy(l => l.UserId == id);
                //var userLocation = locations.FirstOrDefault();

                //var system = await _unitOfWork.SystemRepository.GetCurrent();
                //var user = await _unitOfWork.CaptainRepository.GetUserByID(id);


                //double agentDistance = Utility.distance(double.Parse(userLocation.Lat), double.Parse(userLocation.Long),
                //            double.Parse(resullt.PickupLocationLat), double.Parse(resullt.PickupLocationLong));
                //double customerDistance = Utility.distance(double.Parse(resullt.PickupLocationLat), double.Parse(resullt.PickupLocationLong),
                //              double.Parse(resullt.DropLocationLat), double.Parse(resullt.DropLocationLong));


                //var payments = await _unitOfWork.CaptainRepository.GetUserPaymentBy(p => p.OrderId == orderCurrentStatus.OrderId);
                //var payment = payments.FirstOrDefault();


                //return Ok(new { Order = resullt, AgentDistance = agentDistance, CustomerDistance = customerDistance, DeliveryAmount = payment.Value , IsDriverAttachInvoice = isDriverAttachInvoice , IsDriverPaidOrder = isDriverPaidOrder });


            }
            catch (Exception e)
            {
                return new { };// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }




        public async Task<bool> IgnoreOrderAsync(OrderRequest orderRequest, CancellationToken cancellationToken)
        {


            var userNewRequests = await _unitOfWork.CaptainRepository.DeleteCaptainUserNewRequestByOrderIdAsync(orderRequest.OrderId,cancellationToken);
            var deleteResult = await _unitOfWork.CaptainRepository.DeleteCaptainUserPaymentByOrderIdAsync(orderRequest.OrderId,cancellationToken);

            //var userNewRequests = await _unitOfWork.CaptainRepository.DeleteUserNewRequestByUserID(orderRequest.UserId);
            //var oldOrderPayment = await _unitOfWork.CaptainRepository.GetUserPaymentBy(p => p.OrderId == orderRequest.OrderId);
            //var deleteResult = await _unitOfWork.CaptainRepository.DeleteUserPayment(oldOrderPayment.FirstOrDefault().Id);


            CaptainUserIgnoredRequest driverRequest = new()
            {
                OrderId = orderRequest.OrderId,
                CaptainUserAccountId = orderRequest.CaptainUserAccountId,
                AgentId = userNewRequests?.AgentId,
                CreationDate = DateTime.Now
            };

            var insertNewRequestResult = await _unitOfWork.CaptainRepository.InsertCaptainUserIgnoredRequestAsync(driverRequest,cancellationToken);
            var result = await _unitOfWork.Save(cancellationToken);
            if (result == 0) throw new ServiceUnavailableException("Service Unavailable");

            return true;


            //////////// the new behavior of the penalties//////////////
            //var systemSetting = await _unitOfWork.SystemRepository.GetCurrent();
            //if (systemSetting != null)
            //{
            //    if (systemSetting.IgnorPerTypeId == (long)IgnorPer.Days)
            //    {
            //        var userIgnoredRequests = await _unitOfWork.CaptainRepository.GetUserIgnoredRequestBy(u => u.UserId == orderRequest.UserId &&
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

            //            var insertUserIgnoredPenalty_result = await _unitOfWork.CaptainRepository.InsertUserIgnoredPenalty(userIgnoredPenalty);
            //            result = await _unitOfWork.Save();
            //            if (result == 0) return new ObjectResult("Server not available") { StatusCode = 707 };
            //        }

            //    }
            //    else if (systemSetting.IgnorPerTypeId == (long)IgnorPer.Months)
            //    {
            //        var userIgnoredRequests = await _unitOfWork.CaptainRepository.GetUserIgnoredRequestBy(u => u.UserId == orderRequest.UserId &&

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

            //            var insertUserIgnoredPenalty_result = await _unitOfWork.CaptainRepository.InsertUserIgnoredPenalty(userIgnoredPenalty);
            //            result = await _unitOfWork.Save();
            //            if (result == 0) return new ObjectResult("Server not available") { StatusCode = 707 };
            //        }
            //    }
            //    else if (systemSetting.IgnorPerTypeId == (long)IgnorPer.Years)
            //    {
            //        var userIgnoredRequests = await _unitOfWork.CaptainRepository.GetUserIgnoredRequestBy(u => u.UserId == orderRequest.UserId &&

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

            //            var insertUserIgnoredPenalty_result = await _unitOfWork.CaptainRepository.InsertUserIgnoredPenalty(userIgnoredPenalty);
            //            result = await _unitOfWork.Save();
            //            if (result == 0) return new ObjectResult("Server not available") { StatusCode = 707 };
            //        }
            //    }
            //}
            //////////// end - the new behavior of the penalties//////////////



            //return Ok(true);


        }





        public async Task<bool> FakeCancelAsync(string captainUserAccountId, CancellationToken cancellationToken)
        {


            var usersMessageHub =
                     await _unitOfWork.CaptainRepository.GetCaptainUsersMessageHubsByAsync(u => u.CaptainUserAccountId == captainUserAccountId,cancellationToken);
            var userMessageHub = usersMessageHub.FirstOrDefault();
            if (userMessageHub != null && userMessageHub.Id > 0)
            {

                FirebaseNotificationResponse? responseResult = null;
                var result = await FirebaseNotification.SendNotification(userMessageHub.ConnectionId, "cancelRequest", "151",cancellationToken);

                if (result?.Length > 0)
                    responseResult = JsonConvert.DeserializeObject<FirebaseNotificationResponse>(result);

                if (responseResult == null || responseResult.messageId == "")
                    throw new Exception("Failed to send notification to captain");

            }
            return true;

        }


        public async Task<bool> FakeAssignToCaptainAsync(OrderRequest orderRequest, CancellationToken cancellationToken)
        {


            var order = await _unitOfWork.OrderRepository.GetOnlyOrderByIdAsync(orderRequest.OrderId,cancellationToken);
            if (order == null) return false;

            var usersMessageHub =
                     await _unitOfWork.CaptainRepository.GetCaptainUsersMessageHubsByAsync(u => u.CaptainUserAccountId == orderRequest.CaptainUserAccountId,cancellationToken);
            var userMessageHub = usersMessageHub.FirstOrDefault();
            if (userMessageHub != null && userMessageHub.Id > 0)
            {

                FirebaseNotificationResponse? responseResult = null;
                var result = await FirebaseNotification.SendNotification(userMessageHub.ConnectionId, "assignedRequest", order.Id.ToString(),cancellationToken);

                if (result?.Length > 0)
                    responseResult = JsonConvert.DeserializeObject<FirebaseNotificationResponse>(result);

                if (responseResult == null || responseResult.messageId == "")
                    throw new Exception("Failed to send notification to captain");

            }


            return true;


        }







        public async Task<bool> AssignToCaptainAsync(OrderRequest orderRequest, CancellationToken cancellationToken)
        {

            //check if the orderAssigen not saved before and there is no failure happend that cause the orderAssigned to saving it again
            var ordersAssigned = await _unitOfWork.OrderRepository.GetOrdersAssignmentsByAsync(o => o.OrderId == orderRequest.OrderId,cancellationToken);
            var oldOrderAssigned = ordersAssigned?.FirstOrDefault();
            if (oldOrderAssigned != null && oldOrderAssigned.Id > 0) return true;


            var order = await _unitOfWork.OrderRepository.GetOnlyOrderByIdAsync(orderRequest.OrderId,cancellationToken);
            var locations = await _unitOfWork.CaptainRepository.GetCaptainUsersCurrentLocationsByAsync(l => l.CaptainUserAccountId == orderRequest.CaptainUserAccountId,cancellationToken);
            var userLocation = locations.FirstOrDefault();



            CaptainUserAcceptedRequest driverRequest = new() { OrderId = orderRequest.OrderId, CaptainUserAccountId = orderRequest.CaptainUserAccountId, CreationDate = DateTime.Now };

            /* Customer Distance*/
            var customerOrigin = order?.PickupLocationLat + "," + order?.PickupLocationLong;
            var customerDestination = order?.DropLocationLat + "," + order?.DropLocationLong;

            var customerResponse = await Utility.getDirectionsFromGoogleMap(customerOrigin, customerDestination, "driving",cancellationToken);
            var customerResponseResult = JsonConvert.DeserializeObject<GoogleMapsResponse>(customerResponse);
            double customerDistance = 0.0;
            var customerResponseDistance = customerResponseResult?.Routes?.FirstOrDefault()?.Legs?.FirstOrDefault()?.distance;
            var customerResponseDuration = customerResponseResult?.Routes?.FirstOrDefault()?.Legs?.FirstOrDefault()?.duration;
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
            var agentOrigin = userLocation?.Lat + "," + userLocation?.Long;
            var agentDestination = order?.PickupLocationLat + "," + order?.PickupLocationLong;
            var agentResponse = await Utility.getDirectionsFromGoogleMap(agentOrigin, agentDestination, "driving",cancellationToken);

            var agentResponseResult = JsonConvert.DeserializeObject<GoogleMapsResponse>(agentResponse);
            var agentResponseDistance = agentResponseResult?.Routes?.FirstOrDefault()?.Legs?.FirstOrDefault()?.distance;
            var agentResponseDuration = agentResponseResult?.Routes?.FirstOrDefault()?.Legs?.FirstOrDefault()?.duration;
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
                    a.AgentId == order.AgentId && a.IsCurrent == true,cancellationToken);
            var agentDeliveryPrice = agentDeliveryPrices.FirstOrDefault();
            if (agentDeliveryPrice != null && agentDeliveryPrice?.Id > 0)
            {

                AgentOrderDeliveryPrice agentOrderDeliveryPrice = new ()
                {
                    OrderId = order?.Id,
                    AgentDeliveryPriceId = agentDeliveryPrice.Id,
                    CreationDate = DateTime.Now
                };
                var insertAgentOrderDeliveryPriceResult =
                    await _unitOfWork.AgentRepository.InsertAgentOrderDeliveryPriceAsync(agentOrderDeliveryPrice,cancellationToken);
            }
            else
            {

                var agent = await _unitOfWork.AgentRepository.GetAgentByIdAsync(order.AgentId,cancellationToken);
                var cityPrices =
                    await _unitOfWork.CountryRepository.GetCitiesPricesByAsync(a =>
                        a.CityId == agent.CityId && a.IsCurrent == true,cancellationToken);
                var cityPrice = cityPrices.FirstOrDefault();

                if (cityPrice != null && cityPrice?.Id > 0)
                {
                    CityOrderPrice cityOrderPrice = new ()
                    {
                        OrderId = order.Id,
                        CityPriceId = cityPrice.Id,
                        CreationDate = DateTime.Now
                    };
                    var insertCityOrderPriceResult =
                        await _unitOfWork.CountryRepository.InsertCityOrderPriceAsync(cityOrderPrice,cancellationToken);
                }
                else
                {
                    var countriesPrices = await _unitOfWork.CountryRepository.GetCountriesPricesByAsync(c => c.CountryId == agent.CountryId,cancellationToken);
                    var countryPrice = countriesPrices.FirstOrDefault();
                    if (countryPrice != null && countryPrice?.Id > 0)
                    {
                        CountryOrderPrice countryOrderPrice = new ()
                        {
                            OrderId = order.Id,
                            CountryPriceId = countryPrice.Id,
                            CreationDate = DateTime.Now
                        };
                        var insertCountryOrderPriceResult =
                            await _unitOfWork.CountryRepository.InsertCountryOrderPriceAsync(countryOrderPrice,cancellationToken);
                    }
                }
            }



            agentDistance = Math.Round(agentDistance);
            customerDistance = Math.Round(customerDistance);



            OrderAssignment orderAssignment = new()
            {
                OrderId = orderRequest.OrderId,
                CaptainUserAccountId = orderRequest.CaptainUserAccountId,
                ToAgentKilometer = agentDistance.ToString(),
                ToAgentTime = agentResponseDuration?.text,
                ToCustomerKilometer = customerDistance.ToString(),
                ToCustomerTime = customerResponseDuration?.text,
                CreationDate = DateTime.Now

            };

            OrderCurrentStatus orderCurrentStatus = new()
            {
                OrderId = orderRequest.OrderId,
                OrderStatusTypeId = (long)OrderStatusTypes.AssignedToCaptain,
                IsCurrent = true,
                CreationDate = DateTime.Now
                //Type = "AssginedToDriver"
            };

            CaptainUserCurrentStatus userCurrentStatus = new()
            {
                CaptainUserAccountId = orderRequest.CaptainUserAccountId,
                StatusTypeId = (long)StatusTypes.Progress,
                IsCurrent = true
            };


            RunningOrder runningOrder = new()
            {
                OrderId = orderRequest.OrderId,
                CaptainUserAccountId = orderRequest.CaptainUserAccountId,
                CreationDate = DateTime.Now
            };


            //check if there no QrCode inserted incase if the order assigned to captain throught the Admin or Support and didn't create QrCode for the order from request new order
            var oldQrCode = await _unitOfWork.OrderRepository.GetQrcodeByOrderIdAsync(order.Id,cancellationToken);
            if (oldQrCode == null || oldQrCode.Id <= 0)
            {
                /* Create QrCode and Insert*/
                var qRCode = Utility.CreateQRCode(orderRequest.CaptainUserAccountId, order.Id);
                var qRCodeResult = await _unitOfWork.OrderRepository.InsertQrCodeAsync(qRCode,cancellationToken);
                /* Create QrCode and Insert*/
            }


            var updatedOrder = await _unitOfWork.OrderRepository.UpdateOrderCurrentStatusAsync(order.Id, (long)OrderStatusTypes.AssignedToCaptain,cancellationToken);
            var userNewRequests = await _unitOfWork.CaptainRepository.DeleteCaptainUserNewRequestByUserIdAsync(orderRequest.CaptainUserAccountId,cancellationToken);
            var insertRunningOrderResult = await _unitOfWork.OrderRepository.InsertRunningOrderAsync(runningOrder,cancellationToken);
            var insertOrderStatusResult = await _unitOfWork.OrderRepository.InsertOrderStatusAsync(orderCurrentStatus,cancellationToken);
            var orderAssignmentResult = await _unitOfWork.OrderRepository.InsertOrderAssignmentAsync(orderAssignment,cancellationToken);
            var insertNewRequestResult = await _unitOfWork.CaptainRepository.InsertCaptainUserAcceptedRequestAsync(driverRequest,cancellationToken);
            var insertUserStatusResult = await _unitOfWork.CaptainRepository.InsertCaptainUserCurrentStatusAsync(userCurrentStatus,cancellationToken);
            var result = await _unitOfWork.Save(cancellationToken);
            if (result == 0) throw new ServiceUnavailableException("Service Unavailable");

            _ = _notify.NotifyOrderStatusChanged(OrderStatusTypes.AssignedToCaptain, order.Id, order.AgentId,cancellationToken);

            _ = _notify.SendGoogleCloudMessageToCaptain(orderRequest.CaptainUserAccountId, "assignedRequest", order.Id.ToString(),cancellationToken);

            _ = Task.Run(() =>
            {
                Task.Delay(TimeSpan.FromSeconds(10));
                _ = _notify.ChangeOrderStatusAndNotify(OrderStatusTypes.Progress, order.Id, order.AgentId,cancellationToken);
            },cancellationToken);


            return true;


        }









        public async Task<bool> AcceptOrderAsync(OrderRequest orderRequest, CancellationToken cancellationToken)
        {


            //var maounOrders =
            //    await _unitOfWork.MaounRepository.GetMaounOrderBy(o => o.SenderOrderId == orderRequest.OrderId);
            //var maounOrder = maounOrders.FirstOrDefault();
            //if (maounOrder?.Id > 0)
            //{
            //    User driverUser = await _unitOfWork.CaptainRepository.GetUserByID(orderRequest.UserId);
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
            var ordersAssigned = await _unitOfWork.OrderRepository.GetOrdersAssignmentsByAsync(o => o.OrderId == orderRequest.OrderId,cancellationToken);
            var oldOrderAssigned = ordersAssigned?.FirstOrDefault();
            if (oldOrderAssigned != null && oldOrderAssigned.Id > 0) return true;


            var order = await _unitOfWork.OrderRepository.GetOnlyOrderByIdAsync(orderRequest.OrderId,cancellationToken);
            var locations = await _unitOfWork.CaptainRepository.GetCaptainUsersCurrentLocationsByAsync(l => l.CaptainUserAccountId == orderRequest.CaptainUserAccountId,cancellationToken);
            var userLocation = locations.FirstOrDefault();



            CaptainUserAcceptedRequest driverRequest = new() { OrderId = orderRequest.OrderId, CaptainUserAccountId = orderRequest.CaptainUserAccountId, CreationDate = DateTime.Now };

            //GeoCoordinate agentLocation = new GeoCoordinate(double.Parse(resullt.PickupLocationLat), double.Parse(resullt.PickupLocationLong));
            //GeoCoordinate customerLocation = new GeoCoordinate(double.Parse(resullt.DropLocationLat), double.Parse(resullt.DropLocationLong));
            //GeoCoordinate driverLocation = new GeoCoordinate(double.Parse(userLocation.Lat), double.Parse(userLocation.Long));
            // double agentDistance = Utility.distance(double.Parse(userLocation.Lat), double.Parse(userLocation.Long),
            //             double.Parse(order.PickupLocationLat), double.Parse(order.PickupLocationLong));
            // double customerDistance = Utility.distance(double.Parse(order.PickupLocationLat), double.Parse(order.PickupLocationLong),
            //               double.Parse(order.DropLocationLat), double.Parse(order.DropLocationLong));

            /* Customer Distance*/
            var customerOrigin = order?.PickupLocationLat + "," + order?.PickupLocationLong;
            var customerDestination = order?.DropLocationLat + "," + order?.DropLocationLong;

            var customerResponse = await Utility.getDirectionsFromGoogleMap(customerOrigin, customerDestination, "driving",cancellationToken);
            var customerResponseResult = JsonConvert.DeserializeObject<GoogleMapsResponse>(customerResponse);
            double customerDistance = 0.0;
            var customerResponseDistance = customerResponseResult?.Routes?.FirstOrDefault()?.Legs?.FirstOrDefault()?.distance;
            var customerResponseDuration = customerResponseResult?.Routes?.FirstOrDefault()?.Legs?.FirstOrDefault()?.duration;
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
            var agentOrigin = userLocation?.Lat + "," + userLocation?.Long;
            var agentDestination = order?.PickupLocationLat + "," + order?.PickupLocationLong;
            var agentResponse = await Utility.getDirectionsFromGoogleMap(agentOrigin, agentDestination, "driving",cancellationToken);

            var agentResponseResult = JsonConvert.DeserializeObject<GoogleMapsResponse>(agentResponse);
            var agentResponseDistance = agentResponseResult?.Routes?.FirstOrDefault()?.Legs?.FirstOrDefault()?.distance;
            var agentResponseDuration = agentResponseResult?.Routes?.FirstOrDefault()?.Legs?.FirstOrDefault()?.duration;
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
                    a.AgentId == order.AgentId && a.IsCurrent == true,cancellationToken);
            var agentDeliveryPrice = agentDeliveryPrices.FirstOrDefault();
            if (agentDeliveryPrice != null && agentDeliveryPrice?.Id > 0)
            {

                AgentOrderDeliveryPrice agentOrderDeliveryPrice = new ()
                {
                    OrderId = order?.Id,
                    AgentDeliveryPriceId = agentDeliveryPrice.Id,
                    CreationDate = DateTime.Now
                };
                var insertAgentOrderDeliveryPriceResult =
                    await _unitOfWork.AgentRepository.InsertAgentOrderDeliveryPriceAsync(agentOrderDeliveryPrice,cancellationToken);
            }
            else
            {

                var agent = await _unitOfWork.AgentRepository.GetAgentByIdAsync(order.AgentId,cancellationToken);
                var cityPrices =
                    await _unitOfWork.CountryRepository.GetCitiesPricesByAsync(a =>
                        a.CityId == agent.CityId && a.IsCurrent == true,cancellationToken);
                var cityPrice = cityPrices.FirstOrDefault();

                if (cityPrice != null && cityPrice?.Id > 0)
                {
                    CityOrderPrice cityOrderPrice = new()
                    {
                        OrderId = order.Id,
                        CityPriceId = cityPrice.Id,
                        CreationDate = DateTime.Now
                    };
                    var insertCityOrderPriceResult =
                        await _unitOfWork.CountryRepository.InsertCityOrderPriceAsync(cityOrderPrice,cancellationToken);
                }
                else
                {
                    var countriesPrices = await _unitOfWork.CountryRepository.GetCountriesPricesByAsync(c => c.CountryId == agent.CountryId,cancellationToken);
                    var countryPrice = countriesPrices.FirstOrDefault();
                    if (countryPrice != null && countryPrice?.Id > 0)
                    {
                        CountryOrderPrice countryOrderPrice = new ()
                        {
                            OrderId = order.Id,
                            CountryPriceId = countryPrice.Id,
                            CreationDate = DateTime.Now
                        };
                        var insertCountryOrderPriceResult =
                            await _unitOfWork.CountryRepository.InsertCountryOrderPriceAsync(countryOrderPrice,cancellationToken);
                    }
                }
            }



            agentDistance = Math.Round(agentDistance);
            customerDistance = Math.Round(customerDistance);



            OrderAssignment orderAssignment = new()
            {
                OrderId = orderRequest.OrderId,
                CaptainUserAccountId = orderRequest.CaptainUserAccountId,
                ToAgentKilometer = agentDistance.ToString(),
                ToAgentTime = agentResponseDuration?.text,
                ToCustomerKilometer = customerDistance.ToString(),
                ToCustomerTime = customerResponseDuration?.text,
                CreationDate = DateTime.Now

            };

            OrderCurrentStatus orderCurrentStatus = new()
            {
                OrderId = orderRequest.OrderId,
                OrderStatusTypeId = (long)OrderStatusTypes.AssignedToCaptain,
                IsCurrent = false,
                //Type = "AssginedToDriver"
            };

            CaptainUserCurrentStatus userCurrentStatus = new()
            {
                CaptainUserAccountId = orderRequest.CaptainUserAccountId,
                StatusTypeId = (long)StatusTypes.Progress,
                IsCurrent = true
            };


            RunningOrder runningOrder = new()
            {
                OrderId = orderRequest.OrderId,
                CaptainUserAccountId = orderRequest.CaptainUserAccountId,
                CreationDate = DateTime.Now
            };


            //check if there no QrCode inserted incase if the order assigned to captain throught the Admin or Support and didn't create QrCode for the order from request new order
            var oldQrCode = await _unitOfWork.OrderRepository.GetQrcodeByOrderIdAsync(order.Id,cancellationToken);
            if (oldQrCode == null || oldQrCode.Id <= 0)
            {
                /* Create QrCode and Insert*/
                var qRCode = Utility.CreateQRCode(orderRequest.CaptainUserAccountId, order.Id);
                var qRCodeResult = await _unitOfWork.OrderRepository.InsertQrCodeAsync(qRCode,cancellationToken);
                /* Create QrCode and Insert*/
            }


            var updatedOrder = await _unitOfWork.OrderRepository.UpdateOrderCurrentStatusAsync(order.Id, (long)OrderStatusTypes.AssignedToCaptain,cancellationToken);
            var userNewRequests = await _unitOfWork.CaptainRepository.DeleteCaptainUserNewRequestByUserIdAsync(orderRequest.CaptainUserAccountId,cancellationToken);
            var insertRunningOrderResult = await _unitOfWork.OrderRepository.InsertRunningOrderAsync(runningOrder,cancellationToken);
            var insertOrderStatusResult = await _unitOfWork.OrderRepository.InsertOrderStatusAsync(orderCurrentStatus,cancellationToken);
            var orderAssignmentResult = await _unitOfWork.OrderRepository.InsertOrderAssignmentAsync(orderAssignment,cancellationToken);
            var insertNewRequestResult = await _unitOfWork.CaptainRepository.InsertCaptainUserAcceptedRequestAsync(driverRequest,cancellationToken);
            var insertUserStatusResult = await _unitOfWork.CaptainRepository.InsertCaptainUserCurrentStatusAsync(userCurrentStatus,cancellationToken);
            var result = await _unitOfWork.Save(cancellationToken);
            if (result == 0) throw new ServiceUnavailableException("Service Unavailable");

            _ = _notify.NotifyOrderStatusChanged(OrderStatusTypes.AssignedToCaptain, order.Id, order.AgentId,cancellationToken);

            //TODO var _newnotify = new NotifyService(_serviceProvider);
            /* _ = Task.Run(async () => {
                 Task.Delay(TimeSpan.FromSeconds(10));
                 _ = await _newnotify.ChangeOrderStatusAndNotify(OrderStatusTypes.Progress, order.Id, (long)order.AgentId);
             });*/

            return true;



        }

        /*

                public async Task<bool> RejectOrderAsync( OrderRequest orderRequest)
                {


                        var userNewRequests = await _unitOfWork.CaptainRepository.DeleteCaptainUserNewRequestByOrderIdAsync(orderRequest.OrderId);
                        var deleteResult = await _unitOfWork.CaptainRepository.DeleteCaptainUserPaymentByOrderIdAsync(orderRequest.OrderId);
                        //var oldOrderPayment = await _unitOfWork.CaptainRepository.GetUserPaymentBy(p => p.OrderId == orderRequest.OrderId);



                        CaptainUserRejectedRequest driverRequest = new()
                        {
                            OrderId = orderRequest.OrderId,
                            UserId = orderRequest.CaptainUserAccountId,
                            AgentId = userNewRequests.AgentId,
                            CreatedBy = 1,
                            CreationDate = DateTime.Now
                        };

                        var insertNewRequestResult = await _unitOfWork.CaptainRepository.InsertUserRejectedRequestAsync(driverRequest);
                        var result = await _unitOfWork.Save();
                        if (result == 0) throw new Exception("Service Unavailable");

                        return true;
                        //////////// the new behavior of the penalties//////////////
                        //var systemSetting = await _unitOfWork.SystemRepository.GetCurrent();
                        //if (systemSetting != null)
                        //{
                        //    if (systemSetting.RejectPerTypeId == (long)RejectPer.Days)
                        //    {
                        //        var userRejectedRequests = await _unitOfWork.CaptainRepository.GetUserRejectedRequestBy(u => u.UserId == orderRequest.UserId &&
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

                        //            var insertUserRejectPenalty_result = await _unitOfWork.CaptainRepository.InsertUserRejectPenalty(userRejectPenalty);
                        //            result = await _unitOfWork.Save();
                        //            if (result == 0) return new ObjectResult("Server not available") { StatusCode = 707 };
                        //        }

                        //    }
                        //    else if (systemSetting.RejectPerTypeId == (long)RejectPer.Months)
                        //    {
                        //       var userRejectedRequests = await _unitOfWork.CaptainRepository.GetUserRejectedRequestBy(u => u.UserId == orderRequest.UserId &&
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

                        //            var insertUserRejectPenalty_result = await _unitOfWork.CaptainRepository.InsertUserRejectPenalty(userRejectPenalty);
                        //            result = await _unitOfWork.Save();
                        //            if (result == 0) return new ObjectResult("Server not available") { StatusCode = 707 };
                        //        }
                        //    }
                        //    else if (systemSetting.RejectPerTypeId == (long)RejectPer.Years)
                        //    {
                        //        var userRejectedRequests = await _unitOfWork.CaptainRepository.GetUserRejectedRequestBy(u => u.UserId == orderRequest.UserId &&

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

                        //            var insertUserRejectPenalty_result = await _unitOfWork.CaptainRepository.InsertUserRejectPenalty(userRejectPenalty);
                        //            result = await _unitOfWork.Save();
                        //            if (result == 0) return new ObjectResult("Server not available") { StatusCode = 707 };
                        //        }
                        //    }
                        //}
                        //////////// end - the new behavior of the penalties//////////////

                        //return Ok(true);


                }
        */


        public async Task<bool> OrderPickedUpAsync(long id, CancellationToken cancellationToken)
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




            OrderCurrentStatus orderCurrentStatus = new()
            {
                OrderId = id,
                OrderStatusTypeId = (long)OrderStatusTypes.PickedUp,
                IsCurrent = true,

            };
            var order = await _unitOfWork.OrderRepository.GetOnlyOrderByIdAsync(id,cancellationToken);
            var orderAssgined = await _unitOfWork.OrderRepository.GetOrdersAssignmentsByAsync(a => a.OrderId == id,cancellationToken);


            //TODO need to implement
            /* if (order?.OrderItems == null || order?.OrderItems.Count <= 0)
             {
                 order.OrderItems = await _unitOfWork.OrderRepository.GetOrdersItemsByAsync(o => o.OrderId == order.Id);
             }
*/
            //    decimal order_amount = order.OrderItems.Select(i => i.Price).Sum().GetValueOrDefault();

            decimal order_amount = 0;
            //End - TODO
            // foreach (OrderItem item in order.OrderItems)
            // {
            //     order_amount = +(decimal)item.Price;
            // }

            Bookkeeping bookkeeping = new()
            {
                OrderId = order?.Id,
                CaptainUserAccountId = orderAssgined.FirstOrDefault()?.CaptainUserAccountId,
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

            var captainUserAccountId = orderAssgined?.FirstOrDefault()?.CaptainUserAccountId;
            var userCurrentLocation = await _unitOfWork.CaptainRepository.GetCaptainUsersCurrentLocationsByAsync(l => l.CaptainUserAccountId == captainUserAccountId,cancellationToken);
            var currentLocation = userCurrentLocation.FirstOrDefault();

            OrderStartLocation orderStartLocation = new()
            {
                OrderId = id,
                OrderAssignId = orderAssgined?.FirstOrDefault()?.Id,
                PickedupLat = currentLocation?.Lat,
                PickedupLong = currentLocation?.Long,
                CreationDate = DateTime.Now

            };

            var updatedOrder = await _unitOfWork.OrderRepository.UpdateOrderCurrentStatusAsync(order.Id, (long)OrderStatusTypes.PickedUp,cancellationToken);
            var insertBookkeepingResult = await _unitOfWork.PaymentRepository.InsertBookkeepingAsync(bookkeeping,cancellationToken);
            var insertOrderStartLocationResult = await _unitOfWork.OrderRepository.InsertOrderStartLocationAsync(orderStartLocation,cancellationToken);
            var insertOrderStatusResult = await _unitOfWork.OrderRepository.InsertOrderStatusAsync(orderCurrentStatus,cancellationToken);
            var result = await _unitOfWork.Save(cancellationToken);
            if (result == 0) throw new ServiceUnavailableException("Service Unavailable");

            _ = _notify.NotifyOrderStatusChanged(OrderStatusTypes.PickedUp, order.Id, order.AgentId,cancellationToken);

            return true;

        }



        public async Task<bool> OrderDroppedAsync(long id, CancellationToken cancellationToken)
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




            var orderAssgined = await _unitOfWork.OrderRepository.GetOrdersAssignmentsByAsync(a => a.OrderId == id,cancellationToken);

            var captainUserAccountId = orderAssgined.FirstOrDefault()?.CaptainUserAccountId;
            var userCurrentLocation = await _unitOfWork.CaptainRepository.GetCaptainUsersCurrentLocationsByAsync(l => l.CaptainUserAccountId == captainUserAccountId,cancellationToken);
            var currentLocation = userCurrentLocation.FirstOrDefault();
            var userPayments = await _unitOfWork.CaptainRepository.GetCaptainUsersPaymentsByAsync(p => p.OrderId == id,cancellationToken);
            var userPayment = userPayments.FirstOrDefault();
            userPayment.PaymentStatusTypeId = (long)PaymentStatusTypes.Complete;

            OrderEndLocation orderEndLocation = new()
            {
                OrderId = id,
                OrderAssignId = orderAssgined?.FirstOrDefault()?.Id,
                DroppedLat = currentLocation?.Lat,
                DroppedLong = currentLocation?.Long,
                CreationDate = DateTime.Now

            };
            //var order = await _unitOfWork.OrderRepository.GetOrderByID(id);


            OrderCurrentStatus orderCurrentStatus = new()
            {
                OrderId = id,
                OrderStatusTypeId = (long)OrderStatusTypes.Dropped,
                IsCurrent = true

            };

            OrderCurrentStatus orderDeliveredCurrentStatus = new()
            {
                OrderId = id,
                OrderStatusTypeId = (long)OrderStatusTypes.Delivered,
                IsCurrent = true

            };

            CaptainUserCurrentStatus userCurrentStatus = new()
            {
                CaptainUserAccountId = captainUserAccountId,
                StatusTypeId = (long)StatusTypes.Ready,
                IsCurrent = true
            };


            //if (order.PaymentTypeId == (long)PaymentTypes.Paid) {

            //}

            //delete the order items amount from captain wallet
            var order_items_amount_bookkeeping = await _unitOfWork.PaymentRepository.GetBookkeepingByAsync(b => b.OrderId == id && b.DepositTypeId == (long)DepositTypes.Order_Items_Amount,cancellationToken);
            if (order_items_amount_bookkeeping?.FirstOrDefault()?.Id > 0)
            {
                var bookkeeping_id = order_items_amount_bookkeeping?.FirstOrDefault()?.Id;
                var deleteBookkeepingResult = await _unitOfWork.PaymentRepository.DeleteBookkeepingAsync((long)bookkeeping_id,cancellationToken);
            }

            // add the delivery amount to the captain wallet
            Bookkeeping delivery_bookkeeping = new()
            {
                OrderId = id,
                CaptainUserAccountId = captainUserAccountId,
                DepositTypeId = (long)DepositTypes.Delivery_Amount,
                Value = userPayment.Value,
                CreationDate = DateTime.Now
            };

            var updatedOrder = await _unitOfWork.OrderRepository.UpdateOrderCurrentStatusAsync(id, (long)OrderStatusTypes.Delivered,cancellationToken);
            var insertedDeliveryBookkeeping = await _unitOfWork.PaymentRepository.InsertBookkeepingAsync(delivery_bookkeeping,cancellationToken);
            var insertPaymentResult = await _unitOfWork.CaptainRepository.UpdateCaptainUserPaymentAsync(userPayment,cancellationToken);
            var insertOrderEndLocationResult = await _unitOfWork.OrderRepository.InsertOrderEndLocationAsync(orderEndLocation,cancellationToken);
            var insertUserStatusResult = await _unitOfWork.CaptainRepository.InsertCaptainUserCurrentStatusAsync(userCurrentStatus,cancellationToken);
            var insertOrderStatusResult = await _unitOfWork.OrderRepository.InsertOrderStatusAsync(orderCurrentStatus,cancellationToken);
            var oldRunningOrder = await _unitOfWork.OrderRepository.DeleteRunningOrderByOrderIdAsync(id,cancellationToken);
            var insertOrderDeliveredStatusResult = await _unitOfWork.OrderRepository.InsertOrderStatusAsync(orderDeliveredCurrentStatus, cancellationToken);
            var result = await _unitOfWork.Save(cancellationToken);
            if (result == 0) throw new ServiceUnavailableException("Service Unavailable");

            var order = await _unitOfWork.OrderRepository.GetOnlyOrderByIdAsync(id,cancellationToken);
            _ = _notify.NotifyOrderStatusChanged(OrderStatusTypes.Dropped, order.Id, order.AgentId,cancellationToken);

            //TODO var _newnotify = new NotifyService(_serviceProvider);
            /* _ = Task.Run(async () => {
                 Task.Delay(TimeSpan.FromSeconds(10));
                 _ = await _newnotify.ChangeOrderStatusAndNotify(OrderStatusTypes.Delivered, order.Id, (long)order.AgentId);
             });*/


            return true;
            // /// Check Bonus
            // var userOrdersAssignedPerDay = await _unitOfWork.OrderRepository.GetOrderAssignmentBy(a => a.UserId == userId &&
            //                          a.CreationDate.Value.Date == DateTime.Now.Date);

            // var orderIds = userOrdersAssignedPerDay.Select(a => a.OrderId).ToList();
            // var ordersStatus = await _unitOfWork.OrderRepository.GetOrderStatusBy(s => orderIds.Contains(s.OrderId) &&
            //(s.StatusTypeId == (long)OrderStatusTypes.Dropped));
            // var ordersCount = ordersStatus.Count();
            // var userCountryId = orderAssgined.FirstOrDefault().User.ResidenceCountryId;
            // var bonusPerCountry = await _unitOfWork.CaptainRepository.GetBonusByCountry(userCountryId);
            // if (ordersCount >= bonusPerCountry.OrdersPerDay)
            // {
            //     var userBonus = new UserBonus
            //     {
            //         UserId = userId,
            //         BonusTypeId = (long)BonusTypes.BonusPerDay,
            //         CreationDate = DateTime.Now,
            //         Amount = bonusPerCountry.BonusPerDay
            //     };
            //     var insertedBonus = await _unitOfWork.CaptainRepository.InsertBonus(userBonus);
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




        public async Task<bool> CancelOrderAsync(long id, CancellationToken cancellationToken)
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


            var order = await _unitOfWork.OrderRepository.GetOnlyOrderByIdAsync(id,cancellationToken);
            if (order == null || order.Id <= 0) throw new NoContentException("NoContent");

            var ordersAssgined = await _unitOfWork.OrderRepository.GetOrdersAssignmentsByAsync(a => a.OrderId == id,cancellationToken);
            var orderAssgined = ordersAssgined?.FirstOrDefault();

            if (orderAssgined != null && orderAssgined.Id > 0)
            {
                var userId = orderAssgined.CaptainUserAccountId;
                var userCurrentLocation = await _unitOfWork.CaptainRepository.GetCaptainUsersCurrentLocationsByAsync(l => l.CaptainUserAccountId == userId,cancellationToken);
                var currentLocation = userCurrentLocation.FirstOrDefault();
                var userPayments = await _unitOfWork.CaptainRepository.GetCaptainUsersPaymentsByAsync(p => p.OrderId == id,cancellationToken);
                var userPayment = userPayments.FirstOrDefault();
                if (userPayment != null)
                {
                    userPayment.PaymentStatusTypeId = (long)PaymentStatusTypes.Canclled;
                    // add the delivery amount to the captain wallet
                    Bookkeeping delivery_bookkeeping = new ()
                    {
                        OrderId = id,
                        CaptainUserAccountId = userId,
                        DepositTypeId = (long)DepositTypes.Delivery_Amount,
                        Value = userPayment.Value,
                        CreationDate = DateTime.Now
                    };

                    var insertedDeliveryBookkeeping = await _unitOfWork.PaymentRepository.InsertBookkeepingAsync(delivery_bookkeeping,cancellationToken);
                    var insertPaymentResult = await _unitOfWork.CaptainRepository.UpdateCaptainUserPaymentAsync(userPayment,cancellationToken);
                }

                OrderEndLocation orderEndLocation = new ()
                {
                    OrderId = id,
                    OrderAssignId = orderAssgined.Id,
                    DroppedLat = currentLocation?.Lat,
                    DroppedLong = currentLocation?.Long,
                    CreationDate = DateTime.Now

                };

                CaptainUserCurrentStatus userCurrentStatus = new()
                {
                    CaptainUserAccountId = userId,
                    StatusTypeId = (long)StatusTypes.Ready,
                    IsCurrent = true
                };


                //delete the order items amount from captain wallet
                var order_items_amount_bookkeeping = await _unitOfWork.PaymentRepository.GetBookkeepingByAsync(b => b.OrderId == id && b.DepositTypeId == (long)DepositTypes.Order_Items_Amount,cancellationToken);
                if (order_items_amount_bookkeeping?.FirstOrDefault()?.Id > 0)
                {
                    var bookkeeping_id = order_items_amount_bookkeeping?.FirstOrDefault()?.Id;
                    var deleteBookkeepingResult = await _unitOfWork.PaymentRepository.DeleteBookkeepingAsync(bookkeeping_id ?? 0,cancellationToken);
                }


                var insertOrderEndLocationResult = await _unitOfWork.OrderRepository.InsertOrderEndLocationAsync(orderEndLocation,cancellationToken);
                var insertUserStatusResult = await _unitOfWork.CaptainRepository.InsertCaptainUserCurrentStatusAsync(userCurrentStatus,cancellationToken);
            }


            OrderCurrentStatus orderCurrentStatus = new ()
            {
                OrderId = id,
                OrderStatusTypeId = (long)OrderStatusTypes.Canceled,
                IsCurrent = true

            };


            var updatedOrder = await _unitOfWork.OrderRepository.UpdateOrderCurrentStatusAsync(id, (long)OrderStatusTypes.Canceled,cancellationToken);
            var insertOrderStatusResult = await _unitOfWork.OrderRepository.InsertOrderStatusAsync(orderCurrentStatus,cancellationToken);
            var oldRunningOrder = await _unitOfWork.OrderRepository.DeleteRunningOrderByOrderIdAsync(id,cancellationToken);
            var result = await _unitOfWork.Save(cancellationToken);
            if (result == 0) throw new ServiceUnavailableException("Service Unavailable");

            _ = _notify.NotifyOrderStatusChanged(OrderStatusTypes.Canceled, order.Id, order.AgentId,cancellationToken);

            if (orderAssgined != null && orderAssgined.Id > 0)
            {
                _ = _notify.SendGoogleCloudMessageToCaptain(orderAssgined.CaptainUserAccountId, "cancelRequest", order.Id.ToString(),cancellationToken);
            }


            return true;
        }




        public async Task<object> AddOrderAsync(Order order, HttpContext httpContext, string CouponCode, CancellationToken cancellationToken)
        {


            /* var result = await _orderService.AddNewOrder(order);
             if (result == null) return NoContent();

             return Ok(result);*/


            if (order == null || order.PickupLocationLat == null || order.PickupLocationLong == null ||
                order.PickupLocationLat == "" || order.PickupLocationLong == "")
                throw new Exception("NoContent"); // new ObjectResult("Your request has no data") { StatusCode = 406 };


            if (order.AgentId is null || order.AgentId == "")
            {
                //long agentId = -1;
                //string userType = "";
                Utility.getRequestUserIdFromToken(httpContext, out string agentId, out string userType);
                order.AgentId = agentId;
            }

            order.OrderStatusTypeId = (long)OrderStatusTypes.New;
            var orderInsertResult = await _unitOfWork.OrderRepository.InsertOrderAsync(order,cancellationToken);
            var result = await _unitOfWork.Save(cancellationToken);
            if (result <= 0) throw new ServiceUnavailableException("Service Unavailable");


            OrderCurrentStatus orderCurrentStatus = new()
            {
                OrderId = orderInsertResult.Id,
                OrderStatusTypeId = (long)OrderStatusTypes.New,
                IsCurrent = true
            };
            var insertResult = await _unitOfWork.OrderRepository.InsertOrderStatusAsync(orderCurrentStatus,cancellationToken);
            var resultSecondeOperation = await _unitOfWork.Save(cancellationToken);
            if (resultSecondeOperation <= 0) throw new ServiceUnavailableException("Service Unavailable");


            _ = await _notify.NotifyOrderStatusChanged(OrderStatusTypes.New, orderInsertResult.Id, orderInsertResult.AgentId,cancellationToken);


            _ = SearchForCaptainAndNotifyOrder(orderInsertResult,cancellationToken);

            return new
            {
                OrderNumber = orderInsertResult.Id,
                OrderStatus = "New",
                Message = "Order saved and starting search for near captain"
            };


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
                    await _unitOfWork.CaptainRepository.GetUserNearestLocation(order.PickupLocationLat,
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

                var insertNewRequestResult = await _unitOfWork.CaptainRepository.InsertUserNewRequest(driverRequest);
                result = await _unitOfWork.Save();
                if (result <= 0) return new ObjectResult("Server not available") {StatusCode = 503};

                var usersMessageHub =
                    await _unitOfWork.CaptainRepository.GetUserMessageHubBy(u => u.UserId == captain.Id);
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
                        await _unitOfWork.CaptainRepository.GetUserRejectedRequestBy(r =>
                            r.OrderId == orderInsertResult.Id);
                    var rejectedOrder = ordersReject.FirstOrDefault();
                    if (rejectedOrder != null)
                        isDriverRejectOrder = true;


                    var ordersIgnored =
                        await _unitOfWork.CaptainRepository.GetUserIgnoredRequestBy(r =>
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
        //         var driver = await _unitOfWork.CaptainRepository.GetUserNearestLocation(order.PickupLocationLat, order.PickupLocationLong);
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
        //         var insertNewRequestResult = await _unitOfWork.CaptainRepository.InsertUserNewRequest(driverRequest);
        //         result = await _unitOfWork.Save();
        //         if (result == 0) return new ObjectResult("Server not available") { StatusCode = 707 };
        //
        //         var usersMessageHub = await _unitOfWork.CaptainRepository.GetUserMessageHubBy(u => u.UserId == driver.Id);
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
        //         var user = await _unitOfWork.CaptainRepository.GetUserByID((long)orderAssign.UserId);
        //
        //         //var user = await _unitOfWork.CaptainRepository.GetUserByID((long)driver.Id);
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
        //         //var DriversIgnored = await _unitOfWork.CaptainRepository.GetUserIgnoredRequestBy( u => u.OrderId == order.Id );
        //         //var list = DriversIgnored.Select(u => u.Id).ToList();
        //         //var agentCoordinate = new Geo(0, order.PickupLocationLat, order.PickupLocationLong);
        //
        //         //var drivers = await _unitOfWork.CaptainRepository.GetAllUsersCurrentLocations();
        //         //var nearst = drivers.Select(u => new Geo((long)u.UserId, u.Lat, u.Long)).Where( u => !list.Contains(u.UserID)  ).OrderBy(g => g.GetDistanceTo(agentCoordinate)).First();
        //
        //
        //
        //         //var agentCoordinate = new GeoCoordinate(double.Parse(order.PickupLocationLat), double.Parse(order.PickupLocationLong));
        //
        //         //var drivers = await _unitOfWork.CaptainRepository.GetAllUsersCurrentLocations();
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


        public async Task<Order> DeleteOrderAsync(long id, CancellationToken cancellationToken)
        {
            try
            {
                return await _unitOfWork.OrderRepository.DeleteOrderAsync(id,cancellationToken);
            }
            catch (Exception e)
            {
                return null;// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }



        public async Task<OrderInvoice> AddOrderInvoiceAsync(OrderInvoice orderInvoice, CancellationToken cancellationToken)
        {

            var insertResullt = await _unitOfWork.OrderRepository.InsertOrderInvoiceAsync(orderInvoice,cancellationToken);
            var result = await _unitOfWork.Save(cancellationToken);
            if (result == 0) throw new Exception("Service Unavailable");

            return insertResullt;


        }



        public async Task<PaidOrder> AddPaidOrderAsync(PaidOrder paidOrder, CancellationToken cancellationToken)
        {

            var insertResullt = await _unitOfWork.OrderRepository.InsertPaidOrderAsync(paidOrder,cancellationToken);
            var result = await _unitOfWork.Save(cancellationToken);
            if (result == 0) throw new Exception("Service Unavailable");

            return insertResullt;


        }




        public async Task<object> GetOrderCurrentLocationByOrderIdAsync(long id, CancellationToken cancellationToken) // order id
        {


            var orderStatuses =
                await _unitOfWork.OrderRepository.GetOrderCurrentStatusesByAsync(s => s.OrderId == id && s.IsCurrent == true,cancellationToken);
            var orderStatus = orderStatuses.FirstOrDefault();
            if (orderStatus == null || orderStatus?.OrderStatusTypeId == (long)OrderStatusTypes.Dropped)
                throw new NotFoundException("NotFound");

            var orderAssigns = await _unitOfWork.OrderRepository.GetOrdersAssignmentsByAsync(a => a.OrderId == id,cancellationToken);
            var orderAssign = orderAssigns.FirstOrDefault();
            if (orderAssign == null) throw new NoContentException("NoContent");

            var usersLocations = await _unitOfWork.CaptainRepository.GetCaptainUsersCurrentLocationsByAsync(u => u.CaptainUserAccountId == orderAssign.CaptainUserAccountId,cancellationToken);
            var userLocation = usersLocations.FirstOrDefault();
            if (userLocation == null) throw new NoContentException("NoContent");

            return new { Latitude = userLocation.Lat, Longitude = userLocation.Long };



        }




        public async Task<IEnumerable<OrderItem>> GetOrderItemsAsync(long id, CancellationToken cancellationToken)
        {
            try
            {
                return await _unitOfWork.OrderRepository.GetOrdersItemsByAsync(i => i.OrderId == id,cancellationToken);


            }
            catch (Exception e)
            {
                return new List<OrderItem>();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        public async Task<object> GetQRCodeByOrderIdAsync(long id, CancellationToken cancellationToken)
        {

            var qRCode = await _unitOfWork.OrderRepository.GetQrcodeByOrderIdAsync(id,cancellationToken);
            if (qRCode == null) throw new NotFoundException("NotFound");

            qRCode.QrCodeUrl = Utility.ConvertImgToString(qRCode.Code);

            return new { CodeInBytes = qRCode.Code, QrCodeUrl = qRCode.QrCodeUrl };

        }


        /* public async Task<object> ReportAsync( FilterParameters reportParameters, HttpContext httpContext)
         {
             try
             {

                 var taskResult = await Task.Run(() => {
                     // 1-Check Role and Id
                     //var userType = "";
                     //var userId = long.Parse("0");
                     Utility.getRequestUserIdFromToken(httpContext, out string userId, out string userType);

                     IQueryable<Order> query;

                     if (userType == "Driver")
                     {
                         query = _unitOfWork.OrderRepository.GetUserAcceptedRequestByQuerable(u => u.CaptainUserAccountId == userId).Select(o => o.Order);
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

                 return taskResult;

             }
             catch (Exception e)
             {
                 return new { };// new ObjectResult(e.Message) { StatusCode = 666 };
             }

         }


         */

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



        public async Task<object> SearchDetailsAsync(FilterParameters parameters,CancellationToken cancellationToken)
        {
            try
            {



                    //var query = _unitOfWork.OrderRepository.GetAllOrdersQuerable();
                    var query = _unitOfWork.OrderRepository.GetAllOrdersDetailsQuerable();
                    var skip = (parameters.NumberOfObjectsPerPage * (parameters.Page - 1));
                    var take = parameters.NumberOfObjectsPerPage;
                    var totalResult = 0;
                    var ordersResult = Utility.GetFilter3<OrderDetails>(parameters, query, skip, take, out totalResult);
                    //var ordersResult = Utility.GetFilter2<Order>(parameters, query, skip, take,out totalResult);
                    var orders = await ordersResult.ToListAsync(cancellationToken);
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
               


            }
            catch (Exception e)
            {
                return new { };// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }
        /**/









        public async Task<object> ChartAsync(CancellationToken cancellationToken)
        {
            try
            {
                var taskResult = await Task.Run(() =>
                {
                    return _unitOfWork.OrderRepository.OrdersReportCount();
                },cancellationToken);

                return taskResult;
            }
            catch (Exception e)
            {
                return new { };// new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }




        public async Task<IEnumerable<OrderFilterResponse>> SearchAsync(OrderFilter orderFilter, CancellationToken cancellationToken)
        {
            try
            {
                if (orderFilter?.Page != null)
                    orderFilter.Page = (orderFilter?.NumberOfObjects * (orderFilter?.Page - 1));

                var result = await _unitOfWork.OrderRepository.FilterAsync(orderFilter,cancellationToken);
                return result;
            }
            catch (Exception e)
            {
                return new List<OrderFilterResponse>();// new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }











        public async Task<bool> SearchForCaptainAndNotifyOrder(Order order, CancellationToken cancellationToken)
        {
            try
            {


                using (var scope = _sp.CreateScope())
                {
                    //var _unitOfWork = new UnitOfWork(dbContext);
                    var services = scope.ServiceProvider;
                    var _unitOfWork = services.GetRequiredService<IUnitOfWork>();
                    var _notify = services.GetRequiredService<INotifyService>();
                    if (_unitOfWork == null) return false;
                    if (_notify == null) return false;

                    _ = await _notify.ChangeOrderStatusAndNotify(OrderStatusTypes.SearchingForCaptain, order.Id,
                        order.AgentId,cancellationToken);

                    var captain = await _unitOfWork.CaptainRepository.GetCaptainUserAccountNearestLocationAsync(order.PickupLocationLat, order.PickupLocationLong,cancellationToken);
                    if (captain == null)
                    {
                        _ = await _notify.ChangeOrderStatusAndNotify(OrderStatusTypes.NotAssignedToCaptain, order.Id,
                            order.AgentId,cancellationToken);

                        return false;
                    }


                    CaptainUserNewRequest driverRequest = new()
                    {
                        OrderId = order.Id,
                        CaptainUserAccountId = captain.Id,
                        AgentId = order.AgentId,
                        CreationDate = DateTime.Now
                    };

                    var insertNewRequestResult = await _unitOfWork.CaptainRepository.InsertCaptainUserNewRequestAsync(driverRequest,cancellationToken);
                    var result = await _unitOfWork.Save(cancellationToken);
                    if (result <= 0) return false;

                    var usersMessageHub = await _unitOfWork.CaptainRepository.GetCaptainUsersMessageHubsByAsync(u => u.CaptainUserAccountId == captain.Id,cancellationToken);
                    var userMessageHub = usersMessageHub.FirstOrDefault();
                    if (userMessageHub != null && userMessageHub.Id > 0)
                    {
                        var notificationReuslt = await Utility.SendFirebaseNotification(_hostingEnvironment, "newRequest", order.Id.ToString(), userMessageHub.ConnectionId,cancellationToken);
                        if (notificationReuslt?.Length == 0) return false;
                    }


                    int time = 1000;
                    System.Timers.Timer timer1 = new System.Timers.Timer
                    {
                        Interval = 1000 // one second
                    };
                    timer1.Enabled = true;
                    timer1.Elapsed += new ElapsedEventHandler((sender, elapsedEventArgs) => { time = time + 1000; });



                    bool isDriverAcceptOrder = false;
                    //bool isDriverRejectOrIgnoredOrder = false;
                    //bool isDriverIgnoredOrder = false;


                    do
                    {
                        var orderAssigns = await _unitOfWork.OrderRepository.GetOrdersAssignmentsByAsync(r => r.OrderId == order.Id,cancellationToken);
                        var orderAssign = orderAssigns.FirstOrDefault();
                        if (orderAssign != null && orderAssign?.Id > 0)
                        {
                            isDriverAcceptOrder = true;
                            break;
                        }

                        /*var ordersReject = await _unitOfWork.CaptainRepository.GetUsersRejectedRequestsByAsync(r => r.OrderId == order.Id);
						var rejectedOrder = ordersReject.FirstOrDefault();
						if (rejectedOrder != null)
							isDriverRejectOrIgnoredOrder = true;*/


                        var ordersIgnored = await _unitOfWork.CaptainRepository.GetCaptainUsersIgnoredRequestsByAsync(r => r.OrderId == order.Id,cancellationToken);
                        var ignoredOrder = ordersIgnored.FirstOrDefault();
                        if (ignoredOrder != null && ignoredOrder?.Id > 0)
                            break;

                    } while ( time <= 50000);
                    timer1.Enabled = false;


                    if (isDriverAcceptOrder) // case captain accept the request
                    {
                        /* Create QrCode and Insert*/
                        var qRCode = Utility.CreateQRCode(captain.Id, order.Id);
                        var qRCodeResult = await _unitOfWork.OrderRepository.InsertQrCodeAsync(qRCode,cancellationToken);
                        /* Create QrCode and Insert*/
                        result = await _unitOfWork.Save(cancellationToken);
                        if (result <= 0) return false;

                        _ = await _notify.ChangeOrderStatusAndNotify(OrderStatusTypes.AssignedToCaptain, order.Id, order.AgentId,cancellationToken);
                        return true;
                    }

                    // case captain reject or ignored the request, or didn't received the request because of firebase failure and the timeout passed
                    _ = await _notify.ChangeOrderStatusAndNotify(OrderStatusTypes.NotAssignedToCaptain, order.Id,
                        order.AgentId,cancellationToken);
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }



        /*public async Task<bool> ChangeOrderStatusAndNotify(OrderStatusTypes status, long orderId, long agentId)
		{

			try
			{

				//using (var dbContext = new SenderDBContext())
				using (var scope = _sp.CreateScope())
				{
					//var _unitOfWork = new UnitOfWork(dbContext);
					var services = scope.ServiceProvider;
					var _unitOfWork = services.GetRequiredService<IUnitOfWork>();
					if (_unitOfWork == null) return false;

					OrderCurrentStatus orderCurrentStatus = new OrderCurrentStatus()
					{
						OrderId = orderId,
						StatusTypeId = (long)status,
						IsCurrent = true
					};
					var insertResult = await _unitOfWork.OrderRepository.InsertOrderStatusAsync(orderCurrentStatus);
					var updateOrderResult =
						await _unitOfWork.OrderRepository.UpdateOrderCurrentStatusAsync(orderId, (long)status);

					var result = await _unitOfWork.Save();
					if (result <= 0) return false;

					_ = _notify.NotifyOrderStatusChanged(status, orderId, agentId);

					return true;
				}

			}
			catch (Exception e)
			{
				Console.Out.Write(e.Message);
				Console.Out.Write(e.InnerException);
				return false;
			}


		}

*/

        public async Task<object?> AddNewOrder(Order order, CancellationToken cancellationToken)
        {
            try
            {

                if (order == null || order.PickupLocationLat == null || order.PickupLocationLong == null ||
                    order.PickupLocationLat == "" || order.PickupLocationLong == "")
                    return null; // new ObjectResult("Your request has no data") { StatusCode = 406 };




                order.OrderStatusTypeId = (long)OrderStatusTypes.New;
                var orderInsertResult = await _unitOfWork.OrderRepository.InsertOrderAsync(order,cancellationToken);
                var result = await _unitOfWork.Save(cancellationToken);
                if (result <= 0) return null;


                OrderCurrentStatus orderCurrentStatus = new ()
                {
                    OrderId = orderInsertResult.Id,
                    OrderStatusTypeId = (long)OrderStatusTypes.New,
                    IsCurrent = true
                };
                var insertResult = await _unitOfWork.OrderRepository.InsertOrderStatusAsync(orderCurrentStatus,cancellationToken);
                var resultSecondeOperation = await _unitOfWork.Save(cancellationToken);
                if (resultSecondeOperation <= 0) return null;


                _ = await _notify.NotifyOrderStatusChanged(OrderStatusTypes.New, orderInsertResult.Id,
                    orderInsertResult.AgentId,cancellationToken);


                _ = SearchForCaptainAndNotifyOrder(orderInsertResult,cancellationToken);

                return new
                {
                    OrderNumber = orderInsertResult.Id,
                    OrderStatus = "New",
                    Message = "Order saved and starting search for near captain"
                };


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


    }
}