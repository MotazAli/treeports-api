using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class Agent
    {
        public Agent()
        {
            AgentBranches = new HashSet<AgentBranch>();
            AgentCurrentStatus = new HashSet<AgentCurrentStatus>();
            AgentDeliveryPrices = new HashSet<AgentDeliveryPrice>();
            AgentLocationHistories = new HashSet<AgentLocationHistory>();
            AgentMessageHubs = new HashSet<AgentMessageHub>();
            CaptainUserIgnoredRequests = new HashSet<CaptainUserIgnoredRequest>();
            CaptainUserNewRequests = new HashSet<CaptainUserNewRequest>();
            CouponAssigns = new HashSet<CouponAssign>();
            CouponUsages = new HashSet<CouponUsage>();
            Orders = new HashSet<Order>();
            Webhooks = new HashSet<Webhook>();
        }

        public string Id { get; set; } = null!;
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public long? CountryId { get; set; }
        public long? CityId { get; set; }
        public string? Address { get; set; }
        public string? Mobile { get; set; }
        public long? AgentTypeId { get; set; }
        public bool? IsBranch { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string? Password { get; set; }
        public string? Token { get; set; }
        public string? LocationLat { get; set; }
        public string? LocationLong { get; set; }
        public bool IsDeleted { get; set; }
        public long? StatusTypeId { get; set; }
        public string? Image { get; set; }
        public string? CommercialRegistrationNumber { get; set; }
        public string? Website { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual AgentType? AgentType { get; set; }
        public virtual City? City { get; set; }
        public virtual Country? Country { get; set; }
        public virtual StatusType? StatusType { get; set; }
        public virtual ICollection<AgentBranch> AgentBranches { get; set; }
        public virtual ICollection<AgentCurrentStatus> AgentCurrentStatus { get; set; }
        public virtual ICollection<AgentDeliveryPrice> AgentDeliveryPrices { get; set; }
        public virtual ICollection<AgentLocationHistory> AgentLocationHistories { get; set; }
        public virtual ICollection<AgentMessageHub> AgentMessageHubs { get; set; }
        public virtual ICollection<CaptainUserIgnoredRequest> CaptainUserIgnoredRequests { get; set; }
        public virtual ICollection<CaptainUserNewRequest> CaptainUserNewRequests { get; set; }
        public virtual ICollection<CouponAssign> CouponAssigns { get; set; }
        public virtual ICollection<CouponUsage> CouponUsages { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Webhook> Webhooks { get; set; }
    }
}
