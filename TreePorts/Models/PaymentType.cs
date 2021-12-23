using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class PaymentType
    {
        public PaymentType()
        {
            Orders = new HashSet<Order>();
            UserPayments = new HashSet<CaptainUserPayment>();
        }

        public long Id { get; set; }
        public string Type { get; set; }
        public bool? Allowed { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string ArabicType { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<CaptainUserPayment> UserPayments { get; set; }
    }
}
