using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreePorts.DTO.ReturnDTO
{
	public class CityReponse
	{
		public long Id { get; set; }
		public long? CountryId { get; set; }
		public string Name { get; set; }
		public string ArabicName { get; set; }
	}
}
