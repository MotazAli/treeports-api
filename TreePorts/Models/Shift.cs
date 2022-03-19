using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class Shift
    {
        public Shift()
        {
            CaptainUserShifts = new HashSet<CaptainUserShift>();
        }

        public long Id { get; set; }
        public string? StartHour { get; set; }
        public string? StartMinutes { get; set; }
        public string? EndHour { get; set; }
        public string? EndMinutes { get; set; }
        public int? Day { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public bool? IsDeleted { get; set; }
        public long? CountryId { get; set; }
        public long? CityId { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual City? City { get; set; }
        public virtual Country? Country { get; set; }
        public virtual ICollection<CaptainUserShift> CaptainUserShifts { get; set; }
    }
}
