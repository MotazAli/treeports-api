using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class CaptainUserPaymentHistory
    {
        public long Id { get; set; }
        public string? CaptainUserAccountId { get; set; }
        public long? OrderId { get; set; }
        public long? PaymentTypeId { get; set; }
        public long? SystemSettingId { get; set; }
        public long? PaymentStatusTypeId { get; set; }
        public decimal? Value { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual CaptainUserAccount? CaptainUserAccount { get; set; }
        public virtual Order? Order { get; set; }
        public virtual PaymentStatusType? PaymentStatusType { get; set; }
        public virtual PaymentType? PaymentType { get; set; }
        public virtual SystemSetting? SystemSetting { get; set; }
    }
}
