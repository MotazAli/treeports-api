using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class AdminCurrentStatus
    {
        public long Id { get; set; }
        public string? AdminUserAccountId { get; set; }
        public long? StatusTypeId { get; set; }
        public bool? IsCurrent { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual AdminUserAccount? AdminUserAccount { get; set; }
        public virtual StatusType? StatusType { get; set; }
    }
}
