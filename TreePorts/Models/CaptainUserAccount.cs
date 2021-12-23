using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class CaptainUserAccount
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public long? StatusTypeId { get; set; }
        public string Mobile { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Token { get; set; }
        public bool IsDeleted { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string Fullname { get; set; }
        public string PersonalImage { get; set; }
        public string Password { get; set; }

        public virtual StatusType StatusType { get; set; }
        public virtual CaptainUser User { get; set; }
    }
}
