using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class CaptainUserVehicle
    {
        public long Id { get; set; }
        public string? CaptainUserAccountId { get; set; }
        public long? VehicleId { get; set; }
        public string? PlateNumber { get; set; }
        public string? Model { get; set; }
        public string? VehicleImage { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public string? LicenseImage { get; set; }
        public string? LicenseNumber { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}
