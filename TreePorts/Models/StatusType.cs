﻿using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class StatusType
    {
        public StatusType()
        {
            CaptainUserAccounts = new HashSet<CaptainUserAccount>();
        }

        public long Id { get; set; }
        public string? Type { get; set; }
        public string? ArabicType { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual ICollection<CaptainUserAccount> CaptainUserAccounts { get; set; }
    }
}
