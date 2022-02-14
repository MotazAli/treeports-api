using TreePorts.DTO;

namespace TreePorts.Interfaces.Services;
public interface ISystemService
{
    Task<SystemSetting> GetSystemSettingAsync();
    Task<IEnumerable<Vehicle>> GetVehiclesAsync();
    Task<IEnumerable<BoxType>> GetBoxTypesAsync();   
    Task<IEnumerable<Shift>> GetShiftsAsync();
    Task<IEnumerable<AndroidVersion>> GetAndroidVersionsPagingAsync(FilterParameters parameters);
    Task<AndroidVersion> GetCurrentAndroidVersionAsync();
    Task<IEnumerable<AndroidVersion>> GetAndroidVersionsAsync();
    Task<AndroidVersion> GetAndroidVersionByIdAsync(long id);
    Task<AndroidVersion> AddAndroidVersionAsync(AndroidVersion androidVersion);
    Task<AndroidVersion> UpdateAndroidVersionsAsync(long id, AndroidVersion androidVersion);
    Task<bool> UploadAndroidFileAsync(HttpContext httpContext);
    Task<bool> UploadPromotionFileAsync(HttpContext httpContext);
    Task<object> GetCountriesPricesAsync();
    Task<object> GetCountryPriceByIdAsync(long id);
    Task<IEnumerable<CountryPrice>> GetCountryPricesByCountryIdAsync(long id);
    Task<CountryPrice> AddCountryPriceAsync(HttpContext httpContext, CountryPrice countryPrice);
    Task<bool> DeleteCountriesPricesAsync(long id);
    Task<IEnumerable<Shift>> GetShiftsByShiftDateAsync(Shift shift);
    Task<SystemSetting> GetSystemSettingByIdAsync(long id);
    Task<SystemSetting> AddSystemSettingAsync(SystemSetting setting, HttpContext httpContext);
    Task<CityPrice> AddCityPriceAsync(CityPrice cityPrice, HttpContext httpContext);
    Task<object> GetCitiesPricesAsync();
    Task<object> GetCityPriceByIdAsync(long id);
    Task<IEnumerable<CityPrice>> GetCitiesPricesByCityIdAsync(long id);
    Task<bool> DeleteCityPriceByIdAsync(long id);
    Task<IEnumerable<RejectPerType>> GetRejectPerTypesAsync();
    Task<IEnumerable<IgnorPerType>> IgnorePerTypesAsync();
    Task<IEnumerable<PenaltyPerType>> PenaltyPerTypesAsync();
    Task<IEnumerable<Promotion>> GetPromotionsAsync();
    Task<Promotion> GetCurrentPromotionAsync();
    Task<Promotion> GetPromotionByIdAsync(long id);
    Task<IEnumerable<Promotion>> GetPromotionsPaginationAsync(FilterParameters parameters);
    Task<Promotion> AddPromotionAsync(Promotion promotion);
    Task<Promotion> UpdatePromotionAsync(long id, Promotion promotion);
    Task<bool> DeletePromotionById(long id);
    Task<object> PublishPromotionByIdAsync(long id);
    Task<IEnumerable<PromotionType>> PromotionTypesAsync();
    Task<PromotionType> GetPromotionTypeByIdAsync(long id);
    Task<PromotionType> AddPromotionTypeAsync(PromotionType promotionType);
    Task<PromotionType> UpdatePromotionTypeAsync(long id, PromotionType promotionType);
    Task<bool> DeletePromotionTypeAsync(long id);
    Task<bool> UploadAsync(HttpContext httpContext);
    Task<CaptainUserIgnoredPenalty> GetCurrentUserIgnoredRequestsPenaltyByCaptainUserAccountIdAsync(long userId);
    Task<CaptainUserRejectPenalty> GetCurrentUserRejectedRequestsPenaltyByCaptainUserAccountIdAsync(long userId);
    Task<IEnumerable<CaptainUserIgnoredPenalty>> GetUserIgnoredRequestsPenaltiesByCaptainUserAccountIdAsync(long userId);
    Task<IEnumerable<CaptainUserRejectPenalty>> GetUserRejectedRequestsPenaltiesByCaptainUserAccountIdAsync(long userId);
    Task<IEnumerable<PenaltyStatusType>> GetPeniltyStatusTypesAsync();
    Task<bool> DeleteSystemSettingAsync(long id);
    Task<bool> AddContactMessageAsync(ContactMessage contactMessage);
    Task<bool> ChangeUserStatusAsync(StatusAction statusAction, HttpContext httpContext);
    Task<bool> AddUserMessageHubTokenAsync(UserHubToken userHubToken);
    Task<bool> ForgotPasswordAsync(ResetPassword resetPassword);
    Task<object> SendFirebaseNotificationAsync(FBNotify fbNotify);

}

