using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreePorts.DTO.ReturnDTO
{
	public class WebHookTypeResponse
	{
        public long Id { get; set; }
        public string HookType { get; set; }
        public long? ModifiedBy { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ExpireationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

    }
}
