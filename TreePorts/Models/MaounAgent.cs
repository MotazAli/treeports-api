using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class MaounAgent
    {
        public long Id { get; set; }
        public long? MaounBusinessId { get; set; }
        public long? SenderAgentId { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}
