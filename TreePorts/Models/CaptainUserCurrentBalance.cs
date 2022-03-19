using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class CaptainUserCurrentBalance
    {
        public long Id { get; set; }
        public long? CaptainUserPaymentId { get; set; }
        public long? PaymentStatusTypeId { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual CaptainUserPayment? CaptainUserPayment { get; set; }
        public virtual PaymentStatusType? PaymentStatusType { get; set; }
    }
}
