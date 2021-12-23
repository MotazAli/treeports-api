using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class CountryPrice
    {
        public CountryPrice()
        {
            CountryPriceHistories = new HashSet<CountryPriceHistory>();
        }

        public long Id { get; set; }
        public long? CountryId { get; set; }
        public int? Kilometers { get; set; }
        public decimal? Price { get; set; }
        public int? ExtraKilometers { get; set; }
        public decimal? ExtraKiloPrice { get; set; }
        public bool IsDeleted { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public bool? IsCurrent { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<CountryPriceHistory> CountryPriceHistories { get; set; }
    }
}
