using TreePorts.DTO;
using TreePorts.DTO.Records;

namespace TreePorts.Interfaces.Services;
public interface IAgentService
{
    Task<IEnumerable<Agent>> GetWorkingAgentsAsync(CancellationToken cancellationToken);

    Task<IEnumerable<Agent>> GetActiveAgentsAsync(CancellationToken cancellationToken);

    Task<Agent?> GetAgentByIdAsync(string id, CancellationToken cancellationToken);

    Task<IEnumerable<AgentType>> GetAgentTypesAsync(CancellationToken cancellationToken);

    Task<IEnumerable<Agent>> GetAgentsPagingAsync(FilterParameters parameters, CancellationToken cancellationToken);

    Task<IEnumerable<Agent>> GetNewRegisteredAgentsAsync(CancellationToken cancellationToken);

    Task<IEnumerable<Agent>> GetNewRegisteredAgentsPagingAsync(FilterParameters parameters, CancellationToken cancellationToken);

    Task<Agent?> AddAgentAsync(AgentDto agentDto, CancellationToken cancellationToken);

    Task<string> AcceptRegisterAgentAsync(string id, CancellationToken cancellationToken);

    Task<Agent?> UpdateAgentAsync(string? id, AgentDto agentDto, CancellationToken cancellationToken);
    Task<bool> DeleteAgentAsync(string id, CancellationToken cancellationToken);
    Task<Agent?> LoginAsync(LoginUserDto loginUser, CancellationToken cancellationToken);
    Task<Agent> UpdateAgentLoactionAsync(long id, Agent agent, CancellationToken cancellationToken);

    Task<bool> UploadFileAsync(HttpContext httpContext, CancellationToken cancellationToken);
    Task<object> ReportAsync(HttpContext httpContext, FilterParameters reportParameters, CancellationToken cancellationToken);
    Task<object> SearchAsync(FilterParameters parameters, CancellationToken cancellationToken);
    Task<object> CreateAgentCouponAsync(AgentCouponDto agentCouponDto, CancellationToken cancellationToken);
    Task<object> AssignExistingCouponAsync(AssignCouponDto assignCouponDto, CancellationToken cancellationToken);
    Task<object> ChartAsync(CancellationToken cancellationToken);
    Task<Coupon> CheckCouponAsync(string? agentId, string couponCode, long? countryId, CancellationToken cancellationToken);
    Task<object> GetAgentsDeliveryPricesAsync(CancellationToken cancellationToken);
    //Task<object> GetAgentDeliveryPriceByIdAsync(long id);
    Task<IEnumerable<AgentDeliveryPrice>> GetAgentDeliveryPricesByAgentIdAsync(string id, CancellationToken cancellationToken);
    Task<AgentDeliveryPrice> AddAgentDeliveryPriceAsync(AgentDeliveryPrice agentDeliveryPrice, CancellationToken cancellationToken);
    Task<AgentDeliveryPrice> DeleteDeliveryPriceAsync(long id, CancellationToken cancellationToken);
    Task<string> SendFirebaseNotificationAsync(FBNotify fbNotify, CancellationToken cancellationToken);
}

