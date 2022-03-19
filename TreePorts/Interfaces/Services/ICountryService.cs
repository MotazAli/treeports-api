namespace TreePorts.Interfaces.Services;
public interface ICountryService
{
    Task<IEnumerable<CountryResponse>> GetCountriesAsync(CancellationToken cancellationToken);
    Task<IEnumerable<City>> GetCitiesByCountryIdAsync(long id,CancellationToken cancellationToken);
    Task<CountryResponse?> GetCountryByIdAsync(long id,CancellationToken cancellationToken);
    Task<Country?> UpdateCountryAsync(int id, Country country,CancellationToken cancellationToken);
    Task<bool> DeleteCountryAsync(long id,CancellationToken cancellationToken);
    Task<CountryPrice> AddCountryPriceAsync(CountryPrice countryPrice,CancellationToken cancellationToken);
    Task<CountryProductPrice> AddCountryProductPriceAsync(CountryProductPrice countryProductPrice,CancellationToken cancellationToken);
    Task<bool> DeleteCountryPriceAsync(long id,CancellationToken cancellationToken);
    Task<bool> DeleteCountryProductPriceAsync(long id,CancellationToken cancellationToken);
    Task<IEnumerable<object>?> GetCountriesCitiesAsync(CancellationToken cancellationToken);
    Task<City> AddCityAsync(City city,CancellationToken cancellationToken);
    Task<City?> UpdateCityAsync(long id, City city,CancellationToken cancellationToken);
    Task<bool> DeleteCityAsync(long id,CancellationToken cancellationToken);
}
