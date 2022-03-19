using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class CaptainUserPromotion
    {
        public long Id { get; set; }
        public string? CaptainUserAccountId { get; set; }
        public long? PromotionId { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual CaptainUserAccount? CaptainUserAccount { get; set; }
        public virtual Promotion? Promotion { get; set; }
    }
}
