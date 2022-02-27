using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class TicketMessage
    {
        public long Id { get; set; }
        public long? TicketAssignId { get; set; }
        public bool? IsUser { get; set; }
        public long? UserTypeId { get; set; }
        public string? Message { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}
