using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class SupportUserAccount
    {
        public SupportUserAccount()
        {
            SupportUserCurrentStatus = new HashSet<SupportUserCurrentStatus>();
            SupportUserMessageHubs = new HashSet<SupportUserMessageHub>();
            SupportUserWorkingStates = new HashSet<SupportUserWorkingState>();
            TicketAssignments = new HashSet<TicketAssignment>();
        }

        public string Id { get; set; } = null!;
        public string? SupportUserId { get; set; }
        public long? SupportTypeId { get; set; }
        public long? StatusTypeId { get; set; }
        public string? Email { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string? Password { get; set; }
        public string? Token { get; set; }
        public bool IsDeleted { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual StatusType? StatusType { get; set; }
        public virtual SupportType? SupportType { get; set; }
        public virtual SupportUser? SupportUser { get; set; }
        public virtual ICollection<SupportUserCurrentStatus> SupportUserCurrentStatus { get; set; }
        public virtual ICollection<SupportUserMessageHub> SupportUserMessageHubs { get; set; }
        public virtual ICollection<SupportUserWorkingState> SupportUserWorkingStates { get; set; }
        public virtual ICollection<TicketAssignment> TicketAssignments { get; set; }
    }
}
