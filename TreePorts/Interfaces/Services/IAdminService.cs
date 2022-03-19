using TreePorts.DTO;
using TreePorts.DTO.Records;

namespace TreePorts.Interfaces.Services;
public interface IAdminService
{
    Task<IEnumerable<AdminUser>> GetAdminsUsersAsync(CancellationToken cancellationToken);
    Task<AdminUserResponse?> GetAdminUserByIdAsync(string id, CancellationToken cancellationToken);
    Task<IEnumerable<AdminUser>> GetAdminsUsersPaginationAsync(FilterParameters parameters, CancellationToken cancellationToken);

    Task<AdminUserResponse?> AddAdminUserAsync(AdminUserDto user, CancellationToken cancellationToken);
    Task<AdminUserResponse> UpdateAdminUserAsync(string? adminUserAccountId, AdminUserDto adminUserDto, CancellationToken cancellationToken);
    Task<bool> DeleteAdminUserAsync(string? id, CancellationToken cancellationToken);
    Task<AdminUserResponse> Login(LoginUserDto loginUserDto, CancellationToken cancellationToken);
    Task<bool> UploadFileAsync(HttpContext httpContext, CancellationToken cancellationToken);

    Task<IEnumerable<AdminResponse>> SearchAsync(FilterParameters parameters, CancellationToken cancellationToken);

    //Task<object> ReportAsync(FilterParameters reportParameters);
    Task<string> SendFirebaseNotification(FBNotify fbNotify, CancellationToken cancellationToken);

}
