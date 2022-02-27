using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class CaptainUserBonus
    {
        public long Id { get; set; }
        public string? CaptainUserAccountId { get; set; }
        public bool IsWithdrawed { get; set; }
        public DateTime? WithdrawDate { get; set; }
        public decimal? Amount { get; set; }
        public long? BonusTypeId { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}
