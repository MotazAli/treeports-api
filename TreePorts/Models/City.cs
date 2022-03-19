using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class City
    {
        public City()
        {
            AdminUserCities = new HashSet<AdminUser>();
            AdminUserResidenceCities = new HashSet<AdminUser>();
            Agents = new HashSet<Agent>();
            CaptainUserCities = new HashSet<CaptainUser>();
            CaptainUserResidenceCities = new HashSet<CaptainUser>();
            CityPrices = new HashSet<CityPrice>();
            Shifts = new HashSet<Shift>();
            SupportUserCities = new HashSet<SupportUser>();
            SupportUserResidenceCities = new HashSet<SupportUser>();
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

        public virtual Country? Country { get; set; }
        public virtual ICollection<AdminUser> AdminUserCities { get; set; }
        public virtual ICollection<AdminUser> AdminUserResidenceCities { get; set; }
        public virtual ICollection<Agent> Agents { get; set; }
        public virtual ICollection<CaptainUser> CaptainUserCities { get; set; }
        public virtual ICollection<CaptainUser> CaptainUserResidenceCities { get; set; }
        public virtual ICollection<CityPrice> CityPrices { get; set; }
        public virtual ICollection<Shift> Shifts { get; set; }
        public virtual ICollection<SupportUser> SupportUserCities { get; set; }
        public virtual ICollection<SupportUser> SupportUserResidenceCities { get; set; }
    }
}
