using Microsoft.AspNetCore.Mvc;

namespace TreePorts.Controllers
{
    //[Authorize]
    //[Route("[controller]")]
    [Route("/Countries/")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        // GET: Countries
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(IEnumerable<Country>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                return Ok(await _countryService.GetCountriesAsync());

            }catch(Exception ex)
            {
                return NoContent();
            }
        }

        
        [HttpGet("{id}/Cities")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<City>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetCitiesByCountryId(long id)
        {
            try
            {
                return Ok(await _countryService.GetCitiesByCountryIdAsync(id));

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
                return Ok(await _countryService.GetCountryByIdAsync(id));
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
                return Ok(await _countryService.UpdateCountryAsync(id, country));
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
                return Ok(await _countryService.DeleteCountryAsync(id));
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
                  return Ok(await _countryService.AddCountryPriceAsync(countryPrice));
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
                return Ok(await _countryService.AddCountryProductPriceAsync(countryProductPrice));
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
                return Ok(await _countryService.DeleteCountryPriceAsync(id));
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
                return Ok(await _countryService.DeleteCountryProductPriceAsync(id));
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
                return Ok( await _countryService.GetCountriesCitiesAsync());
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
                return Ok(await _countryService.AddCityAsync(city));
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
                return Ok(await _countryService.UpdateCityAsync(id,city));
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
                return Ok(await _countryService.DeleteCityAsync(id));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


    }
}
