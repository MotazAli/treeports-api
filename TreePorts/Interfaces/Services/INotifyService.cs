using TreePorts.DTO;
using TreePorts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TreePorts.Interfaces.Services
{
    public interface INotifyService 
    {
        Task<bool> NotifyNewOrder(long orderId, long agentId);
        Task<bool> NotifyOrderStatusChanged(OrderStatusTypes status ,long orderId, long agentId);
        Task<bool> ChangeOrderStatusAndNotify(OrderStatusTypes status ,long orderId, long agentId);
        Task<bool> SendGoogleCloudMessageToCaptain(long userId, string title, string message);
    }
}