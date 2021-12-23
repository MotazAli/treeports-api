using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class CouponAssign
    {
        public long Id { get; set; }
        public long? CouponId { get; set; }
        public long? AgentId { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public long? CountryId { get; set; }

        public virtual Agent Agent { get; set; }
        public virtual Country Country { get; set; }
        public virtual Coupon Coupon { get; set; }
    }
}
