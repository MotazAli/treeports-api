using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class CaptainUserBox
    {
        public long Id { get; set; }
        public long? UserVehicleId { get; set; }
        public long? BoxTypeId { get; set; }
        public bool IsDeleted { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual BoxType BoxType { get; set; }
        public virtual CaptainUserVehicle UserVehicle { get; set; }
    }
}
