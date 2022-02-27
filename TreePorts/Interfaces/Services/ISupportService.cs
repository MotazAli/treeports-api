using TreePorts.DTO;
using TreePorts.DTO.Records;

namespace TreePorts.Interfaces.Services;
public interface ISupportService 
{
    Task<IEnumerable<Ticket>> GetTicketsAsyncs();
    Task<Ticket?> GetTicketByIdAsync(long id);
    Task<SupportUser> GetSupportAccountUserBySupportUserAccountIdAsync(string supportUserAccountId);
    Task<object> GetSupportUsersAccountsAsync(FilterParameters parameters);
    Task<bool> AddTicketAsync(Ticket ticket);
    Task<TicketAssignment> GetTicketAssignedByCaptainUserAccountIdAsync(string captainUserAccountId);
    Task<IEnumerable<TicketAssignment>> GetTicketsAssignedBySupportUserAccountIdAsync(string supportUserAccountId);
    Task<bool> UpdateTicketAsync(long ticketId, Ticket ticket);
    Task<bool> UpdateTicketAssignmentByTicketIdAsync(long ticketId, TicketAssignment ticketAssgin);
    Task<IEnumerable<SupportUser>> GetSupportUsersAccountsAsync();
    Task<IEnumerable<SupportType>> GetTicketTypesAsync();
    Task<SupportUser?> UpdateSupportUserAccountAsync(long supportUserAccountId, SupportUser user);
    Task<bool> DeleteSupportUserAccountAsync(string supportUserAccountId);
    Task<SupportUserResponse> AddSupportUserAccountAsync(SupportUserDto supportUserDto);
    Task<SupportUserResponse?> LoginAsync(LoginUserDto user);
    Task<bool> SendMessageAsync(TicketMessage supportMessage);
    Task<bool> UploadFileAsync(HttpContext httpContext);
    Task<object> GetSupportUsersPagingAsync(Pagination pagination, FilterParameters parameters);
    Task<object> SearchAsync(FilterParameters parameters);
    //Task<object> SearchTicketAsync(FilterParameters parameters);
    //Task<object> ReportAsync(FilterParameters reportParameters);
    Task<string> SendFirebaseNotificationAsync(FBNotify fbNotify);

}

