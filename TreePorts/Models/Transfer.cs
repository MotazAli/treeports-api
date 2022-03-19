using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class Transfer
    {
        public long Id { get; set; }
        public long? BookkeepingId { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual Bookkeeping? Bookkeeping { get; set; }
    }
}
