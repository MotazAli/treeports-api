using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class Order
    {
        public Order()
        {
            CouponUsages = new HashSet<CouponUsage>();
            OrderAssignments = new HashSet<OrderAssignment>();
            OrderCurrentStatus = new HashSet<OrderCurrentStatus>();
            OrderEndLocations = new HashSet<OrderEndLocation>();
            OrderItems = new HashSet<OrderItem>();
            OrderStartLocations = new HashSet<OrderStartLocation>();
            OrderStatusHistories = new HashSet<OrderStatusHistory>();
            Qrcodes = new HashSet<Qrcode>();
            UserAcceptedRequests = new HashSet<CaptainUserAcceptedRequest>();
        }

        public long Id { get; set; }
        public long? AgentId { get; set; }
        public long? ProductTypeId { get; set; }
        public string ProductOtherType { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public long? PaymentTypeId { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public string PickupLocationLat { get; set; }
        public string PickupLocationLong { get; set; }
        public string DropLocationLat { get; set; }
        public string DropLocationLong { get; set; }
        public long? CurrentStatus { get; set; }
        public bool IsDeleted { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string CustomerAddress { get; set; }

        public virtual Agent Agent { get; set; }
        public virtual PaymentType PaymentType { get; set; }
        public virtual ProductType ProductType { get; set; }
        public virtual ICollection<CouponUsage> CouponUsages { get; set; }
        public virtual ICollection<OrderAssignment> OrderAssignments { get; set; }
        public virtual ICollection<OrderCurrentStatus> OrderCurrentStatus { get; set; }
        public virtual ICollection<OrderEndLocation> OrderEndLocations { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<OrderStartLocation> OrderStartLocations { get; set; }
        public virtual ICollection<OrderStatusHistory> OrderStatusHistories { get; set; }
        public virtual ICollection<Qrcode> Qrcodes { get; set; }
        public virtual ICollection<CaptainUserAcceptedRequest> UserAcceptedRequests { get; set; }
    }
}
