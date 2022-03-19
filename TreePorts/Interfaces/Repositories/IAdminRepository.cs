using TreePorts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TreePorts.Interfaces.Repositories
{
    public interface IAdminRepository
    {
        Task<List<AdminUser>> GetAdminsUsersAsync(CancellationToken cancellationToken);

        Task<List<AdminUser>> GetAdminsUsersPaginationAsync(int skip , int take,CancellationToken cancellationToken);
        Task<AdminUser?> GetAdminUserByIdAsync(string? id,CancellationToken cancellationToken);
        
        Task<List<AdminUser>> GetAdminUserByAsync(Expression<Func<AdminUser, bool>> predicate,CancellationToken cancellationToken);
        Task<List<AdminUserAccount>> GetAdminUserAccountByAsync(Expression<Func<AdminUserAccount, bool>> predicate,CancellationToken cancellationToken);
        Task<AdminUserAccount?> GetAdminUserAccountByEmailAsync(string? email,CancellationToken cancellationToken);
        Task<AdminUserAccount?> GetAdminUserAccountByIdAsync(string? id,CancellationToken cancellationToken);
        Task<AdminUserAccount?> GetAdminUserAccountByAdminUserIdAsync(string? id,CancellationToken cancellationToken);
        Task<AdminUserAccount> InsertAdminUserAccountAsync(AdminUserAccount userAccount,CancellationToken cancellationToken);
        Task<AdminUser> InsertAdminUserAsync(AdminUser user,CancellationToken cancellationToken);
        Task<AdminUser?> UpdateAdminUserAsync(AdminUser user,CancellationToken cancellationToken);
        Task<AdminUser?> UpdateAdminUserImageAsync(AdminUser user,CancellationToken cancellationToken);
        Task<AdminUserAccount?> UpdateAdminUserAccountAsync(AdminUserAccount account,CancellationToken cancellationToken);
        Task<AdminUser?> DeleteAdminUserAsync(string? id,CancellationToken cancellationToken);

        Task<List<AdminCurrentStatus>> GetAdminsCurrentStatusAllAsync(CancellationToken cancellationToken);
        Task<AdminCurrentStatus?> GetAdminCurrentStatusByIdAsync(long id,CancellationToken cancellationToken);
        Task<List<AdminCurrentStatus>> GetAdminCurrentStatusByAsync(Expression<Func<AdminCurrentStatus, bool>> predicate,CancellationToken cancellationToken);
        Task<AdminCurrentStatus> InsertAdminCurrentStatusAsync(AdminCurrentStatus adminCurrentStatus,CancellationToken cancellationToken);
        Task<AdminCurrentStatus?> UpdateAdminUserAccountCurrentStatusAsync(AdminCurrentStatus adminCurrentStatus,CancellationToken cancellationToken);
        Task<AdminCurrentStatus?> DeleteAdminCurrentStatusAsync(string? id,CancellationToken cancellationToken);


        Task<List<AdminUserMessageHub>> GetAllAdminUsersMessageHubAsync(CancellationToken cancellationToken);
        Task<AdminUserMessageHub?> GetAdminUserMessageHubByIdAsync(long id,CancellationToken cancellationToken);
        Task<List<AdminUserMessageHub>> GetAdminUserMessageHubByAsync(Expression<Func<AdminUserMessageHub, bool>> predicate,CancellationToken cancellationToken);
        Task<AdminUserMessageHub> InsertAdminUserMessageHubAsync(AdminUserMessageHub adminUserMessageHub,CancellationToken cancellationToken);
        Task<AdminUserMessageHub?> UpdateAdminUserMessageHubAsync(AdminUserMessageHub adminUserMessageHub,CancellationToken cancellationToken);

        IQueryable<AdminUser> GetByQuerable(Expression<Func<AdminUser, bool>> predicate);
        IQueryable<AdminUser> GetAllQuerable();
    }
}
