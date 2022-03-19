using System.Linq.Expressions;

namespace TreePorts.Interfaces.Repositories
{
    public interface IAgentRepository
    {
        Task<IEnumerable<Agent>> GetAgentsAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Agent>> GetAgentsPagingAsync(int skip,int take,CancellationToken cancellationToken);
        Task<IEnumerable<Agent>> GetNewRegisteredAgentsPagingAsync(int skip, int take,CancellationToken cancellationToken);
        Task<IEnumerable<Agent>> GetActiveAgentsAsync(CancellationToken cancellationToken);
        Task<Agent?> GetAgentByIdAsync(string id,CancellationToken cancellationToken);
        Task<Agent?> GetAgentByEmailAsync(string email,CancellationToken cancellationToken);
        Task<IEnumerable<Agent>> GetAgentsByAsync(Expression<Func<Agent, bool>> predicate,CancellationToken cancellationToken);
        IQueryable<Agent> GetByQuerable(Expression<Func<Agent, bool>> predicate);
        Task<Agent> InsertAgentAsync(Agent agent,CancellationToken cancellationToken);
        Task<Agent?> UpdateAgentAsync(Agent agent,CancellationToken cancellationToken);
        Task<Agent?> UpdateAgentImageAsync(Agent agent,CancellationToken cancellationToken);
        Task<Agent?> UpdateAgentLocationAsync(Agent agent,CancellationToken cancellationToken);
        Task<Agent?> UpdateAgentTokenAsync(Agent agent,CancellationToken cancellationToken);
        Task<Agent?> DeleteAgentAsync(string id,CancellationToken cancellationToken);


        Task<IEnumerable<AgentCurrentStatus>> GetAgentsCurrentStatusesAsync(CancellationToken cancellationToken);
        Task<AgentCurrentStatus?> GetAgentCurrentStatusByIdAsync(long id,CancellationToken cancellationToken);
        Task<IEnumerable<AgentCurrentStatus>> GetAgentsCurrentStatusByAsync(Expression<Func<AgentCurrentStatus, bool>> predicate,CancellationToken cancellationToken);
        Task<AgentCurrentStatus> InsertAgentCurrentStatusAsync(AgentCurrentStatus agentCurrentStatus,CancellationToken cancellationToken);
        Task<AgentCurrentStatus?> UpdateAgentCurrentStatusAsync(AgentCurrentStatus agentCurrentStatus,CancellationToken cancellationToken);
        Task<AgentCurrentStatus?> DeleteAgentCurrentStatusAsync(long id,CancellationToken cancellationToken);

        Task<IEnumerable<AgentType>> GetAgentTypesAsync(CancellationToken cancellationToken);
        Task<AgentType?> GetAgentTypeByIdAsync(long id,CancellationToken cancellationToken);
        IQueryable<Agent> GetByQuerable();
        Task<Coupon> InsertCouponAsync(Coupon coupon,CancellationToken cancellationToken);
        Task<Coupon?> GetCouponAsync(long id,CancellationToken cancellationToken);
        Task<bool> IsValidCouponAsync(string couponName, string? agentId, long? countryId,CancellationToken cancellationToken);
        Task<IEnumerable<Order>> GetAgentOrdersAsync(string agentId,CancellationToken cancellationToken);
        Task<Order?> GetAgentOrderAsync(string agentId, long orderId,CancellationToken cancellationToken);
      //  Coupon GetAgentCoupon(long agentId, long orderId,CancellationToken cancellationToken);
        Task<Coupon?> GetCouponByCodeAsync(string Code,CancellationToken cancellationToken);
        Task<object> AgentReportCountAsync(CancellationToken cancellationToken);

        Task<Coupon?> GetAssignedCoupon(string agentId, long orderId,CancellationToken cancellationToken);
        Task<CouponUsage> InsertCouponUsageAsync(CouponUsage couponUsage,CancellationToken cancellationToken);



        //Task<List<AgentDeliveryPrice>> GetAgentsDeliveryPrices(,CancellationToken cancellationToken);
        //Task<AgentDeliveryPrice> GetAgentDeliveryPriceById(long id,CancellationToken cancellationToken);
        Task<object> GetAgentsDeliveryPricesAsync(CancellationToken cancellationToken);
        Task<object> GetAgentDeliveryPriceByIdAsync(long id,CancellationToken cancellationToken);
        Task<IEnumerable<AgentDeliveryPrice>> GetAgentDeliveryPriceByAsync(Expression<Func<AgentDeliveryPrice, bool>> predicate,CancellationToken cancellationToken);
        Task<AgentDeliveryPrice> InsertAgentDeliveryPriceAsync(AgentDeliveryPrice agentDeliveryPrice,CancellationToken cancellationToken);
        Task<AgentDeliveryPrice?> DeleteAgentDeliveryPriceAsync(long id,CancellationToken cancellationToken);
        
        
        Task<IEnumerable<AgentOrderDeliveryPrice>> GetAgentsOrdersDeliveryPricesAsync(CancellationToken cancellationToken);
        Task<AgentOrderDeliveryPrice?> GetAgentOrderDeliveryPriceByIdAsync(long id,CancellationToken cancellationToken);
        Task<IEnumerable<AgentOrderDeliveryPrice>> GetAgentOrderDeliveryPriceByAsync(Expression<Func<AgentOrderDeliveryPrice, bool>> predicate,CancellationToken cancellationToken);
        Task<AgentOrderDeliveryPrice> InsertAgentOrderDeliveryPriceAsync(AgentOrderDeliveryPrice agentOrderDeliveryPrice,CancellationToken cancellationToken);
        Task<AgentOrderDeliveryPrice?> UpdateAgentOrderDeliveryPriceAsync(AgentOrderDeliveryPrice agentOrderDeliveryPrice,CancellationToken cancellationToken);
        Task<AgentOrderDeliveryPrice?> DeleteAgentOrderDeliveryPriceAsync(long id,CancellationToken cancellationToken);
        
        
        Task<IEnumerable<AgentMessageHub>> GetAllAgentsMessageHubAsync(CancellationToken cancellationToken);
        Task<AgentMessageHub?> GetAgentMessageHubByIdAsync(long id,CancellationToken cancellationToken);
        Task<IEnumerable<AgentMessageHub>> GetAgentsMessageHubByAsync(Expression<Func<AgentMessageHub, bool>> predicate,CancellationToken cancellationToken);
        Task<AgentMessageHub> InsertAgentMessageHubAsync(AgentMessageHub agentMessageHub,CancellationToken cancellationToken);
        
        
    }
}
