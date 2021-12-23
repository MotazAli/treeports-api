using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class Shift
    {
        public long Id { get; set; }
        public string StartHour { get; set; }
        public string StartMinutes { get; set; }
        public string EndHour { get; set; }
        public string EndMinutes { get; set; }
        public int? Day { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public bool? IsDeleted { get; set; }
        public long? CountryId { get; set; }
        public long? CityId { get; set; }
    }
}
