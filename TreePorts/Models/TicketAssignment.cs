using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class TicketAssignment
    {
        public TicketAssignment()
        {
            TicketMessages = new HashSet<TicketMessage>();
        }

        public long Id { get; set; }
        public long? TicketId { get; set; }
        public string? SupportUserAccountId { get; set; }
        public string? CaptainUserAccountId { get; set; }
        public long? TicketStatusTypeId { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual CaptainUserAccount? CaptainUserAccount { get; set; }
        public virtual SupportUserAccount? SupportUserAccount { get; set; }
        public virtual Ticket? Ticket { get; set; }
        public virtual TicketStatusType? TicketStatusType { get; set; }
        public virtual ICollection<TicketMessage> TicketMessages { get; set; }
    }
}
