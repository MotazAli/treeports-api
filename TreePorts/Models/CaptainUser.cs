using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class CaptainUser
    {
        public CaptainUser()
        {
            CaptainUserAccounts = new HashSet<CaptainUserAccount>();
        }

        public string Id { get; set; } = null!;
        public string? NationalNumber { get; set; }
        public long? CountryId { get; set; }
        public long? CityId { get; set; }
        public string? Mobile { get; set; }
        public long? ResidenceCountryId { get; set; }
        public long? ResidenceCityId { get; set; }
        public string? RecidenceImage { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? NationalNumberExpireDate { get; set; }
        public string? StcPay { get; set; }
        public string? Gender { get; set; }
        public string? PersonalImage { get; set; }
        public string? NbsherNationalNumberImage { get; set; }
        public string? NationalNumberFrontImage { get; set; }
        public string? VehicleRegistrationImage { get; set; }
        public string? DrivingLicenseImage { get; set; }
        public string? VehiclePlateNumber { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual City? City { get; set; }
        public virtual Country? Country { get; set; }
        public virtual ICollection<CaptainUserAccount> CaptainUserAccounts { get; set; }
    }
}
