using TreePorts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TreePorts.Interfaces.Repositories
{
    public interface ISupportRepository
    {
        Task<List<SupportUser>> GetSupportUsersAsync();
        Task<SupportUser> GetSupportUserByIdAsync(long id);
        Task<SupportUser> GetSupportUserByEmailAsync(string email);
        Task<List<SupportUser>> GetSupportUsersByAsync(Expression<Func<SupportUser, bool>> predicate);
        Task<List<SupportUserAccount>> GetSupportUsersAccountsByAsync(Expression<Func<SupportUserAccount, bool>> predicate);


        Task<SupportUserAccount> GetSupportUserAccountByEmailAsync(string email);
        Task<SupportUser> InsertSupportUserAsync(SupportUser user);
        Task<SupportUser> UpdateSupportUserAsync(SupportUser user);

        Task<SupportUserAccount> GetSupportUserAccountBySupportUserIdAsync(long id);
        Task<SupportUserAccount> UpdateSupportUserAccountAsync(SupportUserAccount account);
        Task<SupportUser> DeleteSupportUserAsync(long id);

        Task<List<SupportType>> GetSupportTypesAsync();
        Task<List<SupportStatusType>> GetSupportStatusTypesAsync();
        Task<List<Support>> GetSupportsAsync();
        Task<Support> GetSupportByIdAsync(long id);
        Task<List<Support>> GetSupportsByAsync(Expression<Func<Support, bool>> predicate);
        Task<Support> InsertSupportAsync(Support support);
        Task<Support> UpdateSupportAsync(Support support);


        Task<List<SupportAssignment>> GetSupportsAssignmentsAsync();
        Task<SupportAssignment> GetSupportAssignmentByIdAsync(long id);
        Task<List<SupportAssignment>> GetSupportsAssignmentsByAsync(Expression<Func<SupportAssignment, bool>> predicate);
        Task<SupportAssignment> InsertSupportAssignmentAsync(SupportAssignment supportAssignment);
        Task<SupportAssignment> UpdateSupportAssignmentAsync(SupportAssignment supportAssignment);



        Task<List<SupportMessage>> GetSupportsMessagesAsync();
        Task<SupportMessage> GetSupportMessageByIdAsync(long id);
        Task<List<SupportMessage>> GetSupportsMessagesByAsync(Expression<Func<SupportMessage, bool>> predicate);
        Task<SupportMessage> InsertSupportMessageAsync(SupportMessage supportMessage);
        Task<SupportMessage> UpdateSupportMessageAsync(SupportMessage supportMessage);



        Task<List<SupportUserCurrentStatus>> GetSupportUsersCurrentStatusAsync();
        Task<List<SupportUserCurrentStatus>> GetSupportUserCurrentStatusBySupportUserIdAndIsCurrentAsync(long supportUserId, bool isCurrent = true);
        Task<SupportUserCurrentStatus> GetSupportUserCurrentStatusByIdAsync(long id);
        //Task<SupportUserCurrentStatus> GetSupportUserCurrentStatusBySupportUserIdAndIsCurrentAsync(long supportUserId, bool isCurrent = true );
        Task<List<SupportUserCurrentStatus>> GetSupportUsersCurrentStatusByAsync(Expression<Func<SupportUserCurrentStatus, bool>> predicate);
        Task<SupportUserCurrentStatus> InsertSupportUserCurrentStatusAsync(SupportUserCurrentStatus supportUserStatus);
        Task<SupportUserCurrentStatus> UpdateSupportUserCurrentStatusAsync(SupportUserCurrentStatus supportUserStatus);

        Task<List<SupportUserMessageHub>> GetSupportUsersMessageHubAsync();
        Task<SupportUserMessageHub> GetSupportUserMessageHubByIdAsync(long id);
        Task<List<SupportUserMessageHub>> GetSupportUsersMessageHubByAsync(Expression<Func<SupportUserMessageHub, bool>> predicate);
        Task<SupportUserMessageHub> InsertSupportUserMessageHubAsync(SupportUserMessageHub supportUserMessageHub);
        Task<SupportUserMessageHub> UpdateSupportUserMessageHubAsync(SupportUserMessageHub supportUserMessageHub);


        Task<List<SupportUserWorkingState>> GetSupportUsersWorkingStatesAsync();
        Task<SupportUserWorkingState> GetSupportUserWorkingStateByIdAsync(long id);
        Task<List<SupportUserWorkingState>> GetSupportUsersWorkingStateByAsync(Expression<Func<SupportUserWorkingState, bool>> predicate);
        Task<SupportUserWorkingState> InsertSupportUserWorkingStateAsync(SupportUserWorkingState supportUserWorkingState);
        Task<SupportUserWorkingState> UpdateSupportUserWorkingStateAsync(SupportUserWorkingState supportUserWorkingState);


        IQueryable<SupportUser> GetSupportUserByQuerable(Expression<Func<SupportUser, bool>> predicate);
        IQueryable<Support> GetSupportByQuerable(Expression<Func<Support, bool>> predicate);
         IQueryable<SupportUser> GetSupportUserQuerable();
        IQueryable<Support> GetSupportQuerable();
        IQueryable<SupportUser> GetSupportUserByStatusTypeId(long? statusTypeId, IQueryable<SupportUser> query);

    }
}
