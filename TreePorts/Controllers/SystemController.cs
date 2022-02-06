using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using TreePorts.DTO;
using TreePorts.Hubs;
using TreePorts.Models;
using TreePorts.Utilities;

namespace TreePorts.Controllers
{
    //[Authorize]
    //[Route("[controller]")]
    [Route("/Systems/")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        //private IHubContext<MessageHub> _HubContext;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IMemoryCache _cache;

        public SystemController(IUnitOfWork unitOfWork, IMemoryCache cache, IWebHostEnvironment hostingEnvironment)//, IHubContext<MessageHub> hubcontext)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
            //_HubContext = hubcontext;
            _hostingEnvironment = hostingEnvironment;
        }


        // GET: System
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SystemSetting))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetSystemSetting()
        {
            try 
            {
                return  Ok( await _unitOfWork.SystemRepository.GetCurrentSystemSettingAsync());
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
        public async Task<IActionResult> GetVehicles()
        {
            try
            {
                return Ok(await _unitOfWork.SystemRepository.GetVehiclesAsync());
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
        public async Task<IActionResult> GetBoxTypes()
        {
            try
            {
                return Ok(await _unitOfWork.SystemRepository.GetBoxTypesAsync());
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
        public async Task<IActionResult> GetShifts()
        {
            try
            {
                return Ok(await _unitOfWork.SystemRepository.GetShiftsAsync());
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
        public async Task<IActionResult> GetAndroidVersionsPaging([FromQuery] FilterParameters parameters ) // [FromQuery(Name ="page")] int? page , [FromQuery(Name = "objects")] int? objects)//[FromBody] Pagination pagination)
        {
            try
            {

                /*if (page == null || objects == null) return NoContent();//


                var skip = ((int)objects * ((int)page - 1));
                var take = (int)objects;*/

                var skip = (parameters.NumberOfObjectsPerPage * (parameters.Page - 1));
                var take = parameters.NumberOfObjectsPerPage;
                var result = await _unitOfWork.SystemRepository.GetAndroidVersionsPaginationAsync(skip,take);
                return Ok(result);
                //var AgentIDs = users.Select(s => s.AgentId);
                //var newAgents = await _unitOfWork.AgentRepository.GetBy(a => AgentIDs.Contains(a.Id));

                //var versions = await _unitOfWork.SystemRepository.GetAndroidVersionsAsync();
                /*var total = versions.Count;
                var skip = (pagination.NumberOfObjectsPerPage * (pagination.Page - 1));
                var take = pagination.NumberOfObjectsPerPage;
                var result = versions.Skip(skip).Take(take).ToList();*/



                //var total =  Math.Ceiling(( ((decimal)users.Count) / ((decimal)pagination.NumberOfObjectsPerPage) ));
                //var skip = (pagination.NumberOfObjectsPerPage * (pagination.Page - 1));
                //var take = pagination.NumberOfObjectsPerPage;
                //var result = users.Skip(skip).Take(take).ToList();

                //return Ok(new { AndroidVersions = result, Total = total, Page = pagination.Page });
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
        public async Task<IActionResult> GetCurrentAndroidVersion()
        {
            try
            {
                var current = await _unitOfWork.SystemRepository.GetCurrentAndroidVersionAsync();
                //var current = result.FirstOrDefault();
                //if (current == null) return NotFound("No current android version available");

                return Ok(current);
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
        public async Task<IActionResult> GetAndroidVersions()
        {
            try
            {
                var result = await _unitOfWork.SystemRepository.GetAndroidVersionsAsync();
                return Ok(result);
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
        public async Task<IActionResult> AndroidVersions(long id)
        {
            try
            {
                var result = await _unitOfWork.SystemRepository.GetAndroidVersionByIdAsync(id);
                //if (result == null) return NotFound();

                return Ok(result);
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
        public async Task<IActionResult> AddAndroidVersion([FromBody] AndroidVersion androidVersion)
        {
            try
            {
                var insertResult = await _unitOfWork.SystemRepository.InsertAndroidVersionAsync(androidVersion);
                var result = await _unitOfWork.Save();
                if (result == 0)
                    return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                return Ok(insertResult);
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
        public async Task<IActionResult> UpdateAndroidVersions(long id,[FromBody] AndroidVersion androidVersion)
        {
            try
            {
                if (id <= 0 || androidVersion == null || androidVersion.Id <= 0) return NoContent();



                var updateResult = await _unitOfWork.SystemRepository.UpdateAndroidVersionAsync(androidVersion);
                var result = await _unitOfWork.Save();
                if (result == 0)
                    return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                return Ok(updateResult);
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
        public async Task<IActionResult> UploadAndroidFile()
        {
            try
            {
                //var description = "";
                //var AndroidVersionID = "";
                //var request = HttpContext.Request;
                //if(request.Form != null && request.Form["description"] != "")
                //    description = request.Form["description"];


                //if (request.Form != null && request.Form["AndroidVersionID"] != "" && request.Form["AndroidVersionID"].Count > 0)
                //    AndroidVersionID = request.Form["AgentID"];
                //else if (request.Form != null && request.Form.Files["AndroidVersionID"] != null)
                //{
                //    AndroidVersionID = request.Form.Files["AndroidVersionID"].FileName;

                //}



                var request = HttpContext.Request;

                var files = HttpContext.Request.Form.Files;
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {

                        var FolderPath = _hostingEnvironment.ContentRootPath + "/Assets/Apps/"+ file.Name;
                        //FolderPath = FolderPath.Replace('/', '\\');
                        if (!Directory.Exists(FolderPath))
                            Directory.CreateDirectory(FolderPath);

                        //var filePath = Path.Combine(uploads, file.FileName);
                        string filePath = _hostingEnvironment.ContentRootPath + "/Assets/Apps/"+ file.Name + "/" + file.FileName;
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                    }
                }


                return Ok(true);
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
        public async Task<IActionResult> UploadPromotionFile()
        {
            try
            {
                //var description = "";
                //var AndroidVersionID = "";
                //var request = HttpContext.Request;
                //if(request.Form != null && request.Form["description"] != "")
                //    description = request.Form["description"];


                //if (request.Form != null && request.Form["AndroidVersionID"] != "" && request.Form["AndroidVersionID"].Count > 0)
                //    AndroidVersionID = request.Form["AgentID"];
                //else if (request.Form != null && request.Form.Files["AndroidVersionID"] != null)
                //{
                //    AndroidVersionID = request.Form.Files["AndroidVersionID"].FileName;

                //}



                var request = HttpContext.Request;

                var files = HttpContext.Request.Form.Files;
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {

                        var FolderPath = _hostingEnvironment.ContentRootPath + "/Assets/Images/" + file.Name;
                        //FolderPath = FolderPath.Replace('/', '\\');
                        if (!Directory.Exists(FolderPath))
                            Directory.CreateDirectory(FolderPath);

                        //var filePath = Path.Combine(uploads, file.FileName);
                        string filePath = _hostingEnvironment.ContentRootPath + "/Assets/Images/" + file.Name + "/" + file.FileName;
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                    }
                }


                return Ok(true);
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
        public async Task<IActionResult> GetCountriesPrices()
        {
            try
            {
                var result = await _unitOfWork.CountryRepository.GetCountriesPricesAsync();

                return Ok(result);
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
        public async Task<IActionResult> GetCountryPriceById(long id)
        {
            try
            {
                var result = await _unitOfWork.CountryRepository.GetCountryPriceByIdAsync(id);

                return Ok(result);
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
        public async Task<IActionResult> GetCountryPricesByCountryId(long id)
        {
            try
            {
                var result = await _unitOfWork.CountryRepository.GetCountriesPricesByAsync(cp => cp.CountryId == id);

                return Ok(result);
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
        public async Task<IActionResult> AddCountryPrice([FromBody] CountryPrice countryPrice)
        {
            try
            {
                long modifierID = -1;
                string modifierType = "";
                Utility.getRequestUserIdFromToken(HttpContext, out modifierID, out modifierType);
                countryPrice.CreatedBy = modifierID;
                var insertResult = await _unitOfWork.CountryRepository.InsertCountryPriceAsync(countryPrice);
                var result = await _unitOfWork.Save();
                if (result == 0)
                    return new ObjectResult("Service Unavailable") { StatusCode = 503 };


                return Ok(insertResult);
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
        public async Task<IActionResult> DeleteCountriesPricesById(long id)
        {
            try
            {
                var result = await _unitOfWork.CountryRepository.DeleteCountryPriceAsync(id);
                if (result == null) return NoContent();


                return Ok(true);
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
        public async Task<IActionResult> GetShiftsByDate([FromBody] Shift shift)
        {
            try
            {
                var result = await _unitOfWork.SystemRepository.GetShiftsByShiftDateAsync(shift);
                return Ok(result);
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
        public async Task<IActionResult> GetSystemSettingById(long id)
        {
            try
            {
                var result = await _unitOfWork.SystemRepository.GetSystemSettingByIdAsync(id);
                return Ok(result);
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
        public async Task<IActionResult> AddSystemSetting([FromBody] SystemSetting setting)
        {
            try {
                long modifierID = -1;
                string modifierType = "";
                Utility.getRequestUserIdFromToken(HttpContext, out modifierID,out modifierType);

                setting.CreatedBy = modifierID;
                var insertResult = await _unitOfWork.SystemRepository.InsertSystemSettingAsync(setting);
                var result = await _unitOfWork.Save();
                if (result == 0)
                    return new ObjectResult("Service Unavailable") { StatusCode = 503 };


                return Ok(insertResult);
            } catch (Exception e) {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        

        
        
        
        // POST: CitiesPrices
        [HttpPost("Countries/Cities/Prices")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CityPrice))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddCityPrice([FromBody] CityPrice cityPrice)
        {
            try
            {
                long modifierID = -1;
                string modifierType = "";
                Utility.getRequestUserIdFromToken(HttpContext, out modifierID, out modifierType);

                cityPrice.CreatedBy = modifierID;
                var insertResult = await _unitOfWork.CountryRepository.InsertCityPriceAsync(cityPrice);
                var result = await _unitOfWork.Save();
                if (result <= 0)
                    return new ObjectResult("Service Unavailable") { StatusCode = 503 };


                return Ok(insertResult);
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
        public async Task<IActionResult> GetCitiesPrices()
        {
            try
            {
                
                var result = await _unitOfWork.CountryRepository.GetCitiesPricesAsync();
                return Ok(result);
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
        public async Task<IActionResult> GetCityPriceById(long id)
        {
            try
            {
                
                var result = await _unitOfWork.CountryRepository.GetCityPriceByIdAsync(id);
                return Ok(result);
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
        public async Task<IActionResult> GetCitiesPricesByCityId(long id)
        {
            try
            {
                var result = await _unitOfWork.CountryRepository.GetCitiesPricesByAsync(cp => cp.CityId == id && cp.IsDeleted == false);
                return Ok(result);
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
        public async Task<IActionResult> DeleteCityPriceById(long id)
        {
            try
            {
                
                var result = await _unitOfWork.CountryRepository.DeleteCityPriceAsync(id);
                if (result == null) return NoContent();

                return Ok(true);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }

        // POST: System/
        //[HttpGet("RejectPerTypes")]
        [HttpGet("Reject/Types")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RejectPerType>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetRejectPerTypes()
        {
            try
            {
                var result = await _unitOfWork.SystemRepository.GetRejectPerTypesAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        // POST: System/
        //[HttpGet("IgnorePerTypes")]
        [HttpGet("Ignore/Types")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<IgnorPerType>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> IgnorePerTypes()
        {
            try
            {
                var result = await _unitOfWork.SystemRepository.GetIgnorPerTypesAsync();
                return Ok(result);
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
        public async Task<IActionResult> PenaltyPerTypes()
        {
            try
            {
                var result = await _unitOfWork.SystemRepository.GetPenaltyPerTypesAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        [HttpGet("Promotions")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Promotion>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetPromotions()
        {
            try
            {
                var result = await _unitOfWork.SystemRepository.GetPromotionsAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        [HttpGet("Promotions/Current")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Promotion))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetCurrentPromotion()
        {
            try
            {
                var result = await _unitOfWork.SystemRepository.GetPromotionsByAsync(p =>
                p.ExpireDate.Value.Day >= DateTime.Now.Day &&
                p.ExpireDate.Value.Month == DateTime.Now.Month &&
                p.ExpireDate.Value.Year == DateTime.Now.Year);

                var currentPromotion = result.FirstOrDefault();
                return Ok(currentPromotion);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        [HttpGet("Promotions/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Promotion))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Promotions(long id)
        {
            try
            {
                var result = await _unitOfWork.SystemRepository.GetPromotionByIdAsync(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }



        [HttpGet("Promotions/Paging")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Promotion>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetPromotionsPaging([FromQuery] FilterParameters parameters)//[FromQuery(Name ="page")] int? page, [FromQuery(Name = "objects")] int? objects)//[FromBody] Pagination pagination)
        {
            try
            {

                /*if (page == null || objects == null) return NoContent();


                var skip = ((int)objects * ((int)page - 1));
                var take = (int)objects;*/

                var skip = (parameters.NumberOfObjectsPerPage * (parameters.Page - 1));
                var take = parameters.NumberOfObjectsPerPage;

                var result = await _unitOfWork.SystemRepository.GetPromotionsPaginationAsync(skip,take);
                return Ok(result);
                //var promotions = await _unitOfWork.SystemRepository.GetAllPromotions();
                //var AgentIDs = users.Select(s => s.AgentId);
                //var newAgents = await _unitOfWork.AgentRepository.GetBy(a => AgentIDs.Contains(a.Id));

                /*var total = promotions.Count;
                var skip = (pagination.NumberOfObjectsPerPage * (pagination.Page - 1));
                var take = pagination.NumberOfObjectsPerPage;
                var result = promotions.Skip(skip).Take(take).ToList();*/



                //var total =  Math.Ceiling(( ((decimal)users.Count) / ((decimal)pagination.NumberOfObjectsPerPage) ));
                //var skip = (pagination.NumberOfObjectsPerPage * (pagination.Page - 1));
                //var take = pagination.NumberOfObjectsPerPage;
                //var result = users.Skip(skip).Take(take).ToList();

                //return Ok(new { Promotions = result, Total = total, Page = pagination.Page });
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
        public async Task<IActionResult> AddPromotion([FromBody] Promotion promotion)
        {
            try
            {
                var insertResult = await _unitOfWork.SystemRepository.InsertPromotionAsync(promotion);
                var result = await _unitOfWork.Save();
                if (result == 0)
                    return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                return Ok(insertResult);
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
        public async Task<IActionResult> UpdatePromotion(long id , [FromBody] Promotion promotion)
        {
            try
            {

                if (id <= 0 || promotion == null || promotion.Id <= 0) return NoContent();

                var insertResult = await _unitOfWork.SystemRepository.UpdatePromotionAsync(promotion);
                var result = await _unitOfWork.Save();
                if (result == 0)
                    return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                return Ok(insertResult);
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
        public async Task<IActionResult> DeletePromotionById(long id)
        {
            try
            {
                var insertResult = await _unitOfWork.SystemRepository.DeletePromotionAsync(id);
                var result = await _unitOfWork.Save();
                if (result == 0)
                    return new ObjectResult("Server not available") { StatusCode = 707 };

                return Ok(true);
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
        public async Task<IActionResult> PublishPromotionById(long id)
        {
            try
            {
                string result = "";
                FirebaseNotificationResponse responseResult = null;

                result = FirebaseNotification.SendNotificationToTopic(FirebaseTopics.Captains, "newPromotion", id.ToString());

                if (result != "")
                    responseResult = JsonConvert.DeserializeObject<FirebaseNotificationResponse>(result);

                if (responseResult == null || responseResult.messageId == "")
                    return NotFound("Failed to publish the promotion");
                else if (responseResult != null && responseResult.messageId != "")
                    return Ok(new { Result = true, Message = "Promotion published successfuly" });
                else
                    return new ObjectResult("Service Unavailable") { StatusCode = 503 };
                //await _HubContext.Clients.All.SendAsync("newPromotion", id.ToString());

               
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }




        [HttpGet("Promotions/Types")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PromotionType>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PromotionTypes()
        {
            try
            {
                var result = await _unitOfWork.SystemRepository.GetPromotionTypesAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }


        [HttpGet("Promotions/Types/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PromotionType))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetPromotionTypeById(long id)
        {
            try
            {
                var result = await _unitOfWork.SystemRepository.GetPromotionTypeByIdAsync(id);
                return Ok(result);
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
        public async Task<IActionResult> AddPromotionType([FromBody] PromotionType promotionType)
        {
            try
            {
                var insertResult = await _unitOfWork.SystemRepository.InsertPromotionTypeAsync(promotionType);
                var result = await _unitOfWork.Save();
                if (result == 0)
                    return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                return Ok(insertResult);
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
        public async Task<IActionResult> UpdatePromotionType(long id,[FromBody] PromotionType promotionType)
        {
            try
            {

                if (id <= 0 || promotionType == null || promotionType.Id <= 0) return NoContent();//

                var insertResult = await _unitOfWork.SystemRepository.UpdatePromotionTypeAsync(promotionType);
                var result = await _unitOfWork.Save();
                if (result == 0)
                    return new ObjectResult("Service Unavailable") { StatusCode = 707 };

                return Ok(insertResult);
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
        public async Task<IActionResult> DeletePromotionType(long id)
        {
            try
            {
                var insertResult = await _unitOfWork.SystemRepository.DeletePromotionTypeAsync(id);
                var result = await _unitOfWork.Save();
                if (result == 0)
                    return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                return Ok(true);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }

        [HttpPost("Upload")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Upload()
        {
            try
            {
                //var description = "";
                //var UserID = "";
                var request = HttpContext.Request;
                //if(request.Form != null && request.Form["description"] != "")
                //    description = request.Form["description"];


                //if (request.Form != null && request.Form["UserID"] != "" && request.Form["UserID"].Count > 0)
                //    UserID = request.Form["UserID"];
                //else if (request.Form != null && request.Form.Files["UserID"] != null)
                //{
                //    UserID = request.Form.Files["UserID"].FileName;

                //}


                //var promotionFolderPath = _hostingEnvironment.ContentRootPath + "/Assets/Images/Promotions";
                ////UserFolderPath = UserFolderPath.Replace('/', '\\');
                //if (!Directory.Exists(promotionFolderPath))
                //    Directory.CreateDirectory(promotionFolderPath);


                var files = HttpContext.Request.Form.Files;
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        

                        var FolderPath = _hostingEnvironment.ContentRootPath + "/Assets/Images/" + file.Name ;
                        FolderPath = FolderPath.Replace('/', '\\');
                        if (!Directory.Exists(FolderPath))
                            Directory.CreateDirectory(FolderPath);

                        //var filePath = Path.Combine(uploads, file.FileName);
                        string filePath = _hostingEnvironment.ContentRootPath + "/Assets/Images/"+ file.Name + "/" + file.FileName;
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                    }
                }


                return Ok(true);
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
        public async Task<IActionResult> GetCurrentUserIgnoredRequestsPenalty(long userId)
        {
            try
            {
                var result = await _unitOfWork.SystemRepository.GetUserIgnoredPenaltyByUserIdAsync(userId);
                
                return Ok(result);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }

        // POST: System/
        //[HttpGet("GetCurrentUserRejectedRequestsPenalty")]
        [HttpGet("Penalties/Captains/{id}/Reject/Current")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CaptainUserRejectPenalty))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetCurrentUserRejectedRequestsPenalty(long userId)
        {

            try
            {
                var result = await _unitOfWork.SystemRepository.GetUserRejectedPenaltyByUserIdAsync(userId);

                return Ok(result);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }

        }

        // POST: System/
        //[HttpGet("GetUserIgnoredRequestsPenalties")]
        [HttpGet("Penalties/Captains/{id}/Ignore")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CaptainUserIgnoredPenalty>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetUserIgnoredRequestsPenalties(long userId)
        {

            try
            {
                var result = await _unitOfWork.SystemRepository.GetUserIgnoredPenaltiesByUserIdAsync(userId);

                return Ok(result);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }

        // POST: System/
        //[HttpGet("GetUserRejectedRequestsPenalties")]
        [HttpGet("Penalties/Captains/{id}/Reject")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CaptainUserRejectPenalty>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetUserRejectedRequestsPenalties(long userId)
        {
            try
            {
                var result = await _unitOfWork.SystemRepository.GetUserRejectedPenaltiesByUserIdAsync(userId);

                return Ok(result);
            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }

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
        public async Task<IActionResult> GetPeniltyStatusTypes()
        {
            try
            {
                var result = await _unitOfWork.SystemRepository.GetPeniltyStatusTypesAsync();
                return Ok(result);
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
        public async Task<IActionResult> DeleteSystemSetting(long id)
        {
            try
            {
                var deleteResult = await _unitOfWork.SystemRepository.DeleteSystemSettingAsync(id);
                var result = await _unitOfWork.Save();
                if (result == 0)
                    return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                return Ok(true);
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
        public async Task<IActionResult> AddContactMessage([FromBody] ContactMessage contactMessage)
        {
            try
            {

                string mailResult = await Utility.sendGridMail(
                    contactMessage.Email,
                    contactMessage.Name,
                    contactMessage.Subject,
                    contactMessage.Message,
                    false
                    );

                var insertResult = await _unitOfWork.SystemRepository.InsertContactMessageAsync(contactMessage);
                var result = await _unitOfWork.Save();

                if (result == 0)
                    return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                return Ok(true);
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
        public async Task<IActionResult> ChangeUserStatus([FromBody] StatusAction statusAction)
        {

            try
            {
                long modifierID = -1;
                string modifierType = "";
                Utility.getRequestUserIdFromToken(HttpContext, out modifierID, out modifierType);

                switch (statusAction.UserType.ToLower())
                {
                    case "driver":
                        {
                            var users = await _unitOfWork.CaptainRepository.GetUsersAccountsByAsync(u => u.UserId == statusAction.UserId);
                            var user = users.FirstOrDefault();
                            if (user == null) return NotFound();

                            user.StatusTypeId = statusAction.StatusTypeId;

                            CaptainUserCurrentStatus userCurrentStatus = new CaptainUserCurrentStatus()
                            {
                                UserId = user.UserId,
                                StatusTypeId = statusAction.StatusTypeId,
                                IsCurrent = true,
                                CreationDate = DateTime.Now,
                                CreatedBy = modifierID,
                                ModifiedBy = modifierID

                            };

                            if (statusAction.StatusTypeId == (long) StatusTypes.Suspended ||
                                statusAction.StatusTypeId == (long) StatusTypes.Stopped)
                            {
                                CaptainUserActivity userActivity = new CaptainUserActivity()
                                {
                                    UserId = user.UserId,
                                    StatusTypeId = (long) StatusTypes.Inactive,
                                    IsCurrent = true,
                                    CreationDate = DateTime.Now
                                };
                                var insertedUserActivity = await _unitOfWork.CaptainRepository.InsertUserActivityAsync(userActivity);
                            }

                            var insertResult = await _unitOfWork.CaptainRepository.InsertUserCurrentStatusAsync(userCurrentStatus);
                            var updateResult = await _unitOfWork.CaptainRepository.UpdateUserAccountAsync(user);
                            var result = await _unitOfWork.Save();

                            if (result == 0)
                                return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                            string statusCacheKey = $"{user.UserId}:driver:status";
                            Utility.SetCacheForAuth(statusCacheKey, user.StatusTypeId, _cache);

                            return Ok(true);
                        }
                    case "support":
                        {
                            var users = await _unitOfWork.SupportRepository.GetSupportUsersAccountsByAsync(u => u.SupportUserId == statusAction.UserId);
                            var user = users.FirstOrDefault();
                            if (user == null) return NotFound();

                            user.StatusTypeId = statusAction.StatusTypeId;

                            SupportUserCurrentStatus userCurrentStatus = new SupportUserCurrentStatus()
                            {
                                SupportUserId = user.SupportUserId,
                                StatusTypeId = statusAction.StatusTypeId,
                                IsCurrent = true,
                                CreationDate = DateTime.Now,
                                CreatedBy = modifierID,
                                ModifiedBy = modifierID

                            };

                            var insertResult = await _unitOfWork.SupportRepository.InsertSupportUserCurrentStatusAsync(userCurrentStatus);
                            var updateResult = await _unitOfWork.SupportRepository.UpdateSupportUserAccountAsync(user);
                            var result = await _unitOfWork.Save();

                            if (result == 0)
                                return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                            string statusCacheKey = $"{user.SupportUserId}:support:status";
                            Utility.SetCacheForAuth(statusCacheKey, user.StatusTypeId, _cache);

                            return Ok(true);
                        }
                    case "admin":
                        {
                            var user = await _unitOfWork.AdminRepository.GetAdminUserAccountByAdminUserIdAsync(statusAction.UserId);
                            //var user = users.FirstOrDefault();
                            if (user == null) return NotFound();

                            user.StatusTypeId = statusAction.StatusTypeId;

                            AdminCurrentStatus userCurrentStatus = new AdminCurrentStatus()
                            {
                                AdminId = user.AdminUserId,
                                StatusTypeId = statusAction.StatusTypeId,
                                IsCurrent = true,
                                CreationDate = DateTime.Now,
                                CreatedBy = modifierID,
                                ModifiedBy = modifierID

                            };

                            var insertResult = await _unitOfWork.AdminRepository.InsertAdminCurrentStatusAsync(userCurrentStatus);
                            var updateResult = await _unitOfWork.AdminRepository.UpdateAdminUserAccountAsync(user);
                            var result = await _unitOfWork.Save();

                            if (result == 0)
                                return new ObjectResult("Service Unavailable") { StatusCode = 503 };


                            string statusCacheKey = $"{user.AdminUserId}:admin:status";
                            Utility.SetCacheForAuth(statusCacheKey, user.StatusTypeId, _cache);

                            return Ok(true);
                        }
                    case "agent":
                        {
                            var user = await _unitOfWork.AgentRepository.GetAgentByIdAsync(statusAction.UserId);
                            if (user == null) return NotFound();

                            user.StatusTypeId = statusAction.StatusTypeId;

                            AgentCurrentStatus userCurrentStatus = new AgentCurrentStatus()
                            {
                               AgentId = user.Id,
                                StatusId = statusAction.StatusTypeId,
                                IsCurrent = true,
                                CreationDate = DateTime.Now,
                                CreatedBy = modifierID,
                                ModifiedBy = modifierID

                            };

                            var insertResult = await _unitOfWork.AgentRepository.InsertAgentCurrentStatusAsync(userCurrentStatus);
                            var updateResult = await _unitOfWork.AgentRepository.UpdateAgentAsync(user);
                            var result = await _unitOfWork.Save();

                            if (result == 0)
                                return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                            string statusCacheKey = $"{user.Id}:agent:status";
                            Utility.SetCacheForAuth(statusCacheKey, user.StatusTypeId, _cache);

                            return Ok(true);
                        }

                    default: { return NotFound(" User not found"); }
                }
                
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
        public async Task<IActionResult> AddUserMessageHubToken([FromBody] UserHubToken userHubToken)
        {

            try
            {
                switch (userHubToken.UserType.ToLower())
                {
                    case "driver":
                        {
                            var insertedUser = await _unitOfWork.CaptainRepository.InsertUserMessageHubAsync(userHubToken.UserId, userHubToken.Token);
                            var result = await _unitOfWork.Save();
                            if (result == 0) new ObjectResult("Service Unavailable") { StatusCode = 503 };

                            return Ok(true);
                        }

                    case "support":
                        {

                            SupportUserMessageHub supportUserMessageHub = new SupportUserMessageHub()
                            {
                                SupportUserId = userHubToken.UserId,
                                ConnectionId = userHubToken.Token
                            };

                            var insertedUser = await _unitOfWork.SupportRepository.InsertSupportUserMessageHubAsync(supportUserMessageHub);
                            var result = await _unitOfWork.Save();
                            if (result == 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                            return Ok(true);
                        }
                    case "agent":
                        {
                            var insertedUser = await _unitOfWork.CaptainRepository.InsertUserMessageHubAsync(userHubToken.UserId, userHubToken.Token);
                            var result = await _unitOfWork.Save();
                            if (result == 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                            return Ok(true);
                        }
                    case "admin":
                        {
                            var insertedUser = await _unitOfWork.CaptainRepository.InsertUserMessageHubAsync(userHubToken.UserId, userHubToken.Token);
                            var result = await _unitOfWork.Save();
                            if (result == 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                            return Ok(true);
                        }
                    default:
                        {
                            return NotFound("User not found");
                        }

                }

                
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
        public async Task<IActionResult> ForgotPassword([FromBody] ResetPassword resetPassword)
        {
            try
            {

                switch (resetPassword.UserType.ToLower())
                {
                    case "driver": {
                            var accounts = await _unitOfWork.CaptainRepository.GetUsersAccountsByAsync(d => d.Mobile == resetPassword.Username);
                            if(accounts == null || accounts?.Count <= 0)
                                return Unauthorized();

                            var account = accounts.FirstOrDefault();
                            if (account?.StatusTypeId == (long)StatusTypes.Stopped || account?.StatusTypeId == (long)StatusTypes.Suspended)
                                return Unauthorized();

                            var user = await _unitOfWork.CaptainRepository.GetUserByIdAsync(account.Id);
                            var country = await _unitOfWork.CountryRepository.GetCountryByIdAsync((long)user.ResidenceCountryId);

                            byte[] passwordHash, passwordSalt;
                            var password = Utility.GeneratePassword();
                            Utility.CreatePasswordHash(password, out passwordHash, out passwordSalt);

                            account.PasswordHash = passwordHash;
                            account.PasswordSalt = passwordSalt;


                            

                            var updatedUser = await _unitOfWork.CaptainRepository.UpdateUserAccountAsync(account);
                            var result = await _unitOfWork.Save();

                            if (result == 0)
                                return new ObjectResult("Service Unavailable") { StatusCode = 503 };

                            var message = "Welcome to Sender, your password has reset, the password is " + password;
                            var phone = country.Code + account.Mobile;
                            var responseResult = Utility.SendSMS(message, phone);
                            //if (!responseResult)
                            //    return new ObjectResult("Server not available") { StatusCode = 707 };


                            return Ok(true);
                        }
                    default: { return NoContent(); }
                }
                

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
        public async Task<IActionResult> SendFirebaseNotification([FromBody] FBNotify fbNotify)
        {
            string result = "";
            FirebaseNotificationResponse responseResult = null;
            try
            {
                if (fbNotify?.Topic != null && fbNotify?.Topic != "")
                {
                    
                    switch (fbNotify.Topic.ToLower())
                    {
                        case FirebaseTopics.Captains:
                            {
                                if (fbNotify.UserId > 0)
                                {
                                    var usersMessageHub = await _unitOfWork.CaptainRepository.GetUsersMessageHubsByAsync(u => u.UserId == fbNotify.UserId);
                                    var userMessageHub = usersMessageHub.FirstOrDefault();
                                    if (userMessageHub != null && userMessageHub.Id > 0)
                                    {
                                        result = FirebaseNotification.SendNotification(userMessageHub.ConnectionId, fbNotify.Title, fbNotify.Message);
                                    }
                                }
                                else if (fbNotify.UserIds?.Count > 0)
                                {
                                    var usersMessageHub = await _unitOfWork.CaptainRepository.GetUsersMessageHubsByAsync(u => fbNotify.UserIds.Contains((long)u.UserId));
                                    if (usersMessageHub != null && usersMessageHub?.Count > 0)
                                    {
                                        var tokens = usersMessageHub.Select(u => u.ConnectionId).ToList();
                                        result = FirebaseNotification.SendNotificationToMultiple(tokens, fbNotify.Title, fbNotify.Message);
                                    }
                                }
                                else
                                {
                                    result = FirebaseNotification.SendNotificationToTopic(fbNotify.Topic.ToLower(), fbNotify.Title, fbNotify.Message);
                                }

                                break;
                            }
                        case FirebaseTopics.Agents:
                            {
                                result = FirebaseNotification.SendNotificationToTopic(fbNotify.Topic.ToLower(), fbNotify.Title, fbNotify.Message);
                                break;
                            }
                        case FirebaseTopics.Supports:
                            {
                                result = FirebaseNotification.SendNotificationToTopic(fbNotify.Topic.ToLower(), fbNotify.Title, fbNotify.Message);
                                break;
                            }
                        case FirebaseTopics.Admins:
                            {
                                result = FirebaseNotification.SendNotificationToTopic(fbNotify.Topic.ToLower(), fbNotify.Title, fbNotify.Message);
                                break;
                            }
                    }
                }


                if (result != "")
                    responseResult = JsonConvert.DeserializeObject<FirebaseNotificationResponse>(result);

                if (responseResult == null || responseResult.messageId == "")
                    return NotFound("Failed to send the message") ;
                else if (responseResult != null && responseResult.messageId != "")
                    return Ok(new { Result = true, Message = "Message sent successfuly" });
                else
                    return new ObjectResult("Service Unavailable") { StatusCode = 503 };

            }
            catch (Exception e)
            {
                return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
            }
        }
        /**/



    }
}
