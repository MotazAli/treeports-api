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
        // Task<List<Country>> GetAll();
        Task<IEnumerable<Country>> GetCountriesAsync();
        Task<Country> GetCountryByIdAsync(long id);
        Task<List<Country>> GetCountriesByAsync(Expression<Func<Country, bool>> predicate);
        Task<Country> InsertCountryAsync(Country country);

        Task<List<Country>> InsertCountriesAsync(List<Country> countries);
        Task<Country> UpdateCountryAsync(Country country);

        Task<Country> DeleteCountryAsync(long id);

        Task<object> GetCountryAllDataAsync(long id);


        Task<List<City>> GetCitiesAsync();
        Task<City> GetCityByIdAsync(long id);
        Task<List<City>> GetCitiesByAsync(Expression<Func<City, bool>> predicate);
        Task<City> InsertCityAsync(City city);

        Task<City> UpdateCityAsync(City city);

        Task<City> DeleteCityAsync(long id);

        Task<List<City>> InsertCitiesAsync(List<City> cities);

        Task<IEnumerable> GetCountriesCitiesAsync();


        //Task<List<CountryPrice>> GetAllCountriesPrices();
        //Task<CountryPrice> GetCountryPriceByID(long ID);
        Task<object> GetCountriesPricesAsync();
        Task<object> GetCountryPriceByIdAsync(long id);
        Task<List<CountryPrice>> GetCountriesPricesByAsync(Expression<Func<CountryPrice, bool>> predicate);
        Task<CountryPrice> InsertCountryPriceAsync(CountryPrice countryPrice);

        Task<List<CountryPrice>> InsertCountriesPricesAsync(List<CountryPrice> countriesPrices);
        Task<CountryPrice> DeleteCountryPriceAsync(long id);
        
        
        Task<List<CountryOrderPrice>> GetCountriesOrderPricesAsync();
        Task<CountryOrderPrice> GetCountryOrderPriceByIdAsync(long id);
        Task<List<CountryOrderPrice>> GetCountriesOrderPricesByAsync(Expression<Func<CountryOrderPrice, bool>> predicate);
        Task<CountryOrderPrice> InsertCountryOrderPriceAsync(CountryOrderPrice countryOrderPrice);
        Task<CountryOrderPrice> DeleteCountryOrderPriceAsync(long id);
        


        Task<List<CountryProductPrice>> GetCountriesProductsPricesAsync();
        Task<CountryProductPrice> GetCountryProductPriceByIdAsync(long id);
        Task<List<CountryProductPrice>> GetCountriesProductsPricesByAsync(Expression<Func<CountryProductPrice, bool>> predicate);
        Task<CountryProductPrice> InsertCountryProductPriceAsync(CountryProductPrice countryProductPrice);

        Task<List<CountryProductPrice>> InsertCountriesProductsPricesAsync(List<CountryProductPrice> countriesProductsPrices);
        Task<CountryProductPrice> DeleteCountryProductPriceAsync(long id);
        Task<IEnumerable<CountryProductPrice>> DeleteCountryProductPriceByCountryIdAsync(long id);
        Task<IEnumerable<CountryProductPrice>> DeleteCountryProductPriceByProductIdAsync(long id);

        /*Task<List<CountryProductPrice>> DeleteCountryProductPriceByCountryIdAsync(long id);
        Task<List<CountryProductPrice>> DeleteCountryProductPriceByProductIdAsync(long id);
*/



        //Task<List<CityPrice>> GetAllCitiesPrices();
        //Task<CityPrice> GetCityPriceById(long id);

        Task<object> GetCitiesPricesAsync();
        Task<object> GetCityPriceByIdAsync(long id);
        Task<List<CityPrice>> GetCitiesPricesByAsync(Expression<Func<CityPrice, bool>> predicate);
        Task<CityPrice> InsertCityPriceAsync(CityPrice cityPrice);
        Task<CityPrice> DeleteCityPriceAsync(long id);
        
        
        Task<List<CityOrderPrice>> GetCitiesOrderPricesAsync();
        Task<CityOrderPrice> GetCityOrderPriceByIdAsync(long id);
        Task<List<CityOrderPrice>> GetCitiesOrderPricesByAsync(Expression<Func<CityOrderPrice, bool>> predicate);
        Task<CityOrderPrice> InsertCityOrderPriceAsync(CityOrderPrice cityOrderPrice);
        Task<CityOrderPrice> DeleteCityOrderPriceAsync(long id);

        
        
        
        
        


    }
}
