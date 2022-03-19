using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreePorts.DTO.ReturnDTO
{
	public record class CountryResponse
	{
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Iso { get; set; }
        public long? Code { get; set; }
        public string? ArabicName { get; set; }
        public string? CurrencyName { get; set; }
        public string? CurrencyArabicName { get; set; }
        public string? CurrencyIso { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public List<CityResponse>? Cities { get; set; }

    }
}
