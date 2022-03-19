using TreePorts.DTO;
using TreePorts.DTO.Records;

namespace TreePorts.Interfaces.Services;
public interface ISystemService
{
    Task<SystemSetting?> GetSystemSettingAsync(CancellationToken cancellationToken);
    Task<IEnumerable<Vehicle>> GetVehiclesAsync(CancellationToken cancellationToken);
    Task<IEnumerable<BoxType>> GetBoxTypesAsync(CancellationToken cancellationToken);   
    Task<IEnumerable<Shift>> GetShiftsAsync(CancellationToken cancellationToken);
    Task<IEnumerable<AndroidVersion>> GetAndroidVersionsPagingAsync(FilterParameters parameters,CancellationToken cancellationToken);
    Task<AndroidVersion?> GetCurrentAndroidVersionAsync(CancellationToken cancellationToken);
    Task<IEnumerable<AndroidVersion>> GetAndroidVersionsAsync(CancellationToken cancellationToken);
    Task<AndroidVersion?> GetAndroidVersionByIdAsync(long id,CancellationToken cancellationToken);
    Task<AndroidVersion> AddAndroidVersionAsync(AndroidVersion androidVersion,CancellationToken cancellationToken);
    Task<AndroidVersion?> UpdateAndroidVersionsAsync(long id, AndroidVersion androidVersion,CancellationToken cancellationToken);
    Task<bool> UploadAndroidFileAsync(HttpContext httpContext,CancellationToken cancellationToken);
    Task<bool> UploadPromotionFileAsync(HttpContext httpContext,CancellationToken cancellationToken);
    Task<object> GetCountriesPricesAsync(CancellationToken cancellationToken);
    Task<object> GetCountryPriceByIdAsync(long id,CancellationToken cancellationToken);
    Task<IEnumerable<CountryPrice>> GetCountryPricesByCountryIdAsync(long id,CancellationToken cancellationToken);
    Task<CountryPrice> AddCountryPriceAsync(HttpContext httpContext, CountryPrice countryPrice,CancellationToken cancellationToken);
    Task<bool> DeleteCountriesPricesAsync(long id,CancellationToken cancellationToken);
    Task<IEnumerable<Shift>> GetShiftsByShiftDateAsync(Shift shift,CancellationToken cancellationToken);
    Task<SystemSetting?> GetSystemSettingByIdAsync(long id,CancellationToken cancellationToken);
    Task<SystemSetting> AddSystemSettingAsync(SystemSetting setting, HttpContext httpContext,CancellationToken cancellationToken);
    Task<CityPrice> AddCityPriceAsync(CityPrice cityPrice, HttpContext httpContext,CancellationToken cancellationToken);
    Task<object> GetCitiesPricesAsync(CancellationToken cancellationToken);
    Task<object> GetCityPriceByIdAsync(long id,CancellationToken cancellationToken);
    Task<IEnumerable<CityPrice>> GetCitiesPricesByCityIdAsync(long id,CancellationToken cancellationToken);
    Task<bool> DeleteCityPriceByIdAsync(long id,CancellationToken cancellationToken);
    //Task<IEnumerable<RejectPerType>> GetRejectPerTypesAsync(,CancellationToken cancellationToken);
    Task<IEnumerable<IgnorPerType>> IgnorePerTypesAsync(CancellationToken cancellationToken);
    Task<IEnumerable<PenaltyPerType>> PenaltyPerTypesAsync(CancellationToken cancellationToken);
    Task<IEnumerable<Promotion>> GetPromotionsAsync(CancellationToken cancellationToken);
    Task<Promotion?> GetCurrentPromotionAsync(CancellationToken cancellationToken);
    Task<Promotion?> GetPromotionByIdAsync(long id,CancellationToken cancellationToken);
    Task<IEnumerable<Promotion>> GetPromotionsPaginationAsync(FilterParameters parameters,CancellationToken cancellationToken);
    Task<Promotion> AddPromotionAsync(Promotion promotion,CancellationToken cancellationToken);
    Task<Promotion?> UpdatePromotionAsync(long id, Promotion promotion,CancellationToken cancellationToken);
    Task<bool> DeletePromotionById(long id,CancellationToken cancellationToken);
    Task<object> PublishPromotionByIdAsync(long id,CancellationToken cancellationToken);
    Task<IEnumerable<PromotionType>> PromotionTypesAsync(CancellationToken cancellationToken);
    Task<PromotionType?> GetPromotionTypeByIdAsync(long id,CancellationToken cancellationToken);
    Task<PromotionType> AddPromotionTypeAsync(PromotionType promotionType,CancellationToken cancellationToken);
    Task<PromotionType?> UpdatePromotionTypeAsync(long id, PromotionType promotionType,CancellationToken cancellationToken);
    Task<bool> DeletePromotionTypeAsync(long id,CancellationToken cancellationToken);
    Task<bool> UploadAsync(HttpContext httpContext,CancellationToken cancellationToken);
    Task<CaptainUserIgnoredPenalty?> GetCurrentUserIgnoredRequestsPenaltyByCaptainUserAccountIdAsync(string captainUserAccountId,CancellationToken cancellationToken);
    //Task<CaptainUserRejectPenalty> GetCurrentUserRejectedRequestsPenaltyByCaptainUserAccountIdAsync(string captainUserAccountId,CancellationToken cancellationToken);
    Task<IEnumerable<CaptainUserIgnoredPenalty>> GetUserIgnoredRequestsPenaltiesByCaptainUserAccountIdAsync(string captainUserAccountId,CancellationToken cancellationToken);
    //Task<IEnumerable<CaptainUserRejectPenalty>> GetUserRejectedRequestsPenaltiesByCaptainUserAccountIdAsync(string captainUserAccountId,CancellationToken cancellationToken);
    Task<IEnumerable<PenaltyStatusType>> GetPeniltyStatusTypesAsync(CancellationToken cancellationToken);
    Task<bool> DeleteSystemSettingAsync(long id,CancellationToken cancellationToken);
    Task<bool> AddContactMessageAsync(ContactMessage contactMessage,CancellationToken cancellationToken);
    Task<bool> ChangeUserStatusAsync(StatusAction statusAction, HttpContext httpContext,CancellationToken cancellationToken);
    Task<bool> AddUserMessageHubTokenAsync(UserHubToken userHubToken,CancellationToken cancellationToken);
    Task<bool> ForgotPasswordAsync(ResetPassword resetPassword, CancellationToken cancellationToken);
    Task<object> SendFirebaseNotificationAsync(FBNotify fbNotify,CancellationToken cancellationToken);

}

