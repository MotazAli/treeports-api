using System.Linq.Expressions;

namespace TreePorts.Interfaces.Repositories
{
    public interface ISupportRepository
    {
        Task<List<SupportUser>> GetSupportUsersAsync();
        Task<SupportUser?> GetSupportUserByIdAsync(string id);
        Task<List<SupportUser>> GetSupportUsersByAsync(Expression<Func<SupportUser, bool>> predicate);
        Task<List<SupportUserAccount>> GetSupportUsersAccountsByAsync(Expression<Func<SupportUserAccount, bool>> predicate);


        Task<SupportUserAccount?> GetSupportUserAccountByEmailAsync(string email);
        Task<SupportUser> InsertSupportUserAsync(SupportUser user);
        Task<SupportUser?> UpdateSupportUserAsync(SupportUser user);

        Task<SupportUserAccount> InsertSupportUserAccountAsync(SupportUserAccount account);
        Task<SupportUserAccount?> GetSupportUserAccountBySupportUserIdAsync(string id);
        Task<SupportUserAccount?> UpdateSupportUserAccountAsync(SupportUserAccount account);
        Task<SupportUserAccount?> DeleteSupportUserAccountAsync(string id);

        Task<List<SupportType>> GetSupportTypesAsync();
        Task<List<TicketStatusType>> GetTicketStatusTypesAsync();
        Task<List<Ticket>> GetTicketsAsync();
        Task<Ticket?> GetTicketByIdAsync(long id);
        Task<List<Ticket>> GetTicketsByAsync(Expression<Func<Ticket, bool>> predicate);
        Task<Ticket> InsertTicketAsync(Ticket ticket);
        Task<Ticket?> UpdateTicketAsync(Ticket ticket);


        Task<List<TicketAssignment>> GetTicketsAssignmentsAsync();
        Task<TicketAssignment?> GetTicketAssignmentByIdAsync(long id);
        Task<List<TicketAssignment>> GetTicketsAssignmentsByAsync(Expression<Func<TicketAssignment, bool>> predicate);
        Task<TicketAssignment> InsertTicketAssignmentAsync(TicketAssignment ticketAssignment);
        Task<TicketAssignment?> UpdateTicketAssignmentAsync(TicketAssignment ticketAssignment);



        Task<List<TicketMessage>> GetTicketMessagesAsync();
        Task<TicketMessage?> GetTicketMessageByIdAsync(long id);
        Task<List<TicketMessage>> GetTicketMessagesByAsync(Expression<Func<TicketMessage, bool>> predicate);
        Task<TicketMessage> InsertTicketMessageAsync(TicketMessage ticketMessage);
        Task<TicketMessage?> UpdateTicketMessageAsync(TicketMessage ticketMessage);



        Task<List<SupportUserCurrentStatus>> GetSupportUsersCurrentStatusAsync();
        Task<List<SupportUserCurrentStatus>> GetSupportUserCurrentStatusBySupportUserIdAndIsCurrentAsync(string supportUserAccountId, bool isCurrent = true);
        Task<SupportUserCurrentStatus?> GetSupportUserCurrentStatusByIdAsync(long id);
        //Task<SupportUserCurrentStatus> GetSupportUserCurrentStatusBySupportUserIdAndIsCurrentAsync(long supportUserId, bool isCurrent = true );
        Task<List<SupportUserCurrentStatus>> GetSupportUsersCurrentStatusByAsync(Expression<Func<SupportUserCurrentStatus, bool>> predicate);
        Task<SupportUserCurrentStatus> InsertSupportUserCurrentStatusAsync(SupportUserCurrentStatus supportUserStatus);
        Task<SupportUserCurrentStatus?> UpdateSupportUserCurrentStatusAsync(SupportUserCurrentStatus supportUserStatus);

        Task<List<SupportUserMessageHub>> GetSupportUsersMessageHubAsync();
        Task<SupportUserMessageHub?> GetSupportUserMessageHubByIdAsync(long id);
        Task<List<SupportUserMessageHub>> GetSupportUsersMessageHubByAsync(Expression<Func<SupportUserMessageHub, bool>> predicate);
        Task<SupportUserMessageHub> InsertSupportUserMessageHubAsync(SupportUserMessageHub supportUserMessageHub);
        Task<SupportUserMessageHub?> UpdateSupportUserMessageHubAsync(SupportUserMessageHub supportUserMessageHub);


        Task<List<SupportUserWorkingState>> GetSupportUsersWorkingStatesAsync();
        Task<SupportUserWorkingState?> GetSupportUserWorkingStateByIdAsync(long id);
        Task<List<SupportUserWorkingState>> GetSupportUsersWorkingStateByAsync(Expression<Func<SupportUserWorkingState, bool>> predicate);
        Task<SupportUserWorkingState> InsertSupportUserWorkingStateAsync(SupportUserWorkingState supportUserWorkingState);
        Task<SupportUserWorkingState?> UpdateSupportUserWorkingStateAsync(SupportUserWorkingState supportUserWorkingState);


        IQueryable<SupportUser> GetSupportUserByQuerable(Expression<Func<SupportUser, bool>> predicate);
        IQueryable<Ticket> GetTicketByQuerable(Expression<Func<Ticket, bool>> predicate);
         IQueryable<SupportUser> GetSupportUserQuerable();
        IQueryable<Ticket> GetTicketQuerable();
        IQueryable<SupportUser> GetSupportUserByStatusTypeId(long? statusTypeId, IQueryable<SupportUser> query);

    }
}
