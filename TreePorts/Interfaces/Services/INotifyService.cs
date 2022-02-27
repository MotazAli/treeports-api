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
        Task<bool> NotifyNewOrder(long orderId, string agentId);
        Task<bool> NotifyOrderStatusChanged(OrderStatusTypes status ,long orderId, string agentId);
        Task<bool> ChangeOrderStatusAndNotify(OrderStatusTypes status ,long orderId, string agentId);
        Task<bool> SendGoogleCloudMessageToCaptain(string captainUserAccountId, string title, string message);
    }
}