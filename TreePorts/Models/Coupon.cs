using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class Coupon
    {
        public long Id { get; set; }
        public string? CouponName { get; set; }
        public double? DiscountPercent { get; set; }
        public int? NumberOfUse { get; set; }
        public DateTime? ExpireDate { get; set; }
        public long? CouponTypeId { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}
