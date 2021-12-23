using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class SupportAssignment
    {
        public SupportAssignment()
        {
            SupportMessages = new HashSet<SupportMessage>();
        }

        public long Id { get; set; }
        public long? SupportId { get; set; }
        public long? SupportUserId { get; set; }
        public long? UserId { get; set; }
        public long? CurrentStatusId { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual Support Support { get; set; }
        public virtual SupportUser SupportUser { get; set; }
        public virtual CaptainUser User { get; set; }
        public virtual ICollection<SupportMessage> SupportMessages { get; set; }
    }
}
