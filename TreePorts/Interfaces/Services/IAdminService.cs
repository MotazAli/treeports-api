using TreePorts.DTO;

namespace TreePorts.Interfaces.Services;
public interface IAdminService
{
    Task<IEnumerable<AdminUser>> GetAdminsUsersAsync();
    Task<AdminUser> GetAdminUserByIdAsync(long id);
    Task<IEnumerable<AdminUser>> GetAdminsUsersPaginationAsync(FilterParameters parameters);

    Task<object> AddAdminUserAsync(AdminUser user);
    Task<AdminUser> UpdateAdminUserAsync(long? id, AdminUser user);
    Task<bool> DeleteAdminUserAsync(long id);
    Task<AdminUserAccount> Login(LoginUser user);
    Task<bool> UploadFileAsync(HttpContext httpContext);

    Task<IEnumerable<AdminResponse>> SearchAsync(FilterParameters parameters);

    Task<object> ReportAsync(FilterParameters reportParameters);
    Task<string> SendFirebaseNotification(FBNotify fbNotify);

}
