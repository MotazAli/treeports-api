using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class AdminUser
    {
        public AdminUser()
        {
            AdminUserAccounts = new HashSet<AdminUserAccount>();
        }

        public long Id { get; set; }
        public string Fullname { get; set; }
        public string NationalNumber { get; set; }
        public long? CountryId { get; set; }
        public long? CityId { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public int? BirthDay { get; set; }
        public int? BirthMonth { get; set; }
        public int? BirthYear { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int? ResidenceExpireDay { get; set; }
        public int? ResidenceExpireMonth { get; set; }
        public int? ResidenceExpireYear { get; set; }
        public string Image { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public long? ResidenceCountryId { get; set; }
        public long? ResidenceCityId { get; set; }

        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<AdminUserAccount> AdminUserAccounts { get; set; }
    }
}
