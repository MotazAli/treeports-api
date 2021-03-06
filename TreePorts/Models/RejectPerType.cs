using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class RejectPerType
    {
        public RejectPerType()
        {
            SystemSettings = new HashSet<SystemSetting>();
        }

        public long Id { get; set; }
        public string Type { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string ArabicType { get; set; }

        public virtual ICollection<SystemSetting> SystemSettings { get; set; }
    }
}
