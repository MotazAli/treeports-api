using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class PenaltyStatusType
    {
        public PenaltyStatusType()
        {
            UserIgnoredPenalties = new HashSet<CaptainUserIgnoredPenalty>();
            UserRejectPenalties = new HashSet<CaptainUserRejectPenalty>();
        }

        public long Id { get; set; }
        public string Type { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual ICollection<CaptainUserIgnoredPenalty> UserIgnoredPenalties { get; set; }
        public virtual ICollection<CaptainUserRejectPenalty> UserRejectPenalties { get; set; }
    }
}
