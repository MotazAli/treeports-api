using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class CaptainUser
    {
        public CaptainUser()
        {
            OrderAssignments = new HashSet<OrderAssignment>();
            Qrcodes = new HashSet<Qrcode>();
            SupportAssignments = new HashSet<SupportAssignment>();
            UserAcceptedRequests = new HashSet<CaptainUserAcceptedRequest>();
            UserAccounts = new HashSet<CaptainUserAccount>();
            UserActiveHistories = new HashSet<CaptainUserActiveHistory>();
            UserBonus = new HashSet<CaptainUserBonus>();
            UserCurrentActivities = new HashSet<CaptainUserCurrentActivity>();
            UserCurrentLocations = new HashSet<CaptainUserCurrentLocation>();
            UserCurrentStatus = new HashSet<CaptainUserCurrentStatus>();
            UserIgnoredPenalties = new HashSet<CaptainUserIgnoredPenalty>();
            UserIgnoredRequests = new HashSet<CaptainUserIgnoredRequest>();
            UserInactiveHistories = new HashSet<CaptainUserInactiveHistory>();
            UserNewRequests = new HashSet<CaptainUserNewRequest>();
            UserPayments = new HashSet<CaptainUserPayment>();
            UserRejectPenalties = new HashSet<CaptainUserRejectPenalty>();
            UserRejectedRequests = new HashSet<CaptainUserRejectedRequest>();
            UserShifts = new HashSet<CaptainUserShift>();
            UserStatusHistories = new HashSet<CaptainUserStatusHistory>();
            UserVehicles = new HashSet<CaptainUserVehicle>();
        }

        public long Id { get; set; }
        public string NationalNumber { get; set; }
        public long? CountryId { get; set; }
        public long? CityId { get; set; }
        public int? BirthDay { get; set; }
        public int? BirthMonth { get; set; }
        public int? BirthYear { get; set; }
        public string Mobile { get; set; }
        public int? ResidenceExpireDay { get; set; }
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
        public long? ResidenceCityId { get; set; }
        public int? NationalNoExpireDay { get; set; }
        public int? NationalNoExpireMonth { get; set; }
        public int? NationalNoExpireYear { get; set; }
        public string RecidenceImg { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? NationalNumberExpDate { get; set; }
        public string StcPay { get; set; }
        public bool? Gender { get; set; }
        public string PersonalImage { get; set; }
        public string NbsherNationalNumberImage { get; set; }
        public string NationalNumberFrontImage { get; set; }
        public string VehicleRegistrationImage { get; set; }
        public string DrivingLicenseImage { get; set; }
        public string VehiclePlateNumber { get; set; }

        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<OrderAssignment> OrderAssignments { get; set; }
        public virtual ICollection<Qrcode> Qrcodes { get; set; }
        public virtual ICollection<SupportAssignment> SupportAssignments { get; set; }
        public virtual ICollection<CaptainUserAcceptedRequest> UserAcceptedRequests { get; set; }
        public virtual ICollection<CaptainUserAccount> UserAccounts { get; set; }
        public virtual ICollection<CaptainUserActiveHistory> UserActiveHistories { get; set; }
        public virtual ICollection<CaptainUserBonus> UserBonus { get; set; }
        public virtual ICollection<CaptainUserCurrentActivity> UserCurrentActivities { get; set; }
        public virtual ICollection<CaptainUserCurrentLocation> UserCurrentLocations { get; set; }
        public virtual ICollection<CaptainUserCurrentStatus> UserCurrentStatus { get; set; }
        public virtual ICollection<CaptainUserIgnoredPenalty> UserIgnoredPenalties { get; set; }
        public virtual ICollection<CaptainUserIgnoredRequest> UserIgnoredRequests { get; set; }
        public virtual ICollection<CaptainUserInactiveHistory> UserInactiveHistories { get; set; }
        public virtual ICollection<CaptainUserNewRequest> UserNewRequests { get; set; }
        public virtual ICollection<CaptainUserPayment> UserPayments { get; set; }
        public virtual ICollection<CaptainUserRejectPenalty> UserRejectPenalties { get; set; }
        public virtual ICollection<CaptainUserRejectedRequest> UserRejectedRequests { get; set; }
        public virtual ICollection<CaptainUserShift> UserShifts { get; set; }
        public virtual ICollection<CaptainUserStatusHistory> UserStatusHistories { get; set; }
        public virtual ICollection<CaptainUserVehicle> UserVehicles { get; set; }
    }
}
