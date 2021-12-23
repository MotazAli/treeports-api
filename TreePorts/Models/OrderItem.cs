using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class OrderItem
    {
        public long Id { get; set; }
        public long? OrderId { get; set; }
        public string Item { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual Order Order { get; set; }
    }
}
