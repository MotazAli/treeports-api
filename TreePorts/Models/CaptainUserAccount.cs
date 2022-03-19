using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class CaptainUserAccount
    {
        public CaptainUserAccount()
        {
            Bookkeepings = new HashSet<Bookkeeping>();
            CaptainUserAcceptedRequests = new HashSet<CaptainUserAcceptedRequest>();
            CaptainUserAccountHistories = new HashSet<CaptainUserAccountHistory>();
            CaptainUserActiveHistories = new HashSet<CaptainUserActiveHistory>();
            CaptainUserActivities = new HashSet<CaptainUserActivity>();
            CaptainUserBonus = new HashSet<CaptainUserBonus>();
            CaptainUserCurrentActivities = new HashSet<CaptainUserCurrentActivity>();
            CaptainUserCurrentLocations = new HashSet<CaptainUserCurrentLocation>();
            CaptainUserCurrentStatus = new HashSet<CaptainUserCurrentStatus>();
            CaptainUserIgnoredPenalties = new HashSet<CaptainUserIgnoredPenalty>();
            CaptainUserIgnoredRequests = new HashSet<CaptainUserIgnoredRequest>();
            CaptainUserInactiveHistories = new HashSet<CaptainUserInactiveHistory>();
            CaptainUserMessageHubs = new HashSet<CaptainUserMessageHub>();
            CaptainUserNewRequests = new HashSet<CaptainUserNewRequest>();
            CaptainUserPaymentHistories = new HashSet<CaptainUserPaymentHistory>();
            CaptainUserPayments = new HashSet<CaptainUserPayment>();
            CaptainUserPromotions = new HashSet<CaptainUserPromotion>();
            CaptainUserShifts = new HashSet<CaptainUserShift>();
            CaptainUserStatusHistories = new HashSet<CaptainUserStatusHistory>();
            CaptainUserVehicles = new HashSet<CaptainUserVehicle>();
            OrderAssignments = new HashSet<OrderAssignment>();
            OrderInvoices = new HashSet<OrderInvoice>();
            OrderQrcodes = new HashSet<OrderQrcode>();
            PaidOrders = new HashSet<PaidOrder>();
            RunningOrders = new HashSet<RunningOrder>();
            TicketAssignments = new HashSet<TicketAssignment>();
            Tickets = new HashSet<Ticket>();
        }

        public string Id { get; set; } = null!;
        public string? CaptainUserId { get; set; }
        public long? StatusTypeId { get; set; }
        public string? Mobile { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string? Token { get; set; }
        public string? Password { get; set; }
        public bool IsDeleted { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual CaptainUser? CaptainUser { get; set; }
        public virtual StatusType? StatusType { get; set; }
        public virtual ICollection<Bookkeeping> Bookkeepings { get; set; }
        public virtual ICollection<CaptainUserAcceptedRequest> CaptainUserAcceptedRequests { get; set; }
        public virtual ICollection<CaptainUserAccountHistory> CaptainUserAccountHistories { get; set; }
        public virtual ICollection<CaptainUserActiveHistory> CaptainUserActiveHistories { get; set; }
        public virtual ICollection<CaptainUserActivity> CaptainUserActivities { get; set; }
        public virtual ICollection<CaptainUserBonus> CaptainUserBonus { get; set; }
        public virtual ICollection<CaptainUserCurrentActivity> CaptainUserCurrentActivities { get; set; }
        public virtual ICollection<CaptainUserCurrentLocation> CaptainUserCurrentLocations { get; set; }
        public virtual ICollection<CaptainUserCurrentStatus> CaptainUserCurrentStatus { get; set; }
        public virtual ICollection<CaptainUserIgnoredPenalty> CaptainUserIgnoredPenalties { get; set; }
        public virtual ICollection<CaptainUserIgnoredRequest> CaptainUserIgnoredRequests { get; set; }
        public virtual ICollection<CaptainUserInactiveHistory> CaptainUserInactiveHistories { get; set; }
        public virtual ICollection<CaptainUserMessageHub> CaptainUserMessageHubs { get; set; }
        public virtual ICollection<CaptainUserNewRequest> CaptainUserNewRequests { get; set; }
        public virtual ICollection<CaptainUserPaymentHistory> CaptainUserPaymentHistories { get; set; }
        public virtual ICollection<CaptainUserPayment> CaptainUserPayments { get; set; }
        public virtual ICollection<CaptainUserPromotion> CaptainUserPromotions { get; set; }
        public virtual ICollection<CaptainUserShift> CaptainUserShifts { get; set; }
        public virtual ICollection<CaptainUserStatusHistory> CaptainUserStatusHistories { get; set; }
        public virtual ICollection<CaptainUserVehicle> CaptainUserVehicles { get; set; }
        public virtual ICollection<OrderAssignment> OrderAssignments { get; set; }
        public virtual ICollection<OrderInvoice> OrderInvoices { get; set; }
        public virtual ICollection<OrderQrcode> OrderQrcodes { get; set; }
        public virtual ICollection<PaidOrder> PaidOrders { get; set; }
        public virtual ICollection<RunningOrder> RunningOrders { get; set; }
        public virtual ICollection<TicketAssignment> TicketAssignments { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
