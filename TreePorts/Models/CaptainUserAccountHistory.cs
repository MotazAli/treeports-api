﻿using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class CaptainUserAccountHistory
    {
        public long Id { get; set; }
        public long? AccountId { get; set; }
        public long? StatusTypeId { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual StatusType StatusType { get; set; }
    }
}
