﻿using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class AdminType
    {
        public AdminType()
        {
            AdminUserAccounts = new HashSet<AdminUserAccount>();
        }

        public long Id { get; set; }
        public string Type { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual ICollection<AdminUserAccount> AdminUserAccounts { get; set; }
    }
}
