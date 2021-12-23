using TreePorts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreePorts.DTO.ReturnDTO
{
	public class OrderResponse
	{
		public OrderResponse()
		{
			OrderItems = new HashSet<OrderItemResponse>();
            //OrderAssignments = new HashSet<OrderAssignReponse>();
            UserAcceptedRequests = new HashSet<UserAcceptedResponse>();
        }
		public long Id { get; set; }
        public long? AgentId { get; set; }
        public long? ProductTypeId { get; set; }
        public string ProductOtherType { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public long? PaymentTypeId { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public string PickupLocationLat { get; set; }
        public string PickupLocationLong { get; set; }
        public string DropLocationLat { get; set; }
        public string DropLocationLong { get; set; }
        public long? CurrentStatus { get; set; }
        public bool IsDeleted { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string CustomerAddress { get; set; }
        public string AgnetName { get; set; }
		public string QRCodeUrl { get; set; }
        public byte[] Code { get; set; }
        public string PaymentType { get; set; }
        public string PaymentArabicType { get; set; }
        public string ProductType { get; set; }
        public string ProductArabicType { get; set; }

        public decimal? OrderDeliveryPaymentAmount { get; set; }
       

        public virtual ICollection<OrderItemResponse> OrderItems { get; set; }
       // public virtual ICollection<OrderAssignReponse> OrderAssignments { get; set; }
        public virtual ICollection<UserAcceptedResponse> UserAcceptedRequests { get; set; }


    }


    public class OrderDetails
    {
        public OrderDetails()
        {
            OrderItems = new HashSet<OrderItem>();
            OrderStatusHistories = new HashSet<OrderStatusHistory>();
        }
        public Order Order { get; set; }
        public OrderCurrentStatus OrderCurrentStatus { get; set; }
        public virtual ICollection<OrderStatusHistory> OrderStatusHistories { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public ProductType ProductType { get; set; }
        public PaymentType PaymentType { get; set; }
        //public List<OrderItem> OrdersItem { get; set; }
        
        //public List<OrderStatusHistory> OrderStatusHistories { get; set; }
        public User Captain { get; set; }
        public UserPayment DeliveryPayment { get; set; }
        public Qrcode QrCode { get; set; }
        public Agent Agent { get; set; }
        public Country Country { get; set; }
        public City City { get; set; }


        //public virtual ICollection<OrderItem> OrderItems { get; set; }
        //public OrderCurrentStatus OrderCurrentStatus { get; set; }
        //public virtual ICollection<OrderStatusHistory> OrderStatusHistories { get; set; }

    }


    public class OrderFilterResponse
    {
	    public long? OrderId { get; set; }
	    public long? OrderCurrentStatus { get; set; }
	    public string AgentName { get; set; }
	    public long? ProductTypeId { get; set; }
	    public long? PaymentTypeId { get; set; }
	    public string CustomerName { get; set; }
	    public string CustomerAddress { get; set; }
	    public string CustomerPhone { get; set; }
	    public decimal? DeliveryAmount { get; set; }
	    public string CaptainName { get; set; }
	    public DateTime? OrderCreationDate { get; set; }
	    public string DurationToCurrentStatus { get; set; }
	    public DateTime? OrderStatusHistoryCreationDate { get; set; }
	    public string DurationToStatusHistory { get; set; }
    }
    
    
    public class OrderFilter
    {
	    public long? OrderId { get; set; }
	    public long? AgentId { get; set; }
	    public long? ProductTypeId { get; set; }
	    public long? PaymentTypeId { get; set; }
	    public long? OrderCurrentStatusId { get; set; }
	    public long? OrderStatusHistoryStatusId { get; set; }
	    public DateTime? StartDate { get; set; }
	    public DateTime? EndDate { get; set; }
	    public int? Page { get; set; }
	    public int? NumberOfObjects { get; set; }
    }


}
