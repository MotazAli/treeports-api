using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class CaptainUserBox
    {
        public long Id { get; set; }
        public long? CaptainUserVehicleId { get; set; }
        public long? BoxTypeId { get; set; }
        public bool IsDeleted { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual BoxType? BoxType { get; set; }
        public virtual CaptainUserVehicle? CaptainUserVehicle { get; set; }
    }
}
