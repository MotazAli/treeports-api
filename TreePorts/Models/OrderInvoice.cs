using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class OrderInvoice
    {
        public long Id { get; set; }
        public string? CaptainUserAccountId { get; set; }
        public long? OrderId { get; set; }
        public long? OrderAssignId { get; set; }
        public string? FileName { get; set; }
        public string? FileType { get; set; }
        public long? PaidOrderId { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}
