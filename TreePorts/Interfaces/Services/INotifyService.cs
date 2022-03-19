using TreePorts.DTO;

namespace TreePorts.Interfaces.Services
{
    public interface INotifyService 
    {
        Task<bool> NotifyNewOrder(long orderId, string agentId,CancellationToken cancellationToken);
        Task<bool> NotifyOrderStatusChanged(OrderStatusTypes status ,long orderId, string agentId,CancellationToken cancellationToken);
        Task<bool> ChangeOrderStatusAndNotify(OrderStatusTypes status ,long orderId, string agentId,CancellationToken cancellationToken);
        Task<bool> SendGoogleCloudMessageToCaptain(string captainUserAccountId, string title, string message,CancellationToken cancellationToken);
    }
}