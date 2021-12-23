using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class Promotion
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Descriptions { get; set; }
        public string Image { get; set; }
        public long? TypeId { get; set; }
        public string Value { get; set; }
        public bool? IsDeleted { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}
