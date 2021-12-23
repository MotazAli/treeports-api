using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class PaidOrder
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public long? OrderId { get; set; }
        public long? OrderAssignId { get; set; }
        public long? Type { get; set; }
        public decimal? Value { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}
