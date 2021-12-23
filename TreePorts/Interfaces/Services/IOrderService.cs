using TreePorts.DTO;
using TreePorts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TreePorts.Interfaces.Services
{
    public interface IOrderService 
    {
        Task<bool> SearchForCaptainAndNotifyOrder(Order order);

        //Task<bool> ChangeOrderStatusAndNotify(OrderStatusTypes status, long orderId, long agentId);

        Task<object> AddNewOrder(Order order);
    }
}