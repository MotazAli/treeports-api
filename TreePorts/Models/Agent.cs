using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class Agent
    {
        public Agent()
        {
            AgentLocationHistories = new HashSet<AgentLocationHistory>();
            CouponAssigns = new HashSet<CouponAssign>();
            CouponUsages = new HashSet<CouponUsage>();
            Orders = new HashSet<Order>();
            WebHooks = new HashSet<WebHook>();
        }

        public long Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public long? CountryId { get; set; }
        public long? CityId { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public long? AgentTypeId { get; set; }
        public bool? IsBranch { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Token { get; set; }
        public string LocationLat { get; set; }
        public string LocationLong { get; set; }
        public bool IsDeleted { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public long? StatusTypeId { get; set; }
        public string Image { get; set; }
        public string CommercialRegistrationNumber { get; set; }
        public string Website { get; set; }

        public virtual AgentType AgentType { get; set; }
        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<AgentLocationHistory> AgentLocationHistories { get; set; }
        public virtual ICollection<CouponAssign> CouponAssigns { get; set; }
        public virtual ICollection<CouponUsage> CouponUsages { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<WebHook> WebHooks { get; set; }
    }
}
