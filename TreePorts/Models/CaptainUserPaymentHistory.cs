﻿using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class CaptainUserPaymentHistory
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public long? OrderId { get; set; }
        public long? PaymentTypeId { get; set; }
        public long? SystemSettingId { get; set; }
        public long? StatusId { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public decimal? Value { get; set; }
    }
}
