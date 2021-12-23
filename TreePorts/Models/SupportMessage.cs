using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class SupportMessage
    {
        public long Id { get; set; }
        public long? SupportAssignId { get; set; }
        public bool? IsUser { get; set; }
        public string Message { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual SupportAssignment SupportAssign { get; set; }
    }
}
