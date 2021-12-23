using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class Support
    {
        public Support()
        {
            SupportAssignments = new HashSet<SupportAssignment>();
        }

        public long Id { get; set; }
        public long? SupportTypeId { get; set; }
        public long? UserId { get; set; }
        public long? StatusTypeId { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string Description { get; set; }

        public virtual SupportType SupportType { get; set; }
        public virtual ICollection<SupportAssignment> SupportAssignments { get; set; }
    }
}
