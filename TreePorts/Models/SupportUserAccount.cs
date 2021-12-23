﻿using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class SupportUserAccount
    {
        public long Id { get; set; }
        public long? SupportUserId { get; set; }
        public long? StatusTypeId { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Token { get; set; }
        public bool IsDeleted { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string Fullname { get; set; }
        public long? SupportTypeId { get; set; }

        public virtual SupportUser SupportUser { get; set; }
    }
}
