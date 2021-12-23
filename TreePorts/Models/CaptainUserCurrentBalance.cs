using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class CaptainUserCurrentBalance
    {
        public long Id { get; set; }
        public long? UserPaymentId { get; set; }
        public long? PaymentStatusTypeId { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual PaymentStatusType PaymentStatusType { get; set; }
        public virtual CaptainUserPayment UserPayment { get; set; }
    }
}
