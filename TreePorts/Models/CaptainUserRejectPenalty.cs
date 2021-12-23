using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class CaptainUserRejectPenalty
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public long? SystemSettingId { get; set; }
        public long? PenaltyStatusTypeId { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual PenaltyStatusType PenaltyStatusType { get; set; }
        public virtual SystemSetting SystemSetting { get; set; }
        public virtual CaptainUser User { get; set; }
    }
}
