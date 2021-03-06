using Microsoft.AspNetCore.Mvc;
using TreePorts.DTO;
using TreePorts.DTO.Records;

namespace TreePorts.Controllers
{
    //[Authorize]
    //[Route("[controller]")]
    [Route("/Systems/")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        private readonly ISystemService _systemService;

        public SystemController(ISystemService systemService)
        {
            _systemService = systemService;
        }


        // GET: System
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SystemSetting))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetSystemSetting(CancellationToken cancellationToken)
        {
            try 
            {
                return  Ok( await _systemService.GetSystemSettingAsync(cancellationToken));
            } 
            catch (Exception e) 
            {
                return NoContent();
            }
            
        }


        // GET: System/Vehicles
        [HttpGet("Vehicles")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Vehicle>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetVehicles(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _systemService.GetVehiclesAsync(cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();
            }
           
        }

        // GET: System/BoxTypes
        [HttpGet("BoxTypes")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BoxType>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetBoxTypes(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _systemService.GetBoxTypesAsync(cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();
            }
        }


        // GET: System/Shifts
        //[HttpGet("GetAllShifts")]
        [HttpGet("Shifts")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Shift>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetShifts(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _systemService.GetShiftsAsync(cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();
            }
        }



        //[HttpPost("AndroidVersions")]
        [HttpGet("Androids/Paging")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Shift>))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAndroidVersionsPaging([FromQuery] FilterParameters parameters, CancellationToken cancellationToken) // [FromQuery(Name ="page")] int? page , [FromQuery(Name = "objects")] int? objects)//[FromBody] Pagination pagination)
        {
            try
            {
                return Ok(await _systemService.GetAndroidVersionsPagingAsync(parameters,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }

        // GET: System/AndroidVersions
        //[HttpGet("CurrentAndroidVersion")]
        [HttpGet("Androids/Current")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AndroidVersion))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetCurrentAndroidVersion(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _systemService.GetCurrentAndroidVersionAsync(cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }

        // GET: System/AndroidVersions
        //[HttpGet("AndroidVersions")]
        [HttpGet("Androids")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AndroidVersion>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAndroidVersions(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _systemService.GetAndroidVersionsAsync(cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        // GET: System/AndroidVersions
        //[HttpGet("AndroidVersions/{id}")]
        [HttpGet("Androids/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AndroidVersion))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AndroidVersions(long id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _systemService.GetAndroidVersionByIdAsync(id,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }




        // POST: System/AndroidVersions
        //[HttpPost("AddAndroidVersions")]
        [HttpPost("Androids")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AndroidVersion))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddAndroidVersion([FromBody] AndroidVersion androidVersion, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _systemService.AddAndroidVersionAsync(androidVersion,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        // POST: System/AndroidVersions
        //[HttpPut("AndroidVersions")]
        [HttpPut("Androids/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AndroidVersion))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateAndroidVersions(long id,[FromBody] AndroidVersion androidVersion, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _systemService.UpdateAndroidVersionsAsync(id,androidVersion,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }



        // POST: Agent
        //[AllowAnonymous]
        //[HttpPost("UploadAndroidFile")]
        [HttpPost("Androids/UploadFile")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UploadAndroidFile(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _systemService.UploadAndroidFileAsync(HttpContext,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }


        }



        // POST: Agent
        //[AllowAnonymous]
        //[HttpPost("UploadPromotionFile")]
        [HttpPost("Promotions/UploadFile")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UploadPromotionFile(CancellationToken cancellationToken)
        {
            try
            {
                
                return Ok(await _systemService.UploadPromotionFileAsync(HttpContext,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }


        }


       


        // GET: System/Countries/Prices
        [HttpGet("Countries/Prices")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetCountriesPrices(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _systemService.GetCountriesPricesAsync(cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }
        
        // GET: System/CountriesPrices/{id}
        [HttpGet("Countries/Prices/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetCountryPriceById(long id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _systemService.GetCountryPriceByIdAsync(id,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent(); // new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }
        
        
        // GET: System/CountriesPrices/Country/{id}
        [HttpGet("Countries/{id}/Prices")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CountryPrice>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetCountryPricesByCountryId(long id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _systemService.GetCountryPricesByCountryIdAsync(id,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent(); // new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }
        
        
        // POST: System
        [HttpPost("Countries/Prices")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CountryPrice))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddCountryPrice([FromBody] CountryPrice countryPrice, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _systemService.AddCountryPriceAsync(HttpContext,countryPrice,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }
        
        // GET: System/CountriesPrices/Country/{id}
        [HttpDelete("Countries/Prices/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCountriesPricesById(long id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _systemService.DeleteCountriesPricesAsync(id,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }



        // POST: System/GetShiftsByDate
        //[HttpPost("GetShiftsByDate")]
        [HttpPost("Shifts/Dates")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Shift>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetShiftsByDate([FromBody] Shift shift, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _systemService.GetShiftsByShiftDateAsync(shift,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }

        // GET: api/System/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SystemSetting))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetSystemSettingById(long id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _systemService.GetSystemSettingByIdAsync(id,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }

        // POST: System
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SystemSetting))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddSystemSetting([FromBody] SystemSetting setting, CancellationToken cancellationToken)
        {
            try {
                return Ok(await _systemService.AddSystemSettingAsync(setting,HttpContext,cancellationToken));
            } catch (Exception e) {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        

        
        
        
        // POST: CitiesPrices
        [HttpPost("Countries/Cities/Prices")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CityPrice))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddCityPrice([FromBody] CityPrice cityPrice, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _systemService.AddCityPriceAsync(cityPrice,HttpContext,cancellationToken));
            }
            catch (Exception e)
            {
                return new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }
        
        
        
        // GET: CitiesPrices
        [HttpGet("Countries/Cities/Prices")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetCitiesPrices(CancellationToken cancellationToken)
        {
            try
            {
                
                return Ok(await _systemService.GetCitiesPricesAsync(cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }
        
        
        // GET: CitiesPrices
        [HttpGet("Countries/Cities/Prices/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetCityPriceById(long id, CancellationToken cancellationToken)
        {
            try
            {
                
                return Ok(await _systemService.GetCityPriceByIdAsync(id,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }
        
        
        // GET: CitiesPrices
        [HttpGet("Countries/Cities/{id}/Prices")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CityPrice>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetCitiesPricesByCityId(long id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _systemService.GetCitiesPricesByCityIdAsync(id,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }
        
        // GET: CitiesPrices
        [HttpDelete("Countries/Cities/Prices/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCityPriceById(long id, CancellationToken cancellationToken)
        {
            try
            {
                
                return Ok(await _systemService.DeleteCityPriceByIdAsync(id,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }

        /*// POST: System/
        //[HttpGet("RejectPerTypes")]
        [HttpGet("Reject/Types")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RejectPerType>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetRejectPerTypes()
        {
            try
            {
                return Ok(await _systemService.GetRejectPerTypesAsync(cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }*/


        // POST: System/
        //[HttpGet("IgnorePerTypes")]
        [HttpGet("Ignore/Types")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<IgnorPerType>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> IgnorePerTypes(CancellationToken cancellationToken)
        {
            try
            {
                 return Ok(await _systemService.IgnorePerTypesAsync(cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        // POST: System/
        //[HttpGet("PenaltyPerTypes")]
        [HttpGet("Penalties/Types")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PenaltyPerType>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PenaltyPerTypes(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _systemService.PenaltyPerTypesAsync(cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        [HttpGet("Promotions")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Promotion>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetPromotions(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _systemService.GetPromotionsAsync(cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        [HttpGet("Promotions/Current")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Promotion))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetCurrentPromotion(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _systemService.GetCurrentPromotionAsync(cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        [HttpGet("Promotions/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Promotion))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Promotions(long id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _systemService.GetPromotionByIdAsync(id,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }



        [HttpGet("Promotions/Paging")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Promotion>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetPromotionsPaging([FromQuery] FilterParameters parameters, CancellationToken cancellationToken)//[FromQuery(Name ="page")] int? page, [FromQuery(Name = "objects")] int? objects)//[FromBody] Pagination pagination)
        {
            try
            {

                return Ok(await _systemService.GetPromotionsPaginationAsync(parameters,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }




        [HttpPost("Promotions")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Promotion))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddPromotion([FromBody] Promotion promotion, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _systemService.AddPromotionAsync(promotion,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        [HttpPut("Promotions/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Promotion))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdatePromotion(long id , [FromBody] Promotion promotion, CancellationToken cancellationToken)
        {
            try
            {

                return Ok(await _systemService.UpdatePromotionAsync(id,promotion,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        [HttpDelete("Promotions/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeletePromotionById(long id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _systemService.DeletePromotionById(id,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }

        //[HttpGet("PublishPromotion/{id}")]
        [HttpGet("Promotions/{id}/Publish")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PublishPromotionById(long id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _systemService.PublishPromotionByIdAsync(id,cancellationToken));

               
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }




        [HttpGet("Promotions/Types")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PromotionType>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PromotionTypes(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _systemService.PromotionTypesAsync(cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        [HttpGet("Promotions/Types/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PromotionType))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetPromotionTypeById(long id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _systemService.GetPromotionTypeByIdAsync(id,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        [HttpPost("Promotions/Types")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PromotionType))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddPromotionType([FromBody] PromotionType promotionType, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _systemService.AddPromotionTypeAsync(promotionType,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        [HttpPut("Promotions/Types/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PromotionType))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdatePromotionType(long id,[FromBody] PromotionType promotionType, CancellationToken cancellationToken)
        {
            try
            {

                return Ok(await _systemService.UpdatePromotionTypeAsync(id,promotionType,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        [HttpDelete("Promotions/Types/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeletePromotionType(long id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _systemService.DeletePromotionTypeAsync(id,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }

        [HttpPost("Upload")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Upload(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _systemService.UploadAsync(HttpContext,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent(); // new ObjectResult(e.Message) { StatusCode = 666 };
            }


        }




        // POST: System/
        //[HttpGet("GetCurrentUserIgnoredRequestsPenalty")]
        [HttpGet("Penalties/Captains/{id}/Ignore/Current")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CaptainUserIgnoredPenalty))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetCurrentUserIgnoredRequestsPenalty(string id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _systemService.GetCurrentUserIgnoredRequestsPenaltyByCaptainUserAccountIdAsync(id,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }

/*        // POST: System/
        //[HttpGet("GetCurrentUserRejectedRequestsPenalty")]
        [HttpGet("Penalties/Captains/{id}/Reject/Current")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CaptainUserRejectPenalty))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetCurrentUserRejectedRequestsPenalty(string? id)
        {

            try
            {
                return Ok(await _systemService.GetCurrentUserRejectedRequestsPenaltyByCaptainUserAccountIdAsync(id));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }*/

        // POST: System/
        //[HttpGet("GetUserIgnoredRequestsPenalties")]
        [HttpGet("Penalties/Captains/{Id}/Ignore")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CaptainUserIgnoredPenalty>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetUserIgnoredRequestsPenalties(string? Id, CancellationToken cancellationToken)
        {

            try
            {
                return Ok(await _systemService.GetUserIgnoredRequestsPenaltiesByCaptainUserAccountIdAsync(Id??"",cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }

        /*// POST: System/
        //[HttpGet("GetUserRejectedRequestsPenalties")]
        [HttpGet("Penalties/Captains/{Id}/Reject")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CaptainUserRejectPenalty>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetUserRejectedRequestsPenalties(string? Id)
        {
            try
            {
               return Ok(await _systemService.GetUserRejectedRequestsPenaltiesByCaptainUserAccountIdAsync(Id));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }*/

        //// POST: System/
        //[HttpGet("IgnorPerTypes")]
        //public async Task<IActionResult> IgnorPerTypes()
        //{
        //    try
        //    {
        //        var result = await _unitOfWork.SystemRepository.GetIgnorPerTypes();
        //        return Ok(result);
        //    }
        //    catch (Exception e)
        //    {
        //        return new ObjectResult(e.Message) { StatusCode = 666 };
        //    }
        //}

        //// POST: System/
        //[HttpGet("PenaltyPerTypes")]
        //public async Task<IActionResult> PenaltyPerTypes()
        //{
        //    try
        //    {
        //        var result = await _unitOfWork.SystemRepository.GetPenaltyPerTypes();
        //        return Ok(result);
        //    }
        //    catch (Exception e)
        //    {
        //        return new ObjectResult(e.Message) { StatusCode = 666 };
        //    }
        //}


        // POST: System/
        //[HttpGet("PeniltyStatusTypes")]
        [HttpGet("Penilties/Status/Types")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PenaltyStatusType>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetPeniltyStatusTypes(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _systemService.GetPeniltyStatusTypesAsync(cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        // PUT: api/System/5
        /*[HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }*/

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteSystemSetting(long id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _systemService.DeleteSystemSettingAsync(id,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        //[HttpPost("AddContactMessage")]
        [HttpPost("Contacts")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddContactMessage([FromBody] ContactMessage contactMessage, CancellationToken cancellationToken)
        {
            try
            {

                return Ok(await _systemService.AddContactMessageAsync(contactMessage,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }



        //[HttpPost("ChangeUserStatus")]
        [HttpPost("Users/Status")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ChangeUserStatus([FromBody] StatusAction statusAction, CancellationToken cancellationToken)
        {

            try
            {
                return Ok(await _systemService.ChangeUserStatusAsync(statusAction,HttpContext,cancellationToken));
                
            }
            catch (Exception e)
            {
                return NoContent(); // new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }


        //[HttpPost("AddUserMessageHubToken")]
        [HttpPost("Users/MessageHubToken")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddUserMessageHubToken([FromBody] UserHubToken userHubToken, CancellationToken cancellationToken)
        {

            try
            {
                return Ok(await _systemService.AddUserMessageHubTokenAsync(userHubToken,cancellationToken));
                
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }



        //[HttpPost("ForgotPassword")]
        [HttpPost("ChangePassword")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ForgotPassword([FromBody] ResetPassword resetPassword, CancellationToken cancellationToken)
        {
            try
            {

                return Ok(await _systemService.ForgotPasswordAsync(resetPassword, cancellationToken));
                

            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }


        }



        /* SendFirebaseNotification*/
        [HttpPost("SendFirebaseNotification")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> SendFirebaseNotification([FromBody] FBNotify fbNotify, CancellationToken cancellationToken)
        {
            
            try
            {
                return Ok(await _systemService.SendFirebaseNotificationAsync(fbNotify,cancellationToken));
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }
        /**/



    }
}
