using TreePorts.DTO;
using System.Linq.Expressions;
using TreePorts.DTO.Records;

namespace TreePorts.Repositories
{
    public class OrderRepository : IOrderRepository
    {

        private readonly TreePortsDBContext _context;

        public OrderRepository(TreePortsDBContext context)
        {
            _context = context;
        }

        public async Task<Order?> DeleteOrderAsync(long id, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(u => u.Id == id,cancellationToken);
            if (order == null) return null;

            order.IsDeleted = true;
            _context.Entry<Order>(order).State = EntityState.Modified;
            return order;
        }

        public async Task<List<OrderCurrentStatus>> GetCurrentOrdersStatusesAsync(CancellationToken cancellationToken)
        {
            return await _context.OrderCurrentStatuses.Where(o => o.IsCurrent == true).ToListAsync(cancellationToken);
        }

        public async Task<List<OrderAssignment>> GetOrdersAssignmentsAsync(CancellationToken cancellationToken)
        {
            return await _context.OrderAssignments.ToListAsync(cancellationToken);
        }

        public async Task<List<OrderEndLocation>> GetOrdersEndLocationsAsync(CancellationToken cancellationToken)
        {
            return await _context.OrderEndLocations.ToListAsync(cancellationToken);
        }

        public async Task<List<OrderItem>> GetOrdersItemsAsync(CancellationToken cancellationToken)
        {
            return await _context.OrderItems.ToListAsync(cancellationToken);
        }

        public async Task<List<OrderStartLocation>> GetOrdersStartLocationsAsync(CancellationToken cancellationToken)
        {
            return await _context.OrderStartLocations.ToListAsync(cancellationToken);
        }

        public async Task<List<OrderCurrentStatus>> GetOrdersStatusesAsync(CancellationToken cancellationToken)
        {
            return await _context.OrderCurrentStatuses.ToListAsync(cancellationToken);
        }

        public async Task<List<OrderStatusHistory>> GetOrdersStatusHistoriesAsync(CancellationToken cancellationToken)
        {
            return await _context.OrderStatusHistories.ToListAsync(cancellationToken);
        }

        public async Task<List<OrderStatusType>> GetOrderStatusTypesAsync(CancellationToken cancellationToken)
        {
            return await _context.OrderStatusTypes.ToListAsync(cancellationToken);
        }

        public async Task<List<ProductType>> GetProductTypesAsync(CancellationToken cancellationToken)
        {
            return await _context.ProductTypes.ToListAsync(cancellationToken);
        }

        public async Task<List<OrderAssignment>> GetOrdersAssignmentsByAsync(Expression<Func<OrderAssignment, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.OrderAssignments
                .Where(predicate)
                .ToListAsync(cancellationToken);
        }

        public async Task<OrderAssignment?> GetOrderAssignmentByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.OrderAssignments.FirstOrDefaultAsync(a => a.Id == id,cancellationToken);
        }

        public async Task<List<Order>> GetOrdersByAsync(Expression<Func<Order, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.Orders
                .Where(predicate)
                .ToListAsync(cancellationToken);
        }

        public async Task<Order?> GetOrderById_oldBehaviourAsync(long id, CancellationToken cancellationToken)//GetOrderById
        {
            return await _context.Orders.AsNoTracking()
                .Where(a => a.Id == id)
                //.Include(o => o.Agent)
                //.Include(o => o.OrderItems)
                //.Include(o => o.PaymentType)
                //.Include(o => o.ProductType)
                //.Include(o => o.OrderCurrentStatus)
                //.Include(o => o.OrderStatusHistories)
                .FirstOrDefaultAsync(cancellationToken);
        }


        public async Task<Order?> GetOnlyOrderByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.Orders.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id,cancellationToken);
        }


        public async Task<Order?> GetLiteOrderByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.Orders.AsNoTracking()
                .Where(a => a.Id == id)
                //.Include(o => o.Agent)
                //.Include(o => o.OrderItems)
                //.Include(o => o.PaymentType)
                //.Include(o => o.ProductType)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<OrderDetails> GetOrderDetailsByIdAsync(long id, CancellationToken cancellationToken)
        {

            var query = await (from orders in _context.Orders.Where(o => o.Id == id).AsNoTracking()
                               join agents in _context.Agents.AsNoTracking() on orders.AgentId equals agents.Id
                               join productTypes in _context.ProductTypes.AsNoTracking() on orders.ProductTypeId equals productTypes.Id
                               join paymentTypes in _context.PaymentTypes.AsNoTracking() on orders.PaymentTypeId equals paymentTypes.Id
                               join orderCurrentStatus in _context.OrderCurrentStatuses.AsNoTracking() on orders.Id equals orderCurrentStatus.OrderId
                               join country in _context.Countries.AsNoTracking() on agents.CountryId equals country.Id
                               join city in _context.Cities.AsNoTracking() on agents.CityId equals city.Id
                               //where orders.Id == id
                               from deliveryPayments in _context.CaptainUserPayments.Where(p => p.OrderId == id).AsNoTracking().DefaultIfEmpty()
                               from userAcceptedRequests in _context.CaptainUserAcceptedRequests.Where(p => p.OrderId == id).AsNoTracking().DefaultIfEmpty()
                               from qrCodes in _context.OrderQrcodes.Where(q => q.OrderId == id).AsNoTracking().DefaultIfEmpty()
                                   //from items in _context.OrderItems.AsNoTracking().Where(i => i.OrderId == orders.Id).ToList()
                                   //join deliveryPayments in _context.UserPayments.AsNoTracking() on orders.Id equals deliveryPayments.OrderId into tempDeliveryPayments
                                   //// join orderItems in _context.OrderItems on orders.Id equals orderItems.OrderId
                                   //join qrCodes in _context.Qrcodes.AsNoTracking() on orders.Id equals qrCodes.OrderId
                                   //            // join orderStatusHistories in _context.OrderStatusHistories on orders.Id equals orderStatusHistories.OrderId

                                   //join userAcceptedRequests in _context.UserAcceptedRequests.AsNoTracking() on orders.Id equals userAcceptedRequests.OrderId
                                   //join users in _context.Users.AsNoTracking() on userAcceptedRequests.UserId equals users.Id
                                   // join userAccounts in _context.UserAccounts on users.Id equals userAccounts.Id


                                   //from users in _context.Users.AsNoTracking().Where(u =>  userAcceptedRequests.Id > 0 && u.Id == userAcceptedRequests.Id).DefaultIfEmpty()
                                   //from deliveryPayments in tempDeliveryPayments.DefaultIfEmpty()
                               select new OrderDetails()
                               {
                                   Order = orders,
                                   ProductType = productTypes,
                                   PaymentType = paymentTypes,
                                   OrderItems = _context.OrderItems.Where(i => i.OrderId == orders.Id).AsNoTracking().ToList(),
                                   OrderCurrentStatus = orderCurrentStatus,
                                   OrderStatusHistories = _context.OrderStatusHistories.Where(i => i.OrderId == orders.Id).AsNoTracking().ToList(),
                                   Captain = (userAcceptedRequests != null && userAcceptedRequests.Id > 0) ? _context.CaptainUserAccounts.Where(u => u.Id == userAcceptedRequests.CaptainUserAccountId).AsNoTracking().FirstOrDefault() : null,
                                   DeliveryPayment = deliveryPayments,
                                   QrCode = qrCodes,
                                   Agent = agents,
                                   Country = country,
                                   City = city
                               }).FirstOrDefaultAsync(cancellationToken);
            //_context.ChangeTracker.LazyLoadingEnabled = true;

            //var reuslt = query.Where(a => a.Agent.Id == 2).ToList();
            return query;
        }



        public async Task<List<OrderEndLocation>> GetOrdersEndLocationByAsync(Expression<Func<OrderEndLocation, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.OrderEndLocations.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<OrderEndLocation?> GetOrderEndLocationByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.OrderEndLocations.FirstOrDefaultAsync(a => a.Id == id,cancellationToken);
        }

        public async Task<List<OrderItem>> GetOrdersItemsByAsync(Expression<Func<OrderItem, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.OrderItems.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<OrderItem?> GetOrderItemByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.OrderItems.FirstOrDefaultAsync(a => a.Id == id,cancellationToken);
        }

        public async Task<List<Order>> GetOrdersAsync(CancellationToken cancellationToken)
        {
            return await _context.Orders
                //.Include(o => o.Agent)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<OrderStartLocation>> GetOrdersStartLocationByAsync(Expression<Func<OrderStartLocation, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.OrderStartLocations.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<OrderStartLocation?> GetOrderStartLocationByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.OrderStartLocations.FirstOrDefaultAsync(a => a.Id == id,cancellationToken);
        }

        public async Task<List<OrderCurrentStatus>> GetOrderCurrentStatusesByAsync(Expression<Func<OrderCurrentStatus, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.OrderCurrentStatuses.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<List<OrderStatusHistory>> GetOrdersStatusHistoriesByAsync(Expression<Func<OrderStatusHistory, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.OrderStatusHistories.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<OrderCurrentStatus?> GetOrderStatusByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.OrderCurrentStatuses.FirstOrDefaultAsync(a => a.Id == id,cancellationToken);
        }

        public async Task<OrderStatusHistory?> GetOrderStatusHistoryByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.OrderStatusHistories.FirstOrDefaultAsync(a => a.Id == id,cancellationToken);
        }

        public async Task<OrderStatusType?> GetOrderStatusTypeByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.OrderStatusTypes.FirstOrDefaultAsync(a => a.Id == id,cancellationToken);
        }

        public async Task<ProductType?> GetProductTypeByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.ProductTypes.FirstOrDefaultAsync(a => a.Id == id,cancellationToken);
        }

        public async Task<Order> InsertOrderAsync(Order order, CancellationToken cancellationToken)
        {
            order.CreationDate = DateTime.Now;
            var insertResult = await _context.Orders.AddAsync(order,cancellationToken);
            return insertResult.Entity;
        }

        public async Task<OrderAssignment> InsertOrderAssignmentAsync(OrderAssignment assign, CancellationToken cancellationToken)
        {
            assign.CreationDate = DateTime.Now;
            var insertResult = await _context.OrderAssignments.AddAsync(assign,cancellationToken);
            return insertResult.Entity;
        }

        public async Task<OrderEndLocation> InsertOrderEndLocationAsync(OrderEndLocation orderEndLocation, CancellationToken cancellationToken)
        {
            orderEndLocation.CreationDate = DateTime.Now;
            var insertResult = await _context.OrderEndLocations.AddAsync(orderEndLocation,cancellationToken);
            return insertResult.Entity;
        }

        public async Task<OrderItem> InsertOrderItemAsync(OrderItem orderItem, CancellationToken cancellationToken)
        {
            orderItem.CreationDate = DateTime.Now;
            var insertResult = await _context.OrderItems.AddAsync(orderItem,cancellationToken);
            return insertResult.Entity;
        }

        public async Task<OrderStartLocation> InsertOrderStartLocationAsync(OrderStartLocation orderStartLocation, CancellationToken cancellationToken)
        {
            orderStartLocation.CreationDate = DateTime.Now;
            var insertResult = await _context.OrderStartLocations.AddAsync(orderStartLocation,cancellationToken);
            return insertResult.Entity;
        }

        public async Task<OrderCurrentStatus> InsertOrderStatusAsync(OrderCurrentStatus orderStatus, CancellationToken cancellationToken)
        {
            var oldstatus = await _context.OrderCurrentStatuses.FirstOrDefaultAsync(s => s.OrderId == orderStatus.OrderId,cancellationToken);
            if (oldstatus != null && oldstatus.Id > 0)
            {
                OrderStatusHistory history = new ()
                {
                    OrderId = oldstatus.OrderId,
                    OrderStatusTypeId = oldstatus.OrderStatusTypeId,
                    CreationDate = oldstatus.CreationDate,

                };

                await this.InsertOrderStatusHistoryAsync(history,cancellationToken);

                oldstatus.OrderStatusTypeId = orderStatus.OrderStatusTypeId;
                oldstatus.CreationDate = DateTime.Now;

                _context.Entry<OrderCurrentStatus>(oldstatus).State = EntityState.Modified;
                return oldstatus;


            }

            orderStatus.CreationDate = DateTime.Now;
            var insertResult = await _context.OrderCurrentStatuses.AddAsync(orderStatus,cancellationToken);
            return insertResult.Entity;
        }

        public async Task<OrderStatusHistory> InsertOrderStatusHistoryAsync(OrderStatusHistory orderStatus, CancellationToken cancellationToken)
        {
            orderStatus.CreationDate = DateTime.Now;
            var insertResult = await _context.OrderStatusHistories.AddAsync(orderStatus,cancellationToken);
            return insertResult.Entity;
        }

        public async Task<Order?> UpdateOrderAsync(Order order, CancellationToken cancellationToken)
        {
            var oldOrder = await _context.Orders.FirstOrDefaultAsync(o => o.Id == order.Id,cancellationToken);
            if (oldOrder == null) return null;

            oldOrder.AgentId = order.AgentId;
            oldOrder.Description = order.Description;
            oldOrder.MoreDetails = order.MoreDetails;
            oldOrder.DropLocationLat = order.DropLocationLat;
            oldOrder.DropLocationLong = order.DropLocationLong;
            oldOrder.PickupLocationLat = order.PickupLocationLat;
            oldOrder.PickupLocationLong = order.PickupLocationLong;
            oldOrder.ProductTypeId = order.ProductTypeId;
            oldOrder.CustomerAddress = order.CustomerAddress;
            oldOrder.PaymentTypeId = order.PaymentTypeId;
            oldOrder.CustomerName = order.CustomerName;
            oldOrder.CustomerPhone = order.CustomerPhone;
            oldOrder.ModifiedBy = order.ModifiedBy;
            oldOrder.ModificationDate = DateTime.Now;
            oldOrder.DropLocationLat = order.DropLocationLat;
            oldOrder.DropLocationLong = order.DropLocationLong;
            oldOrder.PickupLocationLat = order.PickupLocationLat;
            oldOrder.PickupLocationLong = order.PickupLocationLong;

            /*if (order.OrderItems != null && order.OrderItems.Count > 0)
            {
                _context.OrderItems.RemoveRange(oldOrder.OrderItems);
                oldOrder.OrderItems = order.OrderItems;
            }*/


            _context.Entry<Order>(oldOrder).State = EntityState.Modified;
            return oldOrder;
        }

        public async Task<OrderAssignment?> UpdateOrderAssignmentAsync(OrderAssignment assign, CancellationToken cancellationToken)
        {
            var oldOrderAssign = await _context.OrderAssignments.FirstOrDefaultAsync(a => a.Id == assign.Id,cancellationToken);
            if (oldOrderAssign == null) return null;


            oldOrderAssign.OrderId = assign.OrderId;
            oldOrderAssign.CaptainUserAccountId = assign.CaptainUserAccountId;
            oldOrderAssign.ToAgentKilometer = assign.ToAgentKilometer;
            oldOrderAssign.ToAgentTime = assign.ToAgentTime;
            oldOrderAssign.ToCustomerKilometer = assign.ToCustomerKilometer;
            oldOrderAssign.ToCustomerTime = assign.ToCustomerTime;
            oldOrderAssign.ModifiedBy = assign.ModifiedBy;
            oldOrderAssign.ModificationDate = DateTime.Now;

            _context.Entry<OrderAssignment>(oldOrderAssign).State = EntityState.Modified;
            return oldOrderAssign;
        }

        public async Task<OrderEndLocation?> UpdateOrderEndLocationAsync(OrderEndLocation orderEndLocation, CancellationToken cancellationToken)
        {
            var oldOrderEndLocaion = await _context.OrderEndLocations.FirstOrDefaultAsync(a => a.Id == orderEndLocation.Id,cancellationToken);
            if (oldOrderEndLocaion == null) return null;


            oldOrderEndLocaion.OrderId = orderEndLocation.OrderId;
            oldOrderEndLocaion.OrderAssignId = orderEndLocation.OrderAssignId;
            oldOrderEndLocaion.DroppedLat = orderEndLocation.DroppedLat;
            oldOrderEndLocaion.DroppedLong = orderEndLocation.DroppedLong;
            oldOrderEndLocaion.ModifiedBy = orderEndLocation.ModifiedBy;
            oldOrderEndLocaion.ModificationDate = DateTime.Now;

            _context.Entry<OrderEndLocation>(oldOrderEndLocaion).State = EntityState.Modified;
            return oldOrderEndLocaion;
        }

        public async Task<OrderItem?> UpdateOrderItemAsync(OrderItem orderItem, CancellationToken cancellationToken)
        {
            var oldOrderItem = await _context.OrderItems.FirstOrDefaultAsync(a => a.Id == orderItem.Id,cancellationToken);
            if (oldOrderItem == null) return null;


            oldOrderItem.OrderId = orderItem.OrderId;
            oldOrderItem.Name = orderItem.Name;
            oldOrderItem.Price = orderItem.Price;
            oldOrderItem.Quantity = orderItem.Quantity;
            oldOrderItem.Description = orderItem.Description;
            oldOrderItem.ModifiedBy = orderItem.ModifiedBy;
            oldOrderItem.ModificationDate = DateTime.Now;

            _context.Entry<OrderItem>(oldOrderItem).State = EntityState.Modified;
            return oldOrderItem;
        }

        public async Task<OrderStartLocation?> UpdateOrderStartLocationAsync(OrderStartLocation orderStartLocation, CancellationToken cancellationToken)
        {
            var oldOrderStartLocation = await _context.OrderStartLocations.FirstOrDefaultAsync(a => a.Id == orderStartLocation.Id,cancellationToken);
            if (oldOrderStartLocation == null) return null;


            oldOrderStartLocation.OrderId = orderStartLocation.OrderId;
            oldOrderStartLocation.OrderAssignId = orderStartLocation.OrderAssignId;
            oldOrderStartLocation.PickedupLat = orderStartLocation.PickedupLat;
            oldOrderStartLocation.PickedupLong = orderStartLocation.PickedupLong;
            oldOrderStartLocation.ModifiedBy = orderStartLocation.ModifiedBy;
            oldOrderStartLocation.ModificationDate = DateTime.Now;

            _context.Entry<OrderStartLocation>(oldOrderStartLocation).State = EntityState.Modified;
            return oldOrderStartLocation;
        }



        public async Task<OrderStatusHistory?> UpdateOrderStatusHistoryAsync(OrderStatusHistory orderStatus, CancellationToken cancellationToken)
        {
            var oldOrderStatusHistory = await _context.OrderStatusHistories.FirstOrDefaultAsync(a => a.Id == orderStatus.Id,cancellationToken);
            if (oldOrderStatusHistory == null) return null;


            oldOrderStatusHistory.OrderId = orderStatus.OrderId;
            oldOrderStatusHistory.OrderStatusTypeId = orderStatus.OrderStatusTypeId;
            oldOrderStatusHistory.ModifiedBy = orderStatus.ModifiedBy;
            oldOrderStatusHistory.ModificationDate = DateTime.Now;

            _context.Entry<OrderStatusHistory>(oldOrderStatusHistory).State = EntityState.Modified;
            return oldOrderStatusHistory;
        }

        public async Task<List<OrderInvoice>> GetOrdersInvoicesAsync(CancellationToken cancellationToken)
        {
            return await _context.OrderInvoices.ToListAsync(cancellationToken);
        }

        public async Task<OrderInvoice?> GetOrderInvoiceByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.OrderInvoices.FirstOrDefaultAsync(i => i.Id == id,cancellationToken);
        }

        public async Task<List<OrderInvoice>> GetOrdersInvoicesByAsync(Expression<Func<OrderInvoice, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.OrderInvoices.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<OrderInvoice> InsertOrderInvoiceAsync(OrderInvoice orderInvoice, CancellationToken cancellationToken)
        {
            orderInvoice.CreationDate = DateTime.Now;
            var insertResult = await _context.OrderInvoices.AddAsync(orderInvoice,cancellationToken);
            return insertResult.Entity;
        }

        public async Task<OrderInvoice?> UpdateOrderInvoiceAsync(OrderInvoice orderInvoice, CancellationToken cancellationToken)
        {
            var oldOrderInvoice = await _context.OrderInvoices.FirstOrDefaultAsync(i => i.Id == orderInvoice.Id,cancellationToken);
            if (oldOrderInvoice == null) return null;


            oldOrderInvoice.CaptainUserAccountId = orderInvoice.CaptainUserAccountId;
            oldOrderInvoice.OrderId = orderInvoice.OrderId;
            oldOrderInvoice.OrderAssignId = orderInvoice.OrderAssignId;
            oldOrderInvoice.FileName = orderInvoice.FileName;
            oldOrderInvoice.FileType = orderInvoice.FileType;
            oldOrderInvoice.PaidOrderId = orderInvoice.PaidOrderId;
            oldOrderInvoice.ModifiedBy = orderInvoice.ModifiedBy;
            oldOrderInvoice.ModificationDate = DateTime.Now;

            _context.Entry<OrderInvoice>(oldOrderInvoice).State = EntityState.Modified;
            return oldOrderInvoice;


        }

        public async Task<OrderInvoice?> DeleteOrderInvoiceAsync(long id, CancellationToken cancellationToken)
        {
            var oldOrderInvoice = await _context.OrderInvoices.FirstOrDefaultAsync(i => i.Id == id,cancellationToken);
            if (oldOrderInvoice == null) return null;

            _context.OrderInvoices.Remove(oldOrderInvoice);
            return oldOrderInvoice;

        }

        public async Task<List<PaidOrder>> GetPaidOrdersAsync(CancellationToken cancellationToken)
        {
            return await _context.PaidOrders.ToListAsync(cancellationToken);
        }

        public async Task<PaidOrder?> GetPaidOrderByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.PaidOrders.FirstOrDefaultAsync(p => p.Id == id,cancellationToken);
        }

        public async Task<List<PaidOrder>> GetPaidOrdersByAsync(Expression<Func<PaidOrder, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.PaidOrders.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<PaidOrder> InsertPaidOrderAsync(PaidOrder paidOrder, CancellationToken cancellationToken)
        {
            paidOrder.CreationDate = DateTime.Now;
            var insertResult = await _context.PaidOrders.AddAsync(paidOrder,cancellationToken);
            return insertResult.Entity;
        }

        public async Task<PaidOrder?> UpdatePaidOrderAsync(PaidOrder paidOrder, CancellationToken cancellationToken)
        {
            var oldPaidOrder = await _context.PaidOrders.FirstOrDefaultAsync(p => p.Id == paidOrder.Id,cancellationToken);
            if (oldPaidOrder == null) return null;


            oldPaidOrder.CaptainUserAccountId = paidOrder.CaptainUserAccountId;
            oldPaidOrder.OrderId = paidOrder.OrderId;
            oldPaidOrder.OrderAssignId = paidOrder.OrderAssignId;
            oldPaidOrder.Type = paidOrder.Type;
            oldPaidOrder.Value = paidOrder.Value;
            oldPaidOrder.ModifiedBy = paidOrder.ModifiedBy;
            oldPaidOrder.ModificationDate = DateTime.Now;

            _context.Entry<PaidOrder>(oldPaidOrder).State = EntityState.Modified;
            return oldPaidOrder;
        }

        public async Task<PaidOrder?> DeletePaidOrderAsync(long id, CancellationToken cancellationToken)
        {
            var oldPaidOrder = await _context.PaidOrders.FirstOrDefaultAsync(p => p.Id == id,cancellationToken);
            if (oldPaidOrder == null) return null;


            _context.PaidOrders.Remove(oldPaidOrder);
            return oldPaidOrder;

        }

        public async Task<OrderCurrentStatus?> DeleteOrderStatusAsync(long id, CancellationToken cancellationToken)
        {
            var oldOrderCurrentStatus = await _context.OrderCurrentStatuses.FirstOrDefaultAsync(p => p.Id == id,cancellationToken);
            if (oldOrderCurrentStatus == null) return null;


            _context.OrderCurrentStatuses.Remove(oldOrderCurrentStatus);
            return oldOrderCurrentStatus;
        }

        public async Task<List<PaymentType>> GetPaymentTypesAsync(CancellationToken cancellationToken)
        {
            return await _context.PaymentTypes.ToListAsync(cancellationToken);
        }

        public async Task<PaymentType?> GetPaymentTypeByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.PaymentTypes.FirstOrDefaultAsync(p => p.Id == id,cancellationToken);
        }

        public async Task<Order?> DeleteOrderPermenetAsync(long id, CancellationToken cancellationToken)
        {
            var oldOrder = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id,cancellationToken);
            if (oldOrder == null) return null;

            _context.Orders.Remove(oldOrder);
            return oldOrder;

        }

        public async Task<List<RunningOrder>> GetRunningOrdersAsync(CancellationToken cancellationToken)
        {
            return await _context.RunningOrders.ToListAsync(cancellationToken);
        }

        public async Task<RunningOrder?> GetRunningOrderByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.RunningOrders.FirstOrDefaultAsync(r => r.Id == id,cancellationToken);
        }

        public async Task<List<RunningOrder>> GetRunningOrdersByAsync(Expression<Func<RunningOrder, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.RunningOrders.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<RunningOrder> InsertRunningOrderAsync(RunningOrder runningOrder, CancellationToken cancellationToken)
        {
            runningOrder.CreationDate = DateTime.Now;
            var insertResult = await _context.RunningOrders.AddAsync(runningOrder,cancellationToken);
            return insertResult.Entity;
        }

        public async Task<RunningOrder?> UpdateRunningOrderAsync(RunningOrder runningOrder, CancellationToken cancellationToken)
        {
            var oldRunningOrder = await _context.RunningOrders.FirstOrDefaultAsync(r => r.Id == runningOrder.Id,cancellationToken);
            if (oldRunningOrder == null) return null;// throw new NotFoundException("Running order not found");

            oldRunningOrder.OrderId = runningOrder.OrderId;
            oldRunningOrder.CaptainUserAccountId = runningOrder.CaptainUserAccountId;
            oldRunningOrder.ModificationDate = DateTime.Now;
            oldRunningOrder.ModifiedBy = runningOrder.ModifiedBy;
            _context.Entry<RunningOrder>(oldRunningOrder).State = EntityState.Modified;
            return oldRunningOrder;
        }


        public async Task<RunningOrder?> DeleteRunningOrderAsync(long id, CancellationToken cancellationToken)
        {
            var oldRunningOrder = await _context.RunningOrders.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
            if (oldRunningOrder == null) return null;// throw new NotFoundException("Running order not found");

            _context.RunningOrders.Remove(oldRunningOrder);
            return oldRunningOrder;
        }

        public async Task<RunningOrder?> DeleteRunningOrderByOrderIdAsync(long id, CancellationToken cancellationToken)
        {
            var oldRunningOrder = await _context.RunningOrders.FirstOrDefaultAsync(r => r.OrderId == id,cancellationToken);
            if (oldRunningOrder == null) return null; // throw new NotFoundException("Running order not found");

            _context.RunningOrders.Remove(oldRunningOrder);
            return oldRunningOrder;
        }

        /* QRCode For Order*/
        // Get By Order
        public async Task<OrderQrcode?> GetQrcodeByOrderIdAsync(long id, CancellationToken cancellationToken)
        {
            var qRCode = await _context.OrderQrcodes.FirstOrDefaultAsync(qr => qr.OrderId == id,cancellationToken);
            return qRCode;
        }
        // Insert
        public async Task<OrderQrcode> InsertQrCodeAsync(OrderQrcode qrcode, CancellationToken cancellationToken)
        {
            qrcode.CreationDate = DateTime.Now;
            var qRCode = await _context.OrderQrcodes.AddAsync(qrcode,cancellationToken);
            return qRCode.Entity;
        }
        // Delete
        public async Task<OrderQrcode?> DeleteQrCodeAsync(long id, CancellationToken cancellationToken)
        {
            var olderQRCode = await _context.OrderQrcodes.FirstOrDefaultAsync(qr => qr.Id == id,cancellationToken);
            if (olderQRCode == null) return null; // throw new NotFoundException("QRCode Not Found not found");
            _context.Remove(olderQRCode);
            return olderQRCode;
        }
        /* QRCode For Order*/

        /* Order Filtration*/



        public IQueryable<Order> GetByQuerable(Expression<Func<Order, bool>> predicate)
        {
            return _context.Orders
                //.Include(o => o.OrderCurrentStatus)
                //.Include(o => o.Agent)
                //.Include(o => o.Qrcodes)
                //.Include(o => o.PaymentType)
                //.Include(o => o.ProductType)
                //.Include(o => o.UserAcceptedRequests).ThenInclude(u => u.User).ThenInclude(c => c.City)
                //.Include(o => o.UserAcceptedRequests).ThenInclude(u => u.User).ThenInclude(c => c.Country)
                //.Include(o => o.OrderItems)
                //.Include(o => o.UserAcceptedRequests).ThenInclude(u => u.User).ThenInclude(c => c.UserAccounts)
                .Where(predicate)
                ;
        }

        public IQueryable<Order> GetAllOrdersQuerable()
        {

            return _context.Orders
                //.Include(o => o.OrderCurrentStatus)
                //.Include(o => o.OrderStatusHistories)
                //.Include(o => o.Agent).ThenInclude(o => o.Country)
                //.Include(o => o.Agent).ThenInclude(o => o.City)
                //.Include(o => o.PaymentType).Include(o => o.Qrcodes)
                //.Include(o => o.ProductType).Include(o => o.OrderItems)
                //.Include(o => o.UserAcceptedRequests).ThenInclude(u => u.User).ThenInclude(c => c.City)
                //.Include(c => c.UserAcceptedRequests).ThenInclude(u => u.User).ThenInclude(c => c.Country)
                //.Include(o => o.UserAcceptedRequests).ThenInclude(u => u.User).ThenInclude(c => c.UserAccounts)
             ;

        }



        //public IQueryable<Order> GetAllOrdersQuerable()
        //{
        //    return _context.Orders.AsNoTracking();
        //}


        public IQueryable<OrderDetails> GetAllOrdersDetailsQuerable()
        {


            var query = from orders in _context.Orders.AsNoTracking()
                            //join orderItems in _context.OrderItems on orders.Id equals orderItems.OrderId
                            //join qrCodes in _context.Qrcodes on orders.Id equals qrCodes.OrderId
                            //join orderCurrentStatus in _context.OrderCurrentStatuses on orders.Id equals orderCurrentStatus.OrderId
                            //join orderStatusHistories in _context.OrderStatusHistories on orders.Id equals orderStatusHistories.OrderId
                            //join agents in _context.Agents on orders.AgentId equals agents.Id
                            //join userAcceptedRequests in _context.UserAcceptedRequests on orders.Id equals userAcceptedRequests.OrderId
                            //join users in _context.Users on userAcceptedRequests.UserId equals users.Id
                            //join userAccounts in _context.UserAccounts on users.Id equals userAccounts.Id

                        select new OrderDetails()
                        {
                            Order = orders,
                            //ProductType = orders.ProductType,
                            //PaymentType = orders.PaymentType,
                            //OrderItems = orders.OrderItems,
                            //OrderCurrentStatus = orders.OrderCurrentStatus.FirstOrDefault(),
                            //OrderStatusHistories = orders.OrderStatusHistories,
                            //Captain = orders.UserAcceptedRequests.FirstOrDefault().User,
                            //DeliveryPayment = orders.UserAcceptedRequests.FirstOrDefault().User.UserPayments.FirstOrDefault(),
                            //QrCode = orders.Qrcodes.FirstOrDefault(),
                            //Agent = orders.Agent,
                            //Country = orders.Agent.Country,
                            //City = orders.Agent.City
                        };

            //var reuslt = query.Where(a => a.Agent.Id == 2).ToList();
            return query;
        }


        public IQueryable<Order> GetAllCaptainOrdersByCaptainUserAccountId(string id)
        {
            var acceptedOrders = _context.CaptainUserAcceptedRequests
                //.Include(o => o.Order).ThenInclude(o => o.UserAcceptedRequests)
                .Where(u => u.CaptainUserAccountId == id)
                .Select(AcceptedOrder => AcceptedOrder.OrderId);

            //var rejectedOrders = _context.UserRejectedRequests.Include(o => o.Order)
            //                             .ThenInclude(o => o.UserRejectedRequests)
            //                        .Where(u => u.UserId == id).Select(RejectedOrders => RejectedOrders.Order); 
            //var ignoredOrders = _context.UserIgnoredRequests.Include(o => o.Order)
            //                            .ThenInclude(o => o.UserIgnoredRequests)
            //                        .Where(u => u.UserId == id).Select(IgnoredOrders => IgnoredOrders.Order);

            //var allOrders = acceptedOrders.Union(rejectedOrders).Union(ignoredOrders).OrderByDescending(o => o.CreationDate);
            var allOrders =  _context.Orders.Where(o => acceptedOrders.Contains(o.Id) ).OrderByDescending(o => o.CreationDate);

            return allOrders;
        }

        public Object GetDriverOrdersWithType(long id)
        {
            return new object();
        }

        public async Task<Order?> UpdateOrderLocationAsync(Order order, CancellationToken cancellationToken)
        {
            var oldOrder = await _context.Orders.FirstOrDefaultAsync(o => o.Id == order.Id,cancellationToken);
            if (oldOrder == null) return null;

            oldOrder.DropLocationLat = order.DropLocationLat;
            oldOrder.DropLocationLong = order.DropLocationLong;
            oldOrder.PickupLocationLat = order.PickupLocationLat;
            oldOrder.PickupLocationLong = order.PickupLocationLong;


            /*if (order.OrderItems != null && order.OrderItems.Count > 0)
            {
                _context.OrderItems.RemoveRange(oldOrder.OrderItems);
                oldOrder.OrderItems = order.OrderItems;
            }*/


            _context.Entry<Order>(oldOrder).State = EntityState.Modified;
            return oldOrder;
        }
        //public Object GetDriverOrdersWithType(long id)

        //{
        //    var acceptedOrders = _context.UserAcceptedRequests.Include(o => o.Order)
        //                             .Where(u => u.UserId == id).Select(AcceptedOrder => AcceptedOrder.Order);
        //    var rejectedOrders = _context.UserRejectedRequests.Include(o => o.Order)
        //                            .Where(u => u.UserId == id).Select(AcceptedOrder => AcceptedOrder.Order);
        //    var ignoredOrders = _context.UserIgnoredRequests.Include(o => o.Order)
        //                         .Where(u => u.UserId == id).Select(AcceptedOrder => AcceptedOrder.Order);
        //    return new 
        //    {
        //        AcceptedOrders = acceptedOrders,
        //        AcceptedOrdersCount = acceptedOrders.Count(),
        //        RejectedOrders = rejectedOrders,
        //        RejectedOrdersCount = rejectedOrders.Count(),
        //        IgnoredOrders = ignoredOrders,
        //        IgnoredOrdersCount = ignoredOrders.Count()
        //    };


        //}

        /* Order Filtration*/


        /**/
        public int GetTotalOrdersCount()
        {
            return _context.Orders.Count();
        }



        public object OrdersReportCount()
        {
            var totalOrders = _context.Orders.Count();

            var newOrders = _context.OrderCurrentStatuses.Where(o => o.OrderStatusTypeId == (long)OrderStatusTypes.New).Count();
            var assignedOrders = _context.OrderCurrentStatuses.Where(o => o.OrderStatusTypeId == (long)OrderStatusTypes.AssignedToCaptain).Count();
            var deliveredOrders = _context.OrderCurrentStatuses.Where(o => o.OrderStatusTypeId == (long)OrderStatusTypes.Delivered).Count();
            var cancelledOrders = _context.OrderCurrentStatuses.Where(o => o.OrderStatusTypeId == (long)OrderStatusTypes.Canceled).Count();
            var pickedUpOrders = _context.OrderCurrentStatuses.Where(o => o.OrderStatusTypeId == (long)OrderStatusTypes.PickedUp).Count();
            var progressOrders = _context.OrderCurrentStatuses.Where(o => o.OrderStatusTypeId == (long)OrderStatusTypes.Progress).Count();
            var droppedOrders = _context.OrderCurrentStatuses.Where(o => o.OrderStatusTypeId == (long)OrderStatusTypes.Dropped).Count();
            var endOrders = _context.OrderCurrentStatuses.Where(o => o.OrderStatusTypeId == (long)OrderStatusTypes.End).Count();
            return new { OrdersCount = totalOrders, DeliveredCount = deliveredOrders, CancelldCount = cancelledOrders, NewOrders = newOrders,

                AssignedToCaptainCount = assignedOrders, PickedUpCount = pickedUpOrders, InProgressCount = progressOrders,
                DroppedCount = droppedOrders, EndCount = endOrders

            };
        }

        /**/
        /*
         * 
         */
        public object GetUserOrdersGroupedByDay(Expression<Func<OrderCurrentStatus, bool>> predicate)
        {
            var result = _context.OrderCurrentStatuses.Where(predicate).GroupBy(p => p.CreationDate, p => p,
                (key, g) => new { CreationDate = key, g = g.ToList() });
            return result;
        }
        public IQueryable<CaptainUserAcceptedRequest> GetUserAcceptedRequestByQuerable(Expression<Func<CaptainUserAcceptedRequest, bool>> predicate)
        {
            var result = _context.CaptainUserAcceptedRequests
                //.Include(o => o.Order).ThenInclude(o => o.OrderCurrentStatus)
                //.Include(o => o.Order).ThenInclude(o => o.OrderItems).Include(o => o.Order)
                //.ThenInclude(o => o.PaymentType)
                .Where(predicate);

            return result;

        }

        public async Task<Order?> UpdateOrderCurrentStatusAsync(long orderId, long CurrentStatusId, CancellationToken cancellationToken)
        {
            var oldOrder = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId,cancellationToken);
            if (oldOrder == null) return null;

            oldOrder.OrderStatusTypeId = CurrentStatusId;
            oldOrder.ModificationDate = DateTime.Now;
            _context.Entry<Order>(oldOrder).State = EntityState.Modified;
            return oldOrder;
        }



        public async Task<List<OrderFilterResponse>> ReportAsync(OrderFilter orderFilter, CancellationToken cancellationToken)
        {
            bool isOrderHasWhereQuery = false;
            string selectColumnsQuery = "";
            string orderQuery = " \n ";
            string whereOrderQuery = "";
            string bodyQuery = "";
            string orderStatusHistoriesQuery = " \n ";
            string paginationQuery = " \n ";

            selectColumnsQuery = $@"
       select DISTINCT
       Orders.ID as OrderID,
       Orders.CurrentStatus as OrderCurrentStatus,
       Agents.Fullname as AgentName,
       Orders.ProductTypeID as ProductTypeID,
       Orders.PaymentTypeID as PaymentTypeID,
       Orders.CustomerName as CustomerName,
       Orders.CustomerAddress,
       Orders.CustomerPhone,
       UserPayments.Value as DeliveryAmount,
       Users.FirstName+' '+ Users.FamilyName as CaptainName,
       Orders.CreationDate as OrderCreationDate,
       convert(varchar(2),FORMAT(DATEDIFF(second,Orders.CreationDate, OrderCurrentStatuses.CreationDate)/3600,'0#')) +':'+
       convert(varchar(2),FORMAT(DATEDIFF(second, Orders.CreationDate, OrderCurrentStatuses.CreationDate)%3600/60,'0#')) +':'+
       convert(varchar(2),FORMAT(DATEDIFF(second, Orders.CreationDate, OrderCurrentStatuses.CreationDate)%60,'0#')) AS DurationToCurrentStatus,
       OrderStatusHistories.CreationDate as OrderStatusHistoryCreationDate,
       convert(varchar(2),FORMAT(DATEDIFF(second,Orders.CreationDate, OrderStatusHistories.CreationDate)/3600,'0#')) +':'+
       convert(varchar(2),FORMAT(DATEDIFF(second, Orders.CreationDate, OrderStatusHistories.CreationDate)%3600/60,'0#')) +':'+
       convert(varchar(2),FORMAT(DATEDIFF(second, Orders.CreationDate, OrderStatusHistories.CreationDate)%60,'0#')) AS DurationToStatusHistory ";


            if (orderFilter?.OrderId != null && orderFilter?.OrderId > 0)
            {
                whereOrderQuery += $" O.ID = {orderFilter?.OrderId} and ";
                isOrderHasWhereQuery = true;
            }


            if (orderFilter?.AgentId != null && orderFilter?.AgentId > 0)
            {
                whereOrderQuery += $" O.AgentID = {orderFilter?.AgentId} and ";
                isOrderHasWhereQuery = true;
            }


            if (orderFilter?.OrderCurrentStatusId != null && orderFilter?.OrderCurrentStatusId > 0)
            {
                whereOrderQuery += $" O.CurrentStatus = {orderFilter?.OrderCurrentStatusId} and ";
                isOrderHasWhereQuery = true;
            }

            if (orderFilter?.ProductTypeId != null && orderFilter?.ProductTypeId > 0)
            {
                whereOrderQuery += $" O.ProductTypeID = {orderFilter?.ProductTypeId} and ";
                isOrderHasWhereQuery = true;
            }

            if (orderFilter?.PaymentTypeId != null && orderFilter?.PaymentTypeId > 0)
            {
                whereOrderQuery += $" O.PaymentTypeID = {orderFilter?.PaymentTypeId} and ";
                isOrderHasWhereQuery = true;
            }

            if (orderFilter?.StartDate != null && orderFilter?.EndDate != null)
            {
                whereOrderQuery += $" O.CreationDate BETWEEN '{orderFilter?.StartDate.ToString()}' AND '{orderFilter?.EndDate.ToString()}' and ";
                isOrderHasWhereQuery = true;
            }


            if (isOrderHasWhereQuery)
            {
                whereOrderQuery = whereOrderQuery.Trim();
                whereOrderQuery = whereOrderQuery.Remove(whereOrderQuery.LastIndexOf(' ')).TrimEnd();
                orderQuery += $" from ( select O.* from Orders as O where  {whereOrderQuery} ) Orders ";

            }
            else
                orderQuery += $" from Orders ";

            if (orderFilter?.OrderStatusHistoryStatusId != null && orderFilter?.OrderStatusHistoryStatusId > 0)
                orderStatusHistoriesQuery +=
                    $" inner join OrderStatusHistories  on Orders.ID = OrderStatusHistories.OrderID and OrderStatusHistories.OrderStatusID = {orderFilter?.OrderStatusHistoryStatusId.ToString()} ";
            else
                orderStatusHistoriesQuery += " left join OrderStatusHistories  on Orders.ID = OrderStatusHistories.OrderID ";

            bodyQuery = $@"
            left join OrderAssignments on OrderAssignments.OrderID = Orders.ID
            left join Users on Users.ID = OrderAssignments.UserID
            join Agents on Orders.AgentID = Agents.ID
            left join UserPayments on Users.ID = UserPayments.UserID
            left join OrderCurrentStatuses on Orders.ID = OrderCurrentStatuses.OrderID
            {orderStatusHistoriesQuery}
            order by Orders.CreationDate desc ";

            if (orderFilter?.Page != null && orderFilter?.Page > 0)
                paginationQuery += $" OFFSET {orderFilter?.Page.ToString()} ROWS ";
            else
                paginationQuery += $" OFFSET 0 ROWS ";

            if (orderFilter?.NumberOfObjects != null && orderFilter?.NumberOfObjects > 0)
                paginationQuery += $" FETCH NEXT {orderFilter?.NumberOfObjects} ROWS ONLY ";


            string query = selectColumnsQuery + orderQuery + bodyQuery + paginationQuery;


            List<OrderFilterResponse> result = new ();
            var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync(cancellationToken);
            var command = conn.CreateCommand();
            command.CommandText = query;
            using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (reader.Read())
            {

                long? orderID = null;
                long? orderCurrentStatusId = null;
                string? agentName = null;
                long? productTypeID = null;
                long? paymentTypeID = null;
                string? customerName = null;
                string? customerAddress = null;
                string? customerPhone = null;
                decimal? deliveryAmount = null;
                string? captainName = null;
                DateTime? orderCreationDate = null;
                string? durationToCurrentStatus = null;
                DateTime? orderStatusHistoryCreationDate = null;
                string? durationToStatusHistory = null;


                if (!reader.IsDBNull(reader.GetOrdinal("OrderID")))
                    orderID = reader.GetInt64(reader.GetOrdinal("OrderID"));

                if (!reader.IsDBNull(reader.GetOrdinal("OrderCurrentStatus")))
                    orderCurrentStatusId = reader.GetInt64(reader.GetOrdinal("OrderCurrentStatus"));

                if (!reader.IsDBNull(reader.GetOrdinal("AgentName")))
                    agentName = reader.GetString(reader.GetOrdinal("AgentName"));

                if (!reader.IsDBNull(reader.GetOrdinal("ProductTypeID")))
                    productTypeID = reader.GetInt64(reader.GetOrdinal("ProductTypeID"));

                if (!reader.IsDBNull(reader.GetOrdinal("PaymentTypeID")))
                    paymentTypeID = reader.GetInt64(reader.GetOrdinal("PaymentTypeID"));

                if (!reader.IsDBNull(reader.GetOrdinal("CustomerName")))
                    customerName = reader.GetString(reader.GetOrdinal("CustomerName"));

                if (!reader.IsDBNull(reader.GetOrdinal("CustomerAddress")))
                    customerAddress = reader.GetString(reader.GetOrdinal("CustomerAddress"));

                if (!reader.IsDBNull(reader.GetOrdinal("CustomerPhone")))
                    customerPhone = reader.GetString(reader.GetOrdinal("CustomerPhone"));

                if (!reader.IsDBNull(reader.GetOrdinal("DeliveryAmount")))
                    deliveryAmount = reader.GetDecimal(reader.GetOrdinal("DeliveryAmount"));

                if (!reader.IsDBNull(reader.GetOrdinal("CaptainName")))
                    captainName = reader.GetString(reader.GetOrdinal("CaptainName"));

                if (!reader.IsDBNull(reader.GetOrdinal("OrderCreationDate")))
                    orderCreationDate = reader.GetDateTime(reader.GetOrdinal("OrderCreationDate"));

                if (!reader.IsDBNull(reader.GetOrdinal("DurationToCurrentStatus")))
                    durationToCurrentStatus = reader.GetString(reader.GetOrdinal("DurationToCurrentStatus"));

                if (!reader.IsDBNull(reader.GetOrdinal("OrderStatusHistoryCreationDate")))
                    orderStatusHistoryCreationDate = reader.GetDateTime(reader.GetOrdinal("OrderStatusHistoryCreationDate"));

                if (!reader.IsDBNull(reader.GetOrdinal("DurationToStatusHistory")))
                    durationToStatusHistory = reader.GetString(reader.GetOrdinal("DurationToStatusHistory"));

                OrderFilterResponse data = new (
                
                    OrderId : orderID,
                    OrderCurrentStatus : orderCurrentStatusId,
                    AgentName : agentName,
                    ProductTypeId : productTypeID,
                    PaymentTypeId : paymentTypeID,
                    CustomerName : customerName,
                    CustomerAddress : customerAddress,
                    CustomerPhone : customerPhone,
                    DeliveryAmount : deliveryAmount,
                    CaptainName : captainName,
                    OrderCreationDate : orderCreationDate,
                    DurationToCurrentStatus : durationToCurrentStatus,
                    OrderStatusHistoryCreationDate : orderStatusHistoryCreationDate,
                    DurationToStatusHistory : durationToStatusHistory
                );

                result.Add(data);


            }

            //reader.Close();
            return result;
        }

        public async Task<List<OrderFilterResponse>> FilterAsync(OrderFilter orderFilter, CancellationToken cancellationToken)
        {
            bool isOrderHasWhereQuery = false;
            string selectColumnsQuery = "";
            string orderQuery = " \n ";
            string whereOrderQuery = "";
            string bodyQuery = "";
            //string orderStatusHistoriesQuery = " \n ";
            string paginationQuery = " \n ";

            selectColumnsQuery = $@"
       select 
       Orders.ID as OrderID,
       Orders.ProductTypeID,
       Orders.PaymentTypeID,
       Orders.CurrentStatus as OrderCurrentStatus,
       Agents.Fullname as AgentName,
       Orders.CustomerName,
       Orders.CustomerPhone,
       Orders.CustomerAddress,
       UserPayments.Value as DeliveryAmount,
       Users.FirstName+' '+ Users.FamilyName as CaptainName,
       Orders.CreationDate as OrderCreationDate,
       convert(varchar(2),FORMAT(DATEDIFF(second,Orders.CreationDate, OrderCurrentStatuses.CreationDate)/3600,'0#')) +':'+
       convert(varchar(2),FORMAT(DATEDIFF(second, Orders.CreationDate, OrderCurrentStatuses.CreationDate)%3600/60,'0#')) +':'+
       convert(varchar(2),FORMAT(DATEDIFF(second, Orders.CreationDate, OrderCurrentStatuses.CreationDate)%60,'0#')) AS DurationToCurrentStatus 
       from ";


            if (orderFilter?.OrderId != null && orderFilter?.OrderId > 0)
            {
                whereOrderQuery += $" Orders.ID = {orderFilter?.OrderId} and ";
                isOrderHasWhereQuery = true;
            }


            if (orderFilter?.AgentId != null && orderFilter?.AgentId > 0)
            {
                whereOrderQuery += $" Orders.AgentID = {orderFilter?.AgentId} and ";
                isOrderHasWhereQuery = true;
            }


            if (orderFilter?.OrderCurrentStatusId != null && orderFilter?.OrderCurrentStatusId > 0)
            {
                whereOrderQuery += $" Orders.CurrentStatus = {orderFilter?.OrderCurrentStatusId} and ";
                isOrderHasWhereQuery = true;
            }

            if (orderFilter?.ProductTypeId != null && orderFilter?.ProductTypeId > 0)
            {
                whereOrderQuery += $" Orders.ProductTypeID = {orderFilter?.ProductTypeId} and ";
                isOrderHasWhereQuery = true;
            }

            if (orderFilter?.PaymentTypeId != null && orderFilter?.PaymentTypeId > 0)
            {
                whereOrderQuery += $" Orders.PaymentTypeID = {orderFilter?.PaymentTypeId} and ";
                isOrderHasWhereQuery = true;
            }

            if (orderFilter?.StartDate != null && orderFilter?.EndDate != null)
            {
                whereOrderQuery += $" Orders.CreationDate BETWEEN '{orderFilter?.StartDate.ToString()}' AND '{orderFilter?.EndDate.ToString()}' and ";
                isOrderHasWhereQuery = true;
            }


            if (orderFilter?.Page != null && orderFilter?.Page > 0)
                paginationQuery += $" OFFSET {orderFilter?.Page.ToString()} ROWS ";
            else
                paginationQuery += $" OFFSET 0 ROWS ";

            if (orderFilter?.NumberOfObjects != null && orderFilter?.NumberOfObjects > 0)
                paginationQuery += $" FETCH NEXT {orderFilter?.NumberOfObjects} ROWS ONLY ";




            if (isOrderHasWhereQuery)
            {
                whereOrderQuery = whereOrderQuery.Trim();
                whereOrderQuery = whereOrderQuery.Remove(whereOrderQuery.LastIndexOf(' ')).TrimEnd();
                whereOrderQuery = $" where {whereOrderQuery}";

            }
            // else
            //     orderQuery += $@" from ( select O.* from Orders as O order by O.CreationDate desc {paginationQuery} ) Orders  ";
            //

            orderQuery += $@" ( select Orders.ID , Orders.AgentID , Orders.CurrentStatus , Orders.CustomerName , Orders.CustomerPhone , Orders.CustomerAddress , Orders.CreationDate , Orders.ProductTypeID , Orders.PaymentTypeID 
                                from Orders 
                                {whereOrderQuery}   
                                order by Orders.CreationDate desc 
                                {paginationQuery} 
                               ) Orders ";



            bodyQuery = $@"
            join Agents on Orders.AgentID = Agents.ID
            left join UserPayments on Orders.ID = UserPayments.OrderID
            left join OrderCurrentStatuses on Orders.ID = OrderCurrentStatuses.OrderID
            left join (
                        select OrderAssignments.OrderID, Users.FirstName , Users.FamilyName
                        from OrderAssignments
                        inner join Users on Users.ID = OrderAssignments.UserID
                      ) Users on Users.OrderID = Orders.ID
            ";


            // if (orderFilter?.OrderStatusHistoryStatusId != null && orderFilter?.OrderStatusHistoryStatusId > 0)
            //     orderStatusHistoriesQuery +=
            //         $@"left join (
            //             SELECT OrderID, MAX(CreationDate) AS CreationDate
            //             FROM OrderStatusHistories
            //             GROUP BY OrderID
            //           ) OrderStatusHistories 
            //             on Orders.ID = OrderStatusHistories.OrderID = {orderFilter?.OrderStatusHistoryStatusId.ToString()}";
            //         








            string query = selectColumnsQuery + orderQuery + bodyQuery;


            List<OrderFilterResponse> result = new();
            var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync(cancellationToken);
            var command = conn.CreateCommand();
            command.CommandText = query;
            using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (reader.Read())
            {

                long? orderID = null;
                long? orderCurrentStatusId = null;
                string? agentName = null;
                long? productTypeID = null;
                long? paymentTypeID = null;
                string? customerName = null;
                string? customerAddress = null;
                string? customerPhone = null;
                decimal? deliveryAmount = null;
                string? captainName = null;
                DateTime? orderCreationDate = null;
                string? durationToCurrentStatus = null;
                // DateTime? orderStatusHistoryCreationDate = null;
                // string durationToStatusHistory = null;


                if (!reader.IsDBNull(reader.GetOrdinal("OrderID")))
                    orderID = reader.GetInt64(reader.GetOrdinal("OrderID"));

                if (!reader.IsDBNull(reader.GetOrdinal("OrderCurrentStatus")))
                    orderCurrentStatusId = reader.GetInt64(reader.GetOrdinal("OrderCurrentStatus"));

                if (!reader.IsDBNull(reader.GetOrdinal("AgentName")))
                    agentName = reader.GetString(reader.GetOrdinal("AgentName"));

                if (!reader.IsDBNull(reader.GetOrdinal("ProductTypeID")))
                    productTypeID = reader.GetInt64(reader.GetOrdinal("ProductTypeID"));

                if (!reader.IsDBNull(reader.GetOrdinal("PaymentTypeID")))
                    paymentTypeID = reader.GetInt64(reader.GetOrdinal("PaymentTypeID"));

                if (!reader.IsDBNull(reader.GetOrdinal("CustomerName")))
                    customerName = reader.GetString(reader.GetOrdinal("CustomerName"));

                if (!reader.IsDBNull(reader.GetOrdinal("CustomerAddress")))
                    customerAddress = reader.GetString(reader.GetOrdinal("CustomerAddress"));

                if (!reader.IsDBNull(reader.GetOrdinal("CustomerPhone")))
                    customerPhone = reader.GetString(reader.GetOrdinal("CustomerPhone"));

                if (!reader.IsDBNull(reader.GetOrdinal("DeliveryAmount")))
                    deliveryAmount = reader.GetDecimal(reader.GetOrdinal("DeliveryAmount"));

                if (!reader.IsDBNull(reader.GetOrdinal("CaptainName")))
                    captainName = reader.GetString(reader.GetOrdinal("CaptainName"));

                if (!reader.IsDBNull(reader.GetOrdinal("OrderCreationDate")))
                    orderCreationDate = reader.GetDateTime(reader.GetOrdinal("OrderCreationDate"));

                if (!reader.IsDBNull(reader.GetOrdinal("DurationToCurrentStatus")))
                    durationToCurrentStatus = reader.GetString(reader.GetOrdinal("DurationToCurrentStatus"));

                // if (!reader.IsDBNull(reader.GetOrdinal("OrderStatusHistoryCreationDate")))
                //     orderStatusHistoryCreationDate = reader.GetDateTime(reader.GetOrdinal("OrderStatusHistoryCreationDate"));
                //
                // if (!reader.IsDBNull(reader.GetOrdinal("DurationToStatusHistory")))
                //     durationToStatusHistory = reader.GetString(reader.GetOrdinal("DurationToStatusHistory"));
                //
                OrderFilterResponse data = new (
                
                    OrderId : orderID,
                    OrderCurrentStatus : orderCurrentStatusId,
                    AgentName : agentName,
                    ProductTypeId : productTypeID,
                    PaymentTypeId : paymentTypeID,
                    CustomerName : customerName,
                    CustomerAddress : customerAddress,
                    CustomerPhone : customerPhone,
                    DeliveryAmount : deliveryAmount,
                    CaptainName : captainName,
                    OrderCreationDate : orderCreationDate,
                    DurationToCurrentStatus : durationToCurrentStatus,
                    OrderStatusHistoryCreationDate: null,
                    DurationToStatusHistory :null
                );

                result.Add(data);


            }

            //reader.Close();
            return result;
        }



        public async Task<List<Order>> GetOrdersPaginationAsync(int skip, int take, CancellationToken cancellationToken)
        {
            return await _context.Orders
                .OrderByDescending(o => o.CreationDate)
                .Skip(skip)
                .Take(take)
                .ToListAsync(cancellationToken);
        }


        public async Task<OrderAssignment?> DeleteOrderAssignmentAsync(long id, CancellationToken cancellationToken) 
        {
            var oldOrderAssigne = await _context.OrderAssignments.FirstOrDefaultAsync(a => a.Id == id,cancellationToken);
            if (oldOrderAssigne == null) return null;

            _context.OrderAssignments.Remove(oldOrderAssigne);
            return oldOrderAssigne;
        }

    }
}
