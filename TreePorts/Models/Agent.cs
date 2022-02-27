using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class Agent
    {
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
    }
}
