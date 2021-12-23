using TreePorts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreePorts.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        ICaptainRepository CaptainRepository { get; }
        ICaptainUserAccountRepository CaptainUserAccountRepository { get; }
        ICountryRepository CountryRepository { get; }
        ISystemRepository SystemRepository { get; }
        IAdminRepository AdminRepository { get; }
        ISupportRepository SupportRepository { get; }
        IAgentRepository AgentRepository { get; }
        IOrderRepository OrderRepository { get; }
        IHookRepository HookRepository { get; }
        IPaymentRepository PaymentRepository { get; }
        Task<int> Save();
    }
}
