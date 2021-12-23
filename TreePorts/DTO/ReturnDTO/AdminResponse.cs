using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreePorts.DTO.ReturnDTO
{
	public class AdminResponse
	{
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
        public string CountryName { get; set; }
        public string CityName { get; set; }
        public string ResidenceCountryName { get; set; }
        public string ResidenceCityName { get; set; }
        public string CountryArabicName { get; set; }
        public string CityArabicName { get; set; }
        public string ResidenceCountryArabicName { get; set; }
        public string ResidenceCityArabicName { get; set; }

    }
}
