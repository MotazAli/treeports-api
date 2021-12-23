using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class CouponType
    {
        public CouponType()
        {
            Coupons = new HashSet<Coupon>();
        }

        public long Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Coupon> Coupons { get; set; }
    }
}
