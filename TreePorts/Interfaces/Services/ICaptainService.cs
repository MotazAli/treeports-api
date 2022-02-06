using TreePorts.DTO;

namespace TreePorts.Interfaces.Services;
public interface ICaptainService
{

    Task<IEnumerable<CaptainUser>> GetUsersAsync();
    Task<CaptainUser> GetUserByIdAsync(long id);
    Task<IEnumerable<CaptainUserAccount>> GetUsersPagingAsync(FilterParameters parameters);
    Task<IEnumerable<CaptainUserAccount>> GetNewCaptainsUsersAsync(FilterParameters parameters);
    Task<object> GetDirectionsMapAsync(string origin, string destination, string mode);
    Task<long> AddCaptainAsync(HttpContext httpContext, CaptainUser user);
    Task<object> LoginAsync(LoginDriver driver);
    Task<bool> ChangePasswordAsync(DriverPhone driver);
    Task<bool> UploadAsync(HttpContext httpContext);
    Task<object> AcceptRegisterCaptainByIdAsync(long id, HttpContext httpContext);
    Task<object> UpdateCaptainAsync(long id, CaptainUser user);
    Task<bool> UpdateCaptainCurrentLocationAsync(CaptainUserCurrentLocation userCurrentLocation);
    Task<bool> DeleteCaptainUserAccountAsync(long id);
    Task<object> GetOrdersPaymentsByCaptainUserAccountIdAsync(long id);
    Task<object> GetBookkeepingPagingByCaptainUserAccountIdAsync(long id, FilterParameters parameters);
    Task<IEnumerable<Bookkeeping>> GetBookkeepingByCaptainUserAccountIdAsync(long id);
    Task<decimal> GetUntransferredBookkeepingByCaptainUserAccountIdAsync(long id);
    Task<IEnumerable<Order>> GetAllOrdersAssignmentsByCaptainUserAccountIdAsync(long id, FilterParameters parameters);
    Task<CaptainUserShift> AddCaptainUserShiftAsync(CaptainUserShift userShift);
    Task<CaptainUserShift> DeleteCaptainUserShiftAsync(long userId, long shiftId);
    Task<CaptainUserShift> GetCaptainUsershiftAsync(long userId, long shiftId);
    Task<object> GetShiftsAndUserShiftsByDateAsync(long id, Shift shift);
    Task<CaptainUserActivity> CaptainUserActivitiesAsync(CaptainUserActivity userActivity);
    Task<object> ReportsAsync(FilterParameters reportParameters, HttpContext httpContext);
    Task<object> SearchAsync(FilterParameters parameters);
    Task<object> ChartsAsync();
    Task<object> CheckBonusPerMonthAsync(BonusCheckDto bonusCheckDto);
    Task<object> CheckBonusPerYearAsync(BonusCheckDto bonusCheckDto);
    Task<string> SendFirebaseNotificationAsync(FBNotify fbNotify);
    Task<IEnumerable<CaptainUser>> GetCaptainsUsersNearToLocation(Location location);
}

