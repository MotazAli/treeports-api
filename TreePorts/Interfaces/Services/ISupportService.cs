using TreePorts.DTO;
using TreePorts.DTO.Records;

namespace TreePorts.Interfaces.Services;
public interface ISupportService 
{
    Task<IEnumerable<Ticket>> GetTicketsAsyncs(CancellationToken cancellationToken);
    Task<Ticket?> GetTicketByIdAsync(long id,CancellationToken cancellationToken);
    Task<SupportUser> GetSupportAccountUserBySupportUserAccountIdAsync(string supportUserAccountId,CancellationToken cancellationToken);
    Task<object> GetSupportUsersAccountsAsync(FilterParameters parameters,CancellationToken cancellationToken);
    Task<bool> AddTicketAsync(Ticket ticket,CancellationToken cancellationToken);
    Task<TicketAssignment> GetTicketAssignedByCaptainUserAccountIdAsync(string captainUserAccountId,CancellationToken cancellationToken);
    Task<IEnumerable<TicketAssignment>> GetTicketsAssignedBySupportUserAccountIdAsync(string supportUserAccountId,CancellationToken cancellationToken);
    Task<bool> UpdateTicketAsync(long ticketId, Ticket ticket,CancellationToken cancellationToken);
    Task<bool> UpdateTicketAssignmentByTicketIdAsync(long ticketId, TicketAssignment ticketAssgin,CancellationToken cancellationToken);
    Task<IEnumerable<SupportUser>> GetSupportUsersAccountsAsync(CancellationToken cancellationToken);
    Task<IEnumerable<SupportType>> GetTicketTypesAsync(CancellationToken cancellationToken);
    Task<SupportUser?> UpdateSupportUserAccountAsync(long supportUserAccountId, SupportUser user,CancellationToken cancellationToken);
    Task<bool> DeleteSupportUserAccountAsync(string supportUserAccountId,CancellationToken cancellationToken);
    Task<SupportUserResponse> AddSupportUserAccountAsync(SupportUserDto supportUserDto,CancellationToken cancellationToken);
    Task<SupportUserResponse?> LoginAsync(LoginUserDto user,CancellationToken cancellationToken);
    Task<bool> SendMessageAsync(TicketMessage supportMessage,CancellationToken cancellationToken);
    Task<bool> UploadFileAsync(HttpContext httpContext,CancellationToken cancellationToken);
    Task<object> GetSupportUsersPagingAsync(Pagination pagination, FilterParameters parameters,CancellationToken cancellationToken);
    Task<object> SearchAsync(FilterParameters parameters,CancellationToken cancellationToken);
    //Task<object> SearchTicketAsync(FilterParameters parameters,CancellationToken cancellationToken);
    //Task<object> ReportAsync(FilterParameters reportParameters,CancellationToken cancellationToken);
    Task<string> SendFirebaseNotificationAsync(FBNotify fbNotify,CancellationToken cancellationToken);

}

