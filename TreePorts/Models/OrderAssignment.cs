using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class OrderAssignment
    {
        public OrderAssignment()
        {
            OrderEndLocations = new HashSet<OrderEndLocation>();
            OrderStartLocations = new HashSet<OrderStartLocation>();
        }

        public long Id { get; set; }
        public long? OrderId { get; set; }
        public long? UserId { get; set; }
        public string ToAgentKilometer { get; set; }
        public string ToAgentTime { get; set; }
        public string ToCustomerKilometer { get; set; }
        public string ToCustomerTime { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual Order Order { get; set; }
        public virtual CaptainUser User { get; set; }
        public virtual ICollection<OrderEndLocation> OrderEndLocations { get; set; }
        public virtual ICollection<OrderStartLocation> OrderStartLocations { get; set; }
    }
}
