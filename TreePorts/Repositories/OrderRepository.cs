using Microsoft.EntityFrameworkCore;
using TreePorts.DTO;
using TreePorts.DTO.ReturnDTO;
using TreePorts.Interfaces.Repositories;
using TreePorts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TreePorts.Repositories
{
    public class OrderRepository : IOrderRepository
    {

        private readonly TreePortsDBContext _context;

        public OrderRepository(TreePortsDBContext context)
        {
            _context = context;
        }

        public async Task<Order> DeleteOrderAsync(long id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(u => u.Id == id);
            if (order == null) return null;

            order.IsDeleted = true;
            _context.Entry<Order>(order).State = EntityState.Modified;
            return order;
        }

        public async Task<List<OrderCurrentStatus>> GetCurrentOrdersStatusesAsync()
        {
            return await _context.OrderCurrentStatuses.Where(o => o.IsCurrent == true).ToListAsync();
        }

        public async Task<List<OrderAssignment>> GetOrdersAssignmentsAsync()
        {
            return await _context.OrderAssignments.ToListAsync();
        }

        public async Task<List<OrderEndLocation>> GetOrdersEndLocationsAsync()
        {
            return await _context.OrderEndLocations.ToListAsync();
        }

        public async Task<List<OrderItem>> GetOrdersItemsAsync()
        {
            return await _context.OrderItems.ToListAsync();
        }

        public async Task<List<OrderStartLocation>> GetOrdersStartLocationsAsync()
        {
            return await _context.OrderStartLocations.ToListAsync();
        }

        public async Task<List<OrderCurrentStatus>> GetOrdersStatusesAsync()
        {
            return await _context.OrderCurrentStatuses.ToListAsync();
        }

        public async Task<List<OrderStatusHistory>> GetOrdersStatusHistoriesAsync()
        {
            return await _context.OrderStatusHistories.ToListAsync();
        }

        public async Task<List<OrderStatusType>> GetOrderStatusTypesAsync()
        {
            return await _context.OrderStatusTypes.ToListAsync();
        }

        public async Task<List<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }

        public async Task<List<OrderAssignment>> GetOrdersAssignmentsByAsync(Expression<Func<OrderAssignment, bool>> predicate)
        {
            return await _context.OrderAssignments
                .Where(predicate)
                .Include(u => u.User)
                .ToListAsync();
        }

        public async Task<OrderAssignment> GetOrderAssignmentByIdAsync(long id)
        {
            return await _context.OrderAssignments.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Order>> GetOrdersByAsync(Expression<Func<Order, bool>> predicate)
        {
            return await _context.Orders
                .Where(predicate)
                .Include(o => o.Agent)
                .ToListAsync();
        }

        public async Task<Order> GetOrderById_oldBehaviourAsync(long id)//GetOrderById
        {
            return await _context.Orders.AsNoTracking()
                .Where(a => a.Id == id)
                .Include(o => o.Agent)
                .Include(o => o.OrderItems)
                .Include(o => o.PaymentType)
                .Include(o => o.ProductType)
                .Include(o => o.OrderCurrentStatus)
                .Include(o => o.OrderStatusHistories)
                .FirstOrDefaultAsync();
        }


        public async Task<Order> GetOnlyOrderByIdAsync(long id)
        {
            return await _context.Orders.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }


        public async Task<Order> GetLiteOrderByIdAsync(long id)
        {
            return await _context.Orders.AsNoTracking()
                .Where(a => a.Id == id)
                .Include(o => o.Agent)
                .Include(o => o.OrderItems)
                .Include(o => o.PaymentType)
                .Include(o => o.ProductType)
                .FirstOrDefaultAsync();
        }

        public async Task<OrderDetails> GetOrderDetailsByIdAsync(long id)
        {

            var query = await (from orders in _context.Orders.Where(o => o.Id == id).AsNoTracking()
                               join agents in _context.Agents.AsNoTracking() on orders.AgentId equals agents.Id
                               join productTypes in _context.ProductTypes.AsNoTracking() on orders.ProductTypeId equals productTypes.Id
                               join paymentTypes in _context.PaymentTypes.AsNoTracking() on orders.PaymentTypeId equals paymentTypes.Id
                               join orderCurrentStatus in _context.OrderCurrentStatuses.AsNoTracking() on orders.Id equals orderCurrentStatus.OrderId
                               join country in _context.Countries.AsNoTracking() on agents.CountryId equals country.Id
                               join city in _context.Cities.AsNoTracking() on agents.CityId equals city.Id
                               //where orders.Id == id
                               from deliveryPayments in _context.UserPayments.Where(p => p.OrderId == id).AsNoTracking().DefaultIfEmpty()
                               from userAcceptedRequests in _context.UserAcceptedRequests.Where(p => p.OrderId == id).AsNoTracking().DefaultIfEmpty()
                               from qrCodes in _context.Qrcodes.Where(q => q.OrderId == id).AsNoTracking().DefaultIfEmpty()
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
                                   Captain = (userAcceptedRequests != null && userAcceptedRequests.Id > 0) ? _context.Users.Where(u => u.Id == userAcceptedRequests.UserId).AsNoTracking().FirstOrDefault() : null,
                                   DeliveryPayment = deliveryPayments,
                                   QrCode = qrCodes,
                                   Agent = agents,
                                   Country = country,
                                   City = city
                               }).FirstOrDefaultAsync();
            //_context.ChangeTracker.LazyLoadingEnabled = true;

            //var reuslt = query.Where(a => a.Agent.Id == 2).ToList();
            return query;
        }



        public async Task<List<OrderEndLocation>> GetOrdersEndLocationByAsync(Expression<Func<OrderEndLocation, bool>> predicate)
        {
            return await _context.OrderEndLocations.Where(predicate).ToListAsync();
        }

        public async Task<OrderEndLocation> GetOrderEndLocationByIdAsync(long id)
        {
            return await _context.OrderEndLocations.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<OrderItem>> GetOrdersItemsByAsync(Expression<Func<OrderItem, bool>> predicate)
        {
            return await _context.OrderItems.Where(predicate).ToListAsync();
        }

        public async Task<OrderItem> GetOrderItemByIdAsync(long id)
        {
            return await _context.OrderItems.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            return await _context.Orders.Include(o => o.Agent).ToListAsync();
        }

        public async Task<List<OrderStartLocation>> GetOrdersStartLocationByAsync(Expression<Func<OrderStartLocation, bool>> predicate)
        {
            return await _context.OrderStartLocations.Where(predicate).ToListAsync();
        }

        public async Task<OrderStartLocation> GetOrderStartLocationByIdAsync(long id)
        {
            return await _context.OrderStartLocations.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<OrderCurrentStatus>> GetOrderCurrentStatusesByAsync(Expression<Func<OrderCurrentStatus, bool>> predicate)
        {
            return await _context.OrderCurrentStatuses.Where(predicate).ToListAsync();
        }

        public async Task<List<OrderStatusHistory>> GetOrdersStatusHistoriesByAsync(Expression<Func<OrderStatusHistory, bool>> predicate)
        {
            return await _context.OrderStatusHistories.Where(predicate).ToListAsync();
        }

        public async Task<OrderCurrentStatus> GetOrderStatusByIdAsync(long id)
        {
            return await _context.OrderCurrentStatuses.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<OrderStatusHistory> GetOrderStatusHistoryByIdAsync(long id)
        {
            return await _context.OrderStatusHistories.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<OrderStatusType> GetOrderStatusTypeByIdAsync(long id)
        {
            return await _context.OrderStatusTypes.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<ProductType> GetProductTypeByIdAsync(long id)
        {
            return await _context.ProductTypes.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Order> InsertOrderAsync(Order order)
        {
            order.CreationDate = DateTime.Now;
            var insertResult = await _context.Orders.AddAsync(order);
            return insertResult.Entity;
        }

        public async Task<OrderAssignment> InsertOrderAssignmentAsync(OrderAssignment assign)
        {
            assign.CreationDate = DateTime.Now;
            var insertResult = await _context.OrderAssignments.AddAsync(assign);
            return insertResult.Entity;
        }

        public async Task<OrderEndLocation> InsertOrderEndLocationAsync(OrderEndLocation orderEndLocation)
        {
            orderEndLocation.CreationDate = DateTime.Now;
            var insertResult = await _context.OrderEndLocations.AddAsync(orderEndLocation);
            return insertResult.Entity;
        }

        public async Task<OrderItem> InsertOrderItemAsync(OrderItem orderItem)
        {
            orderItem.CreationDate = DateTime.Now;
            var insertResult = await _context.OrderItems.AddAsync(orderItem);
            return insertResult.Entity;
        }

        public async Task<OrderStartLocation> InsertOrderStartLocationAsync(OrderStartLocation orderStartLocation)
        {
            orderStartLocation.CreationDate = DateTime.Now;
            var insertResult = await _context.OrderStartLocations.AddAsync(orderStartLocation);
            return insertResult.Entity;
        }

        public async Task<OrderCurrentStatus> InsertOrderStatusAsync(OrderCurrentStatus orderStatus)
        {
            var oldstatus = await _context.OrderCurrentStatuses.FirstOrDefaultAsync(s => s.OrderId == orderStatus.OrderId);
            if (oldstatus != null && oldstatus.Id > 0)
            {
                OrderStatusHistory history = new OrderStatusHistory()
                {
                    OrderId = oldstatus.OrderId,
                    OrderStatusId = oldstatus.StatusTypeId,
                    CreationDate = oldstatus.CreationDate,

                };

                await this.InsertOrderStatusHistoryAsync(history);

                oldstatus.StatusTypeId = orderStatus.StatusTypeId;
                oldstatus.CreationDate = DateTime.Now;

                _context.Entry<OrderCurrentStatus>(oldstatus).State = EntityState.Modified;
                return oldstatus;


            }

            orderStatus.CreationDate = DateTime.Now;
            var insertResult = await _context.OrderCurrentStatuses.AddAsync(orderStatus);
            return insertResult.Entity;
        }

        public async Task<OrderStatusHistory> InsertOrderStatusHistoryAsync(OrderStatusHistory orderStatus)
        {
            orderStatus.CreationDate = DateTime.Now;
            var insertResult = await _context.OrderStatusHistories.AddAsync(orderStatus);
            return insertResult.Entity;
        }

        public async Task<Order> UpdateOrderAsync(Order order)
        {
            var oldOrder = await _context.Orders.FirstOrDefaultAsync(o => o.Id == order.Id);
            if (oldOrder == null) return null;

            oldOrder.AgentId = order.AgentId;
            oldOrder.Description = order.Description;
            oldOrder.Details = order.Details;
            oldOrder.DropLocationLat = order.DropLocationLat;
            oldOrder.DropLocationLong = order.DropLocationLong;
            oldOrder.PickupLocationLat = order.PickupLocationLat;
            oldOrder.PickupLocationLong = order.PickupLocationLong;
            oldOrder.ProductTypeId = order.ProductTypeId;
            oldOrder.CustomerAddress = order.CustomerAddress;
            oldOrder.PaymentType = order.PaymentType;
            oldOrder.CustomerName = order.CustomerName;
            oldOrder.CustomerPhone = order.CustomerPhone;
            oldOrder.ModifiedBy = order.ModifiedBy;
            oldOrder.ModificationDate = DateTime.Now;
            oldOrder.DropLocationLat = order.DropLocationLat;
            oldOrder.DropLocationLong = order.DropLocationLong;
            oldOrder.PickupLocationLat = order.PickupLocationLat;
            oldOrder.PickupLocationLong = order.PickupLocationLong;

            if (order.OrderItems != null && order.OrderItems.Count > 0)
            {
                _context.OrderItems.RemoveRange(oldOrder.OrderItems);
                oldOrder.OrderItems = order.OrderItems;
            }


            _context.Entry<Order>(oldOrder).State = EntityState.Modified;
            return oldOrder;
        }

        public async Task<OrderAssignment> UpdateOrderAssignmentAsync(OrderAssignment assign)
        {
            var oldOrderAssign = await _context.OrderAssignments.FirstOrDefaultAsync(a => a.Id == assign.Id);
            if (oldOrderAssign == null) return null;


            oldOrderAssign.OrderId = assign.OrderId;
            oldOrderAssign.UserId = assign.UserId;
            oldOrderAssign.ToAgentKilometer = assign.ToAgentKilometer;
            oldOrderAssign.ToAgentTime = assign.ToAgentTime;
            oldOrderAssign.ToCustomerKilometer = assign.ToCustomerKilometer;
            oldOrderAssign.ToCustomerTime = assign.ToCustomerTime;
            oldOrderAssign.ModifiedBy = assign.ModifiedBy;
            oldOrderAssign.ModificationDate = DateTime.Now;

            _context.Entry<OrderAssignment>(oldOrderAssign).State = EntityState.Modified;
            return oldOrderAssign;
        }

        public async Task<OrderEndLocation> UpdateOrderEndLocationAsync(OrderEndLocation orderEndLocation)
        {
            var oldOrderEndLocaion = await _context.OrderEndLocations.FirstOrDefaultAsync(a => a.Id == orderEndLocation.Id);
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

        public async Task<OrderItem> UpdateOrderItemAsync(OrderItem orderItem)
        {
            var oldOrderItem = await _context.OrderItems.FirstOrDefaultAsync(a => a.Id == orderItem.Id);
            if (oldOrderItem == null) return null;


            oldOrderItem.OrderId = orderItem.OrderId;
            oldOrderItem.Item = orderItem.Item;
            oldOrderItem.Price = orderItem.Price;
            oldOrderItem.Quantity = orderItem.Quantity;
            oldOrderItem.Description = orderItem.Description;
            oldOrderItem.ModifiedBy = orderItem.ModifiedBy;
            oldOrderItem.ModificationDate = DateTime.Now;

            _context.Entry<OrderItem>(oldOrderItem).State = EntityState.Modified;
            return oldOrderItem;
        }

        public async Task<OrderStartLocation> UpdateOrderStartLocationAsync(OrderStartLocation orderStartLocation)
        {
            var oldOrderStartLocation = await _context.OrderStartLocations.FirstOrDefaultAsync(a => a.Id == orderStartLocation.Id);
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



        public async Task<OrderStatusHistory> UpdateOrderStatusHistoryAsync(OrderStatusHistory orderStatus)
        {
            var oldOrderStatusHistory = await _context.OrderStatusHistories.FirstOrDefaultAsync(a => a.Id == orderStatus.Id);
            if (oldOrderStatusHistory == null) return null;


            oldOrderStatusHistory.OrderId = orderStatus.OrderId;
            oldOrderStatusHistory.OrderStatusId = orderStatus.OrderStatusId;
            oldOrderStatusHistory.ModifiedBy = orderStatus.ModifiedBy;
            oldOrderStatusHistory.ModificationDate = DateTime.Now;

            _context.Entry<OrderStatusHistory>(oldOrderStatusHistory).State = EntityState.Modified;
            return oldOrderStatusHistory;
        }

        public async Task<List<OrderInvoice>> GetOrdersInvoicesAsync()
        {
            return await _context.OrderInvoices.ToListAsync();
        }

        public async Task<OrderInvoice> GetOrderInvoiceByIdAsync(long id)
        {
            return await _context.OrderInvoices.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<OrderInvoice>> GetOrdersInvoicesByAsync(Expression<Func<OrderInvoice, bool>> predicate)
        {
            return await _context.OrderInvoices.Where(predicate).ToListAsync();
        }

        public async Task<OrderInvoice> InsertOrderInvoiceAsync(OrderInvoice orderInvoice)
        {
            orderInvoice.CreationDate = DateTime.Now;
            var insertResult = await _context.OrderInvoices.AddAsync(orderInvoice);
            return insertResult.Entity;
        }

        public async Task<OrderInvoice> UpdateOrderInvoiceAsync(OrderInvoice orderInvoice)
        {
            var oldOrderInvoice = await _context.OrderInvoices.FirstOrDefaultAsync(i => i.Id == orderInvoice.Id);
            if (oldOrderInvoice == null) return null;


            oldOrderInvoice.UserId = orderInvoice.UserId;
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

        public async Task<OrderInvoice> DeleteOrderInvoiceAsync(long id)
        {
            var oldOrderInvoice = await _context.OrderInvoices.FirstOrDefaultAsync(i => i.Id == id);
            if (oldOrderInvoice == null) return null;

            _context.OrderInvoices.Remove(oldOrderInvoice);
            return oldOrderInvoice;

        }

        public async Task<List<PaidOrder>> GetPaidOrdersAsync()
        {
            return await _context.PaidOrders.ToListAsync();
        }

        public async Task<PaidOrder> GetPaidOrderByIdAsync(long id)
        {
            return await _context.PaidOrders.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<PaidOrder>> GetPaidOrdersByAsync(Expression<Func<PaidOrder, bool>> predicate)
        {
            return await _context.PaidOrders.Where(predicate).ToListAsync();
        }

        public async Task<PaidOrder> InsertPaidOrderAsync(PaidOrder paidOrder)
        {
            paidOrder.CreationDate = DateTime.Now;
            var insertResult = await _context.PaidOrders.AddAsync(paidOrder);
            return insertResult.Entity;
        }

        public async Task<PaidOrder> UpdatePaidOrderAsync(PaidOrder paidOrder)
        {
            var oldPaidOrder = await _context.PaidOrders.FirstOrDefaultAsync(p => p.Id == paidOrder.Id);
            if (oldPaidOrder == null) return null;


            oldPaidOrder.UserId = paidOrder.UserId;
            oldPaidOrder.OrderId = paidOrder.OrderId;
            oldPaidOrder.OrderAssignId = paidOrder.OrderAssignId;
            oldPaidOrder.Type = paidOrder.Type;
            oldPaidOrder.Value = paidOrder.Value;
            oldPaidOrder.ModifiedBy = paidOrder.ModifiedBy;
            oldPaidOrder.ModificationDate = DateTime.Now;

            _context.Entry<PaidOrder>(oldPaidOrder).State = EntityState.Modified;
            return oldPaidOrder;
        }

        public async Task<PaidOrder> DeletePaidOrderAsync(long id)
        {
            var oldPaidOrder = await _context.PaidOrders.FirstOrDefaultAsync(p => p.Id == id);
            if (oldPaidOrder == null) return null;


            _context.PaidOrders.Remove(oldPaidOrder);
            return oldPaidOrder;

        }

        public async Task<OrderCurrentStatus> DeleteOrderStatusAsync(long id)
        {
            var oldOrderCurrentStatus = await _context.OrderCurrentStatuses.FirstOrDefaultAsync(p => p.Id == id);
            if (oldOrderCurrentStatus == null) return null;


            _context.OrderCurrentStatuses.Remove(oldOrderCurrentStatus);
            return oldOrderCurrentStatus;
        }

        public async Task<List<PaymentType>> GetPaymentTypesAsync()
        {
            return await _context.PaymentTypes.ToListAsync();
        }

        public async Task<PaymentType> GetPaymentTypeByIdAsync(long id)
        {
            return await _context.PaymentTypes.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Order> DeleteOrderPermenetAsync(long id)
        {
            var oldOrder = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (oldOrder == null) return null;

            _context.Orders.Remove(oldOrder);
            return oldOrder;

        }

        public async Task<List<RunningOrder>> GetRunningOrdersAsync()
        {
            return await _context.RunningOrders.ToListAsync();
        }

        public async Task<RunningOrder> GetRunningOrderByIdAsync(long id)
        {
            return await _context.RunningOrders.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<RunningOrder>> GetRunningOrdersByAsync(Expression<Func<RunningOrder, bool>> predicate)
        {
            return await _context.RunningOrders.Where(predicate).ToListAsync();
        }

        public async Task<RunningOrder> InsertRunningOrderAsync(RunningOrder runningOrder)
        {
            runningOrder.CreationDate = DateTime.Now;
            var insertResult = await _context.RunningOrders.AddAsync(runningOrder);
            return insertResult.Entity;
        }

        public async Task<RunningOrder> UpdateRunningOrderAsync(RunningOrder runningOrder)
        {
            var oldRunningOrder = await _context.RunningOrders.FirstOrDefaultAsync(r => r.Id == runningOrder.Id);
            if (oldRunningOrder == null) return null;// throw new NotFoundException("Running order not found");

            oldRunningOrder.OrderId = runningOrder.OrderId;
            oldRunningOrder.UserId = runningOrder.UserId;
            oldRunningOrder.ModificationDate = DateTime.Now;
            oldRunningOrder.ModifiedBy = runningOrder.ModifiedBy;
            _context.Entry<RunningOrder>(oldRunningOrder).State = EntityState.Modified;
            return oldRunningOrder;
        }


        public async Task<RunningOrder> DeleteRunningOrderAsync(long id)
        {
            var oldRunningOrder = await _context.RunningOrders.FirstOrDefaultAsync(r => r.Id == id);
            if (oldRunningOrder == null) return null;// throw new NotFoundException("Running order not found");

            _context.RunningOrders.Remove(oldRunningOrder);
            return oldRunningOrder;
        }

        public async Task<RunningOrder> DeleteRunningOrderByOrderIdAsync(long id)
        {
            var oldRunningOrder = await _context.RunningOrders.FirstOrDefaultAsync(r => r.OrderId == id);
            if (oldRunningOrder == null) return null; // throw new NotFoundException("Running order not found");

            _context.RunningOrders.Remove(oldRunningOrder);
            return oldRunningOrder;
        }

        /* QRCode For Order*/
        // Get By Order
        public async Task<Qrcode> GetQrcodeByOrderIdAsync(long id)
        {
            var qRCode = await _context.Qrcodes.FirstOrDefaultAsync(qr => qr.OrderId == id);
            return qRCode;
        }
        // Insert
        public async Task<Qrcode> InsertQrCodeAsync(Qrcode qrcode)
        {
            qrcode.CreationDate = DateTime.Now;
            var qRCode = await _context.Qrcodes.AddAsync(qrcode);
            return qRCode.Entity;
        }
        // Delete
        public async Task<Qrcode> DeleteQrCodeAsync(long id)
        {
            var olderQRCode = await _context.Qrcodes.FirstOrDefaultAsync(qr => qr.Id == id);
            if (olderQRCode == null) return null; // throw new NotFoundException("QRCode Not Found not found");
            _context.Remove(olderQRCode);
            return olderQRCode;
        }
        /* QRCode For Order*/

        /* Order Filtration*/



        public IQueryable<Order> GetByQuerable(Expression<Func<Order, bool>> predicate)
        {
            return _context.Orders.Include(o => o.OrderCurrentStatus).Include(o => o.Agent).Include(o => o.Qrcodes).Include(o => o.PaymentType)
                .Include(o => o.ProductType)
                .Include(o => o.UserAcceptedRequests).ThenInclude(u => u.User).ThenInclude(c => c.City)
                .Include(o => o.UserAcceptedRequests).ThenInclude(u => u.User).ThenInclude(c => c.Country)
                .Include(o => o.OrderItems)
                .Include(o => o.UserAcceptedRequests).ThenInclude(u => u.User).ThenInclude(c => c.UserAccounts)
                .Where(predicate)
                ;
        }

        public IQueryable<Order> GetAllOrdersQuerable()
        {

            return _context.Orders.Include(o => o.OrderCurrentStatus).Include(o => o.OrderStatusHistories).Include(o => o.Agent).ThenInclude(o => o.Country).Include(o => o.Agent).ThenInclude(o => o.City).Include(o => o.PaymentType).Include(o => o.Qrcodes)
                .Include(o => o.ProductType).Include(o => o.OrderItems)
                .Include(o => o.UserAcceptedRequests).ThenInclude(u => u.User).ThenInclude(c => c.City).Include(c => c.UserAcceptedRequests)
                .ThenInclude(u => u.User).ThenInclude(c => c.Country)
                   .Include(o => o.UserAcceptedRequests).ThenInclude(u => u.User).ThenInclude(c => c.UserAccounts)
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
                            ProductType = orders.ProductType,
                            PaymentType = orders.PaymentType,
                            OrderItems = orders.OrderItems,
                            OrderCurrentStatus = orders.OrderCurrentStatus.FirstOrDefault(),
                            OrderStatusHistories = orders.OrderStatusHistories,
                            Captain = orders.UserAcceptedRequests.FirstOrDefault().User,
                            DeliveryPayment = orders.UserAcceptedRequests.FirstOrDefault().User.UserPayments.FirstOrDefault(),
                            QrCode = orders.Qrcodes.FirstOrDefault(),
                            Agent = orders.Agent,
                            Country = orders.Agent.Country,
                            City = orders.Agent.City
                        };

            //var reuslt = query.Where(a => a.Agent.Id == 2).ToList();
            return query;
        }


        public IQueryable<Order> GetAllDriverOrders(long id)
        {
            var acceptedOrders = _context.UserAcceptedRequests.Include(o => o.Order)
                                              .ThenInclude(o => o.UserAcceptedRequests)

                                    .Where(u => u.UserId == id).Select(AcceptedOrder => AcceptedOrder.Order);

            //var rejectedOrders = _context.UserRejectedRequests.Include(o => o.Order)
            //                             .ThenInclude(o => o.UserRejectedRequests)
            //                        .Where(u => u.UserId == id).Select(RejectedOrders => RejectedOrders.Order); 
            //var ignoredOrders = _context.UserIgnoredRequests.Include(o => o.Order)
            //                            .ThenInclude(o => o.UserIgnoredRequests)
            //                        .Where(u => u.UserId == id).Select(IgnoredOrders => IgnoredOrders.Order);

            //var allOrders = acceptedOrders.Union(rejectedOrders).Union(ignoredOrders).OrderByDescending(o => o.CreationDate);
            var allOrders = acceptedOrders.OrderByDescending(o => o.CreationDate);

            return allOrders;
        }

        public Object GetDriverOrdersWithType(long id)
        {
            return new object();
        }

        public async Task<Order> UpdateOrderLocationAsync(Order order)
        {
            var oldOrder = await _context.Orders.FirstOrDefaultAsync(o => o.Id == order.Id);
            if (oldOrder == null) return null;

            oldOrder.DropLocationLat = order.DropLocationLat;
            oldOrder.DropLocationLong = order.DropLocationLong;
            oldOrder.PickupLocationLat = order.PickupLocationLat;
            oldOrder.PickupLocationLong = order.PickupLocationLong;


            if (order.OrderItems != null && order.OrderItems.Count > 0)
            {
                _context.OrderItems.RemoveRange(oldOrder.OrderItems);
                oldOrder.OrderItems = order.OrderItems;
            }


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
            ; }



        public Object OrdersReportCount()
        {
            var totalOrders = _context.Orders.Count();

            var newOrders = _context.OrderCurrentStatuses.Where(o => o.StatusTypeId == (long)OrderStatusTypes.New).Count();
            var assignedOrders = _context.OrderCurrentStatuses.Where(o => o.StatusTypeId == (long)OrderStatusTypes.AssignedToCaptain).Count();
            var deliveredOrders = _context.OrderCurrentStatuses.Where(o => o.StatusTypeId == (long)OrderStatusTypes.Delivered).Count();
            var cancelledOrders = _context.OrderCurrentStatuses.Where(o => o.StatusTypeId == (long)OrderStatusTypes.Canceled).Count();
            var pickedUpOrders = _context.OrderCurrentStatuses.Where(o => o.StatusTypeId == (long)OrderStatusTypes.PickedUp).Count();
            var progressOrders = _context.OrderCurrentStatuses.Where(o => o.StatusTypeId == (long)OrderStatusTypes.Progress).Count();
            var droppedOrders = _context.OrderCurrentStatuses.Where(o => o.StatusTypeId == (long)OrderStatusTypes.Dropped).Count();
            var endOrders = _context.OrderCurrentStatuses.Where(o => o.StatusTypeId == (long)OrderStatusTypes.End).Count();
            return new { OrdersCount = totalOrders, DeliveredCount = deliveredOrders, CancelldCount = cancelledOrders, NewOrders = newOrders,

                AssignedToCaptainCount = assignedOrders, PickedUpCount = pickedUpOrders, InProgressCount = progressOrders,
                DroppedCount = droppedOrders, EndCount = endOrders

            };
        }

        /**/
        /*
         * 
         */
        public Object GetUserOrdersGroupedByDay(Expression<Func<OrderCurrentStatus, bool>> predicate)
        {
            var result = _context.OrderCurrentStatuses.Where(predicate).GroupBy(p => p.CreationDate, p => p,
                (key, g) => new { CreationDate = key, g = g.ToList() });
            return result;
        }
        public IQueryable<CaptainUserAcceptedRequest> GetUserAcceptedRequestByQuerable(Expression<Func<CaptainUserAcceptedRequest, bool>> predicate)
        {
            var result = _context.UserAcceptedRequests.Include(o => o.Order).ThenInclude(o => o.OrderCurrentStatus)
                .Include(o => o.Order).ThenInclude(o => o.OrderItems).Include(o => o.Order)
                .ThenInclude(o => o.PaymentType).Where(predicate);

            return result;

        }

        public async Task<Order> UpdateOrderCurrentStatusAsync(long orderId, long CurrentStatusId)
        {
            Order oldOrder = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            if (oldOrder == null) return null;

            oldOrder.CurrentStatus = CurrentStatusId;
            oldOrder.ModificationDate = DateTime.Now;
            _context.Entry<Order>(oldOrder).State = EntityState.Modified;
            return oldOrder;
        }



        public async Task<List<OrderFilterResponse>> ReportAsync(OrderFilter orderFilter)
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


            List<OrderFilterResponse> result = new List<OrderFilterResponse>();
            var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync();
            var command = conn.CreateCommand();
            command.CommandText = query;
            using var reader = await command.ExecuteReaderAsync();
            while (reader.Read())
            {

                long? orderID = null;
                long? orderCurrentStatusId = null;
                string agentName = null;
                long? productTypeID = null;
                long? paymentTypeID = null;
                string customerName = null;
                string customerAddress = null;
                string customerPhone = null;
                decimal? deliveryAmount = null;
                string captainName = null;
                DateTime? orderCreationDate = null;
                string durationToCurrentStatus = null;
                DateTime? orderStatusHistoryCreationDate = null;
                string durationToStatusHistory = null;


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

                OrderFilterResponse data = new OrderFilterResponse()
                {
                    OrderId = orderID,
                    OrderCurrentStatus = orderCurrentStatusId,
                    AgentName = agentName,
                    ProductTypeId = productTypeID,
                    PaymentTypeId = paymentTypeID,
                    CustomerName = customerName,
                    CustomerAddress = customerAddress,
                    CustomerPhone = customerPhone,
                    DeliveryAmount = deliveryAmount,
                    CaptainName = captainName,
                    OrderCreationDate = orderCreationDate,
                    DurationToCurrentStatus = durationToCurrentStatus,
                    OrderStatusHistoryCreationDate = orderStatusHistoryCreationDate,
                    DurationToStatusHistory = durationToStatusHistory
                };

                result.Add(data);


            }

            //reader.Close();
            return result;
        }

        public async Task<List<OrderFilterResponse>> FilterAsync(OrderFilter orderFilter)
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


            List<OrderFilterResponse> result = new List<OrderFilterResponse>();
            var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync();
            var command = conn.CreateCommand();
            command.CommandText = query;
            using var reader = await command.ExecuteReaderAsync();
            while (reader.Read())
            {

                long? orderID = null;
                long? orderCurrentStatusId = null;
                string agentName = null;
                long? productTypeID = null;
                long? paymentTypeID = null;
                string customerName = null;
                string customerAddress = null;
                string customerPhone = null;
                decimal? deliveryAmount = null;
                string captainName = null;
                DateTime? orderCreationDate = null;
                string durationToCurrentStatus = null;
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
                OrderFilterResponse data = new OrderFilterResponse()
                {
                    OrderId = orderID,
                    OrderCurrentStatus = orderCurrentStatusId,
                    AgentName = agentName,
                    ProductTypeId = productTypeID,
                    PaymentTypeId = paymentTypeID,
                    CustomerName = customerName,
                    CustomerAddress = customerAddress,
                    CustomerPhone = customerPhone,
                    DeliveryAmount = deliveryAmount,
                    CaptainName = captainName,
                    OrderCreationDate = orderCreationDate,
                    DurationToCurrentStatus = durationToCurrentStatus,
                    // OrderStatusHistoryCreationDate = orderStatusHistoryCreationDate,
                    // DurationToStatusHistory = durationToStatusHistory
                };

                result.Add(data);


            }

            //reader.Close();
            return result;
        }



        public async Task<List<Order>> GetOrdersPaginationAsync(int skip, int take)
        {
            return await _context.Orders
                .OrderByDescending(o => o.CreationDate)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }


        public async Task<OrderAssignment> DeleteOrderAssignmentAsync(long id) 
        {
            var oldOrderAssigne = await _context.OrderAssignments.FirstOrDefaultAsync(a => a.Id == id);
            if (oldOrderAssigne == null) return null;

            _context.OrderAssignments.Remove(oldOrderAssigne);
            return oldOrderAssigne;
        }

    }
}
