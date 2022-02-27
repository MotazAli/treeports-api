using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class AgentMessageHub
    {
        public long Id { get; set; }
        public string? AgentId { get; set; }
        public string? ConnectionId { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}
