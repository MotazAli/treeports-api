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
        Task<List<Agent>> GetAgentsAsync();
        Task<List<Agent>> GetActiveAgentsAsync();
        Task<Agent> GetAgentByIdAsync(long id);
        Task<Agent> GetAgentByEmailAsync(string email);
        Task<List<Agent>> GetAgentsByAsync(Expression<Func<Agent, bool>> predicate);
        IQueryable<Agent> GetByQuerable(Expression<Func<Agent, bool>> predicate);
        Task<Agent> InsertAgentAsync(Agent agent);
        Task<Agent> UpdateAgentAsync(Agent agent);
        Task<Agent> UpdateAgentImageAsync(Agent agent);
        Task<Agent> UpdateAgentLocationAsync(Agent agent);
        Task<Agent> UpdateAgentTokenAsync(Agent agent);
        Task<Agent> DeleteAgentAsync(long id);


        Task<List<AgentCurrentStatus>> GetAgentsCurrentStatusesAsync();
        Task<AgentCurrentStatus> GetAgentCurrentStatusByIdAsync(long id);
        Task<List<AgentCurrentStatus>> GetAgentsCurrentStatusByAsync(Expression<Func<AgentCurrentStatus, bool>> predicate);
        Task<AgentCurrentStatus> InsertAgentCurrentStatusAsync(AgentCurrentStatus agentCurrentStatus);
        Task<AgentCurrentStatus> UpdateAgentCurrentStatusAsync(AgentCurrentStatus agentCurrentStatus);
        Task<AgentCurrentStatus> DeleteAgentCurrentStatusAsync(long id);

        Task<List<AgentType>> GetAgentTypesAsync();
        Task<AgentType> GetAgentTypeByIdAsync(long id);
        IQueryable<Agent> GetByQuerable();
        Task<Coupon> InsertCouponAsync(Coupon coupon);
        Task<Coupon> GetCouponAsync(long id);
        Task<bool> IsValidCouponAsync(string coupon, long? agentId, long? countryId);
        List<Order> GetAgentOrders(long agentId);
        Order GetAgentOrder(long agentId, long orderId);
      //  Coupon GetAgentCoupon(long agentId, long orderId);
        Task<Coupon> GetCouponByCodeAsync(string Code);
        Task<object> AgentReportCountAsync();

        Coupon GetAssignedCoupon(long agentId, long orderId);
        Task<CouponUsage> InsertCouponUsageAsync(CouponUsage couponUsage);



        //Task<List<AgentDeliveryPrice>> GetAgentsDeliveryPrices();
        //Task<AgentDeliveryPrice> GetAgentDeliveryPriceById(long id);
        Task<object> GetAgentsDeliveryPricesAsync();
        Task<object> GetAgentDeliveryPriceByIdAsync(long id);
        Task<List<AgentDeliveryPrice>> GetAgentDeliveryPriceByAsync(Expression<Func<AgentDeliveryPrice, bool>> predicate);
        Task<AgentDeliveryPrice> InsertAgentDeliveryPriceAsync(AgentDeliveryPrice agentDeliveryPrice);
        Task<AgentDeliveryPrice> DeleteAgentDeliveryPriceAsync(long id);
        
        
        Task<List<AgentOrderDeliveryPrice>> GetAgentsOrdersDeliveryPricesAsync();
        Task<AgentOrderDeliveryPrice> GetAgentOrderDeliveryPriceByIdAsync(long id);
        Task<List<AgentOrderDeliveryPrice>> GetAgentOrderDeliveryPriceByAsync(Expression<Func<AgentOrderDeliveryPrice, bool>> predicate);
        Task<AgentOrderDeliveryPrice> InsertAgentOrderDeliveryPriceAsync(AgentOrderDeliveryPrice agentOrderDeliveryPrice);
        Task<AgentOrderDeliveryPrice> UpdateAgentOrderDeliveryPriceAsync(AgentOrderDeliveryPrice agentOrderDeliveryPrice);
        Task<AgentOrderDeliveryPrice> DeleteAgentOrderDeliveryPriceAsync(long id);
        
        
        Task<List<AgentMessageHub>> GetAllAgentsMessageHubAsync();
        Task<AgentMessageHub> GetAgentMessageHubByIdAsync(long id);
        Task<List<AgentMessageHub>> GetAgentsMessageHubByAsync(Expression<Func<AgentMessageHub, bool>> predicate);
        Task<AgentMessageHub> InsertAgentMessageHubAsync(AgentMessageHub agentMessageHub);
        
        
    }
}
