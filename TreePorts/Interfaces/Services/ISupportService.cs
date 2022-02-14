using TreePorts.DTO;

namespace TreePorts.Interfaces.Services;
public interface ISupportService 
{
    Task<IEnumerable<Support>> GetTicketsAsyncs();
    Task<Support> GetTicketByIdAsync(long id);
    Task<SupportUser> GetSupportAccountUserBySupportUserAccountIdAsync(long supportUserAccountId);
    Task<object> GetSupportUsersAccountsAsync(FilterParameters parameters);
    Task<bool> AddTicketAsync(Support support);
    Task<SupportAssignment> GetTicketAssignedByCaptainIdAsync(long id);
    Task<IEnumerable<SupportAssignment>> GetTicketsAssignedBySupportUserAccountIdAsync(long supportUserAccountId);
    Task<bool> UpdateTicketAsync(long ticketId, Support support);
    Task<bool> UpdateTicketAssignmentByTicketIdAsync(long ticketId, SupportAssignment supportAssgin);
    Task<IEnumerable<SupportUser>> GetSupportUsersAccountsAsync();
    Task<IEnumerable<SupportType>> GetTicketTypesAsync();
    Task<SupportUser> UpdateSupportUserAccountAsync(long supportUserAccountId, SupportUser user);
    Task<bool> DeleteSupportUserAccountAsync(long supportUserAccountId);
    Task<object> AddSupportUserAccountAsync(SupportUser user);
    Task<SupportUserAccount> LoginAsync(LoginUser user);
    Task<bool> SendMessageAsync(SupportMessage supportMessage);
    Task<bool> UploadFileAsync(HttpContext httpContext);
    Task<object> GetSupportUsersPagingAsync(Pagination pagination, FilterParameters parameters);
    Task<object> SearchAsync(FilterParameters parameters);
    Task<object> SearchTicketAsync(FilterParameters parameters);
    Task<object> ReportAsync(FilterParameters reportParameters);
    Task<string> SendFirebaseNotificationAsync(FBNotify fbNotify);

}

