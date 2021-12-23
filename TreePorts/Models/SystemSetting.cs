using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class SystemSetting
    {
        public SystemSetting()
        {
            UserIgnoredPenalties = new HashSet<CaptainUserIgnoredPenalty>();
            UserPayments = new HashSet<CaptainUserPayment>();
            UserRejectPenalties = new HashSet<CaptainUserRejectPenalty>();
        }

        public long Id { get; set; }
        public bool? AllowUserToReject { get; set; }
        public bool? AllowUserToWorkOutShift { get; set; }
        public bool? AllowUserPayment { get; set; }
        public bool? AllowFixedPricePerCountry { get; set; }
        public bool? AllowPricePerProductCountry { get; set; }
        public long? RejectPerTypeId { get; set; }
        public int? RejectRequestsNumbers { get; set; }
        public long? RejectPenaltyPerTypeId { get; set; }
        public int? RejectPenaltyPeriodNumber { get; set; }
        public long? IgnorPerTypeId { get; set; }
        public int? IgnorRequestsNumbers { get; set; }
        public long? IgnorPenaltyPerTypeId { get; set; }
        public int? IgnorPenaltyPeriodNumber { get; set; }
        public bool? IsCurrent { get; set; }
        public bool IsDeleted { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual IgnorPerType IgnorPerType { get; set; }
        public virtual RejectPerType RejectPerType { get; set; }
        public virtual ICollection<CaptainUserIgnoredPenalty> UserIgnoredPenalties { get; set; }
        public virtual ICollection<CaptainUserPayment> UserPayments { get; set; }
        public virtual ICollection<CaptainUserRejectPenalty> UserRejectPenalties { get; set; }
    }
}
