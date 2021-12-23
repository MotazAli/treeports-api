using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class Bonus
    {
        public decimal? BonusPerMonth { get; set; }
        public long? OrdersPerMonth { get; set; }
        public decimal? BonusPerYear { get; set; }
        public long? OrdersPerYear { get; set; }
        public long? CountryId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public long Id { get; set; }
        public decimal? BonusPerDay { get; set; }
        public long? OrdersPerDay { get; set; }

        public virtual Country Country { get; set; }
    }
}
