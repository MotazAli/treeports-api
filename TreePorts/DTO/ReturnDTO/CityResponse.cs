
namespace TreePorts.DTO.ReturnDTO
{
	public record class CityResponse
	{
        public long Id { get; set; }
        public long? CountryId { get; set; }
        public string? Name { get; set; }
        public string? ArabicName { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}
