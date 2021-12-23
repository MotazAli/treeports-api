using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class AgentLocationHistory
    {
        public long Id { get; set; }
        public long? AgentId { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual Agent Agent { get; set; }
    }
}
