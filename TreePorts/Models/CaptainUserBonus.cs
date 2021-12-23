using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class CaptainUserBonus
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public bool IsWithdrawed { get; set; }
        public DateTime? WithdrawDate { get; set; }
        public decimal? Amount { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public long? BonusTypeId { get; set; }

        public virtual BonusType BonusType { get; set; }
        public virtual CaptainUser User { get; set; }
    }
}
