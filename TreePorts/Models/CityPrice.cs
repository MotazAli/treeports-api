using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class CityPrice
    {
        public CityPrice()
        {
            CityOrderPrices = new HashSet<CityOrderPrice>();
        }

        public long Id { get; set; }
        public long? CityId { get; set; }
        public int? Kilometers { get; set; }
        public decimal? Price { get; set; }
        public int? ExtraKilometers { get; set; }
        public decimal? ExtraKiloPrice { get; set; }
        public bool? IsCurrent { get; set; }
        public bool? IsDeleted { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual City? City { get; set; }
        public virtual ICollection<CityOrderPrice> CityOrderPrices { get; set; }
    }
}
