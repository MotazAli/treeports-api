using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class SystemSetting
    {
        public SystemSetting()
        {
            CaptainUserIgnoredPenalties = new HashSet<CaptainUserIgnoredPenalty>();
            CaptainUserPaymentHistories = new HashSet<CaptainUserPaymentHistory>();
            CaptainUserPayments = new HashSet<CaptainUserPayment>();
        }

        public long Id { get; set; }
        public bool? AllowUserToWorkOutShift { get; set; }
        public bool? AllowUserPayment { get; set; }
        public bool? AllowFixedPricePerCountry { get; set; }
        public bool? AllowPricePerProductCountry { get; set; }
        public long? IgnorePerTypeId { get; set; }
        public int? IgnoreRequestsNumbers { get; set; }
        public long? IgnorePenaltyPerTypeId { get; set; }
        public int? IgnorePenaltyPeriodNumber { get; set; }
        public bool? IsCurrent { get; set; }
        public bool IsDeleted { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual PenaltyPerType? IgnorePenaltyPerType { get; set; }
        public virtual IgnorPerType? IgnorePerType { get; set; }
        public virtual ICollection<CaptainUserIgnoredPenalty> CaptainUserIgnoredPenalties { get; set; }
        public virtual ICollection<CaptainUserPaymentHistory> CaptainUserPaymentHistories { get; set; }
        public virtual ICollection<CaptainUserPayment> CaptainUserPayments { get; set; }
    }
}
