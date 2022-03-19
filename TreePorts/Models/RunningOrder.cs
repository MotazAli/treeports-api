using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class RunningOrder
    {
        public long Id { get; set; }
        public long? OrderId { get; set; }
        public string? CaptainUserAccountId { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual CaptainUserAccount? CaptainUserAccount { get; set; }
        public virtual Order? Order { get; set; }
    }
}
