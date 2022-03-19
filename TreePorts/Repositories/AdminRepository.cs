using System.Linq.Expressions;
using TreePorts.Utilities;

namespace TreePorts.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly TreePortsDBContext _context;

        public AdminRepository(TreePortsDBContext context)
        {
            _context = context;
        }
        public async Task<AdminUser?> DeleteAdminUserAsync(string? id, CancellationToken cancellationToken )
        {
            var userAccount = await _context.AdminUserAccounts.FirstOrDefaultAsync(u => u.AdminUserId == id, cancellationToken);
            if (userAccount == null) return null;

            userAccount.IsDeleted = true;
            _context.Entry<AdminUserAccount>(userAccount).State = EntityState.Modified;
            var user = await _context.AdminUsers.FirstOrDefaultAsync(u => u.Id == id,cancellationToken);
            return user;
        }

        public async Task<AdminCurrentStatus?> DeleteAdminCurrentStatusAsync(string? id, CancellationToken cancellationToken)
        {
            var oldStatus = await _context.AdminCurrentStatuses.FirstOrDefaultAsync(u => u.AdminUserAccountId == id,cancellationToken);
            if (oldStatus == null) return null;

            _context.AdminCurrentStatuses.Remove(oldStatus);
            return oldStatus;
        }

        public async Task<List<AdminUserMessageHub>> GetAllAdminUsersMessageHubAsync(CancellationToken cancellationToken)
        {
            return await _context.AdminUserMessageHubs.ToListAsync(cancellationToken);
        }

        public async Task<AdminUserMessageHub?> GetAdminUserMessageHubByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.AdminUserMessageHubs.FirstOrDefaultAsync(h => h.Id == id, cancellationToken);
        }

        public async Task<List<AdminUserMessageHub>> GetAdminUserMessageHubByAsync(Expression<Func<AdminUserMessageHub, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.AdminUserMessageHubs.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<AdminUserMessageHub> InsertAdminUserMessageHubAsync(AdminUserMessageHub adminUserMessageHub, CancellationToken cancellationToken)
        {
            var oldUserHub = await _context.AdminUserMessageHubs.FirstOrDefaultAsync(h => h.AdminUserAccountId == adminUserMessageHub.AdminUserAccountId, cancellationToken);
            if (oldUserHub != null && oldUserHub.Id > 0)
            {
                oldUserHub.ConnectionId = adminUserMessageHub.ConnectionId;
                oldUserHub.ModificationDate = DateTime.Now;
                _context.Entry<AdminUserMessageHub>(oldUserHub).State = EntityState.Modified;
                return oldUserHub;
            }
            else
            {
                //SupportUserMessageHub newHub = new UserMessageHub() { UserId = ID, ConnectionId = ConnectionID, CreationDate = DateTime.Now, CreatedBy = 1 };
                adminUserMessageHub.CreationDate = DateTime.Now;
                var insertResult = await _context.AdminUserMessageHubs.AddAsync(adminUserMessageHub,cancellationToken);
                return insertResult.Entity;
            }
        }

        public async Task<AdminUserMessageHub?> UpdateAdminUserMessageHubAsync(AdminUserMessageHub adminUserMessageHub, CancellationToken cancellationToken)
        {
            var oldUserHub = await _context.AdminUserMessageHubs.FirstOrDefaultAsync(h => h.AdminUserAccountId == adminUserMessageHub.AdminUserAccountId,cancellationToken);
            if (oldUserHub == null ) return null;

            oldUserHub.AdminUserAccountId = adminUserMessageHub.AdminUserAccountId;
            oldUserHub.ConnectionId = adminUserMessageHub.ConnectionId;
            oldUserHub.ModificationDate = DateTime.Now;
            _context.Entry<AdminUserMessageHub>(oldUserHub).State = EntityState.Modified;
            return oldUserHub;
        }

        public async Task<List<AdminCurrentStatus>> GetAdminCurrentStatusByAsync(Expression<Func<AdminCurrentStatus, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.AdminCurrentStatuses.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<AdminCurrentStatus?> GetAdminCurrentStatusByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.AdminCurrentStatuses.FirstOrDefaultAsync(a => a.Id == id,cancellationToken);
        }

        public async Task<List<AdminCurrentStatus>> GetAdminsCurrentStatusAllAsync(CancellationToken cancellationToken)
        {
            return await _context.AdminCurrentStatuses.ToListAsync(cancellationToken);
        }

        public async Task<List<AdminUser>> GetAdminsUsersAsync(CancellationToken cancellationToken)
        {
            return await _context.AdminUsers.ToListAsync(cancellationToken);
        }

        public async Task<List<AdminUser>> GetAdminUserByAsync(Expression<Func<AdminUser, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.AdminUsers.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<List<AdminUserAccount>> GetAdminUserAccountByAsync(Expression<Func<AdminUserAccount, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.AdminUserAccounts.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<AdminUser?> GetAdminUserByIdAsync(string? id, CancellationToken cancellationToken)
        {
            return await _context.AdminUsers.FirstOrDefaultAsync(u => u.Id == id,cancellationToken);
        }

        public async Task<AdminUserAccount?> GetAdminUserAccountByIdAsync(string? id, CancellationToken cancellationToken) {
            return await _context.AdminUserAccounts.FirstOrDefaultAsync(u => u.Id == id,cancellationToken);
        }

        public async Task<AdminUserAccount?> GetAdminUserAccountByAdminUserIdAsync(string? id, CancellationToken cancellationToken)
        {
            return await _context.AdminUserAccounts.FirstOrDefaultAsync(u => u.Id == id,cancellationToken);
        }

        public async Task<AdminUser> InsertAdminUserAsync(AdminUser user, CancellationToken cancellationToken)
        {
            user.Id = Guid.NewGuid().ToString();
            user.CreationDate = DateTime.Now;
            var result = await _context.AdminUsers.AddAsync(user,cancellationToken);
            return result.Entity;
        }

        public async Task<AdminUserAccount> InsertAdminUserAccountAsync(AdminUserAccount userAccount, CancellationToken cancellationToken)
        {
            userAccount.Id = Guid.NewGuid().ToString();
            userAccount.CreationDate = DateTime.Now;
            var result = await _context.AdminUserAccounts.AddAsync(userAccount,cancellationToken);
            return result.Entity;
        }

        public async Task<AdminCurrentStatus> InsertAdminCurrentStatusAsync(AdminCurrentStatus adminCurrentStatus, CancellationToken cancellationToken)
        {
            var oldStatus = await _context.AdminCurrentStatuses.FirstOrDefaultAsync(a => a.AdminUserAccountId == adminCurrentStatus.AdminUserAccountId && a.IsCurrent == true, cancellationToken);
            if (oldStatus != null)
            {
                oldStatus.IsCurrent = false;
                oldStatus.ModifiedBy = adminCurrentStatus.ModifiedBy;
                oldStatus.ModificationDate = DateTime.Now;
                _context.Entry<AdminCurrentStatus>(oldStatus).State = EntityState.Modified;

            }
            adminCurrentStatus.IsCurrent = true;
            adminCurrentStatus.CreationDate = DateTime.Now;
            var insertResult = await _context.AdminCurrentStatuses.AddAsync(adminCurrentStatus,cancellationToken);
            return insertResult.Entity;
        }

        public async Task<AdminUser?> UpdateAdminUserAsync(AdminUser user, CancellationToken cancellationToken)
        {
            var oldUser = await _context.AdminUsers.FirstOrDefaultAsync(u => u.Id == user.Id,cancellationToken);
            if (oldUser == null) return null;


            

            oldUser.FirstName = user.FirstName ?? oldUser.FirstName;
            oldUser.LastName = user.LastName ?? oldUser.LastName;
            oldUser.NationalNumber = user.NationalNumber ?? oldUser.NationalNumber;
            oldUser.CountryId = user.CountryId ?? oldUser.CountryId;
            oldUser.CityId = user.CityId ?? oldUser.CityId;
            oldUser.Address = user.Address ?? oldUser.Address;
            oldUser.Gender = user.Gender ?? oldUser.Gender;
            oldUser.BirthDate = user.BirthDate ?? oldUser.BirthDate;
            oldUser.Mobile = user.Mobile ?? oldUser.Mobile;
            oldUser.ResidenceExpireDate = user.ResidenceExpireDate ?? oldUser.ResidenceExpireDate;
            oldUser.ResidenceCountryId = user.ResidenceCountryId ?? oldUser.ResidenceCountryId;
            oldUser.ResidenceCityId = user.ResidenceCityId ?? oldUser.ResidenceCityId;
            oldUser.PersonalImage = user.PersonalImage ?? oldUser.PersonalImage;
            oldUser.ModifiedBy = user.ModifiedBy ?? oldUser.ModifiedBy;
            oldUser.CreatedBy = user.CreatedBy ?? oldUser.CreatedBy;
            oldUser.CreationDate = user.CreationDate ?? oldUser.CreationDate;
            oldUser.ModificationDate = DateTime.Now;

            /*if (user.AdminUserAccounts?.Count > 0)
                await UpdateAdminUserAccountAsync(user.AdminUserAccounts?.FirstOrDefault());*/

            _context.Entry<AdminUser>(oldUser).State = EntityState.Modified;
            return oldUser;
        }

        public async Task<AdminUser?> UpdateAdminUserImageAsync(AdminUser user, CancellationToken cancellationToken)
        {
            var oldUser = await _context.AdminUsers.FirstOrDefaultAsync(u => u.Id == user.Id,cancellationToken);
            if (oldUser == null) return null;

            oldUser.PersonalImage = user.PersonalImage ?? oldUser.PersonalImage;
            oldUser.ModifiedBy = user.ModifiedBy ?? oldUser.ModifiedBy;
            oldUser.ModificationDate = DateTime.Now;

            _context.Entry<AdminUser>(oldUser).State = EntityState.Modified;
            return oldUser;
        }


        private async Task<AdminUserAccount?> UpdateAdminUserAccountEmailProperty(AdminUserAccount adminUserAccount, CancellationToken cancellationToken) {

            var targetAdminUserAccount = await _context.AdminUserAccounts.FirstOrDefaultAsync(u => u.Id == adminUserAccount.Id,cancellationToken);
            if (targetAdminUserAccount == null) return null;

            targetAdminUserAccount.Email = targetAdminUserAccount.Email;
            _context.Entry<AdminUserAccount>(targetAdminUserAccount).State = EntityState.Modified;
            return targetAdminUserAccount;

        }

        public async Task<AdminUserAccount?> UpdateAdminUserAccountAsync(AdminUserAccount account, CancellationToken cancellationToken)
        {
            var oldAccount = await _context.AdminUserAccounts.FirstOrDefaultAsync(a => a.Id == account.Id,cancellationToken);
            if (oldAccount == null) return null;

            oldAccount.AdminTypeId = account.AdminTypeId ?? oldAccount.AdminTypeId;
            oldAccount.StatusTypeId = account.StatusTypeId ?? oldAccount.StatusTypeId;
            oldAccount.Email = account.Email ?? oldAccount.Email;
            oldAccount.PasswordHash = account.PasswordHash ?? oldAccount.PasswordHash;
            oldAccount.PasswordSalt = account.PasswordSalt ?? oldAccount.PasswordSalt;
            oldAccount.Token = account.Token ?? oldAccount.Token;
            oldAccount.ModifiedBy = account.ModifiedBy ?? oldAccount.ModifiedBy;
            oldAccount.IsDeleted = account.IsDeleted;
            oldAccount.CreatedBy = account.CreatedBy ?? oldAccount.CreatedBy;
            oldAccount.CreationDate = account.CreationDate ?? oldAccount.CreationDate;
            oldAccount.ModificationDate = DateTime.Now;
            _context.Entry<AdminUserAccount>(oldAccount).State = EntityState.Modified;

            return oldAccount;
        }

        public async Task<AdminCurrentStatus?> UpdateAdminUserAccountCurrentStatusAsync(AdminCurrentStatus adminCurrentStatus,CancellationToken cancellationToken)
        {
            var oldStatus = await _context.AdminCurrentStatuses.FirstOrDefaultAsync(a => a.Id == adminCurrentStatus.Id, cancellationToken);
            if (oldStatus == null) throw new NotFoundException("User not found");

            oldStatus.AdminUserAccountId = adminCurrentStatus.AdminUserAccountId;
            oldStatus.StatusTypeId = adminCurrentStatus.StatusTypeId;
            oldStatus.IsCurrent = adminCurrentStatus.IsCurrent;
            oldStatus.ModifiedBy = adminCurrentStatus.ModifiedBy;
            oldStatus.ModificationDate = DateTime.Now;
            _context.Entry<AdminCurrentStatus>(oldStatus).State = EntityState.Modified;
            return oldStatus;
        }


        public async Task<AdminUserAccount?> GetAdminUserAccountByEmailAsync(string? email, CancellationToken cancellationToken) {
            return await _context.AdminUserAccounts.FirstOrDefaultAsync(u => u.Email == email,cancellationToken);
        }


        

        public IQueryable<AdminUser> GetByQuerable(Expression<Func<AdminUser, bool>> predicate)
		{
            return _context.AdminUsers.Where(predicate);
        }

        public async Task<List<AdminUser>> GetAdminsUsersPaginationAsync(int skip, int take, CancellationToken cancellationToken) 
        {
            return await _context.AdminUsers.OrderByDescending(a => a.CreationDate).Skip(skip).Take(take).ToListAsync(cancellationToken);
        }



        public IQueryable<AdminUser> GetAllQuerable()
        {
            return _context.AdminUsers
                .OrderByDescending(a => a.CreationDate);
                //.Include(a => a.Country)
                //.Include(a => a.City);

        }
    }
}
