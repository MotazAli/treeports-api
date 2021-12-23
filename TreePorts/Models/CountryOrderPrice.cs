using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class CountryOrderPrice
    {
        public long Id { get; set; }
        public long? OrderId { get; set; }
        public long? CountryPriceId { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}
