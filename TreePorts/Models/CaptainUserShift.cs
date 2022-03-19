using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class CaptainUserShift
    {
        public long Id { get; set; }
        public string? CaptainUserAccountId { get; set; }
        public string? StartHour { get; set; }
        public string? StartMinutes { get; set; }
        public string? EndHour { get; set; }
        public string? EndMinutes { get; set; }
        public long? ShiftId { get; set; }
        public bool? IsDeleted { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual CaptainUserAccount? CaptainUserAccount { get; set; }
        public virtual Shift? Shift { get; set; }
    }
}
