using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class CaptainUserShift
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public string StartHour { get; set; }
        public string StartMinutes { get; set; }
        public string EndHour { get; set; }
        public string EndMinutes { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public long? ShiftId { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual CaptainUser User { get; set; }
    }
}
