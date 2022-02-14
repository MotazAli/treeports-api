using TreePorts.DTO;
using TreePorts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TreePorts.Interfaces.Services
{
    public interface IOrderService 
    {

        Task<IEnumerable<Order>> GetOrdersAsync();
        Task<object> GetOrdersPaginationAsync(FilterParameters parameters);
        Task<object> UserOrdersPagingAsync(long id, FilterParameters parameters);
        Task<IEnumerable<OrderStatusHistory>> GetOrdersStatusHistoriesByOrderIdAsync(long id);
        Task<OrderDetails> GetOrderDetailsByOrderIdAsync(long id);
        Task<Order> GetOrderByIdAsync(long id);
        Task<object> GetOrderDetailsAsync(long orderId, long captainId);
        Task<object> GetRunningOrderByCaptainIdAsync(long captainId);
        Task<bool> IgnoreOrderAsync(OrderRequest orderRequest);
        Task<bool> AssignToCaptainAsync(OrderRequest orderRequest);
        Task<bool> AcceptOrderAsync(OrderRequest orderRequest);
        Task<bool> RejectOrderAsync(OrderRequest orderRequest);
        Task<bool> OrderPickedUpAsync(long id);
        Task<bool> OrderDroppedAsync(long id);
        Task<bool> CancelOrderAsync(long id);
        Task<object> AddOrderAsync(Order order, HttpContext httpContext, string CouponCode);
        Task<Order> DeleteOrderAsync(long id);
        Task<OrderInvoice> AddOrderInvoiceAsync(OrderInvoice orderInvoice);
        Task<PaidOrder> AddPaidOrderAsync(PaidOrder paidOrder);
        Task<object> GetOrderCurrentLocationByOrderIdAsync(long id);
        Task<IEnumerable<OrderItem>> GetOrderItemsAsync(long id);
        Task<object> GetQRCodeByOrderIdAsync(long id);
        Task<object> ReportAsync(FilterParameters reportParameters, HttpContext httpContext);
        Task<object> SearchDetailsAsync(FilterParameters parameters);
        Task<object> ChartAsync();
        Task<IEnumerable<OrderFilterResponse>> SearchAsync(OrderFilter orderFilter);


        // fakes
        Task<bool> FakeCancelAsync(long id);
        Task<bool> FakeAssignToCaptainAsync(OrderRequest orderRequest);

        Task<bool> SearchForCaptainAndNotifyOrder(Order order);
        //Task<bool> ChangeOrderStatusAndNotify(OrderStatusTypes status, long orderId, long agentId);
        Task<object> AddNewOrder(Order order);
    }
}