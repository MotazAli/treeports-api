﻿using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class Webhook
    {
        public long Id { get; set; }
        public string? Url { get; set; }
        public string? AgentId { get; set; }
        public DateTime? ExpireDate { get; set; }
        public long? WebhookTypeId { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual Agent? Agent { get; set; }
        public virtual WebhookType? WebhookType { get; set; }
    }
}
