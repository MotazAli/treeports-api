using TreePorts.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TreePorts.Interfaces.Repositories
{
    public interface ICountryRepository
    {
        // Task<List<Country>> GetAll(CancellationToken cancellationToken);
        Task<IEnumerable<CountryResponse>> GetCountriesAsync(CancellationToken cancellationToken);
        Task<CountryResponse?> GetCountryByIdAsync(long id ,CancellationToken cancellationToken);
        Task<List<Country>> GetCountriesByAsync(Expression<Func<Country, bool>> predicate,CancellationToken cancellationToken);
        Task<Country> InsertCountryAsync(Country country,CancellationToken cancellationToken);

        Task<List<Country>> InsertCountriesAsync(List<Country> countries,CancellationToken cancellationToken);
        Task<Country?> UpdateCountryAsync(Country country,CancellationToken cancellationToken);

        Task<Country?> DeleteCountryAsync(long id,CancellationToken cancellationToken);

        Task<object?> GetCountryAllDataAsync(long id,CancellationToken cancellationToken);


        Task<List<City>> GetCitiesAsync(CancellationToken cancellationToken);
        Task<City?> GetCityByIdAsync(long id,CancellationToken cancellationToken);
        Task<List<City>> GetCitiesByAsync(Expression<Func<City, bool>> predicate,CancellationToken cancellationToken);
        Task<City> InsertCityAsync(City city,CancellationToken cancellationToken);

        Task<City?> UpdateCityAsync(City city,CancellationToken cancellationToken);

        Task<City?> DeleteCityAsync(long id,CancellationToken cancellationToken);

        Task<List<City>> InsertCitiesAsync(List<City> cities,CancellationToken cancellationToken);

        Task<IEnumerable<object>?> GetCountriesCitiesAsync(CancellationToken cancellationToken);


        //Task<List<CountryPrice>> GetAllCountriesPrices(,CancellationToken cancellationToken);
        //Task<CountryPrice> GetCountryPriceByID(long ID,CancellationToken cancellationToken);
        Task<object> GetCountriesPricesAsync(CancellationToken cancellationToken);
        Task<object?> GetCountryPriceByIdAsync(long id,CancellationToken cancellationToken);
        Task<List<CountryPrice>> GetCountriesPricesByAsync(Expression<Func<CountryPrice, bool>> predicate,CancellationToken cancellationToken);
        Task<CountryPrice> InsertCountryPriceAsync(CountryPrice countryPrice,CancellationToken cancellationToken);

        Task<List<CountryPrice>> InsertCountriesPricesAsync(List<CountryPrice> countriesPrices,CancellationToken cancellationToken);
        Task<CountryPrice?> DeleteCountryPriceAsync(long id,CancellationToken cancellationToken);
        
        
        Task<List<CountryOrderPrice>> GetCountriesOrderPricesAsync(CancellationToken cancellationToken);
        Task<CountryOrderPrice?> GetCountryOrderPriceByIdAsync(long id,CancellationToken cancellationToken);
        Task<List<CountryOrderPrice>> GetCountriesOrderPricesByAsync(Expression<Func<CountryOrderPrice, bool>> predicate,CancellationToken cancellationToken);
        Task<CountryOrderPrice> InsertCountryOrderPriceAsync(CountryOrderPrice countryOrderPrice,CancellationToken cancellationToken);
        Task<CountryOrderPrice?> DeleteCountryOrderPriceAsync(long id,CancellationToken cancellationToken);
        


        Task<List<CountryProductPrice>> GetCountriesProductsPricesAsync(CancellationToken cancellationToken);
        Task<CountryProductPrice?> GetCountryProductPriceByIdAsync(long id,CancellationToken cancellationToken);
        Task<List<CountryProductPrice>> GetCountriesProductsPricesByAsync(Expression<Func<CountryProductPrice, bool>> predicate,CancellationToken cancellationToken);
        Task<CountryProductPrice> InsertCountryProductPriceAsync(CountryProductPrice countryProductPrice,CancellationToken cancellationToken);

        Task<List<CountryProductPrice>> InsertCountriesProductsPricesAsync(List<CountryProductPrice> countriesProductsPrices,CancellationToken cancellationToken);
        Task<CountryProductPrice?> DeleteCountryProductPriceAsync(long id,CancellationToken cancellationToken);
        Task<IEnumerable<CountryProductPrice>?> DeleteCountryProductPriceByCountryIdAsync(long id,CancellationToken cancellationToken);
        Task<IEnumerable<CountryProductPrice>?> DeleteCountryProductPriceByProductIdAsync(long id,CancellationToken cancellationToken);

        /*Task<List<CountryProductPrice>> DeleteCountryProductPriceByCountryIdAsync(long id,CancellationToken cancellationToken);
        Task<List<CountryProductPrice>> DeleteCountryProductPriceByProductIdAsync(long id,CancellationToken cancellationToken);
*/



        //Task<List<CityPrice>> GetAllCitiesPrices(,CancellationToken cancellationToken);
        //Task<CityPrice> GetCityPriceById(long id,CancellationToken cancellationToken);

        Task<object> GetCitiesPricesAsync(CancellationToken cancellationToken);
        Task<object?> GetCityPriceByIdAsync(long id,CancellationToken cancellationToken);
        Task<List<CityPrice>> GetCitiesPricesByAsync(Expression<Func<CityPrice, bool>> predicate,CancellationToken cancellationToken);
        Task<CityPrice> InsertCityPriceAsync(CityPrice cityPrice,CancellationToken cancellationToken);
        Task<CityPrice?> DeleteCityPriceAsync(long id,CancellationToken cancellationToken);
        
        
        Task<List<CityOrderPrice>> GetCitiesOrderPricesAsync(CancellationToken cancellationToken);
        Task<CityOrderPrice?> GetCityOrderPriceByIdAsync(long id,CancellationToken cancellationToken);
        Task<List<CityOrderPrice>> GetCitiesOrderPricesByAsync(Expression<Func<CityOrderPrice, bool>> predicate,CancellationToken cancellationToken);
        Task<CityOrderPrice> InsertCityOrderPriceAsync(CityOrderPrice cityOrderPrice,CancellationToken cancellationToken);
        Task<CityOrderPrice?> DeleteCityOrderPriceAsync(long id,CancellationToken cancellationToken);

        
        
        
        
        


    }
}
