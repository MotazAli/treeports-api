using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreePorts.Infrastructure;
using TreePorts.Models;

namespace TreePorts.Controllers
{
    //[Authorize]
    //[Route("[controller]")]
    [Route("/Countries/")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public CountryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Countries
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(IEnumerable<Country>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                return Ok(await _unitOfWork.CountryRepository.GetCountriesAsync());

            }catch(Exception ex)
            {
                return NoContent();
            }
        }

        // GET: Country
        //[AllowAnonymous]
        // [HttpGet]
        // public async Task<List<Country>> Get()
        // {
        //     return await _unitOfWork.CountryRepository.GetAll();
        // }

        // GET: Countries/{id}/Cities/
        //[AllowAnonymous]
        //[HttpGet("GetCitiesByCountry/{id}")]
        [HttpGet("{id}/Cities")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<City>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetCitiesByCountryId(long id)
        {
            try
            {
                return Ok(await _unitOfWork.CountryRepository.GetCitiesByAsync(c => c.CountryId == id));

            }
            catch (Exception ex)
            {
                return NoContent();
            }
        }

        // GET: Countries/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetCountryById(long id)
        {
            try
            {
                var result = await _unitOfWork.CountryRepository.GetCountryAllDataAsync(id);
                return Ok(result);
            }
            catch (Exception e) {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        // PUT: Countries/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Country))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutCountry(int id, [FromBody] Country country)
        {
            try
            {
                var countryupdatedResult = await _unitOfWork.CountryRepository.UpdateCountryAsync(country);
                var result = await _unitOfWork.Save();
                if (result <= 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                return Ok(countryupdatedResult);
            }
            catch (Exception e)
            {
                return NoContent(); // new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }

        // DELETE: Countries/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCountryById(long id)
        {
            try
            {
                var deletedResult = await _unitOfWork.CountryRepository.DeleteCountryAsync(id);
                if (deletedResult == null) return NoContent();

                var result = await _unitOfWork.Save();
                if (result <= 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                return Ok(true);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        // POST: Countries/prices
        [HttpPost("Prices")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CountryPrice))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddPrice([FromBody] CountryPrice countryPrice)
        {
            try {
                var countryPriceInsertedResult = await _unitOfWork.CountryRepository.InsertCountryPriceAsync(countryPrice);
                var result =  await _unitOfWork.Save();
                if (result <= 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                return Ok(countryPriceInsertedResult);
            } catch (Exception e) {
                return NoContent(); // new ObjectResult(e.Message) { StatusCode = 666 };
            }
            
        }

        // POST: Countries/ProductsPrices
        [HttpPost("ProductsPrices")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CountryProductPrice))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddProductPrice([FromBody] CountryProductPrice countryProductPrice)
        {
            try
            {
                var countryProductPriceInsertedResult = await _unitOfWork.CountryRepository.InsertCountryProductPriceAsync(countryProductPrice);
                var result = await _unitOfWork.Save();
                if (result <= 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                return Ok(countryProductPriceInsertedResult);
            }
            catch (Exception e)
            {
                return NoContent(); // new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }

        

        // DELETE: countries/{id}
        [HttpDelete("Prices/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCountryPriceById(long id)
        {
            try
            {
                var deletedResult = await _unitOfWork.CountryRepository.DeleteCountryPriceAsync(id);
                var result = await _unitOfWork.Save();
                if (result <= 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };
                
                return Ok(true);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }

        // DELETE: Countries/ProductsPrices
        [HttpDelete("ProductsPrices/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCountryProductPriceById(long id)
        {
            try
            {
                var deletedResult = await _unitOfWork.CountryRepository.DeleteCountryProductPriceAsync(id);
                var result = await _unitOfWork.Save();
                if (result <= 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                return Ok(true);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }



        // GET: Countries/Cities
        [HttpGet("Cities")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<object>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetCountriesCities()
        
        {
            try 
            {
                return Ok( await _unitOfWork.CountryRepository.GetCountriesCitiesAsync());
            }
            catch (Exception ex)
            {
                return NoContent();
            }
        }


        // POST: Countries/Cities
        [HttpPost("Cities")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(City))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddCity([FromBody] City city) {
            try
            {
                var insertedResult = await _unitOfWork.CountryRepository.InsertCityAsync(city);
                var result = await _unitOfWork.Save();
                if (result <= 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                return Ok(insertedResult);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }

        // PUT: Countries/Cities/{id}
        [HttpPut("Cities/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(City))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateCityById(long id,[FromBody] City city)
        {
            try
            {
                var updatedResult = await _unitOfWork.CountryRepository.UpdateCityAsync(city);
                var result = await _unitOfWork.Save();
                if (result <= 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                return Ok(updatedResult);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        // DELETE: Countries/Cities/{id}
        [HttpDelete("Cities/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCityById(long id)
        {
            try
            {
                var deletedResult = await _unitOfWork.CountryRepository.DeleteCityAsync(id);
                if (deletedResult == null) return NoContent();

                var result = await _unitOfWork.Save();
                if (result <= 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                return Ok(true);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


    }
}
