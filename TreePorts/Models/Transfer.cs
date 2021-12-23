using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class Transfer
    {
        public long Id { get; set; }
        public long? BookkeepingId { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}
