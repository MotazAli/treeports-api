using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class OrderStatusType
    {
        public OrderStatusType()
        {
            OrderCurrentStatus = new HashSet<OrderCurrentStatus>();
            OrderStatusHistories = new HashSet<OrderStatusHistory>();
            Orders = new HashSet<Order>();
        }

        public long Id { get; set; }
        public string? Type { get; set; }
        public string? ArabicType { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual ICollection<OrderCurrentStatus> OrderCurrentStatus { get; set; }
        public virtual ICollection<OrderStatusHistory> OrderStatusHistories { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
