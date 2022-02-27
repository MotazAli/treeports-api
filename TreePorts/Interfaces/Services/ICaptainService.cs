using TreePorts.DTO;
using TreePorts.DTO.Records;

namespace TreePorts.Interfaces.Services;
public interface ICaptainService
{

    Task<IEnumerable<CaptainUser>> GetCaptainUsersAsync();
    Task<CaptainUserVechicleResponse> GetCaptainUserByCaptainUserIdAsync(string captainUserId);
    Task<CaptainUserVechicleResponse> GetCaptainUserAccountByCaptainUserAccountIdAsync(string captainUserAccountId);
    Task<IEnumerable<CaptainUserAccount>> GetUsersPagingAsync(FilterParameters parameters);
    Task<IEnumerable<CaptainUserAccount>> GetNewCaptainsUsersAsync(FilterParameters parameters);
    Task<object> GetDirectionsMapAsync(string origin, string destination, string mode);
    Task<CaptainUserResponse> AddCaptainAsync(HttpContext httpContext, CaptainUserDto captainUserDto);
    Task<CaptainUserResponse> LoginAsync(LoginCaptainUserDto loginCaptain);
    Task<bool> ChangePasswordAsync(DriverPhone driver);
    Task<bool> UploadAsync(HttpContext httpContext);
    Task<object> AcceptRegisterCaptainByIdAsync(string captainUserAccountId, HttpContext httpContext);
    Task<object> UpdateCaptainUserAsync(string? captainUserAccountId, CaptainUserDto captainUserDto);
    Task<bool> UpdateCaptainCurrentLocationAsync(CaptainUserCurrentLocation userCurrentLocation);
    Task<bool> DeleteCaptainUserAccountAsync(string captainUserAccountId);
    Task<object> GetOrdersPaymentsByCaptainUserAccountIdAsync(string? captainUserAccountId);
    Task<object> GetBookkeepingPagingByCaptainUserAccountIdAsync(string? captainUserAccountId, FilterParameters parameters);
    Task<IEnumerable<Bookkeeping>> GetBookkeepingByCaptainUserAccountIdAsync(string? captainUserAccountId);
    Task<decimal> GetUntransferredBookkeepingByCaptainUserAccountIdAsync(string? captainUserAccountId);
    Task<IEnumerable<Order>> GetAllOrdersAssignmentsByCaptainUserAccountIdAsync(string? captainUserAccountId, FilterParameters parameters);
    Task<CaptainUserShift> AddCaptainUserShiftAsync(CaptainUserShift userShift);
    Task<CaptainUserShift> DeleteCaptainUserShiftAsync(string? captainUserAccountId, long shiftId);
    Task<CaptainUserShift> GetCaptainUsershiftAsync(string? captainUserAccountId, long shiftId);
    Task<object> GetShiftsAndUserShiftsByDateAsync(string captainUserAccountId, Shift shift);
    Task<CaptainUserActivity> CaptainUserActivitiesAsync(CaptainUserActivity userActivity);
    //Task<object> ReportsAsync(FilterParameters reportParameters, HttpContext httpContext);
    //Task<object> SearchAsync(FilterParameters parameters);
    Task<object> ChartsAsync();
    Task<object> CheckBonusPerMonthAsync(BonusCheckDto bonusCheckDto);
    Task<object> CheckBonusPerYearAsync(BonusCheckDto bonusCheckDto);
    Task<string> SendFirebaseNotificationAsync(FBNotify fbNotify);
    Task<IEnumerable<NearCaptainUser>> GetCaptainsUsersNearToLocationAsync(Location location);
}

