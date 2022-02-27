using QRCoder;
using TreePorts.DTO;
using TreePorts.DTO.ReturnDTO;
using System.Linq.Expressions;
using TreePorts.DTO.Records;

namespace TreePorts.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetOrdersAsync();
        //Task<Order> GetOrderByID(long ID);
        Task<Order?> GetOrderById_oldBehaviourAsync(long id);
        Task<Order?> GetOnlyOrderByIdAsync(long id);
        Task<Order?> GetLiteOrderByIdAsync(long id);
        Task<OrderDetails?> GetOrderDetailsByIdAsync(long id);
        Task<Order> InsertOrderAsync(Order order);
        Task<Order?> UpdateOrderAsync(Order order);
        Task<Order?> UpdateOrderCurrentStatusAsync(long orderId, long CurrentStatusId);
        Task<Order?> DeleteOrderAsync(long id);
        Task<Order?> DeleteOrderPermenetAsync(long id);
        Task<List<Order>> GetOrdersByAsync(Expression<Func<Order, bool>> predicate);
        Task<List<ProductType>> GetProductTypesAsync();
        Task<ProductType?> GetProductTypeByIdAsync(long id);

        Task<List<OrderItem>> GetOrdersItemsAsync();
        Task<OrderItem?> GetOrderItemByIdAsync(long id);
        Task<List<OrderItem>> GetOrdersItemsByAsync(Expression<Func<OrderItem, bool>> predicate);
        Task<OrderItem> InsertOrderItemAsync(OrderItem orderItem);
        Task<OrderItem?> UpdateOrderItemAsync(OrderItem orderItem);

        Task<Order?> UpdateOrderLocationAsync(Order order);
        Task<List<OrderStatusType>> GetOrderStatusTypesAsync();
        Task<OrderStatusType?> GetOrderStatusTypeByIdAsync(long id);

        Task<List<OrderCurrentStatus>> GetOrdersStatusesAsync();
        Task<List<OrderCurrentStatus>> GetCurrentOrdersStatusesAsync();
        Task<OrderCurrentStatus?> GetOrderStatusByIdAsync(long id);
        Task<List<OrderCurrentStatus>> GetOrderCurrentStatusesByAsync(Expression<Func<OrderCurrentStatus, bool>> predicate);
        Task<OrderCurrentStatus> InsertOrderStatusAsync(OrderCurrentStatus orderStatus);
        Task<OrderCurrentStatus?> DeleteOrderStatusAsync(long id);


        Task<List<OrderStatusHistory>> GetOrdersStatusHistoriesAsync();
        Task<OrderStatusHistory?> GetOrderStatusHistoryByIdAsync(long id);
        Task<List<OrderStatusHistory>> GetOrdersStatusHistoriesByAsync(Expression<Func<OrderStatusHistory, bool>> predicate);
        Task<OrderStatusHistory> InsertOrderStatusHistoryAsync(OrderStatusHistory orderStatus);
        Task<OrderStatusHistory?> UpdateOrderStatusHistoryAsync(OrderStatusHistory orderStatus);

        Task<List<OrderAssignment>> GetOrdersAssignmentsAsync();
        Task<OrderAssignment?> GetOrderAssignmentByIdAsync(long id);
        Task<List<OrderAssignment>> GetOrdersAssignmentsByAsync(Expression<Func<OrderAssignment, bool>> predicate);

        Task<OrderAssignment> InsertOrderAssignmentAsync(OrderAssignment assign);
        Task<OrderAssignment?> UpdateOrderAssignmentAsync(OrderAssignment assign);
        Task<OrderAssignment?> DeleteOrderAssignmentAsync(long id);


        Task<List<OrderStartLocation>> GetOrdersStartLocationsAsync();
        Task<OrderStartLocation?> GetOrderStartLocationByIdAsync(long id);
        Task<List<OrderStartLocation>> GetOrdersStartLocationByAsync(Expression<Func<OrderStartLocation, bool>> predicate);

        Task<OrderStartLocation> InsertOrderStartLocationAsync(OrderStartLocation orderStartLocation);
        Task<OrderStartLocation?> UpdateOrderStartLocationAsync(OrderStartLocation orderStartLocation);


        Task<List<OrderEndLocation>> GetOrdersEndLocationsAsync();
        Task<OrderEndLocation?> GetOrderEndLocationByIdAsync(long id);
        Task<List<OrderEndLocation>> GetOrdersEndLocationByAsync(Expression<Func<OrderEndLocation, bool>> predicate);

        Task<OrderEndLocation> InsertOrderEndLocationAsync(OrderEndLocation orderEndLocation);
        Task<OrderEndLocation?> UpdateOrderEndLocationAsync(OrderEndLocation orderEndLocation);


        Task<List<OrderInvoice>> GetOrdersInvoicesAsync();
        Task<OrderInvoice?> GetOrderInvoiceByIdAsync(long id);
        Task<List<OrderInvoice>> GetOrdersInvoicesByAsync(Expression<Func<OrderInvoice, bool>> predicate);
        Task<OrderInvoice> InsertOrderInvoiceAsync(OrderInvoice orderInvoice);
        Task<OrderInvoice?> UpdateOrderInvoiceAsync(OrderInvoice orderInvoice);
        Task<OrderInvoice?> DeleteOrderInvoiceAsync(long id);

        Task<List<PaidOrder>> GetPaidOrdersAsync();
        Task<PaidOrder?> GetPaidOrderByIdAsync(long id);
        Task<List<PaidOrder>> GetPaidOrdersByAsync(Expression<Func<PaidOrder, bool>> predicate);
        Task<PaidOrder> InsertPaidOrderAsync(PaidOrder paidOrder);
        Task<PaidOrder?> UpdatePaidOrderAsync(PaidOrder paidOrder);
        Task<PaidOrder?> DeletePaidOrderAsync(long id);

        Task<List<PaymentType>> GetPaymentTypesAsync();
        Task<PaymentType?> GetPaymentTypeByIdAsync(long id);



        Task<List<RunningOrder>> GetRunningOrdersAsync();
        Task<RunningOrder?> GetRunningOrderByIdAsync(long id);
        Task<List<RunningOrder>> GetRunningOrdersByAsync(Expression<Func<RunningOrder, bool>> predicate);
        Task<RunningOrder> InsertRunningOrderAsync(RunningOrder runningOrder);
        Task<RunningOrder?> UpdateRunningOrderAsync(RunningOrder runningOrder);
        Task<RunningOrder?> DeleteRunningOrderAsync(long id);
        Task<RunningOrder?> DeleteRunningOrderByOrderIdAsync(long id);
        /* QRCode For Order*/
        // Get By Order
        Task<OrderQrcode?> GetQrcodeByOrderIdAsync(long id);
        // Insert QrCode
        Task<OrderQrcode> InsertQrCodeAsync(OrderQrcode qrcode);

        // Delete
        Task<OrderQrcode?> DeleteQrCodeAsync(long id);
        /* QRCode For Order*/
        /* Order Filter*/
        IQueryable<OrderDetails> GetAllOrdersDetailsQuerable();
        IQueryable<Order> GetByQuerable(Expression<Func<Order, bool>> predicate);
        IQueryable<Order> GetAllOrdersQuerable();
        IQueryable<Order?> GetAllCaptainOrdersByCaptainUserAccountId(string id);
        IQueryable<CaptainUserAcceptedRequest> GetUserAcceptedRequestByQuerable(Expression<Func<CaptainUserAcceptedRequest, bool>> predicate);

        Task<List<OrderFilterResponse>> FilterAsync(OrderFilter orderFilter);
        Task<List<OrderFilterResponse>> ReportAsync(OrderFilter orderFilter);

        Task<List<Order>> GetOrdersPaginationAsync(int skip, int take);

        /* Order Filter*/

        /*Order Reports*/
        // Daily Order between two Dates
        Object OrdersReportCount();
        /*Order Reports*/
    }
}
