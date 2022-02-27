using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class OrderEndLocation
    {
        public long Id { get; set; }
        public long? OrderId { get; set; }
        public long? OrderAssignId { get; set; }
        public string? DroppedLat { get; set; }
        public string? DroppedLong { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}
