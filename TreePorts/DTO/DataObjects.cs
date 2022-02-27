using MimeKit;
using Newtonsoft.Json;
using TreePorts.Models;
using TreePorts.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Device.Location;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TreePorts.DTO
{
    public class DataObjects
    {
    }


    public enum UserTypes {
    Admin= 1,
    Support= 2,
    Captain= 3,
    Agent= 4
    }


    public enum StatusTypes {
        New = 1,
        Ready = 2,
        Working = 3 ,
        Progress = 4,
        Suspended = 5,
        Stopped = 6,
        Reviewing = 7,
        Penalty = 8,
        Incomplete = 9,
        Complete = 10,
        Active = 11,
        Inactive = 12

    }

    public enum PeniltyPar
    {
        Hours = 1,
        Days = 2,
        Months = 3,
        Years = 4

    }

    

    public enum RejectPer
    {
        Hours = 1,
        Days = 2,
        Months = 3,
        Years = 4
    }


    public enum IgnorPer
    {
        Hours = 1,
        Days = 2,
        Months = 3,
        Years = 4
    }


    public enum PenaltyStatusTypes
    {
        New = 1,
        End = 2,
        Canceled = 3,

    }

    public enum OrderStatusTypes
    {
        New = 1,
        AssignedToCaptain = 2,
        PickedUp = 3,
        Progress = 4,
        Dropped = 5,
        Delivered = 6,
        Canceled = 7,
        End = 8,
        NotAssignedToCaptain = 9,
        SearchingForCaptain = 10
        
    }


    public enum SupportStatusTypes
    {
        New = 1,
        Ready = 2,
        Progress = 3,
        Canceled = 4,
        End = 5
        

    }


    public enum MessageStatusTypes
    {
        New = 1,
        Read = 2,
        Replyed = 3,
        Reviewing = 4,
        NeedAction = 5,
        Canceled = 6,
        Closed = 7,
    }



    public enum PaymentStatusTypes
    {
        New = 1,
        Working = 2,
        Running = 3,
        Transfered = 4,
        Complete = 5,
        Postponed = 6,
        Hold = 7,
        Reviewing = 8,
        Canclled = 9,
        Incomplete = 10
    }

    public enum PromotionTypes
    {
        Ads = 1,
        Discount = 2,
        Offer = 3
    }

    public enum PaymentTypes
    {
        Paid = 1,
        Cash = 2,
        WalletCredit = 3,
        CreditCard = 4
    }


    public enum ProductTypes
    {
        Equipment = 1,
        Furniture = 2,
        Clothes = 3,
        Health = 4,
        Electonics = 5,
        Foods = 6,
        Groceries = 7,
        Merchandise = 8,
        Other = 9
    }


    public enum AgentTypes
    {
        Store = 1,
        Resturant = 2,
        Company = 3,
        Institution = 4,
        Hospital = 5,
        Organization = 6,
        IndividualBusiness = 7,
        Other = 8
    }
    public enum WebHookTypes
	{
        OrderStatus = 1,
        CaptainLocation = 2,
        Coupon =3
	}
    public enum BonusTypes
	{
        BonusPerDay = 1,
        BonusPerMonth = 2,
        BonusPerYear = 3
    }

    public enum DepositTypes
    {
        Order_Items_Amount= 1,
        Delivery_Amount = 2,
        Bonus_Amount = 3
    }

    public enum CouponTypes
	{
        ExpireByDate =1,
        ExpireByUsage =2
	}

    public enum RabbitMQQueues
    {
        CoreServiceNewUser,
        CoreServiceUpdateUser,
        CoreServiceDeleteUser,
        CoreServiceAcceptedUser
    }


    public class RegisterDriver
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Mobile { get; set; }
    }


   


    public class DriverPhone
    {
        [Required]
        public string Mobile { get; set; }
    }


    public class ResetPassword
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string UserType { get; set; }
    }


    public class LoginUser
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "You must enter password 6 digites")]
        public string Password { get; set; }
    }


    public class ImageHelper
    {
        
        public string ImageName { get; set; }
        
        public string ImageBase64 { get; set; }
    }


    

    


    //public class LoginSupport
    //{
    //    [Required]
    //    public string Email { get; set; }
    //    [Required]
    //    [StringLength(6, MinimumLength = 6, ErrorMessage = "You must enter password 6 digites")]
    //    public string Password { get; set; }
    //}



    


    public class Pagination {

        public int Page { get; set; }
        public int NumberOfObjectsPerPage { get; set; }
       

	}


    public class PaginationUser
    {
        public int UserId { get; set; }
        public int Page { get; set; } = 1;
        public int NumberOfObjectsPerPage { get; set; } = 10;
    }


    public class Geo : GeoCoordinate {
        public long UserID { get; set; }

        public Geo() { }
        public Geo(long userId,string latitude, string longitude) : base(double.Parse(latitude), double.Parse(longitude)) {
            UserID = userId;
        }
    } 


    public class UserState { 
        public long UserID { get; set; }
        public long StateID { get; set; }
        public long CreatedBy { get; set; }
    }


    public class SupportRequest
    {
        public long UserID { get; set; }
        public long TypeID { get; set; }
        public long CreatedBy { get; set; }
    }


    
    
    
    public class Location
    {
        public string Lat { get; set; }
        public string Lng { get; set; }
    }


    public class NotificationMetadata
    {
        public string Sender { get; set; }
        public string Reciever { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }


    public class EmailMessage
    {
        public MailboxAddress Sender { get; set; }
        public MailboxAddress Reciever { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }


    



    public static class FirebaseTopics
    {
        public const string Admins = "admins";
        public const string Supports = "supports";
        public const string Agents = "agents";
        public const string Captains = "captains"; 
    }

    public class FirebaseNotificationResponse
    {
        [JsonProperty("message_id")]
        public string messageId { get; set; }
    }


    public class FilterParameters
	{
        public long? FilterById { get; set; }
        public DateTime? FilterByDate { get; set; }
        public string FullName { get; set; }
        public string DriverName { get; set; }
        public long? StatusTypeId { get; set; }
     
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
		public string? FilterByCaptainUserAccountId { get; set; }
        public long? CurrentStatusId { get; set; }
        public string CountryName { get; set; }
        public string CityName { get; set; }
        public string ResidenceCountryName { get; set; }
        public string ResidenceCityName { get; set; }
        public string CountryArabicName { get; set; }
        public string CityArabicName { get; set; }
        public string ResidenceCountryArabicName { get; set; }
        public string ResidenceCityArabicName { get; set; }
        public decimal? OrderDeliveryPaymentAmount { get; set; }
        public int Page { get; set; } = 1;
        public int NumberOfObjectsPerPage { get; set; } = 10;
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string NationalNumber { get; set; }
        public long? StatusUserTypeId { get; set; }
        public long? StatusSupportTypeId { get; set; }
		public string VehiclePlateNumber { get; set; }
        public long? AgentId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }

        public long? ProductTypeId { get; set; }
        public long? PaymentTypeId { get; set; }

        public string OrderAgentName { get; set; }
        public string OrderCityName { get; set; }
        public string OrderCountryName { get; set; }
		public string OrderCaptain { get; set; }

	}


    public class HierarchyFilter 
    {
        public long Id { get; set; }
    
        public DateTime? CreationDate { get; set; }
        [NotMapped]
		public int ProductTypeId { get; set; }
        [NotMapped]
        public int CurrentStatus { get; set; }
        [NotMapped]
        public string Fullname { get; set; }
        [NotMapped]
        public string CustomerName { get; set; }
        [NotMapped]
        public int StatusTypeId { get; set; }


    }
    // Generic Extention Method To Transfer From Object to IQuerable<Object>
    public static class ObjectExtensionMethods
    {
        public static IQueryable<T> ToQueryable<T>(this T instance)
        {
            return new[] { instance }.AsQueryable();
        }
    }
    public class DistanceDetails
	{
		public DistanceDetails()
        {
            
            
		}
		public string UserLong { get; set; }
        public string UserLat { get; set; }
        public string PickedLong { get; set; }
        public string PickedLat { get; set; }
		public double Distance { get; set; }
		public long? UserId { get; set; }
        public  double CalculateDistance()
		{
            this.Distance = Utility.distance(double.Parse(this.UserLat), double.Parse(this.UserLong),
                                        double.Parse(this.PickedLat), double.Parse(this.PickedLong));
            return this.Distance;
        }
	}
    public class AgentCouponDto
	{
        // [Required]
        public long CouponId { get; set; }
        public string[]? ListOfAgentIds { get; set; }
       
        public DateTime ExpireDate { get; set; }
        [Required]
		
		public int CouponLength { get; set; }
        [Required]
        public double DiscountPercent { get; set; }
       
		public int NumberOfUsage { get; set; }
		public long? CountryId { get; set; }
        public long? CouponType { get; set; }

    }
    public class AssignCouponDto
	{
        public long CouponId { get; set; }
        public string[]? ListOfAgentIds { get; set; }
        public long? CountryId { get; set; }
    }
   /* public class BonusCheckDto
	{
		public long userId { get; set; }
		public DateTime date { get; set; }
	}*/
    public class WebhookEvent
    {
        
        /// Webhook unique name
       
        [Required]
        public string WebhookName { get; set; }

        /// Webhook data as JSON string.
      
        public string Data { get; set; }
    }
    public class WebhookSendAttempt
    {
        /// <summary>
        /// <see cref="WebhookEvent"/> foreign id 
        /// </summary>
        [Required]
        public Guid WebhookEventId { get; set; }

        /// <summary>
        /// <see cref="WebhookSubscription"/> foreign id 
        /// </summary>
        [Required]
        public Guid WebhookSubscriptionId { get; set; }

        /// <summary>
        /// Webhook response content that webhook endpoint send back
        /// </summary>
        public string Response { get; set; }

        /// <summary>
        /// Webhook response status code that webhook endpoint send back
        /// </summary>
        public HttpStatusCode? ResponseStatusCode { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// WebhookEvent of this send attempt.
        /// </summary>
        [ForeignKey("WebhookEventId")]
        public virtual WebhookEvent WebhookEvent { get; set; }
    }

    public class WebhookPayload
    {
        public string Id { get; set; }

        public string Event { get; set; }

        public int Attempt { get; set; }

        public dynamic Data { get; set; }

        public DateTime CreationTimeUtc { get; set; }
    }


    public class RabbitMQUser
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("familyName")]
        public string FamilyName { get; set; }

        [JsonProperty("fullname")]
        public string Fullname { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("stcPay")]
        public string StcPay { get; set; }


        [JsonProperty("nationalNumber")]
        public string NationalNumber { get; set; }


        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("countryId")]
        public int CountryId { get; set; }

        [JsonProperty("cityId")]
        public int CityId { get; set; }

        [JsonProperty("coreUserId")]
        public string CoreUserId { get; set; }

        [JsonProperty("supportUserId")]
        public string SupportUserId { get; set; }

        [JsonProperty("userTypeId")]
        public int UserTypeId { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }


        [JsonProperty("session")]
        public RabbitMQUserSession Session { get; set; }


        [JsonProperty("status")]
        public RabbitMQUserStatus Status { get; set; }
    }


    public class RabbitMQUserSession 
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }


        
    }

    public class RabbitMQUserStatus
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("statusTypeId")]
        public int StatusTypeId { get; set; }

        [JsonProperty("isCurrent")]
        public bool IsCurrent { get; set; }

    }


    public class GoogleMapsResponse
	{
        [JsonProperty("routes")]
        public List<Route> Routes { get; set; }
        [JsonProperty("status")]
        public string status { get; set; }
	}
   
    public class Route
    {
        [JsonProperty("legs")]
        public List<Leg> Legs { get; set; }
    }
    public class Leg
	{
        [JsonProperty("distance")]
        public Distance distance { get; set; }
        [JsonProperty("duration")]
        public Duration duration { get; set; }
	}
    public class Distance
	{
        [JsonProperty("text")]
        public string text { get; set; }
        [JsonProperty("value")]
        public string value { get; set; }
	}
    public class Duration
	{
        [JsonProperty("text")]
        public string text { get; set; }
        [JsonProperty("value")]
        public string value { get; set; }
    }
    
}
