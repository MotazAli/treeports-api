using TreePorts.Interfaces.Repositories;
using TreePorts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreePorts.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private TreePortsDBContext _context;
        private ICaptainRepository _CaptainRepository;
        private ICaptainUserAccountRepository _CaptainUserAccountRepository;
        private ICountryRepository _CountryRepository;
        private ISystemRepository _SystemRepository;
        private IAdminRepository _AdminRepository;
        private ISupportRepository _SupportRepository;
        private IAgentRepository _AgentRepositor;
        private IOrderRepository _OrderRepository;
        private IWebhookRepository _HookRepository;
        private IPaymentRepository _PaymentRepository;
        public UnitOfWork(TreePortsDBContext context)
        {
            _context = context;
        }

        public ICaptainRepository CaptainRepository => _CaptainRepository ??= new CaptainRepository(_context);
        public ICaptainUserAccountRepository CaptainUserAccountRepository =>  _CaptainUserAccountRepository ??= new CaptainUserAccountRepository(_context);
        public ICountryRepository CountryRepository => _CountryRepository ??= new CountryRepository(_context);
        public ISystemRepository SystemRepository =>  _SystemRepository ??= new SystemRepository(_context);
        public IAdminRepository AdminRepository => _AdminRepository ??= new AdminRepository(_context);
        public ISupportRepository SupportRepository => _SupportRepository ??= new SupportRepository(_context);
        public IAgentRepository AgentRepository => _AgentRepositor ??= new AgentRepository(_context);
        public IOrderRepository OrderRepository => _OrderRepository ??= new OrderRepository(_context);
		public IWebhookRepository HookRepository => _HookRepository ??= new WebhookRepository(_context);


		public IPaymentRepository PaymentRepository => _PaymentRepository ??= new PaymentRepository(_context);

		public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }


        public bool isContextValied()
        {
            return  (_context != null )? true: false;
        }


        public bool isDatabaseConnect()
        {
            return _context.Database.CanConnect();
        }

    }
}
