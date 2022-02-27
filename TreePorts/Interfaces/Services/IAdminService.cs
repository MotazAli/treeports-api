using TreePorts.DTO;
using TreePorts.DTO.Records;

namespace TreePorts.Interfaces.Services;
public interface IAdminService
{
    Task<IEnumerable<AdminUser>> GetAdminsUsersAsync();
    Task<AdminUserResponse?> GetAdminUserByIdAsync(string id);
    Task<IEnumerable<AdminUser>> GetAdminsUsersPaginationAsync(FilterParameters parameters);

    Task<AdminUserResponse?> AddAdminUserAsync(AdminUserDto user);
    Task<AdminUserResponse> UpdateAdminUserAsync(string? adminUserAccountId, AdminUserDto adminUserDto);
    Task<bool> DeleteAdminUserAsync(string? id);
    Task<AdminUserResponse> Login(LoginUserDto loginUserDto);
    Task<bool> UploadFileAsync(HttpContext httpContext);

    Task<IEnumerable<AdminResponse>> SearchAsync(FilterParameters parameters);

    //Task<object> ReportAsync(FilterParameters reportParameters);
    Task<string> SendFirebaseNotification(FBNotify fbNotify);

}
