﻿using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class OrderStatusHistory
    {
        public long Id { get; set; }
        public long? OrderId { get; set; }
        public long? OrderStatusTypeId { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}
