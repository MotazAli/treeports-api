using QRCoder;
using TreePorts.DTO;
using TreePorts.DTO.ReturnDTO;
using System.Linq.Expressions;
using TreePorts.DTO.Records;

namespace TreePorts.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetOrdersAsync(CancellationToken cancellationToken);
        //Task<Order> GetOrderByID(long ID,CancellationToken cancellationToken);
        Task<Order?> GetOrderById_oldBehaviourAsync(long id,CancellationToken cancellationToken);
        Task<Order?> GetOnlyOrderByIdAsync(long id,CancellationToken cancellationToken);
        Task<Order?> GetLiteOrderByIdAsync(long id,CancellationToken cancellationToken);
        Task<OrderDetails?> GetOrderDetailsByIdAsync(long id,CancellationToken cancellationToken);
        Task<Order> InsertOrderAsync(Order order,CancellationToken cancellationToken);
        Task<Order?> UpdateOrderAsync(Order order,CancellationToken cancellationToken);
        Task<Order?> UpdateOrderCurrentStatusAsync(long orderId, long CurrentStatusId,CancellationToken cancellationToken);
        Task<Order?> DeleteOrderAsync(long id,CancellationToken cancellationToken);
        Task<Order?> DeleteOrderPermenetAsync(long id,CancellationToken cancellationToken);
        Task<List<Order>> GetOrdersByAsync(Expression<Func<Order, bool>> predicate,CancellationToken cancellationToken);
        Task<List<ProductType>> GetProductTypesAsync(CancellationToken cancellationToken);
        Task<ProductType?> GetProductTypeByIdAsync(long id,CancellationToken cancellationToken);

        Task<List<OrderItem>> GetOrdersItemsAsync(CancellationToken cancellationToken);
        Task<OrderItem?> GetOrderItemByIdAsync(long id,CancellationToken cancellationToken);
        Task<List<OrderItem>> GetOrdersItemsByAsync(Expression<Func<OrderItem, bool>> predicate,CancellationToken cancellationToken);
        Task<OrderItem> InsertOrderItemAsync(OrderItem orderItem,CancellationToken cancellationToken);
        Task<OrderItem?> UpdateOrderItemAsync(OrderItem orderItem,CancellationToken cancellationToken);

        Task<Order?> UpdateOrderLocationAsync(Order order,CancellationToken cancellationToken);
        Task<List<OrderStatusType>> GetOrderStatusTypesAsync(CancellationToken cancellationToken);
        Task<OrderStatusType?> GetOrderStatusTypeByIdAsync(long id,CancellationToken cancellationToken);

        Task<List<OrderCurrentStatus>> GetOrdersStatusesAsync(CancellationToken cancellationToken);
        Task<List<OrderCurrentStatus>> GetCurrentOrdersStatusesAsync(CancellationToken cancellationToken);
        Task<OrderCurrentStatus?> GetOrderStatusByIdAsync(long id,CancellationToken cancellationToken);
        Task<List<OrderCurrentStatus>> GetOrderCurrentStatusesByAsync(Expression<Func<OrderCurrentStatus, bool>> predicate,CancellationToken cancellationToken);
        Task<OrderCurrentStatus> InsertOrderStatusAsync(OrderCurrentStatus orderStatus,CancellationToken cancellationToken);
        Task<OrderCurrentStatus?> DeleteOrderStatusAsync(long id,CancellationToken cancellationToken);


        Task<List<OrderStatusHistory>> GetOrdersStatusHistoriesAsync(CancellationToken cancellationToken);
        Task<OrderStatusHistory?> GetOrderStatusHistoryByIdAsync(long id,CancellationToken cancellationToken);
        Task<List<OrderStatusHistory>> GetOrdersStatusHistoriesByAsync(Expression<Func<OrderStatusHistory, bool>> predicate,CancellationToken cancellationToken);
        Task<OrderStatusHistory> InsertOrderStatusHistoryAsync(OrderStatusHistory orderStatus,CancellationToken cancellationToken);
        Task<OrderStatusHistory?> UpdateOrderStatusHistoryAsync(OrderStatusHistory orderStatus,CancellationToken cancellationToken);

        Task<List<OrderAssignment>> GetOrdersAssignmentsAsync(CancellationToken cancellationToken);
        Task<OrderAssignment?> GetOrderAssignmentByIdAsync(long id,CancellationToken cancellationToken);
        Task<List<OrderAssignment>> GetOrdersAssignmentsByAsync(Expression<Func<OrderAssignment, bool>> predicate,CancellationToken cancellationToken);

        Task<OrderAssignment> InsertOrderAssignmentAsync(OrderAssignment assign,CancellationToken cancellationToken);
        Task<OrderAssignment?> UpdateOrderAssignmentAsync(OrderAssignment assign,CancellationToken cancellationToken);
        Task<OrderAssignment?> DeleteOrderAssignmentAsync(long id,CancellationToken cancellationToken);


        Task<List<OrderStartLocation>> GetOrdersStartLocationsAsync(CancellationToken cancellationToken);
        Task<OrderStartLocation?> GetOrderStartLocationByIdAsync(long id,CancellationToken cancellationToken);
        Task<List<OrderStartLocation>> GetOrdersStartLocationByAsync(Expression<Func<OrderStartLocation, bool>> predicate,CancellationToken cancellationToken);

        Task<OrderStartLocation> InsertOrderStartLocationAsync(OrderStartLocation orderStartLocation,CancellationToken cancellationToken);
        Task<OrderStartLocation?> UpdateOrderStartLocationAsync(OrderStartLocation orderStartLocation,CancellationToken cancellationToken);


        Task<List<OrderEndLocation>> GetOrdersEndLocationsAsync(CancellationToken cancellationToken);
        Task<OrderEndLocation?> GetOrderEndLocationByIdAsync(long id,CancellationToken cancellationToken);
        Task<List<OrderEndLocation>> GetOrdersEndLocationByAsync(Expression<Func<OrderEndLocation, bool>> predicate,CancellationToken cancellationToken);

        Task<OrderEndLocation> InsertOrderEndLocationAsync(OrderEndLocation orderEndLocation,CancellationToken cancellationToken);
        Task<OrderEndLocation?> UpdateOrderEndLocationAsync(OrderEndLocation orderEndLocation,CancellationToken cancellationToken);


        Task<List<OrderInvoice>> GetOrdersInvoicesAsync(CancellationToken cancellationToken);
        Task<OrderInvoice?> GetOrderInvoiceByIdAsync(long id,CancellationToken cancellationToken);
        Task<List<OrderInvoice>> GetOrdersInvoicesByAsync(Expression<Func<OrderInvoice, bool>> predicate,CancellationToken cancellationToken);
        Task<OrderInvoice> InsertOrderInvoiceAsync(OrderInvoice orderInvoice,CancellationToken cancellationToken);
        Task<OrderInvoice?> UpdateOrderInvoiceAsync(OrderInvoice orderInvoice,CancellationToken cancellationToken);
        Task<OrderInvoice?> DeleteOrderInvoiceAsync(long id,CancellationToken cancellationToken);

        Task<List<PaidOrder>> GetPaidOrdersAsync(CancellationToken cancellationToken);
        Task<PaidOrder?> GetPaidOrderByIdAsync(long id,CancellationToken cancellationToken);
        Task<List<PaidOrder>> GetPaidOrdersByAsync(Expression<Func<PaidOrder, bool>> predicate,CancellationToken cancellationToken);
        Task<PaidOrder> InsertPaidOrderAsync(PaidOrder paidOrder,CancellationToken cancellationToken);
        Task<PaidOrder?> UpdatePaidOrderAsync(PaidOrder paidOrder,CancellationToken cancellationToken);
        Task<PaidOrder?> DeletePaidOrderAsync(long id,CancellationToken cancellationToken);

        Task<List<PaymentType>> GetPaymentTypesAsync(CancellationToken cancellationToken);
        Task<PaymentType?> GetPaymentTypeByIdAsync(long id,CancellationToken cancellationToken);



        Task<List<RunningOrder>> GetRunningOrdersAsync(CancellationToken cancellationToken);
        Task<RunningOrder?> GetRunningOrderByIdAsync(long id,CancellationToken cancellationToken);
        Task<List<RunningOrder>> GetRunningOrdersByAsync(Expression<Func<RunningOrder, bool>> predicate,CancellationToken cancellationToken);
        Task<RunningOrder> InsertRunningOrderAsync(RunningOrder runningOrder,CancellationToken cancellationToken);
        Task<RunningOrder?> UpdateRunningOrderAsync(RunningOrder runningOrder,CancellationToken cancellationToken);
        Task<RunningOrder?> DeleteRunningOrderAsync(long id,CancellationToken cancellationToken);
        Task<RunningOrder?> DeleteRunningOrderByOrderIdAsync(long id,CancellationToken cancellationToken);
        /* QRCode For Order*/
        // Get By Order
        Task<OrderQrcode?> GetQrcodeByOrderIdAsync(long id,CancellationToken cancellationToken);
        // Insert QrCode
        Task<OrderQrcode> InsertQrCodeAsync(OrderQrcode qrcode,CancellationToken cancellationToken);

        // Delete
        Task<OrderQrcode?> DeleteQrCodeAsync(long id,CancellationToken cancellationToken);
        /* QRCode For Order*/
        /* Order Filter*/
        IQueryable<OrderDetails> GetAllOrdersDetailsQuerable();
        IQueryable<Order> GetByQuerable(Expression<Func<Order, bool>> predicate);
        IQueryable<Order> GetAllOrdersQuerable();
        IQueryable<Order?> GetAllCaptainOrdersByCaptainUserAccountId(string id);
        IQueryable<CaptainUserAcceptedRequest> GetUserAcceptedRequestByQuerable(Expression<Func<CaptainUserAcceptedRequest, bool>> predicate);

        Task<List<OrderFilterResponse>> FilterAsync(OrderFilter orderFilter,CancellationToken cancellationToken);
        Task<List<OrderFilterResponse>> ReportAsync(OrderFilter orderFilter,CancellationToken cancellationToken);

        Task<List<Order>> GetOrdersPaginationAsync(int skip, int take,CancellationToken cancellationToken);

        /* Order Filter*/

        /*Order Reports*/
        // Daily Order between two Dates
        object OrdersReportCount();
        /*Order Reports*/
    }
}
