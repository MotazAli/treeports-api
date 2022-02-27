using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class CaptainUserAccount
    {
        public CaptainUserAccount()
        {
            TicketAssignments = new HashSet<TicketAssignment>();
        }

        public string Id { get; set; } = null!;
        public string? CaptainUserId { get; set; }
        public long? StatusTypeId { get; set; }
        public string? Mobile { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string? Token { get; set; }
        public string? Password { get; set; }
        public bool IsDeleted { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual CaptainUser? CaptainUser { get; set; }
        public virtual StatusType? StatusType { get; set; }
        public virtual ICollection<TicketAssignment> TicketAssignments { get; set; }
    }
}
