using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class AdminUserAccount
    {
        public string Id { get; set; } = null!;
        public string? AdminUserId { get; set; }
        public long? AdminTypeId { get; set; }
        public long? StatusTypeId { get; set; }
        public string? Email { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string? Password { get; set; }
        public string? Token { get; set; }
        public bool IsDeleted { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual AdminType? AdminType { get; set; }
        public virtual AdminUser? AdminUser { get; set; }
    }
}
