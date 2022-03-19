using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class Country
    {
        public Country()
        {
            AdminUserCountries = new HashSet<AdminUser>();
            AdminUserResidenceCountries = new HashSet<AdminUser>();
            Agents = new HashSet<Agent>();
            Bonus = new HashSet<Bonus>();
            CaptainUserCountries = new HashSet<CaptainUser>();
            CaptainUserResidenceCountries = new HashSet<CaptainUser>();
            Cities = new HashSet<City>();
            CountryPrices = new HashSet<CountryPrice>();
            CountryProductPrices = new HashSet<CountryProductPrice>();
            CouponAssigns = new HashSet<CouponAssign>();
            Shifts = new HashSet<Shift>();
            SupportUserCountries = new HashSet<SupportUser>();
            SupportUserResidenceCountries = new HashSet<SupportUser>();
        }

        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Iso { get; set; }
        public long? Code { get; set; }
        public string? ArabicName { get; set; }
        public string? CurrencyName { get; set; }
        public string? CurrencyArabicName { get; set; }
        public string? CurrencyIso { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual ICollection<AdminUser> AdminUserCountries { get; set; }
        public virtual ICollection<AdminUser> AdminUserResidenceCountries { get; set; }
        public virtual ICollection<Agent> Agents { get; set; }
        public virtual ICollection<Bonus> Bonus { get; set; }
        public virtual ICollection<CaptainUser> CaptainUserCountries { get; set; }
        public virtual ICollection<CaptainUser> CaptainUserResidenceCountries { get; set; }
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<CountryPrice> CountryPrices { get; set; }
        public virtual ICollection<CountryProductPrice> CountryProductPrices { get; set; }
        public virtual ICollection<CouponAssign> CouponAssigns { get; set; }
        public virtual ICollection<Shift> Shifts { get; set; }
        public virtual ICollection<SupportUser> SupportUserCountries { get; set; }
        public virtual ICollection<SupportUser> SupportUserResidenceCountries { get; set; }
    }
}
