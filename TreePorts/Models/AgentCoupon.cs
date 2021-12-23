using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class AgentCoupon
    {
        public long Id { get; set; }
        public long? CouponId { get; set; }
        public long? AgentId { get; set; }

        public virtual Agent Agent { get; set; }
        public virtual Coupon Coupon { get; set; }
    }
}
