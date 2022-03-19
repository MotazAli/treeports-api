using TreePorts.DTO;
using System.Linq.Expressions;

namespace TreePorts.Repositories
{
    public class AgentRepository : IAgentRepository
    {

        private readonly TreePortsDBContext _context;

        public AgentRepository(TreePortsDBContext context)
        {
            _context = context;
        }

        public async Task<Agent?> DeleteAgentAsync(string id, CancellationToken cancellationToken)
        {
            var agent = await _context.Agents.FirstOrDefaultAsync(u => u.Id == id,cancellationToken);
            if (agent == null) return null;

            agent.IsDeleted = true;
            _context.Entry<Agent>(agent).State = EntityState.Modified;
            return agent;
        }

        public async Task<AgentCurrentStatus?> DeleteAgentCurrentStatusAsync(long id, CancellationToken cancellationToken)
        {
            var agent = await _context.AgentCurrentStatuses.FirstOrDefaultAsync(u => u.Id == id,cancellationToken);
            if (agent == null) return null;

            _context.AgentCurrentStatuses.Remove(agent);
            return agent;

        }

        public async Task<IEnumerable<AgentCurrentStatus>> GetAgentsCurrentStatusByAsync(Expression<Func<AgentCurrentStatus, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.AgentCurrentStatuses.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<AgentCurrentStatus?> GetAgentCurrentStatusByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.AgentCurrentStatuses.FirstOrDefaultAsync(u => u.Id == id,cancellationToken);
        }

        public async Task<IEnumerable<AgentType>> GetAgentTypesAsync(CancellationToken cancellationToken)
        {
            return await _context.AgentTypes.ToListAsync(cancellationToken);
        }

        public async Task<AgentType?> GetAgentTypeByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.AgentTypes.FirstOrDefaultAsync(a => a.Id == id,cancellationToken);
        }

        public async Task<IEnumerable<Agent>> GetAgentsAsync(CancellationToken cancellationToken)
        {
            return await _context.Agents.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Agent>> GetAgentsPagingAsync(int skip, int take, CancellationToken cancellationToken) 
        {
            return await _context.Agents
                .Where(a => a.StatusTypeId != (long)StatusTypes.Reviewing)
                .OrderByDescending(a => a.CreationDate)
                .Skip(skip)
                .Take(take)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Agent>> GetNewRegisteredAgentsPagingAsync(int skip, int take, CancellationToken cancellationToken) 
        {
            return await _context.Agents
                .Where(a => a.StatusTypeId == (long)StatusTypes.Reviewing)
                .OrderByDescending(a => a.CreationDate)
                .Skip(skip)
                .Take(take)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Agent>> GetActiveAgentsAsync(CancellationToken cancellationToken)
        {
            return await _context.Agents.Where(a => a.IsDeleted == false).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<AgentCurrentStatus>> GetAgentsCurrentStatusesAsync(CancellationToken cancellationToken)
        {
            return await _context.AgentCurrentStatuses.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Agent>> GetAgentsByAsync(Expression<Func<Agent, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.Agents.Where(predicate).ToListAsync(cancellationToken);
        }
        public IQueryable<Agent> GetByQuerable(Expression<Func<Agent, bool>> predicate)
        {
            return _context.Agents.Where(predicate);
        }

        public async Task<Agent?> GetAgentByIdAsync(string id, CancellationToken cancellationToken)
        {
            return await _context.Agents.FirstOrDefaultAsync(a => a.Id == id,  cancellationToken);
        }

        public async Task<Agent?> GetAgentByEmailAsync(string email, CancellationToken cancellationToken) {
            return await _context.Agents.FirstOrDefaultAsync(a => a.Email == email,cancellationToken);
        }



        public async Task<Agent> InsertAgentAsync(Agent agent, CancellationToken cancellationToken)
        {
            agent.Id = Guid.NewGuid().ToString();
            agent.CreationDate = DateTime.Now;
            var result = await _context.Agents.AddAsync(agent,cancellationToken);
            return result.Entity;
        }

        public async Task<AgentCurrentStatus> InsertAgentCurrentStatusAsync(AgentCurrentStatus agentCurrentStatus, CancellationToken cancellationToken)
        {
            var agent = await _context.AgentCurrentStatuses.FirstOrDefaultAsync(a => a.AgentId == agentCurrentStatus.AgentId && a.IsCurrent == true,cancellationToken);
            if (agent != null)
            {
                agent.IsCurrent = false;
                agent.ModifiedBy = agentCurrentStatus.ModifiedBy;
                agent.ModificationDate = DateTime.Now;
                _context.Entry<AgentCurrentStatus>(agent).State = EntityState.Modified;

            }

            agentCurrentStatus.IsCurrent = true;
            agentCurrentStatus.CreationDate = DateTime.Now;
            var result = await _context.AgentCurrentStatuses.AddAsync(agentCurrentStatus,cancellationToken);
            return result.Entity;

        }

        public async Task<Agent?> UpdateAgentAsync(Agent agent, CancellationToken cancellationToken)
        {
            var oldAgent = await _context.Agents.FirstOrDefaultAsync(a => a.Id == agent.Id,cancellationToken);
            if (oldAgent == null) return null;


            oldAgent.Fullname = agent.Fullname ?? oldAgent.Fullname;
            oldAgent.CountryId = agent.CountryId ?? oldAgent.CountryId;
            oldAgent.CityId = agent.CityId ?? oldAgent.CityId;
            oldAgent.Address = agent.Address ?? oldAgent.Address;
            oldAgent.Mobile = agent.Mobile ?? oldAgent.Mobile;
            oldAgent.Email = agent.Email ?? oldAgent.Email;
            oldAgent.AgentTypeId = agent.AgentTypeId ?? oldAgent.AgentTypeId;
            oldAgent.IsBranch = agent.IsBranch ?? oldAgent.IsBranch;
            oldAgent.LocationLat = agent.LocationLat ?? oldAgent.LocationLat;
            oldAgent.LocationLong = agent.LocationLong ?? oldAgent.LocationLong;
            oldAgent.StatusTypeId = agent.StatusTypeId ?? oldAgent.StatusTypeId;
            oldAgent.CommercialRegistrationNumber = agent.CommercialRegistrationNumber ?? oldAgent.CommercialRegistrationNumber;
            oldAgent.Website = agent.Website ?? oldAgent.Website;
            oldAgent.Image = agent.Image ?? oldAgent.Image;
            oldAgent.ModifiedBy = agent.ModifiedBy ?? oldAgent.ModifiedBy;
            oldAgent.CreatedBy = agent.CreatedBy ?? oldAgent.CreatedBy;
            oldAgent.CreationDate = agent.CreationDate ?? oldAgent.CreationDate;
            oldAgent.ModificationDate = DateTime.Now;
            _context.Entry<Agent>(oldAgent).State = EntityState.Modified;
            return oldAgent;
        }


        public async Task<Agent?> UpdateAgentImageAsync(Agent agent, CancellationToken cancellationToken) 
        {
            var oldAgent = await _context.Agents.FirstOrDefaultAsync(a => a.Id == agent.Id,cancellationToken);
            if (oldAgent == null) return null;

            oldAgent.Image = agent.Image ?? oldAgent.Image;
            oldAgent.ModifiedBy = agent.ModifiedBy ?? oldAgent.ModifiedBy;
            oldAgent.ModificationDate = DateTime.Now;
            _context.Entry<Agent>(oldAgent).State = EntityState.Modified;
            return oldAgent;
        }


        public async Task<AgentCurrentStatus?> UpdateAgentCurrentStatusAsync(AgentCurrentStatus agentCurrentStatus, CancellationToken cancellationToken)
        {
            var oldAgentCurrentStatus = await _context.AgentCurrentStatuses.FirstOrDefaultAsync(a => a.AgentId == agentCurrentStatus.AgentId,cancellationToken);
            if (oldAgentCurrentStatus == null) return null;

            oldAgentCurrentStatus.StatusTypeId = agentCurrentStatus.StatusTypeId;
            oldAgentCurrentStatus.IsCurrent = agentCurrentStatus.IsCurrent;
            oldAgentCurrentStatus.ModifiedBy = agentCurrentStatus.ModifiedBy;
            oldAgentCurrentStatus.ModificationDate = DateTime.Now;
            _context.Entry<AgentCurrentStatus>(oldAgentCurrentStatus).State = EntityState.Modified;
            return oldAgentCurrentStatus;
        }

        public async Task<Agent?> UpdateAgentLocationAsync(Agent agent, CancellationToken cancellationToken)
        {
            var oldAgent = await _context.Agents.FirstOrDefaultAsync(a => a.Id == agent.Id,cancellationToken);
            if (oldAgent == null) return null;


            oldAgent.LocationLat = agent.LocationLat;
            oldAgent.LocationLong = agent.LocationLong;
            oldAgent.ModifiedBy = agent.ModifiedBy;
            oldAgent.ModificationDate = DateTime.Now;
            _context.Entry<Agent>(oldAgent).State = EntityState.Modified;
            return oldAgent;
        }

        public async Task<Agent?> UpdateAgentTokenAsync(Agent agent, CancellationToken cancellationToken)
        {
            var oldAgent = await _context.Agents.FirstOrDefaultAsync(a => a.Id == agent.Id,cancellationToken);
            if (oldAgent == null) return null;


            oldAgent.Token = agent.Token;
            oldAgent.ModifiedBy = agent.ModifiedBy;
            oldAgent.ModificationDate = DateTime.Now;
            _context.Entry<Agent>(oldAgent).State = EntityState.Modified;
            return oldAgent;
        }

        public IQueryable<Agent> GetByQuerable()
        {
            var result = _context.Agents;
                //.Include(a => a.AgentType)
                //.Include(a => a.Orders).ThenInclude(o => o.UserAcceptedRequests).ThenInclude(u => u.User)
                //.Include(a => a.Orders).ThenInclude(o => o.OrderItems)
                //.Include(a => a.Country)
                //.Include(a => a.City);
            return result;
        }

        public async Task<Coupon> InsertCouponAsync(Coupon coupon, CancellationToken cancellationToken)
        {
            var result = await _context.Coupons.AddAsync(coupon,cancellationToken);
            return result.Entity;

        }

        public async Task<bool> IsValidCouponAsync(string couponName, string? agentId, long? countryId, CancellationToken cancellationToken)
        {
            var result = await _context.Coupons.FirstOrDefaultAsync(c => c.CouponName == couponName,cancellationToken);
            var couponAssign = new CouponAssign();
            if(result != null)
			{
                 couponAssign = await _context.CouponAssigns.FirstOrDefaultAsync( c => 
                 c.CouponId == result.Id && 
                 c.AgentId == agentId || 
                 c.CouponId == result.Id && 
                 c.CouponId == countryId , cancellationToken);
            }
           
			
			if (couponAssign!=null && result != null && result.CouponTypeId == (long) CouponTypes.ExpireByDate)
			{
				if(result.ExpireDate?.Date >= DateTime.Now.Date)
				{
                    return true;
                }
			}
            if (couponAssign != null && result != null && result.CouponTypeId == (long)CouponTypes.ExpireByUsage)
            {
                var usage = _context.CouponUsages.Count(c => c.CouponId == result.Id && c.AgentId == agentId);
                if (usage < result.NumberOfUse)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<IEnumerable<Order>> GetAgentOrdersAsync(string agentId, CancellationToken cancellationToken)
		{
            var orders = await  _context.Orders.Where(o => o.AgentId == agentId).ToListAsync(cancellationToken);
            return orders;
		}

		public async Task<Order?> GetAgentOrderAsync(string agentId, long orderId, CancellationToken cancellationToken)
		{
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId && o.AgentId == agentId,cancellationToken);
            return order;
        }

		public async Task<Coupon?> GetCouponAsync(long ID, CancellationToken cancellationToken)
		{
            var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.Id == ID,cancellationToken);
            return coupon;
		}
        public async Task<Coupon?> GetCouponByCodeAsync(string couponName, CancellationToken cancellationToken)
		{
             var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.CouponName == couponName,cancellationToken);
           // var coupon = _context.CouponAssigns.Where(c => c.AgentId == agentId && c.CouponId == orderId).Select(c => c.Coupon).FirstOrDefault();
            return coupon;
        }
       
        
        public async Task<Coupon?> GetAssignedCoupon(string agentId, long orderId, CancellationToken cancellationToken)
		{

			var couponAssign = await _context.CouponAssigns.FirstOrDefaultAsync(c => c.AgentId == agentId && c.CouponId == orderId,cancellationToken);
            if (couponAssign == null) return null;

            var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.Id == couponAssign.CouponId, cancellationToken);
			return coupon;
		}

		public async Task<object> AgentReportCountAsync(CancellationToken cancellationToken)
		{
            return await Task.Run(() =>  getAgentReportCount() , cancellationToken );    
        }


        private object getAgentReportCount() {
            var totalAgents = _context.Agents.Count();
            var newAgents = _context.Agents.Count(a => a.StatusTypeId == (long)StatusTypes.New);
            var readyAgents = _context.Agents.Count(a => a.StatusTypeId == (long)StatusTypes.Ready);
            var workingAgents = _context.Agents.Count(a => a.StatusTypeId == (long)StatusTypes.Working);
            var progressAgents = _context.Agents.Count(a => a.StatusTypeId == (long)StatusTypes.Progress);
            var suspendedAgents = _context.Agents.Count(a => a.StatusTypeId == (long)StatusTypes.Suspended);
            var stoppedAgents = _context.Agents.Count(a => a.StatusTypeId == (long)StatusTypes.Stopped);
            var reviewingAgents = _context.Agents.Count(a => a.StatusTypeId == (long)StatusTypes.Reviewing);
            var penaltyAgents = _context.Agents.Count(a => a.StatusTypeId == (long)StatusTypes.Penalty);
            var incompleteAgents = _context.Agents.Count(a => a.StatusTypeId == (long)StatusTypes.Incomplete);
            var completeAgents = _context.Agents.Count(a => a.StatusTypeId == (long)StatusTypes.Complete);
            return new
            {
                AgentsCount = totalAgents,
                NewCount = newAgents,
                ReadyCount = readyAgents,
                WorkingCount = workingAgents,
                ProgressCount = progressAgents,
                SuspendedCount = suspendedAgents,
                StoppedCount = stoppedAgents,
                ReviewingCount = reviewingAgents,
                PenaltyCount = penaltyAgents,
                InCompleteCount = incompleteAgents,
                CompleteCount = completeAgents

            };
        }


		public async Task<CouponUsage> InsertCouponUsageAsync(CouponUsage couponUsage, CancellationToken cancellationToken)
		{
            var couponUsageResult = await _context.CouponUsages.AddAsync(couponUsage,cancellationToken);
            return couponUsageResult.Entity;
		}

        //public async Task<IEnumerable<AgentDeliveryPrice>> GetAgentsDeliveryPrices()
        //{
        //    return await _context.AgentDeliveryPrices.Where(a => a.IsDeleted == false).ToListAsync();
        //}

        //public async Task<AgentDeliveryPrice> GetAgentDeliveryPriceById(long id)
        //{
        //    return await _context.AgentDeliveryPrices.Where(d => d.Id == id).FirstOrDefaultAsync();
        //}


        public async Task<object> GetAgentsDeliveryPricesAsync(CancellationToken cancellationToken)
        {
            var data = from agentDeliveryPrice in _context.AgentDeliveryPrices.AsNoTracking()
                       join agent in _context.Agents.AsNoTracking() on agentDeliveryPrice.AgentId equals agent.Id
                       select new {
                           ID = agent.Id,
                           Name = agent.Fullname,
                           Address = agent.Address,
                           Email = agent.Email,
                           Mobile = agent.Mobile,
                           AgentDeliveryPrice = agentDeliveryPrice
                       };
            return await data.ToListAsync(cancellationToken); 
        }

        public async Task<object?> GetAgentDeliveryPriceByIdAsync(long id, CancellationToken cancellationToken)
        {
            var data = from agentDeliveryPrice in _context.AgentDeliveryPrices.Where( a => a.Id == id).AsNoTracking()
                       join agent in _context.Agents.AsNoTracking() on agentDeliveryPrice.AgentId equals agent.Id
                       select new
                       {
                           Name = agent.Fullname,
                           Address = agent.Address,
                           Email = agent.Email,
                           Mobile = agent.Mobile,
                           AgentDeliveryPrice = agentDeliveryPrice
                       };
            return await data.FirstOrDefaultAsync(cancellationToken);
        }


        public async Task<IEnumerable<AgentDeliveryPrice>> GetAgentDeliveryPriceByAsync(Expression<Func<AgentDeliveryPrice, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.AgentDeliveryPrices.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<AgentDeliveryPrice> InsertAgentDeliveryPriceAsync(AgentDeliveryPrice agentDeliveryPrice, CancellationToken cancellationToken)
        {
            var oldAgentDeliveryPrice = await _context.AgentDeliveryPrices.FirstOrDefaultAsync(d => d.AgentId == agentDeliveryPrice.AgentId && d.IsCurrent == true,cancellationToken);
            if (oldAgentDeliveryPrice != null && oldAgentDeliveryPrice?.Id > 0)
            {
                oldAgentDeliveryPrice.ModificationDate = DateTime.Now;
                oldAgentDeliveryPrice.IsCurrent = false;
                _context.Entry<AgentDeliveryPrice>(oldAgentDeliveryPrice).State = EntityState.Modified;
            }

            agentDeliveryPrice.IsCurrent = true;
            agentDeliveryPrice.CreationDate = DateTime.Now;
            var insertResult = await _context.AgentDeliveryPrices.AddAsync(agentDeliveryPrice);
            return insertResult.Entity;
        }

        public async Task<AgentDeliveryPrice?> DeleteAgentDeliveryPriceAsync(long id, CancellationToken cancellationToken)
        {
            var oldAgentDeliveryPrice = await _context.AgentDeliveryPrices.FirstOrDefaultAsync(d => d.Id == id,cancellationToken);
            if (oldAgentDeliveryPrice == null) return null;
            
            oldAgentDeliveryPrice.ModificationDate = DateTime.Now;
            oldAgentDeliveryPrice.IsCurrent = false;
            oldAgentDeliveryPrice.IsDeleted = true;
            _context.Entry<AgentDeliveryPrice>(oldAgentDeliveryPrice).State = EntityState.Modified;
            return oldAgentDeliveryPrice;
        }

        public async Task<IEnumerable<AgentOrderDeliveryPrice>> GetAgentsOrdersDeliveryPricesAsync(CancellationToken cancellationToken)
        {
            return await _context.AgentOrderDeliveryPrices.ToListAsync(cancellationToken);
        }

        public async Task<AgentOrderDeliveryPrice?> GetAgentOrderDeliveryPriceByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.AgentOrderDeliveryPrices.FirstOrDefaultAsync(o => o.Id == id,cancellationToken);
        }

        public async Task<IEnumerable<AgentOrderDeliveryPrice>> GetAgentOrderDeliveryPriceByAsync(Expression<Func<AgentOrderDeliveryPrice, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.AgentOrderDeliveryPrices.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<AgentOrderDeliveryPrice> InsertAgentOrderDeliveryPriceAsync(AgentOrderDeliveryPrice agentOrderDeliveryPrice, CancellationToken cancellationToken)
        {
            agentOrderDeliveryPrice.CreationDate=DateTime.Now;
            var insertResult = await _context.AgentOrderDeliveryPrices.AddAsync(agentOrderDeliveryPrice,cancellationToken);
            return insertResult.Entity;
        }

        public async Task<AgentOrderDeliveryPrice?> UpdateAgentOrderDeliveryPriceAsync(AgentOrderDeliveryPrice agentOrderDeliveryPrice, CancellationToken cancellationToken)
        {
            var oldAgentOrderDeliveryPrice = await _context.AgentOrderDeliveryPrices
                .FirstOrDefaultAsync(o => o.Id == agentOrderDeliveryPrice.Id,cancellationToken);
            if (oldAgentOrderDeliveryPrice == null) return null;

            oldAgentOrderDeliveryPrice.OrderId = agentOrderDeliveryPrice.OrderId;
            oldAgentOrderDeliveryPrice.AgentDeliveryPriceId = agentOrderDeliveryPrice.AgentDeliveryPriceId;
            oldAgentOrderDeliveryPrice.ModificationDate=DateTime.Now;
            _context.Entry<AgentOrderDeliveryPrice>(oldAgentOrderDeliveryPrice).State = EntityState.Modified;
            return oldAgentOrderDeliveryPrice;
        }

        public async Task<AgentOrderDeliveryPrice?> DeleteAgentOrderDeliveryPriceAsync(long id, CancellationToken cancellationToken)
        {
            var oldAgentOrderDeliveryPrice = await _context.AgentOrderDeliveryPrices.FirstOrDefaultAsync(o => o.Id == id,cancellationToken);
            if (oldAgentOrderDeliveryPrice == null) return null;

            _context.AgentOrderDeliveryPrices.Remove(oldAgentOrderDeliveryPrice);
            return oldAgentOrderDeliveryPrice;
        }

        public async Task<IEnumerable<AgentMessageHub>> GetAllAgentsMessageHubAsync(CancellationToken cancellationToken)
        {
            return await _context.AgentMessageHubs.ToListAsync(cancellationToken);
        }

        public async Task<AgentMessageHub?> GetAgentMessageHubByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.AgentMessageHubs.FirstOrDefaultAsync(a => a.Id == id,cancellationToken);
        }

        public async Task<IEnumerable<AgentMessageHub>> GetAgentsMessageHubByAsync(Expression<Func<AgentMessageHub, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.AgentMessageHubs.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<AgentMessageHub> InsertAgentMessageHubAsync(AgentMessageHub agentMessageHub, CancellationToken cancellationToken)
        {
            var oldUserHub = await _context.AgentMessageHubs.FirstOrDefaultAsync(a => a.AgentId == agentMessageHub.AgentId,cancellationToken);
            if (oldUserHub != null && oldUserHub.Id > 0)
            {
                oldUserHub.ConnectionId = agentMessageHub.ConnectionId;
                oldUserHub.ModificationDate = DateTime.Now;
                _context.Entry<AgentMessageHub>(oldUserHub).State = EntityState.Modified;
                return oldUserHub;
            }
            else
            {
                agentMessageHub.CreationDate = DateTime.Now;
                var insertResult = await _context.AgentMessageHubs.AddAsync(agentMessageHub,cancellationToken);
                return insertResult.Entity;
            }
        }

        
    }
}