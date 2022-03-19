﻿using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class PaymentType
    {
        public PaymentType()
        {
            CaptainUserPaymentHistories = new HashSet<CaptainUserPaymentHistory>();
            CaptainUserPayments = new HashSet<CaptainUserPayment>();
            Orders = new HashSet<Order>();
        }

        public long Id { get; set; }
        public string? Type { get; set; }
        public string? ArabicType { get; set; }
        public bool? Allowed { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual ICollection<CaptainUserPaymentHistory> CaptainUserPaymentHistories { get; set; }
        public virtual ICollection<CaptainUserPayment> CaptainUserPayments { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
