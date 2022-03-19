using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class Ticket
    {
        public Ticket()
        {
            TicketAssignments = new HashSet<TicketAssignment>();
        }

        public long Id { get; set; }
        public long? TicketTypeId { get; set; }
        public string? CaptainUserAccountId { get; set; }
        public long? TicketStatusTypeId { get; set; }
        public string? Description { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual CaptainUserAccount? CaptainUserAccount { get; set; }
        public virtual TicketStatusType? TicketStatusType { get; set; }
        public virtual TicketType? TicketType { get; set; }
        public virtual ICollection<TicketAssignment> TicketAssignments { get; set; }
    }
}
