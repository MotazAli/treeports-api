﻿using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class UserType
    {
        public UserType()
        {
            TicketMessages = new HashSet<TicketMessage>();
        }

        public long Id { get; set; }
        public string? Type { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual ICollection<TicketMessage> TicketMessages { get; set; }
    }
}
