using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class SystemSetting
    {
        public long Id { get; set; }
        public bool? AllowUserToWorkOutShift { get; set; }
        public bool? AllowUserPayment { get; set; }
        public bool? AllowFixedPricePerCountry { get; set; }
        public bool? AllowPricePerProductCountry { get; set; }
        public long? IgnorPerTypeId { get; set; }
        public int? IgnorRequestsNumbers { get; set; }
        public long? IgnorPenaltyPerTypeId { get; set; }
        public int? IgnorPenaltyPeriodNumber { get; set; }
        public bool? IsCurrent { get; set; }
        public bool IsDeleted { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}
