using TreePorts.Utilities;

namespace TreePorts.Services;
public class CountryService : ICountryService
{
    private IUnitOfWork _unitOfWork;

    public CountryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    
    public async Task<IEnumerable<CountryResponse>> GetCountriesAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.CountryRepository.GetCountriesAsync(cancellationToken);

        }
        catch (Exception ex)
        {
            return Enumerable.Empty<CountryResponse>();
        }
    }

   
    
    public async Task<IEnumerable<City>> GetCitiesByCountryIdAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.CountryRepository.GetCitiesByAsync(c => c.CountryId == id, cancellationToken);

        }
        catch (Exception ex)
        {
            return new List<City>();
        }
    }

    
    public async Task<CountryResponse?> GetCountryByIdAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.CountryRepository.GetCountryByIdAsync(id, cancellationToken);
        }
        catch (Exception e)
        {
            return new CountryResponse();// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }


    public async Task<object?> GetHeavyCountryByIdAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.CountryRepository.GetCountryAllDataAsync(id,cancellationToken);
        }
        catch (Exception e)
        {
            return new { };// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }



    public async Task<Country?> UpdateCountryAsync(int id, Country country, CancellationToken cancellationToken)
    {
        
            var countryupdatedResult = await _unitOfWork.CountryRepository.UpdateCountryAsync(country,cancellationToken);
            var result = await _unitOfWork.Save(cancellationToken);
            if (result <= 0) throw new ServiceUnavailableException("Service Unavailable");

            return countryupdatedResult;
    }

    
    public async Task<bool> DeleteCountryAsync(long id, CancellationToken cancellationToken)
    {
       
            var deletedResult = await _unitOfWork.CountryRepository.DeleteCountryAsync(id,cancellationToken);
            //if (deletedResult == null) throw new Exception("NoContent");

            var result = await _unitOfWork.Save(cancellationToken);
            if (result <= 0) throw new ServiceUnavailableException("Service Unavailable");

            return true;
        
    }


    
    public async Task<CountryPrice> AddCountryPriceAsync( CountryPrice countryPrice, CancellationToken cancellationToken)
    {
       
            var countryPriceInsertedResult = await _unitOfWork.CountryRepository.InsertCountryPriceAsync(countryPrice,cancellationToken);
            var result = await _unitOfWork.Save(cancellationToken);
            if (result <= 0) throw new ServiceUnavailableException("Service Unavailable");

            return countryPriceInsertedResult;
        

    }

    
    public async Task<CountryProductPrice> AddCountryProductPriceAsync(CountryProductPrice countryProductPrice, CancellationToken cancellationToken)
    {
        
            var countryProductPriceInsertedResult = await _unitOfWork.CountryRepository.InsertCountryProductPriceAsync(countryProductPrice,cancellationToken);
            var result = await _unitOfWork.Save(cancellationToken);
            if (result <= 0) throw new ServiceUnavailableException("Service Unavailable");

            return countryProductPriceInsertedResult;
        
    }



    
    public async Task<bool> DeleteCountryPriceAsync(long id, CancellationToken cancellationToken)
    {
        
            var deletedResult = await _unitOfWork.CountryRepository.DeleteCountryPriceAsync(id,cancellationToken);
            var result = await _unitOfWork.Save(cancellationToken);
            if (result <= 0) throw new ServiceUnavailableException("Service Unavailable");

            return true;
    }

    
    public async Task<bool> DeleteCountryProductPriceAsync(long id, CancellationToken cancellationToken)
    {
       
            var deletedResult = await _unitOfWork.CountryRepository.DeleteCountryProductPriceAsync(id,cancellationToken);
            var result = await _unitOfWork.Save(cancellationToken);
            if (result <= 0) throw new ServiceUnavailableException("Service Unavailable");

            return true;
    }



    public async Task<IEnumerable<object>?> GetCountriesCitiesAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.CountryRepository.GetCountriesCitiesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            return new List<object>();
        }
    }


    
    public async Task<City> AddCityAsync( City city, CancellationToken cancellationToken)
    {
       
            var insertedResult = await _unitOfWork.CountryRepository.InsertCityAsync(city,cancellationToken);
            var result = await _unitOfWork.Save(cancellationToken);
            if (result <= 0) throw new ServiceUnavailableException("Service Unavailable");

            return insertedResult;
        
    }

    
    public async Task<City?> UpdateCityAsync(long id, City city, CancellationToken cancellationToken)
    {
       
            var updatedResult = await _unitOfWork.CountryRepository.UpdateCityAsync(city,cancellationToken);
            var result = await _unitOfWork.Save(cancellationToken);
            if (result <= 0) throw new ServiceUnavailableException("Service Unavailable");

            return updatedResult;
        
    }


    
    public async Task<bool> DeleteCityAsync(long id, CancellationToken cancellationToken)
    {
        
            var deletedResult = await _unitOfWork.CountryRepository.DeleteCityAsync(id,cancellationToken);
            //if (deletedResult == null) throw new Exception("NoContent");

            var result = await _unitOfWork.Save(cancellationToken);
            if (result <= 0) throw new ServiceUnavailableException("Service Unavailable");

            return true;
        
    }

}
