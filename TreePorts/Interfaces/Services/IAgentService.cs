using TreePorts.DTO;
using TreePorts.DTO.Records;

namespace TreePorts.Interfaces.Services;
public interface IAgentService
{
    Task<IEnumerable<Agent>> GetWorkingAgentsAsync();

    Task<IEnumerable<Agent>> GetActiveAgentsAsync();

    Task<Agent?> GetAgentByIdAsync(string id);

    Task<IEnumerable<AgentType>> GetAgentTypesAsync();

    Task<IEnumerable<Agent>> GetAgentsPagingAsync(FilterParameters parameters);

    Task<IEnumerable<Agent>> GetNewRegisteredAgentsAsync();

    Task<IEnumerable<Agent>> GetNewRegisteredAgentsPagingAsync(FilterParameters parameters);

    Task<Agent?> AddAgentAsync(AgentDto agentDto);

    Task<string> AcceptRegisterAgentAsync(string id);

    Task<Agent?> UpdateAgentAsync(string? id, AgentDto agentDto);
    Task<bool> DeleteAgentAsync(string id);
    Task<Agent?> LoginAsync(LoginUserDto loginUser);
    Task<Agent> UpdateAgentLoactionAsync(long id, Agent agent);

    Task<bool> UploadFileAsync(HttpContext httpContext);
    Task<object> ReportAsync(HttpContext httpContext, FilterParameters reportParameters);
    Task<object> SearchAsync(FilterParameters parameters);
    Task<object> CreateAgentCouponAsync(AgentCouponDto agentCouponDto);
    Task<object> AssignExistingCouponAsync(AssignCouponDto assignCouponDto);
    Task<object> ChartAsync();
    Task<Coupon> CheckCouponAsync(string? agentId, string couponCode, long? countryId);
    Task<object> GetAgentsDeliveryPricesAsync();
    //Task<object> GetAgentDeliveryPriceByIdAsync(long id);
    Task<IEnumerable<AgentDeliveryPrice>> GetAgentDeliveryPricesByAgentIdAsync(string id);
    Task<AgentDeliveryPrice> AddAgentDeliveryPriceAsync(AgentDeliveryPrice agentDeliveryPrice);
    Task<AgentDeliveryPrice> DeleteDeliveryPriceAsync(long id);
    Task<string> SendFirebaseNotificationAsync(FBNotify fbNotify);
}

