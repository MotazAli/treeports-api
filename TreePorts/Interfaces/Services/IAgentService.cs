using TreePorts.DTO;

namespace TreePorts.Interfaces.Services;
public interface IAgentService
{
    Task<IEnumerable<Agent>> GetWorkingAgentsAsync();

    Task<IEnumerable<Agent>> GetActiveAgentsAsync();

    Task<Agent> GetAgentByIdAsync(long id);

    Task<IEnumerable<AgentType>> GetAgentTypesAsync();

    Task<IEnumerable<Agent>> GetAgentsPagingAsync(FilterParameters parameters);

    Task<IEnumerable<Agent>> GetNewRegisteredAgentsAsync();

    Task<IEnumerable<Agent>> GetNewRegisteredAgentsPagingAsync(FilterParameters parameters);

    Task<long> AddAgentAsync(Agent agent);

    Task<string> AcceptRegisterAgentAsync(long id);

    Task<Agent> UpdateAgentAsync(long? id, Agent agent);

    Task<bool> DeleteAgentAsync(long id);


    Task<Agent> LoginAsync(LoginUser user);
    Task<Agent> UpdateAgentLoactionAsync(long id, Agent agent);

    Task<bool> UploadFileAsync(HttpContext httpContext);
    Task<object> ReportAsync(HttpContext httpContext, FilterParameters reportParameters);
    Task<object> SearchAsync(FilterParameters parameters);
    Task<object> CreateAgentCouponAsync(AgentCouponDto agentCouponDto);
    Task<object> AssignExistingCouponAsync(AssignCouponDto assignCouponDto);
    Task<object> ChartAsync();
    Task<Coupon> CheckCouponAsync(long? id, string couponCode, long? countryId);
    Task<object> GetAgentsDeliveryPricesAsync();
    Task<object> GetAgentDeliveryPriceByIdAsync(long id);
    Task<IEnumerable<AgentDeliveryPrice>> GetAgentDeliveryPricesByAgentId(long id);
    Task<AgentDeliveryPrice> AddAgentDeliveryPriceAsync(AgentDeliveryPrice agentDeliveryPrice);
    Task<AgentDeliveryPrice> DeleteDeliveryPriceAsync(long id);
    Task<string> SendFirebaseNotificationAsync(FBNotify fbNotify);
}

