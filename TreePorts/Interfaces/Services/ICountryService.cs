namespace TreePorts.Interfaces.Services;
public interface ICountryService
{
    Task<IEnumerable<Country>> GetCountriesAsync();
    Task<IEnumerable<City>> GetCitiesByCountryIdAsync(long id);
    Task<object?> GetCountryByIdAsync(long id);
    Task<Country?> UpdateCountryAsync(int id, Country country);
    Task<bool> DeleteCountryAsync(long id);
    Task<CountryPrice> AddCountryPriceAsync(CountryPrice countryPrice);
    Task<CountryProductPrice> AddCountryProductPriceAsync(CountryProductPrice countryProductPrice);
    Task<bool> DeleteCountryPriceAsync(long id);
    Task<bool> DeleteCountryProductPriceAsync(long id);
    Task<IEnumerable<object>?> GetCountriesCitiesAsync();
    Task<City> AddCityAsync(City city);
    Task<City?> UpdateCityAsync(long id, City city);
    Task<bool> DeleteCityAsync(long id);
}
