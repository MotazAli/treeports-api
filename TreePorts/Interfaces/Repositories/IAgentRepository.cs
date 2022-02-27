using TreePorts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TreePorts.Interfaces.Repositories
{
    public interface IAgentRepository
    {
        Task<IEnumerable<Agent>> GetAgentsAsync();
        Task<IEnumerable<Agent>> GetAgentsPagingAsync(int skip,int take);
        Task<IEnumerable<Agent>> GetNewRegisteredAgentsPagingAsync(int skip, int take);
        Task<IEnumerable<Agent>> GetActiveAgentsAsync();
        Task<Agent?> GetAgentByIdAsync(string id);
        Task<Agent?> GetAgentByEmailAsync(string email);
        Task<IEnumerable<Agent>> GetAgentsByAsync(Expression<Func<Agent, bool>> predicate);
        IQueryable<Agent> GetByQuerable(Expression<Func<Agent, bool>> predicate);
        Task<Agent> InsertAgentAsync(Agent agent);
        Task<Agent?> UpdateAgentAsync(Agent agent);
        Task<Agent?> UpdateAgentImageAsync(Agent agent);
        Task<Agent?> UpdateAgentLocationAsync(Agent agent);
        Task<Agent?> UpdateAgentTokenAsync(Agent agent);
        Task<Agent?> DeleteAgentAsync(string id);


        Task<IEnumerable<AgentCurrentStatus>> GetAgentsCurrentStatusesAsync();
        Task<AgentCurrentStatus?> GetAgentCurrentStatusByIdAsync(long id);
        Task<IEnumerable<AgentCurrentStatus>> GetAgentsCurrentStatusByAsync(Expression<Func<AgentCurrentStatus, bool>> predicate);
        Task<AgentCurrentStatus> InsertAgentCurrentStatusAsync(AgentCurrentStatus agentCurrentStatus);
        Task<AgentCurrentStatus?> UpdateAgentCurrentStatusAsync(AgentCurrentStatus agentCurrentStatus);
        Task<AgentCurrentStatus?> DeleteAgentCurrentStatusAsync(long id);

        Task<IEnumerable<AgentType>> GetAgentTypesAsync();
        Task<AgentType?> GetAgentTypeByIdAsync(long id);
        IQueryable<Agent> GetByQuerable();
        Task<Coupon> InsertCouponAsync(Coupon coupon);
        Task<Coupon?> GetCouponAsync(long id);
        Task<bool> IsValidCouponAsync(string couponName, string? agentId, long? countryId);
        Task<IEnumerable<Order>> GetAgentOrdersAsync(string agentId);
        Task<Order?> GetAgentOrderAsync(string agentId, long orderId);
      //  Coupon GetAgentCoupon(long agentId, long orderId);
        Task<Coupon?> GetCouponByCodeAsync(string Code);
        Task<object> AgentReportCountAsync();

        Task<Coupon?> GetAssignedCoupon(string agentId, long orderId);
        Task<CouponUsage> InsertCouponUsageAsync(CouponUsage couponUsage);



        //Task<List<AgentDeliveryPrice>> GetAgentsDeliveryPrices();
        //Task<AgentDeliveryPrice> GetAgentDeliveryPriceById(long id);
        Task<object> GetAgentsDeliveryPricesAsync();
        Task<object> GetAgentDeliveryPriceByIdAsync(long id);
        Task<IEnumerable<AgentDeliveryPrice>> GetAgentDeliveryPriceByAsync(Expression<Func<AgentDeliveryPrice, bool>> predicate);
        Task<AgentDeliveryPrice> InsertAgentDeliveryPriceAsync(AgentDeliveryPrice agentDeliveryPrice);
        Task<AgentDeliveryPrice?> DeleteAgentDeliveryPriceAsync(long id);
        
        
        Task<IEnumerable<AgentOrderDeliveryPrice>> GetAgentsOrdersDeliveryPricesAsync();
        Task<AgentOrderDeliveryPrice?> GetAgentOrderDeliveryPriceByIdAsync(long id);
        Task<IEnumerable<AgentOrderDeliveryPrice>> GetAgentOrderDeliveryPriceByAsync(Expression<Func<AgentOrderDeliveryPrice, bool>> predicate);
        Task<AgentOrderDeliveryPrice> InsertAgentOrderDeliveryPriceAsync(AgentOrderDeliveryPrice agentOrderDeliveryPrice);
        Task<AgentOrderDeliveryPrice?> UpdateAgentOrderDeliveryPriceAsync(AgentOrderDeliveryPrice agentOrderDeliveryPrice);
        Task<AgentOrderDeliveryPrice?> DeleteAgentOrderDeliveryPriceAsync(long id);
        
        
        Task<IEnumerable<AgentMessageHub>> GetAllAgentsMessageHubAsync();
        Task<AgentMessageHub?> GetAgentMessageHubByIdAsync(long id);
        Task<IEnumerable<AgentMessageHub>> GetAgentsMessageHubByAsync(Expression<Func<AgentMessageHub, bool>> predicate);
        Task<AgentMessageHub> InsertAgentMessageHubAsync(AgentMessageHub agentMessageHub);
        
        
    }
}
