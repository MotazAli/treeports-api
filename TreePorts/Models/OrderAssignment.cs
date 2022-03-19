using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class OrderAssignment
    {
        public OrderAssignment()
        {
            OrderEndLocations = new HashSet<OrderEndLocation>();
            OrderInvoices = new HashSet<OrderInvoice>();
            OrderStartLocations = new HashSet<OrderStartLocation>();
            PaidOrders = new HashSet<PaidOrder>();
        }

        public long Id { get; set; }
        public long? OrderId { get; set; }
        public string? CaptainUserAccountId { get; set; }
        public string? ToAgentKilometer { get; set; }
        public string? ToAgentTime { get; set; }
        public string? ToCustomerKilometer { get; set; }
        public string? ToCustomerTime { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual CaptainUserAccount? CaptainUserAccount { get; set; }
        public virtual Order? Order { get; set; }
        public virtual ICollection<OrderEndLocation> OrderEndLocations { get; set; }
        public virtual ICollection<OrderInvoice> OrderInvoices { get; set; }
        public virtual ICollection<OrderStartLocation> OrderStartLocations { get; set; }
        public virtual ICollection<PaidOrder> PaidOrders { get; set; }
    }
}
