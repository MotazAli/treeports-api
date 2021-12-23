using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class CaptainUserCurrentActivity
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public bool? IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual CaptainUser User { get; set; }
    }
}
