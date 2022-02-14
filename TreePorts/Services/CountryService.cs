namespace TreePorts.Services;
public class CountryService : ICountryService
{
    private IUnitOfWork _unitOfWork;

    public CountryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    
    public async Task<IEnumerable<Country>> GetCountriesAsync()
    {
        try
        {
            return await _unitOfWork.CountryRepository.GetCountriesAsync();

        }
        catch (Exception ex)
        {
            return new List<Country>();
        }
    }

   
    
    public async Task<IEnumerable<City>> GetCitiesByCountryIdAsync(long id)
    {
        try
        {
            return await _unitOfWork.CountryRepository.GetCitiesByAsync(c => c.CountryId == id);

        }
        catch (Exception ex)
        {
            return new List<City>();
        }
    }

    
    public async Task<object> GetCountryByIdAsync(long id)
    {
        try
        {
            return await _unitOfWork.CountryRepository.GetCountryAllDataAsync(id);
        }
        catch (Exception e)
        {
            return new { };// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }


    
    public async Task<Country> UpdateCountryAsync(int id, Country country)
    {
        
            var countryupdatedResult = await _unitOfWork.CountryRepository.UpdateCountryAsync(country);
            var result = await _unitOfWork.Save();
            if (result <= 0) throw new Exception("Service Unavailable");

            return countryupdatedResult;
    }

    
    public async Task<bool> DeleteCountryAsync(long id)
    {
       
            var deletedResult = await _unitOfWork.CountryRepository.DeleteCountryAsync(id);
            if (deletedResult == null) throw new Exception("NoContent");

            var result = await _unitOfWork.Save();
            if (result <= 0) throw new Exception("Service Unavailable");

            return true;
        
    }


    
    public async Task<CountryPrice> AddCountryPriceAsync( CountryPrice countryPrice)
    {
       
            var countryPriceInsertedResult = await _unitOfWork.CountryRepository.InsertCountryPriceAsync(countryPrice);
            var result = await _unitOfWork.Save();
            if (result <= 0) throw new Exception("Service Unavailable");

            return countryPriceInsertedResult;
        

    }

    
    public async Task<CountryProductPrice> AddCountryProductPriceAsync(CountryProductPrice countryProductPrice)
    {
        
            var countryProductPriceInsertedResult = await _unitOfWork.CountryRepository.InsertCountryProductPriceAsync(countryProductPrice);
            var result = await _unitOfWork.Save();
            if (result <= 0) throw new Exception("Service Unavailable");

            return countryProductPriceInsertedResult;
        
    }



    
    public async Task<bool> DeleteCountryPriceAsync(long id)
    {
        
            var deletedResult = await _unitOfWork.CountryRepository.DeleteCountryPriceAsync(id);
            var result = await _unitOfWork.Save();
            if (result <= 0) throw new Exception("Service Unavailable");

            return true;
    }

    
    public async Task<bool> DeleteCountryProductPriceAsync(long id)
    {
       
            var deletedResult = await _unitOfWork.CountryRepository.DeleteCountryProductPriceAsync(id);
            var result = await _unitOfWork.Save();
            if (result <= 0) throw new Exception("Service Unavailable");

            return true;
    }



    public async Task<IEnumerable<object>> GetCountriesCitiesAsync()
    {
        try
        {
            return await _unitOfWork.CountryRepository.GetCountriesCitiesAsync();
        }
        catch (Exception ex)
        {
            return new List<object>();
        }
    }


    
    public async Task<City> AddCityAsync( City city)
    {
       
            var insertedResult = await _unitOfWork.CountryRepository.InsertCityAsync(city);
            var result = await _unitOfWork.Save();
            if (result <= 0) throw new Exception("Service Unavailable");

            return insertedResult;
        
    }

    
    public async Task<City> UpdateCityAsync(long id, City city)
    {
       
            var updatedResult = await _unitOfWork.CountryRepository.UpdateCityAsync(city);
            var result = await _unitOfWork.Save();
            if (result <= 0) throw new Exception("Service Unavailable");

            return updatedResult;
        
    }


    
    public async Task<bool> DeleteCityAsync(long id)
    {
        
            var deletedResult = await _unitOfWork.CountryRepository.DeleteCityAsync(id);
            if (deletedResult == null) throw new Exception("NoContent");

            var result = await _unitOfWork.Save();
            if (result <= 0) throw new Exception("Service Unavailable");

            return true;
        
    }

}
