
using System.ComponentModel.DataAnnotations.Schema;
namespace TreePorts.Models
{

    //public partial class SenderDBContext : DbContext
    //{
    //    public virtual DbSet<UserLocation> UsersLocations { get; set; }



    //}


    //public class UserLocation 
    //{
    //    //[NotMapped]
    //    public double Lat { get; set; }

    //    //[NotMapped]
    //    public double Long { get; set; }
    //}



    public partial class CaptainUser
    {
        [NotMapped]
        public string CountryName { get; set; }
        [NotMapped]
        public string CityName { get; set; }
        [NotMapped]
        public string ResidenceCountryName { get; set; }
        [NotMapped]
        public string ResidenceCityName { get; set; }
        [NotMapped]
        public string CountryArabicName { get; set; }
        [NotMapped]
        public string CityArabicName { get; set; }
        [NotMapped]
        public string ResidenceCountryArabicName { get; set; }
        [NotMapped]
        public string ResidenceCityArabicName { get; set; }

        // public long? StatusUserTypeId { get; set; }
        [NotMapped]
        public long? CurrentStatusId { get; set; }
        [NotMapped]
        public string ImgUrl { get; set; }

        [NotMapped]
        public double Lat { get; set; }
        [NotMapped]
        public double Long { get; set; }

    }


    public partial class SupportUser
    {
        [NotMapped]
        public string CountryName { get; set; }
        [NotMapped]
        public string CityName { get; set; }
        [NotMapped]
        public string CountryArabicName { get; set; }
        [NotMapped]
        public string CityArabicName { get; set; }
        [NotMapped]
        public string ResidenceCountryName { get; set; }
        [NotMapped]
        public string ResidenceCityName { get; set; }
        [NotMapped]
        public string ResidenceCountryArabicName { get; set; }
        [NotMapped]
        public string ResidenceCityArabicName { get; set; }

        [NotMapped]
        public long? CurrentStatusId { get; set; }
    }


    public partial class AdminUser
    {
        [NotMapped]
        public string CountryName { get; set; }
        [NotMapped]
        public string CityName { get; set; }
        [NotMapped]
        public string CountryArabicName { get; set; }
        [NotMapped]
        public string CityArabicName { get; set; }
        [NotMapped]
        public string ResidenceCountryName { get; set; }
        [NotMapped]
        public string ResidenceCityName { get; set; }
        [NotMapped]
        public string ResidenceCountryArabicName { get; set; }
        [NotMapped]
        public string ResidenceCityArabicName { get; set; }
        [NotMapped]
        public long? CurrentStatusId { get; set; }
    }


    public partial class Agent
    {
        [NotMapped]
        public string CountryName { get; set; }
        [NotMapped]
        public string CityName { get; set; }
        [NotMapped]
        public string CountryArabicName { get; set; }
        [NotMapped]
        public string CityArabicName { get; set; }
        [NotMapped]
        public long? CurrentStatusId { get; set; }
    }



    public partial class Order
    {
        [NotMapped]
        public decimal? OrderDeliveryPaymentAmount { get; set; }
    }
}
