using Microsoft.EntityFrameworkCore;
using TreePorts.DTO;
using TreePorts.Interfaces.Repositories;
using TreePorts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TreePorts.Repositories
{
    public class SupportRepository : ISupportRepository
    {
        private readonly TreePortsDBContext _context;

        public SupportRepository(TreePortsDBContext context)
        {
            _context = context;
        }


        public async Task<SupportUser> GetSupportUserByEmailAsync(string email) 
        {
            return await _context.SupportUsers.FirstOrDefaultAsync( s => s.Email == email);
        }

        public async Task<SupportUserAccount> GetSupportUserAccountByEmailAsync(string email) {
            return await _context.SupportUserAccounts.FirstOrDefaultAsync(s => s.Email == email);
        }

        public async Task<SupportUser> DeleteSupportUserAsync(long id)
        {
            var userAccount = await this.GetSupportUserAccountBySupportUserIdAsync(id); // _context.SupportUserAccounts.Where(u => u.SupportUserId == ID).FirstOrDefaultAsync();
            if (userAccount == null) return null;// throw new NotFoundException("User not found");

            userAccount.IsDeleted = true;
            _context.Entry<SupportUserAccount>(userAccount).State = EntityState.Modified;
            var user = await _context.SupportUsers.Where(u => u.Id == id).FirstOrDefaultAsync();
            return user;
        }


        public async Task<SupportUserAccount> GetSupportUserAccountBySupportUserIdAsync(long id) 
        {
            return  await  _context.SupportUserAccounts.FirstOrDefaultAsync(u => u.SupportUserId == id);
        }

        public async Task<List<Support>> GetSupportsAsync()
        {
            return await _context.Supports.ToListAsync();
        }

        public async Task<List<SupportAssignment>> GetSupportsAssignmentsAsync()
        {
            return await _context.SupportAssignments.ToListAsync();
        }

        public async Task<List<SupportMessage>> GetSupportsMessagesAsync()
        {
            return await _context.SupportMessages.ToListAsync();
        }

        public async Task<List<SupportUser>> GetSupportUsersAsync()
        {
            return await _context.SupportUsers.ToListAsync();
        }

        public async Task<List<SupportUserMessageHub>> GetSupportUsersMessageHubAsync()
        {
            return await _context.SupportUserMessageHubs.ToListAsync();
        }

        public async Task<List<SupportUserCurrentStatus>> GetSupportUsersCurrentStatusAsync()
        {
            return await _context.SupportUserCurrentStatuses.ToListAsync();
        }

        public async Task<List<SupportUserCurrentStatus>> GetSupportUserCurrentStatusBySupportUserIdAndIsCurrentAsync(long supportUserId , bool isCurrent = true)
        {
            return await this.GetSupportUsersCurrentStatusByAsync(u => u.SupportUserId == supportUserId && u.IsCurrent == isCurrent );

        }

        public async Task<List<SupportUserWorkingState>> GetSupportUsersWorkingStatesAsync()
        {
            return await _context.SupportUserWorkingStates.ToListAsync();
        }

       /* public async Task<SupportUserCurrentStatus> GetSupportUserStatusCurrentStateBySupportUserIdAndIsCurrent(long supportUserId, bool isCurrent = true)
        {
            var user = await _context.SupportUserCurrentStatuses.FirstOrDefaultAsync(u => u.SupportUserId == supportUserId && u.IsCurrent == isCurrent);
            return user.FirstOrDefault();
        }*/

		public async Task<List<Support>> GetFilterSupportAsync(FilterParameters parameters, IQueryable<Support> supports)
		{
            if (parameters.FilterById != null)
            {
                supports = supports.Where(o => o.Id == parameters.FilterById);
            }
            if (parameters.FilterByDate != null)
            {
                supports = supports.Where(o => o.CreationDate == parameters.FilterByDate);
            }
           
            if (parameters.StatusTypeId != null)
            {
                supports = supports.Where(o => o.StatusTypeId == parameters.StatusTypeId);
            }
            return await supports.ToListAsync();
        }

       

		public IQueryable<SupportUser> GetSupportUserByQuerable(Expression<Func<SupportUser, bool>> predicate)
		{
            return _context.SupportUsers.Where(predicate);
        }

		public IQueryable<Support> GetSupportByQuerable(Expression<Func<Support, bool>> predicate)
		{
            return _context.Supports.Where(predicate);
        }

		public async Task<List<SupportAssignment>> GetSupportsAssignmentsByAsync(Expression<Func<SupportAssignment, bool>> predicate)
        {
            return await _context.SupportAssignments
                .Where(predicate)
                .Include(s => s.Support)
                .Include(s => s.SupportMessages )
                .Include(s => s.User)
                .ToListAsync();
        }

        public async Task<SupportAssignment> GetSupportAssignmentByIdAsync(long id)
        {
            return await _context.SupportAssignments.FirstOrDefaultAsync( s => s.Id == id);
        }

        public async Task<Support> GetSupportByIdAsync(long id)
        {
            return await _context.Supports.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<SupportMessage>> GetSupportsMessagesByAsync(Expression<Func<SupportMessage, bool>> predicate)
        {
            return await _context.SupportMessages.Where(predicate).ToListAsync();
        }

        public async Task<SupportMessage> GetSupportMessageByIdAsync(long id)
        {
            return await _context.SupportMessages.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Support>> GetSupportsByAsync(Expression<Func<Support, bool>> predicate)
        {
            return await _context.Supports.Where(predicate).ToListAsync();
        }

        public async Task<List<SupportStatusType>> GetSupportStatusTypesAsync()
        {
            return await _context.SupportStatusTypes.ToListAsync();
        }

        public async Task<List<SupportType>> GetSupportTypesAsync()
        {
            return await _context.SupportTypes.ToListAsync();
        }

        public async Task<List<SupportUserAccount>> GetSupportUsersAccountsByAsync(Expression<Func<SupportUserAccount, bool>> predicate)
        {
            return await _context.SupportUserAccounts.Where(predicate).ToListAsync();
        }

        public async Task<List<SupportUser>> GetSupportUsersByAsync(Expression<Func<SupportUser, bool>> predicate)
        {
            return await _context.SupportUsers.Where(predicate).ToListAsync();
        }

        public async Task<SupportUser> GetSupportUserByIdAsync(long id)
        {
            return await _context.SupportUsers.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<SupportUserMessageHub>> GetSupportUsersMessageHubByAsync(Expression<Func<SupportUserMessageHub, bool>> predicate)
        {
            return await _context.SupportUserMessageHubs.Where(predicate).ToListAsync();
        }

        public async Task<SupportUserMessageHub> GetSupportUserMessageHubByIdAsync(long id)
        {
            return await _context.SupportUserMessageHubs.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<SupportUserCurrentStatus>> GetSupportUsersCurrentStatusByAsync(Expression<Func<SupportUserCurrentStatus, bool>> predicate)
        {
            return await _context.SupportUserCurrentStatuses.Where(predicate).ToListAsync();
        }

        public async Task<SupportUserCurrentStatus> GetSupportUserCurrentStatusByIdAsync(long id)
        {
            return await _context.SupportUserCurrentStatuses.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<SupportUserWorkingState>> GetSupportUsersWorkingStateByAsync(Expression<Func<SupportUserWorkingState, bool>> predicate)
        {
            return await _context.SupportUserWorkingStates.Where(predicate).ToListAsync();
        }

        public async Task<SupportUserWorkingState> GetSupportUserWorkingStateByIdAsync(long id)
        {
            return await _context.SupportUserWorkingStates.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Support> InsertSupportAsync(Support support)
        {
            support.StatusTypeId = (long)SupportStatusTypes.New;
            support.CreationDate = DateTime.Now;
            var result = await _context.Supports.AddAsync(support);
            return result.Entity;
        }

        public async Task<SupportAssignment> InsertSupportAssignmentAsync(SupportAssignment supportAssignment)
        {
            supportAssignment.CreationDate = DateTime.Now;
            var result = await _context.SupportAssignments.AddAsync(supportAssignment);
            return result.Entity;
        }

        public async Task<SupportMessage> InsertSupportMessageAsync(SupportMessage supportMessage)
        {
            supportMessage.CreationDate = DateTime.Now;
            var result = await _context.SupportMessages.AddAsync(supportMessage);
            return result.Entity;
        }

        public async Task<SupportUser> InsertSupportUserAsync(SupportUser user)
        {
            user.CreationDate = DateTime.Now;
            var result = await _context.SupportUsers.AddAsync(user);
            return result.Entity;
        }

        public async Task<SupportUserMessageHub> InsertSupportUserMessageHubAsync(SupportUserMessageHub supportUserMessageHub)
        {

            var oldUserHub = await _context.SupportUserMessageHubs.FirstOrDefaultAsync(h => h.SupportUserId == supportUserMessageHub.SupportUserId);
            if (oldUserHub != null && oldUserHub.Id > 0)
            {
                oldUserHub.ConnectionId = supportUserMessageHub.ConnectionId;
                oldUserHub.ModifiedBy = 1;
                oldUserHub.ModificationDate = DateTime.Now;
                _context.Entry<SupportUserMessageHub>(oldUserHub).State = EntityState.Modified;
                return oldUserHub;
            }
            else
            {
                //SupportUserMessageHub newHub = new UserMessageHub() { UserId = ID, ConnectionId = ConnectionID, CreationDate = DateTime.Now, CreatedBy = 1 };
                supportUserMessageHub.CreationDate = DateTime.Now;
                supportUserMessageHub.CreatedBy = 1;
                var insertResult = await _context.SupportUserMessageHubs.AddAsync(supportUserMessageHub);
                return insertResult.Entity;
            }


            //var result = await _context.SupportUserMessageHubs.AddAsync(supportUserMessageHub);
            //return result.Entity;
        }

        public async Task<SupportUserCurrentStatus> InsertSupportUserCurrentStatusAsync(SupportUserCurrentStatus supportUserStatus)
        {
            var usersOldState = await this.GetSupportUsersCurrentStatusByAsync(s => s.SupportUserId == supportUserStatus.SupportUserId && s.IsCurrent == true );
            var userOldState = usersOldState.FirstOrDefault();
            if (userOldState != null) 
            {
                userOldState.IsCurrent = false;
                userOldState.ModifiedBy = supportUserStatus.CreatedBy;
                userOldState.ModificationDate = DateTime.Now;
                _context.Entry<SupportUserCurrentStatus>(userOldState).State = EntityState.Modified;
            }
            supportUserStatus.IsCurrent = true;
            supportUserStatus.CreationDate = DateTime.Now;
            var result = await _context.SupportUserCurrentStatuses.AddAsync(supportUserStatus);
            return result.Entity;
        }

        public async Task<SupportUserWorkingState> InsertSupportUserWorkingStateAsync(SupportUserWorkingState supportUserWorkingState)
        {
            var oldState = await _context.SupportUserWorkingStates.FirstOrDefaultAsync(w => w.SupportUserId == supportUserWorkingState.SupportUserId && w.IsCurrent == true);
            if (oldState != null)
            {
                oldState.IsCurrent = false;
                oldState.ModificationDate = DateTime.Now;
                oldState.ModifiedBy = supportUserWorkingState.ModifiedBy;
                _context.Entry<SupportUserWorkingState>(oldState).State = EntityState.Modified;
            }


            supportUserWorkingState.Id = 0;
            supportUserWorkingState.IsCurrent = true;
            supportUserWorkingState.CreationDate = DateTime.Now;
            var insertResult = await _context.SupportUserWorkingStates.AddAsync(supportUserWorkingState);
            return insertResult.Entity;


        }

        public async Task<Support> UpdateSupportAsync(Support support)
        {

            var oldSupport = await this.GetSupportByIdAsync(support.Id);
            if (oldSupport == null) return null;

            oldSupport.StatusTypeId = support.StatusTypeId ?? oldSupport.StatusTypeId;
            oldSupport.SupportTypeId = support.SupportTypeId ?? oldSupport.SupportTypeId;
            oldSupport.StatusTypeId = support.StatusTypeId ?? oldSupport.StatusTypeId;
            oldSupport.ModifiedBy = support.ModifiedBy ?? oldSupport.ModifiedBy;
            oldSupport.Description = support.Description ?? oldSupport.Description;
            oldSupport.UserId = support.UserId ?? oldSupport.UserId;
            oldSupport.CreatedBy = support.CreatedBy ?? oldSupport.CreatedBy;
            oldSupport.CreationDate = support.CreationDate ?? oldSupport.CreationDate;
            oldSupport.ModificationDate = DateTime.Now;
            _context.Entry<Support>(oldSupport).State = EntityState.Modified;
            return oldSupport;
        }

        public async Task<SupportAssignment> UpdateSupportAssignmentAsync(SupportAssignment supportAssignment)
        {
            var oldSupportAssign = await this.GetSupportAssignmentByIdAsync(supportAssignment.Id);
            if (oldSupportAssign == null) return null;

            oldSupportAssign.SupportUserId = supportAssignment.SupportUserId;
            oldSupportAssign.CurrentStatusId = supportAssignment.CurrentStatusId;
            oldSupportAssign.ModifiedBy = supportAssignment.ModifiedBy;
            oldSupportAssign.ModificationDate = DateTime.Now;
            _context.Entry<SupportAssignment>(oldSupportAssign).State = EntityState.Modified;
            return supportAssignment;
        }

        public async Task<SupportMessage> UpdateSupportMessageAsync(SupportMessage supportMessage)
        {
            var oldSupportMessage = await this.GetSupportMessageByIdAsync(supportMessage.Id);
            if (oldSupportMessage == null) return null;

            oldSupportMessage.Message = supportMessage.Message;
            oldSupportMessage.IsUser = supportMessage.IsUser;
            oldSupportMessage.ModifiedBy = supportMessage.ModifiedBy;
            oldSupportMessage.ModificationDate = DateTime.Now;
            _context.Entry<SupportMessage>(oldSupportMessage).State = EntityState.Modified;
            return oldSupportMessage;
        }

        public async Task<SupportUser> UpdateSupportUserAsync(SupportUser user)
        {
            var oldUser = await this.GetSupportUserByIdAsync(user.Id);
            if (oldUser == null) return null; // throw new NotFoundException("User not found");


            oldUser.Fullname = user.Fullname ?? oldUser.Fullname;
            oldUser.NationalNumber = user.NationalNumber ?? oldUser.NationalNumber;
            oldUser.CountryId = user.CountryId ?? oldUser.CountryId;
            oldUser.CityId = user.CityId ?? oldUser.CityId;
            oldUser.Address = user.Address ?? oldUser.Address;
            oldUser.Gender = user.Gender ?? oldUser.Gender;
            oldUser.BirthDay = user.BirthDay ?? oldUser.BirthDay;
            oldUser.BirthMonth = user.BirthMonth ?? oldUser.BirthMonth;
            oldUser.BirthYear = user.BirthYear ?? oldUser.BirthYear;
            oldUser.Mobile = user.Mobile ?? oldUser.Mobile;
            oldUser.Email = user.Email ?? oldUser.Email;
            oldUser.ResidenceExpireDay = user.ResidenceExpireDay ?? oldUser.ResidenceExpireDay;
            oldUser.ResidenceExpireMonth = user.ResidenceExpireMonth ?? oldUser.ResidenceExpireMonth;
            oldUser.ResidenceExpireYear = user.ResidenceExpireYear ?? oldUser.ResidenceExpireYear;
            oldUser.ResidenceCountryId = user.ResidenceCountryId ?? oldUser.ResidenceCountryId;
            oldUser.ResidenceCityId = user.ResidenceCityId ?? oldUser.ResidenceCityId;
            oldUser.Image = user.Image ?? oldUser.Image;
            oldUser.ModifiedBy = user.ModifiedBy ?? oldUser.ModifiedBy;
            oldUser.CreationDate = user.CreationDate ?? oldUser.CreationDate;
            oldUser.CreatedBy = user.CreatedBy ?? oldUser.CreatedBy;
            oldUser.ModificationDate = DateTime.Now;
            if (user.SupportUserAccounts?.Count() > 0)
                await UpdateSupportUserAccountAsync(user.SupportUserAccounts?.FirstOrDefault());


            _context.Entry<SupportUser>(oldUser).State = EntityState.Modified;
            return oldUser;
        }

        public async Task<SupportUserAccount> UpdateSupportUserAccountAsync(SupportUserAccount account)
        {
            var oldSupportUserAccount = await _context.SupportUserAccounts.FirstOrDefaultAsync(u => u.Id == account.Id);
            if (oldSupportUserAccount == null) return null; // throw new NotFoundException("User not found");

            oldSupportUserAccount.Fullname = account.Fullname ?? oldSupportUserAccount.Fullname;
            oldSupportUserAccount.SupportTypeId = account.SupportTypeId ?? oldSupportUserAccount.SupportTypeId;
            oldSupportUserAccount.Email = account.Email ?? oldSupportUserAccount.Email;
            oldSupportUserAccount.StatusTypeId = account.StatusTypeId ?? oldSupportUserAccount.StatusTypeId;
            oldSupportUserAccount.PasswordSalt = account.PasswordSalt ?? oldSupportUserAccount.PasswordSalt;
            oldSupportUserAccount.PasswordHash = account.PasswordHash ?? oldSupportUserAccount.PasswordHash;
            oldSupportUserAccount.Token = account.Token ?? oldSupportUserAccount.Token;
            oldSupportUserAccount.ModifiedBy = account.ModifiedBy ?? oldSupportUserAccount.ModifiedBy;
            oldSupportUserAccount.CreatedBy = account.CreatedBy ?? oldSupportUserAccount.CreatedBy;
            oldSupportUserAccount.CreationDate = account.CreationDate ?? oldSupportUserAccount.CreationDate;
            oldSupportUserAccount.ModificationDate = DateTime.Now;
            _context.Entry<SupportUserAccount>(oldSupportUserAccount).State = EntityState.Modified;
            return oldSupportUserAccount;
        }

        public async Task<SupportUserMessageHub> UpdateSupportUserMessageHubAsync(SupportUserMessageHub supportUserMessageHub)
        {
            var oldSupportUserMessageHub = await _context.SupportUserMessageHubs.FirstOrDefaultAsync(u => u.Id == supportUserMessageHub.Id);
            if (oldSupportUserMessageHub == null) return null;// throw new NotFoundException("User not found");

            oldSupportUserMessageHub.ConnectionId = oldSupportUserMessageHub.ConnectionId;
            oldSupportUserMessageHub.SupportUserId = oldSupportUserMessageHub.SupportUserId;
            oldSupportUserMessageHub.ModifiedBy = oldSupportUserMessageHub.ModifiedBy;
            oldSupportUserMessageHub.ModificationDate = DateTime.Now;
            _context.Entry<SupportUserMessageHub>(oldSupportUserMessageHub).State = EntityState.Modified;
            return oldSupportUserMessageHub;
        }

        public async Task<SupportUserCurrentStatus> UpdateSupportUserCurrentStatusAsync(SupportUserCurrentStatus supportUserStatus)
        {
            var oldSupportUserStatus = await _context.SupportUserCurrentStatuses.FirstOrDefaultAsync( s => s.Id == supportUserStatus.Id);
            if (oldSupportUserStatus == null) return null;

            oldSupportUserStatus.StatusTypeId = supportUserStatus.StatusTypeId;
            oldSupportUserStatus.IsCurrent = supportUserStatus.IsCurrent;
            oldSupportUserStatus.ModifiedBy = supportUserStatus.ModifiedBy;
            oldSupportUserStatus.ModificationDate = DateTime.Now;
            _context.Entry<SupportUserCurrentStatus>(oldSupportUserStatus).State = EntityState.Modified;
            return oldSupportUserStatus;
        }

        public async Task<SupportUserWorkingState> UpdateSupportUserWorkingStateAsync(SupportUserWorkingState supportUserWorkingState)
        {
            var oldState = await _context.SupportUserWorkingStates.FirstOrDefaultAsync(w => w.SupportUserId == supportUserWorkingState.SupportUserId && w.IsCurrent == true);
            if (oldState == null) return null;// throw new NotFoundException("Support working state not found");

            oldState.IsCurrent = supportUserWorkingState.IsCurrent;
            oldState.SupportUserId = supportUserWorkingState.SupportUserId;
            oldState.SupportStatusTypeId = supportUserWorkingState.SupportStatusTypeId;
            oldState.ModificationDate = DateTime.Now;
            oldState.ModifiedBy = supportUserWorkingState.ModifiedBy;
            _context.Entry<SupportUserWorkingState>(oldState).State = EntityState.Modified;
            return oldState;
        }

		public IQueryable<SupportUser> GetSupportUserQuerable()
        {
            return _context.SupportUsers.Include(a => a.Country).Include(a => a.City); 

        }
        public IQueryable<Support> GetSupportQuerable()
        {
            return _context.Supports;

        }
        public IQueryable<SupportUser> GetSupportUserByStatusTypeId(long? StatusSupportTypeId, IQueryable<SupportUser> query)
        {
            if (StatusSupportTypeId != null)
            {
                var restult = _context.SupportUserAccounts.Where(u => u.StatusTypeId == StatusSupportTypeId)
                    .Include(u => u.SupportUser).ThenInclude(u => u.City).Include(u => u.SupportUser).ThenInclude(u => u.City).Select(c => c.SupportUser);
                  
                return restult;
            }
            return query;
        }
    }
}
