using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class OrderStatusType
    {
        public OrderStatusType()
        {
            OrderStatusHistories = new HashSet<OrderStatusHistory>();
        }

        public long Id { get; set; }
        public string Status { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string ArabicStatus { get; set; }

        public virtual ICollection<OrderStatusHistory> OrderStatusHistories { get; set; }
    }
}
