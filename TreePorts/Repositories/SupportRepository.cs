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


        

        public async Task<SupportUserAccount?> GetSupportUserAccountByEmailAsync(string email) {
            return await _context.SupportUserAccounts.FirstOrDefaultAsync(s => s.Email == email);
        }

        public async Task<SupportUserAccount?> DeleteSupportUserAccountAsync(string id)
        {
            var userAccount = await this.GetSupportUserAccountBySupportUserIdAsync(id); // _context.SupportUserAccounts.Where(u => u.SupportUserId == ID).FirstOrDefaultAsync();
            if (userAccount == null) return null;// throw new NotFoundException("User not found");

            userAccount.IsDeleted = true;
            _context.Entry<SupportUserAccount>(userAccount).State = EntityState.Modified;
            var user = await _context.SupportUserAccounts.Where(u => u.Id == id).FirstOrDefaultAsync();
            return user;
        }


        public async Task<SupportUserAccount?> GetSupportUserAccountBySupportUserIdAsync(string id) 
        {
            return  await  _context.SupportUserAccounts.FirstOrDefaultAsync(u => u.SupportUserId == id);
        }

        public async Task<List<Ticket>> GetTicketsAsync()
        {
            return await _context.Tickets.ToListAsync();
        }

        public async Task<List<TicketAssignment>> GetTicketsAssignmentsAsync()
        {
            return await _context.TicketAssignments.ToListAsync();
        }

        public async Task<List<TicketMessage>> GetTicketMessagesAsync()
        {
            return await _context.TicketMessages.ToListAsync();
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

        public async Task<List<SupportUserCurrentStatus>> GetSupportUserCurrentStatusBySupportUserIdAndIsCurrentAsync(string supportUserAccountId, bool isCurrent = true)
        {
            return await this.GetSupportUsersCurrentStatusByAsync(u => u.SupportUserAccountId == supportUserAccountId && u.IsCurrent == isCurrent );

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

		public async Task<List<Ticket>> GetFilterTicketAsync(FilterParameters parameters, IQueryable<Ticket> tickets)
		{
            if (parameters.FilterById != null)
            {
                tickets = tickets.Where(o => o.Id == parameters.FilterById);
            }
            if (parameters.FilterByDate != null)
            {
                tickets = tickets.Where(o => o.CreationDate == parameters.FilterByDate);
            }
           
            if (parameters.StatusTypeId != null)
            {
                tickets = tickets.Where(o => o.TicketStatusTypeId == parameters.StatusTypeId);
            }
            return await tickets.ToListAsync();
        }

       

		public IQueryable<SupportUser> GetSupportUserByQuerable(Expression<Func<SupportUser, bool>> predicate)
		{
            return _context.SupportUsers.Where(predicate);
        }

		public IQueryable<Ticket> GetTicketByQuerable(Expression<Func<Ticket, bool>> predicate)
		{
            return _context.Tickets.Where(predicate);
        }

		public async Task<List<TicketAssignment>> GetTicketsAssignmentsByAsync(Expression<Func<TicketAssignment, bool>> predicate)
        {
            return await _context.TicketAssignments
                .Where(predicate)
                //.Include(s => s.Support)
                //.Include(s => s.SupportMessages )
                //.Include(s => s.User)
                .ToListAsync();
        }

        public async Task<TicketAssignment?> GetTicketAssignmentByIdAsync(long id)
        {
            return await _context.TicketAssignments.FirstOrDefaultAsync( s => s.Id == id);
        }

        public async Task<Ticket?> GetTicketByIdAsync(long id)
        {
            return await _context.Tickets.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<TicketMessage>> GetTicketMessagesByAsync(Expression<Func<TicketMessage, bool>> predicate)
        {
            return await _context.TicketMessages.Where(predicate).ToListAsync();
        }

        public async Task<TicketMessage?> GetTicketMessageByIdAsync(long id)
        {
            return await _context.TicketMessages.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Ticket>> GetTicketsByAsync(Expression<Func<Ticket, bool>> predicate)
        {
            return await _context.Tickets.Where(predicate).ToListAsync();
        }

        public async Task<List<TicketStatusType>> GetTicketStatusTypesAsync()
        {
            return await _context.TicketStatusTypes.ToListAsync();
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

        public async Task<SupportUser?> GetSupportUserByIdAsync(string id)
        {
            return await _context.SupportUsers.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<SupportUserMessageHub>> GetSupportUsersMessageHubByAsync(Expression<Func<SupportUserMessageHub, bool>> predicate)
        {
            return await _context.SupportUserMessageHubs.Where(predicate).ToListAsync();
        }

        public async Task<SupportUserMessageHub?> GetSupportUserMessageHubByIdAsync(long id)
        {
            return await _context.SupportUserMessageHubs.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<SupportUserCurrentStatus>> GetSupportUsersCurrentStatusByAsync(Expression<Func<SupportUserCurrentStatus, bool>> predicate)
        {
            return await _context.SupportUserCurrentStatuses.Where(predicate).ToListAsync();
        }

        public async Task<SupportUserCurrentStatus?> GetSupportUserCurrentStatusByIdAsync(long id)
        {
            return await _context.SupportUserCurrentStatuses.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<SupportUserWorkingState>> GetSupportUsersWorkingStateByAsync(Expression<Func<SupportUserWorkingState, bool>> predicate)
        {
            return await _context.SupportUserWorkingStates.Where(predicate).ToListAsync();
        }

        public async Task<SupportUserWorkingState?> GetSupportUserWorkingStateByIdAsync(long id)
        {
            return await _context.SupportUserWorkingStates.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Ticket> InsertTicketAsync(Ticket ticket)
        {
            ticket.TicketStatusTypeId = (long)SupportStatusTypes.New;
            ticket.CreationDate = DateTime.Now;
            var result = await _context.Tickets.AddAsync(ticket);
            return result.Entity;
        }

        public async Task<TicketAssignment> InsertTicketAssignmentAsync(TicketAssignment ticketAssignment)
        {
            ticketAssignment.CreationDate = DateTime.Now;
            var result = await _context.TicketAssignments.AddAsync(ticketAssignment);
            return result.Entity;
        }

        public async Task<TicketMessage> InsertTicketMessageAsync(TicketMessage ticketMessage)
        {
            ticketMessage.CreationDate = DateTime.Now;
            var result = await _context.TicketMessages.AddAsync(ticketMessage);
            return result.Entity;
        }

        public async Task<SupportUser> InsertSupportUserAsync(SupportUser user)
        {
            user.Id = Guid.NewGuid().ToString();
            user.CreationDate = DateTime.Now;
            var result = await _context.SupportUsers.AddAsync(user);
            return result.Entity;
        }


        public async Task<SupportUserAccount> InsertSupportUserAccountAsync(SupportUserAccount account)
        {
            account.Id = Guid.NewGuid().ToString();
            account.CreationDate = DateTime.Now;
            var result = await _context.SupportUserAccounts.AddAsync(account);
            return result.Entity;
        }

        public async Task<SupportUserMessageHub> InsertSupportUserMessageHubAsync(SupportUserMessageHub supportUserMessageHub)
        {

            var oldUserHub = await _context.SupportUserMessageHubs.FirstOrDefaultAsync(h => h.SupportUserAccountId == supportUserMessageHub.SupportUserAccountId);
            if (oldUserHub != null && oldUserHub.Id > 0)
            {
                oldUserHub.ConnectionId = supportUserMessageHub.ConnectionId;
                oldUserHub.ModificationDate = DateTime.Now;
                _context.Entry<SupportUserMessageHub>(oldUserHub).State = EntityState.Modified;
                return oldUserHub;
            }
            else
            {
                //SupportUserMessageHub newHub = new UserMessageHub() { UserId = ID, ConnectionId = ConnectionID, CreationDate = DateTime.Now, CreatedBy = 1 };
                supportUserMessageHub.CreationDate = DateTime.Now;
                var insertResult = await _context.SupportUserMessageHubs.AddAsync(supportUserMessageHub);
                return insertResult.Entity;
            }


            //var result = await _context.SupportUserMessageHubs.AddAsync(supportUserMessageHub);
            //return result.Entity;
        }

        public async Task<SupportUserCurrentStatus> InsertSupportUserCurrentStatusAsync(SupportUserCurrentStatus supportUserStatus)
        {
            var usersOldState = await this.GetSupportUsersCurrentStatusByAsync(s => s.SupportUserAccountId == supportUserStatus.SupportUserAccountId && s.IsCurrent == true );
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
            var oldState = await _context.SupportUserWorkingStates.FirstOrDefaultAsync(w => w.SupportUserAccountId == supportUserWorkingState.SupportUserAccountId && w.IsCurrent == true);
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

        public async Task<Ticket?> UpdateTicketAsync(Ticket ticket)
        {

            var oldTicket = await this.GetTicketByIdAsync(ticket.Id);
            if (oldTicket == null) return null;

            oldTicket.TicketStatusTypeId = ticket.TicketStatusTypeId ?? oldTicket.TicketStatusTypeId;
            oldTicket.TicketTypeId = ticket.TicketTypeId ?? oldTicket.TicketTypeId;
            oldTicket.TicketStatusTypeId = ticket.TicketStatusTypeId ?? oldTicket.TicketStatusTypeId;
            oldTicket.ModifiedBy = ticket.ModifiedBy ?? oldTicket.ModifiedBy;
            oldTicket.Description = ticket.Description ?? oldTicket.Description;
            oldTicket.CaptainUserAccountId = ticket.CaptainUserAccountId ?? oldTicket.CaptainUserAccountId;
            oldTicket.CreatedBy = ticket.CreatedBy ?? oldTicket.CreatedBy;
            oldTicket.CreationDate = ticket.CreationDate ?? oldTicket.CreationDate;
            oldTicket.ModificationDate = DateTime.Now;
            _context.Entry<Ticket>(oldTicket).State = EntityState.Modified;
            return oldTicket;
        }

        public async Task<TicketAssignment?> UpdateTicketAssignmentAsync(TicketAssignment ticketAssignment)
        {
            var oldTicketAssign = await this.GetTicketAssignmentByIdAsync(ticketAssignment.Id);
            if (oldTicketAssign == null) return null;

            oldTicketAssign.SupportUserAccountId = ticketAssignment.SupportUserAccountId;
            oldTicketAssign.TicketStatusTypeId = ticketAssignment.TicketStatusTypeId;
            oldTicketAssign.ModifiedBy = ticketAssignment.ModifiedBy;
            oldTicketAssign.ModificationDate = DateTime.Now;
            _context.Entry<TicketAssignment>(oldTicketAssign).State = EntityState.Modified;
            return ticketAssignment;
        }

        public async Task<TicketMessage?> UpdateTicketMessageAsync(TicketMessage ticketMessage)
        {
            var oldTicketMessage = await this.GetTicketMessageByIdAsync(ticketMessage.Id);
            if (oldTicketMessage == null) return null;

            oldTicketMessage.Message = ticketMessage.Message;
            oldTicketMessage.IsUser = ticketMessage.IsUser;
            oldTicketMessage.ModifiedBy = ticketMessage.ModifiedBy;
            oldTicketMessage.ModificationDate = DateTime.Now;
            _context.Entry<TicketMessage>(oldTicketMessage).State = EntityState.Modified;
            return oldTicketMessage;
        }

        public async Task<SupportUser?> UpdateSupportUserAsync(SupportUser user)
        {
            var oldUser = await this.GetSupportUserByIdAsync(user.Id);
            if (oldUser == null) return null; // throw new NotFoundException("User not found");


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
            oldUser.CreationDate = user.CreationDate ?? oldUser.CreationDate;
            oldUser.CreatedBy = user.CreatedBy ?? oldUser.CreatedBy;
            oldUser.ModificationDate = DateTime.Now;
           /* if (user.SupportUserAccounts?.Count() > 0)
                await UpdateSupportUserAccountAsync(user.SupportUserAccounts?.FirstOrDefault());*/

            _context.Entry<SupportUser>(oldUser).State = EntityState.Modified;
            return oldUser;
        }

        public async Task<SupportUserAccount?> UpdateSupportUserAccountAsync(SupportUserAccount account)
        {
            var oldSupportUserAccount = await _context.SupportUserAccounts.FirstOrDefaultAsync(u => u.Id == account.Id);
            if (oldSupportUserAccount == null) return null; // throw new NotFoundException("User not found");

            
            oldSupportUserAccount.SupportTypeId = account.SupportTypeId ?? oldSupportUserAccount.SupportTypeId;
            oldSupportUserAccount.Email = account.Email ?? oldSupportUserAccount.Email;
            oldSupportUserAccount.StatusTypeId = account.StatusTypeId ?? oldSupportUserAccount.StatusTypeId;
            /*oldSupportUserAccount.PasswordSalt = account.PasswordSalt ?? oldSupportUserAccount.PasswordSalt;
            oldSupportUserAccount.PasswordHash = account.PasswordHash ?? oldSupportUserAccount.PasswordHash;*/
            oldSupportUserAccount.Token = account.Token ?? oldSupportUserAccount.Token;
            oldSupportUserAccount.ModifiedBy = account.ModifiedBy ?? oldSupportUserAccount.ModifiedBy;
            oldSupportUserAccount.CreatedBy = account.CreatedBy ?? oldSupportUserAccount.CreatedBy;
            oldSupportUserAccount.CreationDate = account.CreationDate ?? oldSupportUserAccount.CreationDate;
            oldSupportUserAccount.ModificationDate = DateTime.Now;
            _context.Entry<SupportUserAccount>(oldSupportUserAccount).State = EntityState.Modified;
            return oldSupportUserAccount;
        }

        public async Task<SupportUserMessageHub?> UpdateSupportUserMessageHubAsync(SupportUserMessageHub supportUserMessageHub)
        {
            var oldSupportUserMessageHub = await _context.SupportUserMessageHubs.FirstOrDefaultAsync(u => u.Id == supportUserMessageHub.Id);
            if (oldSupportUserMessageHub == null) return null;// throw new NotFoundException("User not found");

            oldSupportUserMessageHub.ConnectionId = oldSupportUserMessageHub.ConnectionId;
            oldSupportUserMessageHub.SupportUserAccountId = oldSupportUserMessageHub.SupportUserAccountId;
            oldSupportUserMessageHub.ModifiedBy = oldSupportUserMessageHub.ModifiedBy;
            oldSupportUserMessageHub.ModificationDate = DateTime.Now;
            _context.Entry<SupportUserMessageHub>(oldSupportUserMessageHub).State = EntityState.Modified;
            return oldSupportUserMessageHub;
        }

        public async Task<SupportUserCurrentStatus?> UpdateSupportUserCurrentStatusAsync(SupportUserCurrentStatus supportUserStatus)
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

        public async Task<SupportUserWorkingState?> UpdateSupportUserWorkingStateAsync(SupportUserWorkingState supportUserWorkingState)
        {
            var oldState = await _context.SupportUserWorkingStates.FirstOrDefaultAsync(w => w.SupportUserAccountId == supportUserWorkingState.SupportUserAccountId && w.IsCurrent == true);
            if (oldState == null) return null;// throw new NotFoundException("Support working state not found");

            oldState.IsCurrent = supportUserWorkingState.IsCurrent;
            oldState.SupportUserAccountId = supportUserWorkingState.SupportUserAccountId;
            oldState.StatusTypeId = supportUserWorkingState.StatusTypeId;
            oldState.ModificationDate = DateTime.Now;
            oldState.ModifiedBy = supportUserWorkingState.ModifiedBy;
            _context.Entry<SupportUserWorkingState>(oldState).State = EntityState.Modified;
            return oldState;
        }

		public IQueryable<SupportUser> GetSupportUserQuerable()
        {
            return _context.SupportUsers;
                //.Include(a => a.Country)
                //.Include(a => a.City); 

        }
        public IQueryable<Ticket> GetTicketQuerable()
        {
            return _context.Tickets;

        }
        public IQueryable<SupportUser?> GetSupportUserByStatusTypeId(long? StatusSupportTypeId, IQueryable<SupportUser> query)
        {
            if (StatusSupportTypeId != null)
            {
                var restult = _context.SupportUserAccounts.Where(u => u.StatusTypeId == StatusSupportTypeId)
                    .Include(u => u.SupportUser)
                    //.ThenInclude(u => u.City)
                    .Include(u => u.SupportUser)
                    //.ThenInclude(u => u.City)
                    .Select(c => c.SupportUser);
                  
                return restult;
            }
            return query;
        }

        
    }
}
