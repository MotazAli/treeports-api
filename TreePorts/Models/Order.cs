using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class Order
    {
        public long Id { get; set; }
        public string? AgentId { get; set; }
        public long? ProductTypeId { get; set; }
        public string? ProductOtherTypeInfo { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public long? PaymentTypeId { get; set; }
        public string? Description { get; set; }
        public string? MoreDetails { get; set; }
        public string? PickupLocationLat { get; set; }
        public string? PickupLocationLong { get; set; }
        public string? DropLocationLat { get; set; }
        public string? DropLocationLong { get; set; }
        public string? CustomerAddress { get; set; }
        public long? CurrentOrderStatusTypeId { get; set; }
        public bool IsDeleted { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}
