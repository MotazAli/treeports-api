using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class Coupon
    {
        public Coupon()
        {
            CouponAssigns = new HashSet<CouponAssign>();
            CouponUsages = new HashSet<CouponUsage>();
        }

        public long Id { get; set; }
        public string Coupon1 { get; set; }
        public double? DiscountPercent { get; set; }
        public int? NumberOfUse { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public long? CouponType { get; set; }

        public virtual CouponType CouponTypeNavigation { get; set; }
        public virtual ICollection<CouponAssign> CouponAssigns { get; set; }
        public virtual ICollection<CouponUsage> CouponUsages { get; set; }
    }
}
