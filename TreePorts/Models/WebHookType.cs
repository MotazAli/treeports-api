using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class WebHookType
    {
        public WebHookType()
        {
            WebHooks = new HashSet<WebHook>();
        }

        public long Id { get; set; }
        public string HookType { get; set; }
        public long? ModifiedBy { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ExpireationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual ICollection<WebHook> WebHooks { get; set; }
    }
}
