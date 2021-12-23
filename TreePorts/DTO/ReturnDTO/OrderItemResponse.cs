using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreePorts.DTO.ReturnDTO
{
	public class OrderItemResponse
	{
		
        public long Id { get; set; }
        public long? OrderId { get; set; }
        public string Item { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

    }
}
