using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class Promotion
    {
        public Promotion()
        {
            CaptainUserPromotions = new HashSet<CaptainUserPromotion>();
        }

        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Descriptions { get; set; }
        public string? Image { get; set; }
        public long? PromotionTypeId { get; set; }
        public string? Value { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual PromotionType? PromotionType { get; set; }
        public virtual ICollection<CaptainUserPromotion> CaptainUserPromotions { get; set; }
    }
}
