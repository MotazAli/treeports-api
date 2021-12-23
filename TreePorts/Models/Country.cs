using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class Country
    {
        public Country()
        {
            AdminUsers = new HashSet<AdminUser>();
            Agents = new HashSet<Agent>();
            Bonus = new HashSet<Bonus>();
            CountryPrices = new HashSet<CountryPrice>();
            CountryProductPrices = new HashSet<CountryProductPrice>();
            CouponAssigns = new HashSet<CouponAssign>();
            SupportUsers = new HashSet<SupportUser>();
            Users = new HashSet<CaptainUser>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Iso { get; set; }
        public long? Code { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string ArabicName { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyArabicName { get; set; }
        public string CurrencyIso { get; set; }

        public virtual ICollection<AdminUser> AdminUsers { get; set; }
        public virtual ICollection<Agent> Agents { get; set; }
        public virtual ICollection<Bonus> Bonus { get; set; }
        public virtual ICollection<CountryPrice> CountryPrices { get; set; }
        public virtual ICollection<CountryProductPrice> CountryProductPrices { get; set; }
        public virtual ICollection<CouponAssign> CouponAssigns { get; set; }
        public virtual ICollection<SupportUser> SupportUsers { get; set; }
        public virtual ICollection<CaptainUser> Users { get; set; }
    }
}
