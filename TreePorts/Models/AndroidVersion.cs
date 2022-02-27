using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class AndroidVersion
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Version { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public string? FileExtension { get; set; }
        public bool? IsCurrent { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}
