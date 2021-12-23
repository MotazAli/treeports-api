using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class OrderStatusHistory
    {
        public long Id { get; set; }
        public long? OrderId { get; set; }
        public long? OrderStatusId { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual Order Order { get; set; }
        public virtual OrderStatusType OrderStatus { get; set; }
    }
}
