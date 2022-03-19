using TreePorts.DTO;
using TreePorts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TreePorts.DTO.Records;

namespace TreePorts.Interfaces.Services
{
    public interface IOrderService 
    {

        Task<IEnumerable<Order>> GetOrdersAsync(CancellationToken cancellationToken);
        Task<object> GetOrdersPaginationAsync(FilterParameters parameters,CancellationToken cancellationToken);
        Task<object> UserOrdersPagingByAgentIdAsync(string agentId, FilterParameters parameters,CancellationToken cancellationToken);
        Task<IEnumerable<OrderStatusHistory>> GetOrdersStatusHistoriesByOrderIdAsync(long id,CancellationToken cancellationToken);
        Task<OrderDetails> GetOrderDetailsByOrderIdAsync(long id,CancellationToken cancellationToken);
        Task<Order> GetOrderByIdAsync(long id,CancellationToken cancellationToken);
        Task<object> GetOrderDetailsAsync(long orderId, string captainUserAccountId,CancellationToken cancellationToken);
        Task<object> GetRunningOrderByCaptainUserAccountIdAsync(string captainUserAccountId,CancellationToken cancellationToken);
        Task<bool> IgnoreOrderAsync(OrderRequest orderRequest,CancellationToken cancellationToken);
        Task<bool> AssignToCaptainAsync(OrderRequest orderRequest,CancellationToken cancellationToken);
        Task<bool> AcceptOrderAsync(OrderRequest orderRequest,CancellationToken cancellationToken);
        //Task<bool> RejectOrderAsync(OrderRequest orderRequest,CancellationToken cancellationToken);
        Task<bool> OrderPickedUpAsync(long id,CancellationToken cancellationToken);
        Task<bool> OrderDroppedAsync(long id,CancellationToken cancellationToken);
        Task<bool> CancelOrderAsync(long id,CancellationToken cancellationToken);
        Task<object> AddOrderAsync(Order order, HttpContext httpContext, string CouponCode,CancellationToken cancellationToken);
        Task<Order> DeleteOrderAsync(long id,CancellationToken cancellationToken);
        Task<OrderInvoice> AddOrderInvoiceAsync(OrderInvoice orderInvoice,CancellationToken cancellationToken);
        Task<PaidOrder> AddPaidOrderAsync(PaidOrder paidOrder,CancellationToken cancellationToken);
        Task<object> GetOrderCurrentLocationByOrderIdAsync(long id,CancellationToken cancellationToken);
        Task<IEnumerable<OrderItem>> GetOrderItemsAsync(long id,CancellationToken cancellationToken);
        Task<object> GetQRCodeByOrderIdAsync(long id,CancellationToken cancellationToken);
        //Task<object> ReportAsync(FilterParameters reportParameters, HttpContext httpContext,CancellationToken cancellationToken);
        Task<object> SearchDetailsAsync(FilterParameters parameters,CancellationToken cancellationToken);
        Task<object> ChartAsync(CancellationToken cancellationToken);
        Task<IEnumerable<OrderFilterResponse>> SearchAsync(OrderFilter orderFilter,CancellationToken cancellationToken);


        // fakes
        Task<bool> FakeCancelAsync(string captainUserAccountId,CancellationToken cancellationToken);
        Task<bool> FakeAssignToCaptainAsync(OrderRequest orderRequest,CancellationToken cancellationToken);

        Task<bool> SearchForCaptainAndNotifyOrder(Order order,CancellationToken cancellationToken);
        //Task<bool> ChangeOrderStatusAndNotify(OrderStatusTypes status, long orderId, long agentId,CancellationToken cancellationToken);
        Task<object> AddNewOrder(Order order,CancellationToken cancellationToken);
    }
}