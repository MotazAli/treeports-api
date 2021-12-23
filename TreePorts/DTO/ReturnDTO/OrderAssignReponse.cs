using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreePorts.DTO.ReturnDTO
{
	public class OrderAssignReponse
	{
        public long Id { get; set; }
        public long? OrderId { get; set; }
        public long? UserId { get; set; }
		public string User { get; set; }
		public string ToAgentKilometer { get; set; }
        public string ToAgentTime { get; set; }
        public string ToCustomerKilometer { get; set; }
        public string ToCustomerTime { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string Mobile { get; set; }
        public string NationalNumber { get; set; }
    }
}
