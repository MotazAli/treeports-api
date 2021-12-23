using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class MaounOrder
    {
        public long Id { get; set; }
        public long? MaounOrderId { get; set; }
        public long? SenderOrderId { get; set; }
        public long? SenderAgentId { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}
