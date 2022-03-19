using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class SupportUser
    {
        public SupportUser()
        {
            SupportUserAccounts = new HashSet<SupportUserAccount>();
        }

        public string Id { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NationalNumber { get; set; }
        public long? CountryId { get; set; }
        public long? CityId { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public string? Mobile { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? ResidenceExpireDate { get; set; }
        public string? PersonalImage { get; set; }
        public long? ResidenceCountryId { get; set; }
        public long? ResidenceCityId { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual City? City { get; set; }
        public virtual Country? Country { get; set; }
        public virtual City? ResidenceCity { get; set; }
        public virtual Country? ResidenceCountry { get; set; }
        public virtual ICollection<SupportUserAccount> SupportUserAccounts { get; set; }
    }
}
