using TreePorts.DTO;
using TreePorts.DTO.Records;

namespace TreePorts.Interfaces.Services;
public interface ICaptainService
{

    Task<IEnumerable<CaptainUser>> GetCaptainUsersAsync(CancellationToken cancellationToken);
    Task<CaptainUserVechicleResponse> GetCaptainUserByCaptainUserIdAsync(string captainUserId,CancellationToken cancellationToken);
    Task<CaptainUserVechicleResponse> GetCaptainUserAccountByCaptainUserAccountIdAsync(string captainUserAccountId,CancellationToken cancellationToken);
    Task<IEnumerable<CaptainUserAccount>> GetUsersPagingAsync(FilterParameters parameters,CancellationToken cancellationToken);
    Task<IEnumerable<CaptainUserAccount>> GetNewCaptainsUsersAsync(FilterParameters parameters,CancellationToken cancellationToken);
    Task<object> GetDirectionsMapAsync(string origin, string destination, string mode,CancellationToken cancellationToken);
    Task<CaptainUserResponse> AddCaptainAsync(HttpContext httpContext, CaptainUserDto captainUserDto,CancellationToken cancellationToken);
    Task<CaptainUserResponse> LoginAsync(LoginCaptainUserDto loginCaptain,CancellationToken cancellationToken);
    Task<bool> ChangePasswordAsync(DriverPhone driver, CancellationToken cancellationToken);
    Task<bool> UploadAsync(HttpContext httpContext,CancellationToken cancellationToken);
    Task<object> AcceptRegisterCaptainByIdAsync(string captainUserAccountId, HttpContext httpContext,CancellationToken cancellationToken);
    Task<object> UpdateCaptainUserAsync(string? captainUserAccountId, CaptainUserDto captainUserDto,CancellationToken cancellationToken);
    Task<bool> UpdateCaptainCurrentLocationAsync(CaptainUserCurrentLocation userCurrentLocation,CancellationToken cancellationToken);
    Task<bool> DeleteCaptainUserAccountAsync(string captainUserAccountId,CancellationToken cancellationToken);
    Task<object> GetOrdersPaymentsByCaptainUserAccountIdAsync(string? captainUserAccountId,CancellationToken cancellationToken);
    Task<object> GetBookkeepingPagingByCaptainUserAccountIdAsync(string? captainUserAccountId, FilterParameters parameters,CancellationToken cancellationToken);
    Task<IEnumerable<Bookkeeping>> GetBookkeepingByCaptainUserAccountIdAsync(string? captainUserAccountId,CancellationToken cancellationToken);
    Task<decimal> GetUntransferredBookkeepingByCaptainUserAccountIdAsync(string? captainUserAccountId,CancellationToken cancellationToken);
    Task<IEnumerable<Order>> GetAllOrdersAssignmentsByCaptainUserAccountIdAsync(string? captainUserAccountId, FilterParameters parameters,CancellationToken cancellationToken);
    Task<CaptainUserShift> AddCaptainUserShiftAsync(CaptainUserShift userShift,CancellationToken cancellationToken);
    Task<CaptainUserShift> DeleteCaptainUserShiftAsync(string? captainUserAccountId, long shiftId,CancellationToken cancellationToken);
    Task<CaptainUserShift> GetCaptainUsershiftAsync(string? captainUserAccountId, long shiftId,CancellationToken cancellationToken);
    Task<object> GetShiftsAndUserShiftsByDateAsync(string captainUserAccountId, Shift shift,CancellationToken cancellationToken);
    Task<CaptainUserActivity> CaptainUserActivitiesAsync(CaptainUserActivity userActivity,CancellationToken cancellationToken);
    //Task<object> ReportsAsync(FilterParameters reportParameters, HttpContext httpContext,CancellationToken cancellationToken);
    //Task<object> SearchAsync(FilterParameters parameters,CancellationToken cancellationToken);
    Task<object> ChartsAsync(CancellationToken cancellationToken);
    Task<object> CheckBonusPerMonthAsync(BonusCheckDto bonusCheckDto,CancellationToken cancellationToken);
    Task<object> CheckBonusPerYearAsync(BonusCheckDto bonusCheckDto,CancellationToken cancellationToken);
    Task<string> SendFirebaseNotificationAsync(FBNotify fbNotify,CancellationToken cancellationToken);
    Task<IEnumerable<NearCaptainUser>> GetCaptainsUsersNearToLocationAsync(Location location,CancellationToken cancellationToken);
}

