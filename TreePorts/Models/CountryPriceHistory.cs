using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class CountryPriceHistory
    {
        public long Id { get; set; }
        public long? CountryPriceId { get; set; }
        public int? Kilometers { get; set; }
        public decimal? Price { get; set; }
        public int? ExtraKilometers { get; set; }
        public decimal? ExtraKiloPrice { get; set; }
        public bool? IsDeleted { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual CountryPrice CountryPrice { get; set; }
    }
}
