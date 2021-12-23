using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreePorts.DTO.ReturnDTO
{
	
	public class UserResponse
	{
        public long Id { get; set; }
       
        public string NationalNumber { get; set; }
        public long? CountryId { get; set; }
		public string CountryName { get; set; }
		public long? CityId { get; set; }
		public string  CityName { get; set; }
		public string Address { get; set; }
      
       
        public string Mobile { get; set; }
     
        public int? ResidenceExpireDay { get; set; }
        public long? StatusTypeId { get; set; }
        public int? ResidenceExpireMonth { get; set; }
        public int? ResidenceExpireYear { get; set; }
        public string PersonalImageName { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string PersonalImageAndroidPath { get; set; }
        public string NationalNumberImageName { get; set; }
        public string NationalNumberImageAndroidPath { get; set; }
        public long? ResidenceCountryId { get; set; }
		public string ResidenceCountryName { get; set; }
		public long? ResidenceCityId { get; set; }
        public string ResidenceCityName { get; set; }
        public string CountryArabicName { get; set; }
        public string CityArabicName { get; set; }
        public string ResidenceCountryArabicName { get; set; }
        public string ResidenceCityArabicName { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? NationalNumberExpDate { get; set; }
        public bool? Gender { get; set; }
        public string StcPay { get; set; }
        public string PersonalImage { get; set; }
        public string NbsherNationalNumberImage { get; set; }
        public string NationalNumberFrontImage { get; set; }
        public string VehicleRegistrationImage { get; set; }
        public string DrivingLicenseImage { get; set; }
        public string VehiclePlateNumber { get; set; }
        //public virtual CityReponse City { get; set; }
        //public virtual CountryResponse Country { get; set; }
    }
}
