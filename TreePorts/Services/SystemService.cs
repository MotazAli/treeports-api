using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using TreePorts.DTO;
using TreePorts.DTO.Records;
using TreePorts.Utilities;

namespace TreePorts.Services;
public class SystemService  : ISystemService
{
    private readonly IUnitOfWork _unitOfWork;
    //private IHubContext<MessageHub> _HubContext;
    private readonly IWebHostEnvironment _hostingEnvironment;
    private readonly IMemoryCache _cache;

    public SystemService(IUnitOfWork unitOfWork, IMemoryCache cache, IWebHostEnvironment hostingEnvironment)//, IHubContext<MessageHub> hubcontext)
    {
        _unitOfWork = unitOfWork;
        _cache = cache;
        //_HubContext = hubcontext;
        _hostingEnvironment = hostingEnvironment;
    }


    
    public async Task<SystemSetting?> GetSystemSettingAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.SystemRepository.GetCurrentSystemSettingAsync(cancellationToken);
        }
        catch (Exception e)
        {
            return new SystemSetting();
        }

    }


    
    public async Task<IEnumerable<Vehicle>> GetVehiclesAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.SystemRepository.GetVehiclesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            return new List<Vehicle>();
        }

    }

   
    public async Task<IEnumerable<BoxType>> GetBoxTypesAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.SystemRepository.GetBoxTypesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            return new List<BoxType>();
        }
    }


    
    public async Task<IEnumerable<Shift>> GetShiftsAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.SystemRepository.GetShiftsAsync(cancellationToken);
        }
        catch (Exception e)
        {
            return new List<Shift>();
        }
    }



    
    public async Task<IEnumerable<AndroidVersion>> GetAndroidVersionsPagingAsync( FilterParameters parameters, CancellationToken cancellationToken) // [FromQuery(Name ="page")] int? page , [FromQuery(Name = "objects")] int? objects)//[FromBody] Pagination pagination)
    {
        try
        {

            /*if (page == null || objects == null) return NoContent();//


            var skip = ((int)objects * ((int)page - 1));
            var take = (int)objects;*/

            var skip = (parameters.NumberOfObjectsPerPage * (parameters.Page - 1));
            var take = parameters.NumberOfObjectsPerPage;
            var result = await _unitOfWork.SystemRepository.GetAndroidVersionsPaginationAsync(skip, take,cancellationToken);
            return result;
            //var AgentIDs = users.Select(s => s.AgentId);
            //var newAgents = await _unitOfWork.AgentRepository.GetBy(a => AgentIDs.Contains(a.Id));

            //var versions = await _unitOfWork.SystemRepository.GetAndroidVersionsAsync(cancellationToken);
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
            return new List<AndroidVersion>();// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }

    
    public async Task<AndroidVersion?> GetCurrentAndroidVersionAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.SystemRepository.GetCurrentAndroidVersionAsync(cancellationToken);
            //var current = result.FirstOrDefault();
            //if (current == null) return NotFound("No current android version available");

            
        }
        catch (Exception e)
        {
            return new AndroidVersion();// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }

    
    public async Task<IEnumerable<AndroidVersion>> GetAndroidVersionsAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.SystemRepository.GetAndroidVersionsAsync(cancellationToken);
        }
        catch (Exception e)
        {
            return new List<AndroidVersion>();// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }


    
    public async Task<AndroidVersion?> GetAndroidVersionByIdAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.SystemRepository.GetAndroidVersionByIdAsync(id,cancellationToken);
            //if (result == null) return NotFound();

            
        }
        catch (Exception e)
        {
            return new AndroidVersion();// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }




    
    public async Task<AndroidVersion> AddAndroidVersionAsync( AndroidVersion androidVersion, CancellationToken cancellationToken)
    {
       
            var insertResult = await _unitOfWork.SystemRepository.InsertAndroidVersionAsync(androidVersion,cancellationToken);
            var result = await _unitOfWork.Save(cancellationToken);
            if (result == 0)
                throw new ServiceUnavailableException("Service Unavailable");

            return insertResult;
        
    }


    
    public async Task<AndroidVersion?> UpdateAndroidVersionsAsync(long id, AndroidVersion androidVersion, CancellationToken cancellationToken)
    {
        
            if (id <= 0 || androidVersion == null || androidVersion.Id <= 0) throw new Exception("NoContent");



            var updateResult = await _unitOfWork.SystemRepository.UpdateAndroidVersionAsync(androidVersion,cancellationToken);
            var result = await _unitOfWork.Save(cancellationToken);
            if (result == 0)
                throw new ServiceUnavailableException("Service Unavailable");

            return updateResult;
        
    }



    
    public async Task<bool> UploadAndroidFileAsync(HttpContext httpContext, CancellationToken cancellationToken)
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



            var request = httpContext.Request;

            var files = httpContext.Request.Form.Files;
            foreach (var file in files)
            {
                if (file.Length > 0)
                {

                    var FolderPath = _hostingEnvironment.ContentRootPath + "/Assets/Apps/" + file.Name;
                    //FolderPath = FolderPath.Replace('/', '\\');
                    if (!Directory.Exists(FolderPath))
                        Directory.CreateDirectory(FolderPath);

                    //var filePath = Path.Combine(uploads, file.FileName);
                    string filePath = _hostingEnvironment.ContentRootPath + "/Assets/Apps/" + file.Name + "/" + file.FileName;
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream,cancellationToken);
                    }
                }
            }


            return true;
        }
        catch (Exception e)
        {
            return false;// new ObjectResult(e.Message) { StatusCode = 666 };
        }


    }



    
    public async Task<bool> UploadPromotionFileAsync(HttpContext httpContext, CancellationToken cancellationToken)
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



            var request = httpContext.Request;

            var files = httpContext.Request.Form.Files;
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
                        await file.CopyToAsync(fileStream,cancellationToken);
                    }
                }
            }


            return true;
        }
        catch (Exception e)
        {
            return false;// new ObjectResult(e.Message) { StatusCode = 666 };
        }


    }





    
    public async Task<object> GetCountriesPricesAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.CountryRepository.GetCountriesPricesAsync(cancellationToken);

            
        }
        catch (Exception e)
        {
            return new {};// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }

    
    public async Task<object> GetCountryPriceByIdAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.CountryRepository.GetCountryPriceByIdAsync(id,cancellationToken);

            
        }
        catch (Exception e)
        {
            return new {}; // new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }


    
    public async Task<IEnumerable<CountryPrice>> GetCountryPricesByCountryIdAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.CountryRepository.GetCountriesPricesByAsync(cp => cp.CountryId == id,cancellationToken);

        }
        catch (Exception e)
        {
            return new List<CountryPrice>(); // new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }


    
    public async Task<CountryPrice> AddCountryPriceAsync(HttpContext httpContext  , CountryPrice countryPrice, CancellationToken cancellationToken)
    {
        
            //long modifierID = -1;
            //string modifierType = "";
            Utility.getRequestUserIdFromToken(httpContext, out string modifierID, out string modifierType);
            countryPrice.CreatedBy = modifierID;
            var insertResult = await _unitOfWork.CountryRepository.InsertCountryPriceAsync(countryPrice,cancellationToken);
            var result = await _unitOfWork.Save(cancellationToken);
            if (result == 0)
                throw new ServiceUnavailableException("Service Unavailable");


            return insertResult;
        
    }

    
    public async Task<bool> DeleteCountriesPricesAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _unitOfWork.CountryRepository.DeleteCountryPriceAsync(id,cancellationToken);
     
            return true;
        }
        catch (Exception e)
        {
            return false;// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }



    
    public async Task<IEnumerable<Shift>> GetShiftsByShiftDateAsync( Shift shift, CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.SystemRepository.GetShiftsByShiftDateAsync(shift,cancellationToken);

        }
        catch (Exception e)
        {
            return new List<Shift>();// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }

    
    public async Task<SystemSetting?> GetSystemSettingByIdAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.SystemRepository.GetSystemSettingByIdAsync(id,cancellationToken);
            
        }
        catch (Exception e)
        {
            return new SystemSetting();// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }

    
    
    public async Task<SystemSetting> AddSystemSettingAsync( SystemSetting setting, HttpContext httpContext, CancellationToken cancellationToken)
    {
        
            //long modifierID = -1;
            //string modifierType = "";
            Utility.getRequestUserIdFromToken(httpContext, out string modifierID, out string modifierType);

            setting.CreatedBy = modifierID;
            var insertResult = await _unitOfWork.SystemRepository.InsertSystemSettingAsync(setting,cancellationToken);
            var result = await _unitOfWork.Save(cancellationToken);
            if (result == 0)
                throw new ServiceUnavailableException("Service Unavailable");


            return insertResult;
        
    }







    
    public async Task<CityPrice> AddCityPriceAsync( CityPrice cityPrice, HttpContext httpContext, CancellationToken cancellationToken)
    {
       
            //long modifierID = -1;
            //string modifierType = "";
            Utility.getRequestUserIdFromToken(httpContext, out string modifierID, out string modifierType);

            cityPrice.CreatedBy = modifierID;
            var insertResult = await _unitOfWork.CountryRepository.InsertCityPriceAsync(cityPrice,cancellationToken);
            var result = await _unitOfWork.Save(cancellationToken);
            if (result <= 0)
                throw new ServiceUnavailableException("Service Unavailable");


            return insertResult;
        
    }



    public async Task<object> GetCitiesPricesAsync(CancellationToken cancellationToken)
    {
        try
        {

            return await _unitOfWork.CountryRepository.GetCitiesPricesAsync(cancellationToken);
            
        }
        catch (Exception e)
        {
            return new { };// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }


    
    public async Task<object?> GetCityPriceByIdAsync(long id, CancellationToken cancellationToken)
    {
        try
        {

            return await _unitOfWork.CountryRepository.GetCityPriceByIdAsync(id,cancellationToken);
            
        }
        catch (Exception e)
        {
            return new { };// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }


    
    public async Task<IEnumerable<CityPrice>> GetCitiesPricesByCityIdAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.CountryRepository.GetCitiesPricesByAsync(cp => cp.CityId == id && cp.IsDeleted == false,cancellationToken);
            
        }
        catch (Exception e)
        {
            return new List<CityPrice>();// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }

    
    public async Task<bool> DeleteCityPriceByIdAsync(long id, CancellationToken cancellationToken)
    {
        
            var result = await _unitOfWork.CountryRepository.DeleteCityPriceAsync(id,cancellationToken);
            if (result == null) throw new NoContentException("NoContent");

            return true;
        
    }

    
   /* public async Task<IEnumerable<RejectPerType>> GetRejectPerTypesAsync()
    {
        try
        {
            return await _unitOfWork.SystemRepository.GetRejectPerTypesAsync(cancellationToken);
            
        }
        catch (Exception e)
        {
            return new List<RejectPerType>();// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }*/


    
    public async Task<IEnumerable<IgnorPerType>> IgnorePerTypesAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.SystemRepository.GetIgnorPerTypesAsync(cancellationToken);
            
        }
        catch (Exception e)
        {
            return new List<IgnorPerType>();// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }


    
    public async Task<IEnumerable<PenaltyPerType>> PenaltyPerTypesAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.SystemRepository.GetPenaltyPerTypesAsync(cancellationToken);
            
        }
        catch (Exception e)
        {
            return new List<PenaltyPerType>();// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }


    
    public async Task<IEnumerable<Promotion>> GetPromotionsAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.SystemRepository.GetPromotionsAsync(cancellationToken);
            
        }
        catch (Exception e)
        {
            return new List<Promotion>();// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }


    
    public async Task<Promotion?> GetCurrentPromotionAsync(CancellationToken cancellationToken)
    {
        try
        {
            var result = await _unitOfWork.SystemRepository.GetPromotionsByAsync(p =>
            p.ExpireDate.Value.Day >= DateTime.Now.Day &&
            p.ExpireDate.Value.Month == DateTime.Now.Month &&
            p.ExpireDate.Value.Year == DateTime.Now.Year,cancellationToken);

            var currentPromotion = result.FirstOrDefault();
            return currentPromotion;
        }
        catch (Exception e)
        {
            return new Promotion();// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }


   
    public async Task<Promotion?> GetPromotionByIdAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.SystemRepository.GetPromotionByIdAsync(id,cancellationToken);
            
        }
        catch (Exception e)
        {
            return new Promotion();// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }



    
    public async Task<IEnumerable<Promotion>> GetPromotionsPaginationAsync(FilterParameters parameters, CancellationToken cancellationToken)//[FromQuery(Name ="page")] int? page, [FromQuery(Name = "objects")] int? objects)//[FromBody] Pagination pagination)
    {
        try
        {

            /*if (page == null || objects == null) return NoContent();


            var skip = ((int)objects * ((int)page - 1));
            var take = (int)objects;*/

            var skip = (parameters.NumberOfObjectsPerPage * (parameters.Page - 1));
            var take = parameters.NumberOfObjectsPerPage;

            var result = await _unitOfWork.SystemRepository.GetPromotionsPaginationAsync(skip, take,cancellationToken);
            return result;
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
            return new List<Promotion>();// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }




    
    public async Task<Promotion> AddPromotionAsync(Promotion promotion, CancellationToken cancellationToken)
    {
       
            var insertResult = await _unitOfWork.SystemRepository.InsertPromotionAsync(promotion,cancellationToken);
            var result = await _unitOfWork.Save(cancellationToken);
            if (result == 0)
                throw new ServiceUnavailableException("Service Unavailable");

            return insertResult;
        
    }


    
    public async Task<Promotion?> UpdatePromotionAsync(long id, Promotion promotion, CancellationToken cancellationToken)
    {
        

            if (id <= 0 || promotion == null || promotion.Id <= 0) throw new Exception( "NoContent");

            var insertResult = await _unitOfWork.SystemRepository.UpdatePromotionAsync(promotion,cancellationToken);
            var result = await _unitOfWork.Save(cancellationToken);
            if (result == 0)
                throw new ServiceUnavailableException("Service Unavailable");

            return insertResult;
        
    }


    
    public async Task<bool> DeletePromotionById(long id, CancellationToken cancellationToken)
    {
        
            var insertResult = await _unitOfWork.SystemRepository.DeletePromotionAsync(id,cancellationToken);
            var result = await _unitOfWork.Save(cancellationToken);
            if (result == 0)
                throw new ServiceUnavailableException("Server not available");

            return true;
        
    }

    
    public async Task<object> PublishPromotionByIdAsync(long id, CancellationToken cancellationToken)
    {
       
            string result = "";
            FirebaseNotificationResponse? responseResult = null;

            result = await FirebaseNotification.SendNotificationToTopic(FirebaseTopics.Captains, "newPromotion", id.ToString());

            if (result != "")
                responseResult = JsonConvert.DeserializeObject<FirebaseNotificationResponse>(result);

            if (responseResult == null || responseResult.messageId == "")
                throw new NotFoundException ("NotFound - Failed to publish the promotion");
            else if (responseResult != null && responseResult.messageId != "")
                return new { Result = true, Message = "Promotion published successfuly" };
            else
                throw new ServiceUnavailableException("Service Unavailable");
            //await _HubContext.Clients.All.SendAsync("newPromotion", id.ToString());


    }



    public async Task<IEnumerable<PromotionType>> PromotionTypesAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.SystemRepository.GetPromotionTypesAsync(cancellationToken);
            
        }
        catch (Exception e)
        {
            return new List<PromotionType>();// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }


    
    public async Task<PromotionType?> GetPromotionTypeByIdAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.SystemRepository.GetPromotionTypeByIdAsync(id,cancellationToken);
            
        }
        catch (Exception e)
        {
            return new PromotionType();// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }


    
    public async Task<PromotionType> AddPromotionTypeAsync( PromotionType promotionType, CancellationToken cancellationToken)
    {
       
            var insertResult = await _unitOfWork.SystemRepository.InsertPromotionTypeAsync(promotionType,cancellationToken);
            var result = await _unitOfWork.Save(cancellationToken);
            if (result == 0)
                throw new ServiceUnavailableException("Service Unavailable");

            return insertResult;
        
    }


    
    public async Task<PromotionType?> UpdatePromotionTypeAsync(long id,  PromotionType promotionType, CancellationToken cancellationToken)
    {
        
            if (id <= 0 || promotionType == null || promotionType.Id <= 0) throw new NoContentException("NoContent");//

            var insertResult = await _unitOfWork.SystemRepository.UpdatePromotionTypeAsync(promotionType,cancellationToken);
            var result = await _unitOfWork.Save(cancellationToken);
            if (result == 0)
                throw new ServiceUnavailableException("Service Unavailable");

            return insertResult;
        
    }


    
    public async Task<bool> DeletePromotionTypeAsync(long id, CancellationToken cancellationToken)
    {
        
            var deletedResult = await _unitOfWork.SystemRepository.DeletePromotionTypeAsync(id,cancellationToken);
            var result = await _unitOfWork.Save(cancellationToken);
            if (result == 0)
                throw new ServiceUnavailableException("Service Unavailable") ;

            return true;
        
    }

    
    public async Task<bool> UploadAsync(HttpContext httpContext, CancellationToken cancellationToken)
    {
        try
        {
            //var description = "";
            //var UserID = "";
            var request = httpContext.Request;
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


            var files = httpContext.Request.Form.Files;
            foreach (var file in files)
            {
                if (file.Length > 0)
                {


                    var FolderPath = _hostingEnvironment.ContentRootPath + "/Assets/Images/" + file.Name;
                    FolderPath = FolderPath.Replace('/', '\\');
                    if (!Directory.Exists(FolderPath))
                        Directory.CreateDirectory(FolderPath);

                    //var filePath = Path.Combine(uploads, file.FileName);
                    string filePath = _hostingEnvironment.ContentRootPath + "/Assets/Images/" + file.Name + "/" + file.FileName;
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream,cancellationToken);
                    }
                }
            }


            return true;
        }
        catch (Exception e)
        {
            return false; // new ObjectResult(e.Message) { StatusCode = 666 };
        }


    }




    
    public async Task<CaptainUserIgnoredPenalty?> GetCurrentUserIgnoredRequestsPenaltyByCaptainUserAccountIdAsync(string captainUserAccountId, CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.SystemRepository.GetCaptainUserIgnoredPenaltyByCaptainUserAccountIdAsync(captainUserAccountId,cancellationToken);

        }
        catch (Exception e)
        {
            return new CaptainUserIgnoredPenalty();// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }

    
    /*public async Task<CaptainUserRejectPenalty> GetCurrentUserRejectedRequestsPenaltyByCaptainUserAccountIdAsync(long userId)
    {

        try
        {
            return await _unitOfWork.SystemRepository.GetUserRejectedPenaltyByUserIdAsync(userId);

            
        }
        catch (Exception e)
        {
            return new CaptainUserRejectPenalty();// new ObjectResult(e.Message) { StatusCode = 666 };
        }

    }*/

    
    public async Task<IEnumerable<CaptainUserIgnoredPenalty>> GetUserIgnoredRequestsPenaltiesByCaptainUserAccountIdAsync(string captainUserAccountId, CancellationToken cancellationToken)
    {

        try
        {
            return await _unitOfWork.SystemRepository.GetCaptainUserIgnoredPenaltiesByCaptainUserAccountIdAsync(captainUserAccountId,cancellationToken);

            
        }
        catch (Exception e)
        {
            return new List<CaptainUserIgnoredPenalty>();// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }

    
   /* public async Task<IEnumerable<CaptainUserRejectPenalty>> GetUserRejectedRequestsPenaltiesByCaptainUserAccountIdAsync(long userId)
    {
        try
        {
            return await _unitOfWork.SystemRepository.GetUserRejectedPenaltiesByUserIdAsync(userId);

        }
        catch (Exception e)
        {
            return new List<CaptainUserRejectPenalty>();// new ObjectResult(e.Message) { StatusCode = 666 };
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


    
    public async Task<IEnumerable<PenaltyStatusType>> GetPeniltyStatusTypesAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _unitOfWork.SystemRepository.GetPeniltyStatusTypesAsync(cancellationToken);
            
        }
        catch (Exception e)
        {
            return new List<PenaltyStatusType>();// new ObjectResult(e.Message) { StatusCode = 666 };
        }
    }


    // PUT: api/System/5
    /*[HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }*/

    
    public async Task<bool> DeleteSystemSettingAsync(long id, CancellationToken cancellationToken)
    {
       
            var deleteResult = await _unitOfWork.SystemRepository.DeleteSystemSettingAsync(id,cancellationToken);
            var result = await _unitOfWork.Save(cancellationToken);
            if (result == 0)
                throw new ServiceUnavailableException("Service Unavailable");

            return true;
        
    }


    
    public async Task<bool> AddContactMessageAsync( ContactMessage contactMessage, CancellationToken cancellationToken)
    {
        

            var mailResult = await Utility.sendGridMail(
                contactMessage?.Email,
                contactMessage?.Name,
                contactMessage?.Subject,
                contactMessage?.Message,
                false,
                cancellationToken
                );

            var insertResult = await _unitOfWork.SystemRepository.InsertContactMessageAsync(contactMessage,cancellationToken);
            var result = await _unitOfWork.Save(cancellationToken);

            if (result == 0)
                throw new ServiceUnavailableException("Service Unavailable");

            return true;
        
    }



   
    public async Task<bool> ChangeUserStatusAsync( StatusAction statusAction, HttpContext httpContext, CancellationToken cancellationToken)
    {

       
            //long modifierID = -1;
            //string modifierType = "";
            Utility.getRequestUserIdFromToken(httpContext, out string modifierID, out string modifierType);

            switch (statusAction.UserType.ToLower())
            {
                case "driver":
                    {
                        var users = await _unitOfWork.CaptainRepository.GetCaptainUsersAccountsByAsync(u => u.Id == statusAction.UserAccountId,cancellationToken);
                        var user = users.FirstOrDefault();
                        if (user == null) throw new NotFoundException("NotFound");

                        user.StatusTypeId = statusAction.StatusTypeId;

                        CaptainUserCurrentStatus userCurrentStatus = new ()
                        {
                            CaptainUserAccountId = user.Id,
                            StatusTypeId = statusAction.StatusTypeId,
                            IsCurrent = true,
                            CreationDate = DateTime.Now,
                            CreatedBy = modifierID,
                            ModifiedBy = modifierID

                        };

                        if (statusAction.StatusTypeId == (long)StatusTypes.Suspended ||
                            statusAction.StatusTypeId == (long)StatusTypes.Stopped)
                        {
                            CaptainUserActivity userActivity = new ()
                            {
                                CaptainUserAccountId = user.Id,
                                StatusTypeId = (long)StatusTypes.Inactive,
                                IsCurrent = true,
                                CreationDate = DateTime.Now
                            };
                            var insertedUserActivity = await _unitOfWork.CaptainRepository.InsertCaptainUserActivityAsync(userActivity,cancellationToken);
                        }

                        var insertResult = await _unitOfWork.CaptainRepository.InsertCaptainUserCurrentStatusAsync(userCurrentStatus,cancellationToken);
                        var updateResult = await _unitOfWork.CaptainRepository.UpdateCaptainUserAccountAsync(user,cancellationToken);
                        var result = await _unitOfWork.Save(cancellationToken);

                        if (result == 0)
                            throw new ServiceUnavailableException("Service Unavailable");

                        string statusCacheKey = $"{user.Id}:driver:status";
                        Utility.SetCacheForAuth(statusCacheKey, user.StatusTypeId, _cache);

                        return true;
                    }
                case "support":
                    {
                        var users = await _unitOfWork.SupportRepository.GetSupportUsersAccountsByAsync(u => u.SupportUserId == statusAction.UserAccountId,cancellationToken);
                        var user = users.FirstOrDefault();
                        if (user == null) throw new NotFoundException("NotFound");

                        user.StatusTypeId = statusAction.StatusTypeId;

                        SupportUserCurrentStatus userCurrentStatus = new()
                        {
                            SupportUserAccountId = user.Id,
                            StatusTypeId = statusAction.StatusTypeId,
                            IsCurrent = true,
                            CreationDate = DateTime.Now,
                            CreatedBy = modifierID,
                            ModifiedBy = modifierID

                        };

                        var insertResult = await _unitOfWork.SupportRepository.InsertSupportUserCurrentStatusAsync(userCurrentStatus,cancellationToken);
                        var updateResult = await _unitOfWork.SupportRepository.UpdateSupportUserAccountAsync(user,cancellationToken);
                        var result = await _unitOfWork.Save(cancellationToken);

                        if (result == 0)
                            throw new ServiceUnavailableException("Service Unavailable");

                        string statusCacheKey = $"{user.Id}:support:status";
                        Utility.SetCacheForAuth(statusCacheKey, user.StatusTypeId, _cache);

                        return true;
                    }
                case "admin":
                    {
                        var user = await _unitOfWork.AdminRepository.GetAdminUserAccountByAdminUserIdAsync(statusAction.UserAccountId,cancellationToken);
                        //var user = users.FirstOrDefault();
                        if (user == null) throw new Exception("NotFound");

                        user.StatusTypeId = statusAction.StatusTypeId;

                        AdminCurrentStatus userCurrentStatus = new ()
                        {
                            AdminUserAccountId = user.Id,
                            StatusTypeId = statusAction.StatusTypeId,
                            IsCurrent = true,
                            CreationDate = DateTime.Now,
                            CreatedBy = modifierID,
                            ModifiedBy = modifierID

                        };

                        var insertResult = await _unitOfWork.AdminRepository.InsertAdminCurrentStatusAsync(userCurrentStatus,cancellationToken);
                        var updateResult = await _unitOfWork.AdminRepository.UpdateAdminUserAccountAsync(user,cancellationToken);
                        var result = await _unitOfWork.Save(cancellationToken);

                        if (result == 0)
                            throw new ServiceUnavailableException("Service Unavailable");


                        string statusCacheKey = $"{user.Id}:admin:status";
                        Utility.SetCacheForAuth(statusCacheKey, user.StatusTypeId, _cache);

                        return true;
                    }
                case "agent":
                    {
                        var user = await _unitOfWork.AgentRepository.GetAgentByIdAsync(statusAction.UserAccountId,cancellationToken);
                        if (user == null) throw new NotFoundException("NotFound");

                        user.StatusTypeId = statusAction.StatusTypeId;

                        AgentCurrentStatus userCurrentStatus = new ()
                        {
                            AgentId = user.Id,
                            StatusTypeId = statusAction.StatusTypeId,
                            IsCurrent = true,
                            CreationDate = DateTime.Now,
                            CreatedBy = modifierID,
                            ModifiedBy = modifierID

                        };

                        var insertResult = await _unitOfWork.AgentRepository.InsertAgentCurrentStatusAsync(userCurrentStatus,cancellationToken);
                        var updateResult = await _unitOfWork.AgentRepository.UpdateAgentAsync(user,cancellationToken);
                        var result = await _unitOfWork.Save(cancellationToken);

                        if (result == 0)
                            throw new ServiceUnavailableException("Service Unavailable");

                        string statusCacheKey = $"{user.Id}:agent:status";
                        Utility.SetCacheForAuth(statusCacheKey, user.StatusTypeId, _cache);

                        return true;
                    }

                default: { throw new NotFoundException("NotFound- User not found"); }
            }

        

    }


    
    public async Task<bool> AddUserMessageHubTokenAsync( UserHubToken userHubToken, CancellationToken cancellationToken)
    {

        
            switch (userHubToken.UserType.ToLower())
            {
                case "captain":
                    {
                        var insertedUser = await _unitOfWork.CaptainRepository.InsertCaptainUserMessageHubByCaptainUserAccountIdAsync(userHubToken.UserAccountId, userHubToken.Token,cancellationToken);
                        var result = await _unitOfWork.Save(cancellationToken);
                        if (result == 0) throw new ServiceUnavailableException("Service Unavailable");

                        return true;
                    }

                case "support":
                    {

                        SupportUserMessageHub supportUserMessageHub = new SupportUserMessageHub()
                        {
                            SupportUserAccountId = userHubToken.UserAccountId,
                            ConnectionId = userHubToken.Token
                        };

                        var insertedUser = await _unitOfWork.SupportRepository.InsertSupportUserMessageHubAsync(supportUserMessageHub,cancellationToken);
                        var result = await _unitOfWork.Save(cancellationToken);
                        if (result == 0) throw new ServiceUnavailableException("Service Unavailable");

                        return true;
                    }
                case "agent":
                    {
                        var insertedUser = await _unitOfWork.CaptainRepository.InsertCaptainUserMessageHubByCaptainUserAccountIdAsync(userHubToken.UserAccountId, userHubToken.Token,cancellationToken);
                        var result = await _unitOfWork.Save(cancellationToken);
                        if (result == 0) throw new ServiceUnavailableException("Service Unavailable");

                        return true;
                    }
                case "admin":
                    {
                        var insertedUser = await _unitOfWork.CaptainRepository.InsertCaptainUserMessageHubByCaptainUserAccountIdAsync(userHubToken.UserAccountId, userHubToken.Token,cancellationToken);
                        var result = await _unitOfWork.Save(cancellationToken);
                        if (result == 0) throw new ServiceUnavailableException("Service Unavailable");

                        return true;
                    }
                default:
                    {
                        throw new NotFoundException("NotFound - User not found");
                    }

            }

    }



    
    public async Task<bool> ForgotPasswordAsync( ResetPassword resetPassword, CancellationToken cancellationToken)
    {
      
            switch (resetPassword.UserType.ToLower())
            {
                case "captain":
                    {
                        var accounts = await _unitOfWork.CaptainRepository.GetCaptainUsersAccountsByAsync(d => d.Mobile == resetPassword.Username,cancellationToken);
                        if (accounts == null || accounts?.Count() <= 0)
                            throw new UnauthorizedException("Unauthorized");

                        var account = accounts?.FirstOrDefault();
                        if (account?.StatusTypeId == (long)StatusTypes.Stopped || account?.StatusTypeId == (long)StatusTypes.Suspended)
                            throw new Exception("Unauthorized");

                        var user = await _unitOfWork.CaptainRepository.GetCaptainUserByIdAsync(account.CaptainUserId,cancellationToken);
                        var country = await _unitOfWork.CountryRepository.GetCountryByIdAsync(user.ResidenceCountryId ?? 0, cancellationToken);

                        byte[] passwordHash, passwordSalt;
                        var password = Utility.GeneratePassword();
                        Utility.CreatePasswordHash(password, out passwordHash, out passwordSalt);

                        account.PasswordHash = passwordHash;
                        account.PasswordSalt = passwordSalt;




                        var updatedUser = await _unitOfWork.CaptainRepository.UpdateCaptainUserAccountAsync(account,cancellationToken);
                        var result = await _unitOfWork.Save(cancellationToken);

                        if (result == 0)
                            throw new ServiceUnavailableException("Service Unavailable");

                        var message = "Welcome to Sender, your password has reset, the password is " + password;
                        var phone = country?.Code + account.Mobile;
                        var responseResult = await Utility.SendSMSAsync(message, phone,cancellationToken);
                        //if (!responseResult)
                        //    return new ObjectResult("Server not available") { StatusCode = 707 };


                        return true;
                    }
                default: { throw new NoContentException("NoContent"); }
            }


    }



    
    public async Task<object> SendFirebaseNotificationAsync(FBNotify fbNotify, CancellationToken cancellationToken)
    {
        string result = "";
        FirebaseNotificationResponse? responseResult = null;
       
            if (fbNotify?.Topic != null && fbNotify?.Topic != "")
            {

                switch (fbNotify.Topic.ToLower())
                {
                    case FirebaseTopics.Captains:
                        {
                            if (fbNotify.UserAccountId != "")
                            {
                                var usersMessageHub = await _unitOfWork.CaptainRepository.GetCaptainUsersMessageHubsByAsync(u => u.CaptainUserAccountId == fbNotify.UserAccountId,cancellationToken);
                                var userMessageHub = usersMessageHub.FirstOrDefault();
                                if (userMessageHub != null && userMessageHub.Id > 0)
                                {
                                    result = await FirebaseNotification.SendNotification(userMessageHub.ConnectionId, fbNotify.Title, fbNotify.Message,cancellationToken);
                                }
                            }
                            else if (fbNotify.UserAccountIds?.Count > 0)
                            {
                                var usersMessageHub = await _unitOfWork.CaptainRepository.GetCaptainUsersMessageHubsByAsync(u => fbNotify.UserAccountIds.Contains(u.CaptainUserAccountId),cancellationToken);
                                if (usersMessageHub != null && usersMessageHub?.Count() > 0)
                                {
                                    var tokens = usersMessageHub.Select(u => u.ConnectionId).ToList();
                                    result = await FirebaseNotification.SendNotificationToMultiple(tokens, fbNotify.Title, fbNotify.Message);
                                }
                            }
                            else
                            {
                                result =await FirebaseNotification.SendNotificationToTopic(fbNotify.Topic.ToLower(), fbNotify.Title, fbNotify.Message);
                            }

                            break;
                        }
                    case FirebaseTopics.Agents:
                        {
                            result =await FirebaseNotification.SendNotificationToTopic(fbNotify.Topic.ToLower(), fbNotify.Title, fbNotify.Message);
                            break;
                        }
                    case FirebaseTopics.Supports:
                        {
                            result = await FirebaseNotification.SendNotificationToTopic(fbNotify.Topic.ToLower(), fbNotify.Title, fbNotify.Message);
                            break;
                        }
                    case FirebaseTopics.Admins:
                        {
                            result = await FirebaseNotification.SendNotificationToTopic(fbNotify.Topic.ToLower(), fbNotify.Title, fbNotify.Message);
                            break;
                        }
                }
            }


            if (result != "")
                responseResult = JsonConvert.DeserializeObject<FirebaseNotificationResponse>(result);

            if (responseResult == null || responseResult.messageId == "")
                throw new NotFoundException("NotFound - Failed to send the message");
            else if (responseResult != null && responseResult.messageId != "")
                return new { Result = true, Message = "Message sent successfuly" };
            else
                throw new ServiceUnavailableException("Service Unavailable");

    }
    /**/


}

