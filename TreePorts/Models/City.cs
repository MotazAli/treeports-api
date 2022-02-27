using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class City
    {
        public City()
        {
            CaptainUsers = new HashSet<CaptainUser>();
        }

        public long Id { get; set; }
        public long? CountryId { get; set; }
        public string? Name { get; set; }
        public string? ArabicName { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual ICollection<CaptainUser> CaptainUsers { get; set; }
    }
}
