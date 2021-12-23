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
        Task<List<AdminUser>> GetAdminsUsersAsync();

        Task<List<AdminUser>> GetAdminsUsersPaginationAsync(int skip , int take);
        Task<AdminUser> GetAdminUserByIdAsync(long id);
        Task<AdminUser> GetAdminUserByEmailAsync(string email);
        Task<List<AdminUser>> GetAdminUserByAsync(Expression<Func<AdminUser, bool>> predicate);
        Task<List<AdminUserAccount>> GetAdminUserAccountByAsync(Expression<Func<AdminUserAccount, bool>> predicate);
        Task<AdminUserAccount> GetAdminUserAccountByEmailAsync(string email);
        Task<AdminUserAccount> GetAdminUserAccountByIdAsync(long id);
        Task<AdminUserAccount> GetAdminUserAccountByAdminUserIdAsync(long id);
        Task<AdminUser> InsertAdminUserAsync(AdminUser user);
        Task<AdminUser> UpdateAdminUserAsync(AdminUser user);
        Task<AdminUser> UpdateAdminUserImageAsync(AdminUser user);
        Task<AdminUserAccount> UpdateAdminUserAccountAsync(AdminUserAccount account);
        Task<AdminUser> DeleteAdminUserAsync(long id);

        Task<List<AdminCurrentStatus>> GetAdminsCurrentStatusAllAsync();
        Task<AdminCurrentStatus> GetAdminCurrentStatusByIdAsync(long id);
        Task<List<AdminCurrentStatus>> GetAdminCurrentStatusByAsync(Expression<Func<AdminCurrentStatus, bool>> predicate);
        Task<AdminCurrentStatus> InsertAdminCurrentStatusAsync(AdminCurrentStatus adminCurrentStatus);
        Task<AdminCurrentStatus> UpdateAdminCurrentStatusAsync(AdminCurrentStatus adminCurrentStatus);
        Task<AdminCurrentStatus> DeleteAdminCurrentStatusAsync(long id);


        Task<List<AdminUserMessageHub>> GetAllAdminUsersMessageHubAsync();
        Task<AdminUserMessageHub> GetAdminUserMessageHubByIdAsync(long id);
        Task<List<AdminUserMessageHub>> GetAdminUserMessageHubByAsync(Expression<Func<AdminUserMessageHub, bool>> predicate);
        Task<AdminUserMessageHub> InsertAdminUserMessageHubAsync(AdminUserMessageHub adminUserMessageHub);
        Task<AdminUserMessageHub> UpdateAdminUserMessageHubAsync(AdminUserMessageHub adminUserMessageHub);

        IQueryable<AdminUser> GetByQuerable(Expression<Func<AdminUser, bool>> predicate);
        IQueryable<AdminUser> GetAllQuerable();
    }
}
