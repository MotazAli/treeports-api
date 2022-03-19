using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class Order
    {
        public Order()
        {
            AgentOrderDeliveryPrices = new HashSet<AgentOrderDeliveryPrice>();
            Bookkeepings = new HashSet<Bookkeeping>();
            CaptainUserAcceptedRequests = new HashSet<CaptainUserAcceptedRequest>();
            CaptainUserIgnoredRequests = new HashSet<CaptainUserIgnoredRequest>();
            CaptainUserNewRequests = new HashSet<CaptainUserNewRequest>();
            CaptainUserPaymentHistories = new HashSet<CaptainUserPaymentHistory>();
            CaptainUserPayments = new HashSet<CaptainUserPayment>();
            CityOrderPrices = new HashSet<CityOrderPrice>();
            CountryOrderPrices = new HashSet<CountryOrderPrice>();
            CouponUsages = new HashSet<CouponUsage>();
            OrderAssignments = new HashSet<OrderAssignment>();
            OrderCurrentStatus = new HashSet<OrderCurrentStatus>();
            OrderEndLocations = new HashSet<OrderEndLocation>();
            OrderInvoices = new HashSet<OrderInvoice>();
            OrderItems = new HashSet<OrderItem>();
            OrderQrcodes = new HashSet<OrderQrcode>();
            OrderStartLocations = new HashSet<OrderStartLocation>();
            OrderStatusHistories = new HashSet<OrderStatusHistory>();
            PaidOrders = new HashSet<PaidOrder>();
            RunningOrders = new HashSet<RunningOrder>();
        }

        public long Id { get; set; }
        public string? AgentId { get; set; }
        public long? ProductTypeId { get; set; }
        public string? ProductOtherTypeInfo { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public long? PaymentTypeId { get; set; }
        public string? Description { get; set; }
        public string? MoreDetails { get; set; }
        public string? PickupLocationLat { get; set; }
        public string? PickupLocationLong { get; set; }
        public string? DropLocationLat { get; set; }
        public string? DropLocationLong { get; set; }
        public string? CustomerAddress { get; set; }
        public long? OrderStatusTypeId { get; set; }
        public bool IsDeleted { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual Agent? Agent { get; set; }
        public virtual OrderStatusType? OrderStatusType { get; set; }
        public virtual PaymentType? PaymentType { get; set; }
        public virtual ProductType? ProductType { get; set; }
        public virtual ICollection<AgentOrderDeliveryPrice> AgentOrderDeliveryPrices { get; set; }
        public virtual ICollection<Bookkeeping> Bookkeepings { get; set; }
        public virtual ICollection<CaptainUserAcceptedRequest> CaptainUserAcceptedRequests { get; set; }
        public virtual ICollection<CaptainUserIgnoredRequest> CaptainUserIgnoredRequests { get; set; }
        public virtual ICollection<CaptainUserNewRequest> CaptainUserNewRequests { get; set; }
        public virtual ICollection<CaptainUserPaymentHistory> CaptainUserPaymentHistories { get; set; }
        public virtual ICollection<CaptainUserPayment> CaptainUserPayments { get; set; }
        public virtual ICollection<CityOrderPrice> CityOrderPrices { get; set; }
        public virtual ICollection<CountryOrderPrice> CountryOrderPrices { get; set; }
        public virtual ICollection<CouponUsage> CouponUsages { get; set; }
        public virtual ICollection<OrderAssignment> OrderAssignments { get; set; }
        public virtual ICollection<OrderCurrentStatus> OrderCurrentStatus { get; set; }
        public virtual ICollection<OrderEndLocation> OrderEndLocations { get; set; }
        public virtual ICollection<OrderInvoice> OrderInvoices { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<OrderQrcode> OrderQrcodes { get; set; }
        public virtual ICollection<OrderStartLocation> OrderStartLocations { get; set; }
        public virtual ICollection<OrderStatusHistory> OrderStatusHistories { get; set; }
        public virtual ICollection<PaidOrder> PaidOrders { get; set; }
        public virtual ICollection<RunningOrder> RunningOrders { get; set; }
    }
}
