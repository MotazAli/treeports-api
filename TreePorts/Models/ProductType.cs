using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class ProductType
    {
        public ProductType()
        {
            CountryProductPrices = new HashSet<CountryProductPrice>();
            Orders = new HashSet<Order>();
        }

        public long Id { get; set; }
        public string? Type { get; set; }
        public string? ArabicType { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual ICollection<CountryProductPrice> CountryProductPrices { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
