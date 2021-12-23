using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class CaptainUserVehicle
    {
        public CaptainUserVehicle()
        {
            UserBoxes = new HashSet<CaptainUserBox>();
        }

        public long Id { get; set; }
        public long? UserId { get; set; }
        public long? VehicleId { get; set; }
        public string PlateNumber { get; set; }
        public string Model { get; set; }
        public string VehicleImageName { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string VehicleImageAndroidPath { get; set; }
        public string LicenseImageName { get; set; }
        public string LicenseImageAndroidPath { get; set; }
        public string LicenseNumber { get; set; }

        public virtual CaptainUser User { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual ICollection<CaptainUserBox> UserBoxes { get; set; }
    }
}
