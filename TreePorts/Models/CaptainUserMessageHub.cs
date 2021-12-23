using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class CaptainUserMessageHub
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public string ConnectionId { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}
