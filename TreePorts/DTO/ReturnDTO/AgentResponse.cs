using TreePorts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreePorts.DTO.ReturnDTO
{
	public class AgentResponse
	{
		public AgentResponse()
		{
            Orders = new HashSet<OrderResponse>();
		}
        public long Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public long? CountryId { get; set; }
        public long? CityId { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public long? AgentTypeId { get; set; }
        public bool? IsBranch { get; set; }
        public string LocationLat { get; set; }
        public string LocationLong { get; set; }
        public bool IsDeleted { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public long? StatusTypeId { get; set; }
        public string Image { get; set; }
        public string CommercialRegistrationNumber { get; set; }
        public string Website { get; set; }
        public string ImageUrl { get; set; }
        public string CountryName { get; set; }
        public string CityName { get; set; }
        
        public string CountryArabicName { get; set; }
        public string CityArabicName { get; set; }

		public ICollection<OrderResponse> Orders { get; set; }
	}


   
}
