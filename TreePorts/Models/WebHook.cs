using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class WebHook
    {
        public long Id { get; set; }
        public string Url { get; set; }
        public long? AgentId { get; set; }
        public long? ModifiedBy { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ExpireationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public long? TypeId { get; set; }

        public virtual Agent Agent { get; set; }
        public virtual WebHookType Type { get; set; }
    }
}
