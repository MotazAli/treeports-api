using System.Linq.Expressions;

namespace TreePorts.Interfaces.Repositories
{
    public interface ISupportRepository
    {
        Task<List<SupportUser>> GetSupportUsersAsync(CancellationToken cancellationToken);
        Task<SupportUser?> GetSupportUserByIdAsync(string id,CancellationToken cancellationToken);
        Task<List<SupportUser>> GetSupportUsersByAsync(Expression<Func<SupportUser, bool>> predicate,CancellationToken cancellationToken);
        Task<List<SupportUserAccount>> GetSupportUsersAccountsByAsync(Expression<Func<SupportUserAccount, bool>> predicate,CancellationToken cancellationToken);


        Task<SupportUserAccount?> GetSupportUserAccountByEmailAsync(string email,CancellationToken cancellationToken);
        Task<SupportUser> InsertSupportUserAsync(SupportUser user,CancellationToken cancellationToken);
        Task<SupportUser?> UpdateSupportUserAsync(SupportUser user,CancellationToken cancellationToken);

        Task<SupportUserAccount> InsertSupportUserAccountAsync(SupportUserAccount account,CancellationToken cancellationToken);
        Task<SupportUserAccount?> GetSupportUserAccountBySupportUserIdAsync(string id,CancellationToken cancellationToken);
        Task<SupportUserAccount?> UpdateSupportUserAccountAsync(SupportUserAccount account,CancellationToken cancellationToken);
        Task<SupportUserAccount?> DeleteSupportUserAccountAsync(string id,CancellationToken cancellationToken);

        Task<List<SupportType>> GetSupportTypesAsync(CancellationToken cancellationToken);
        Task<List<TicketStatusType>> GetTicketStatusTypesAsync(CancellationToken cancellationToken);
        Task<List<Ticket>> GetTicketsAsync(CancellationToken cancellationToken);
        Task<Ticket?> GetTicketByIdAsync(long id,CancellationToken cancellationToken);
        Task<List<Ticket>> GetTicketsByAsync(Expression<Func<Ticket, bool>> predicate,CancellationToken cancellationToken);
        Task<Ticket> InsertTicketAsync(Ticket ticket,CancellationToken cancellationToken);
        Task<Ticket?> UpdateTicketAsync(Ticket ticket,CancellationToken cancellationToken);


        Task<List<TicketAssignment>> GetTicketsAssignmentsAsync(CancellationToken cancellationToken);
        Task<TicketAssignment?> GetTicketAssignmentByIdAsync(long id,CancellationToken cancellationToken);
        Task<List<TicketAssignment>> GetTicketsAssignmentsByAsync(Expression<Func<TicketAssignment, bool>> predicate,CancellationToken cancellationToken);
        Task<TicketAssignment> InsertTicketAssignmentAsync(TicketAssignment ticketAssignment,CancellationToken cancellationToken);
        Task<TicketAssignment?> UpdateTicketAssignmentAsync(TicketAssignment ticketAssignment,CancellationToken cancellationToken);



        Task<List<TicketMessage>> GetTicketMessagesAsync(CancellationToken cancellationToken);
        Task<TicketMessage?> GetTicketMessageByIdAsync(long id,CancellationToken cancellationToken);
        Task<List<TicketMessage>> GetTicketMessagesByAsync(Expression<Func<TicketMessage, bool>> predicate,CancellationToken cancellationToken);
        Task<TicketMessage> InsertTicketMessageAsync(TicketMessage ticketMessage,CancellationToken cancellationToken);
        Task<TicketMessage?> UpdateTicketMessageAsync(TicketMessage ticketMessage,CancellationToken cancellationToken);



        Task<List<SupportUserCurrentStatus>> GetSupportUsersCurrentStatusAsync(CancellationToken cancellationToken);
        Task<List<SupportUserCurrentStatus>> GetSupportUserCurrentStatusBySupportUserIdAndIsCurrentAsync(string supportUserAccountId, bool isCurrent,CancellationToken cancellationToken);
        Task<SupportUserCurrentStatus?> GetSupportUserCurrentStatusByIdAsync(long id,CancellationToken cancellationToken);
        //Task<SupportUserCurrentStatus> GetSupportUserCurrentStatusBySupportUserIdAndIsCurrentAsync(long supportUserId, bool isCurrent = true ,CancellationToken cancellationToken);
        Task<List<SupportUserCurrentStatus>> GetSupportUsersCurrentStatusByAsync(Expression<Func<SupportUserCurrentStatus, bool>> predicate,CancellationToken cancellationToken);
        Task<SupportUserCurrentStatus> InsertSupportUserCurrentStatusAsync(SupportUserCurrentStatus supportUserStatus,CancellationToken cancellationToken);
        Task<SupportUserCurrentStatus?> UpdateSupportUserCurrentStatusAsync(SupportUserCurrentStatus supportUserStatus,CancellationToken cancellationToken);

        Task<List<SupportUserMessageHub>> GetSupportUsersMessageHubAsync(CancellationToken cancellationToken);
        Task<SupportUserMessageHub?> GetSupportUserMessageHubByIdAsync(long id,CancellationToken cancellationToken);
        Task<List<SupportUserMessageHub>> GetSupportUsersMessageHubByAsync(Expression<Func<SupportUserMessageHub, bool>> predicate,CancellationToken cancellationToken);
        Task<SupportUserMessageHub> InsertSupportUserMessageHubAsync(SupportUserMessageHub supportUserMessageHub,CancellationToken cancellationToken);
        Task<SupportUserMessageHub?> UpdateSupportUserMessageHubAsync(SupportUserMessageHub supportUserMessageHub,CancellationToken cancellationToken);


        Task<List<SupportUserWorkingState>> GetSupportUsersWorkingStatesAsync(CancellationToken cancellationToken);
        Task<SupportUserWorkingState?> GetSupportUserWorkingStateByIdAsync(long id,CancellationToken cancellationToken);
        Task<List<SupportUserWorkingState>> GetSupportUsersWorkingStateByAsync(Expression<Func<SupportUserWorkingState, bool>> predicate,CancellationToken cancellationToken);
        Task<SupportUserWorkingState> InsertSupportUserWorkingStateAsync(SupportUserWorkingState supportUserWorkingState,CancellationToken cancellationToken);
        Task<SupportUserWorkingState?> UpdateSupportUserWorkingStateAsync(SupportUserWorkingState supportUserWorkingState,CancellationToken cancellationToken);


        IQueryable<SupportUser> GetSupportUserByQuerable(Expression<Func<SupportUser, bool>> predicate);
        IQueryable<Ticket> GetTicketByQuerable(Expression<Func<Ticket, bool>> predicate);
         IQueryable<SupportUser> GetSupportUserQuerable();
        IQueryable<Ticket> GetTicketQuerable();
        IQueryable<SupportUser> GetSupportUserByStatusTypeId(long? statusTypeId, IQueryable<SupportUser> query);

    }
}
