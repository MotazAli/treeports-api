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

        public async Task<Agent?> DeleteAgentAsync(string id)
        {
            var agent = await _context.Agents.FirstOrDefaultAsync(u => u.Id == id);
            if (agent == null) return null;

            agent.IsDeleted = true;
            _context.Entry<Agent>(agent).State = EntityState.Modified;
            return agent;
        }

        public async Task<AgentCurrentStatus?> DeleteAgentCurrentStatusAsync(long id)
        {
            var agent = await _context.AgentCurrentStatuses.FirstOrDefaultAsync(u => u.Id == id);
            if (agent == null) return null;

            _context.AgentCurrentStatuses.Remove(agent);
            return agent;

        }

        public async Task<IEnumerable<AgentCurrentStatus>> GetAgentsCurrentStatusByAsync(Expression<Func<AgentCurrentStatus, bool>> predicate)
        {
            return await _context.AgentCurrentStatuses.Where(predicate).ToListAsync();
        }

        public async Task<AgentCurrentStatus?> GetAgentCurrentStatusByIdAsync(long id)
        {
            return await _context.AgentCurrentStatuses.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<AgentType>> GetAgentTypesAsync()
        {
            return await _context.AgentTypes.ToListAsync();
        }

        public async Task<AgentType?> GetAgentTypeByIdAsync(long id)
        {
            return await _context.AgentTypes.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Agent>> GetAgentsAsync()
        {
            return await _context.Agents.ToListAsync();
        }

        public async Task<IEnumerable<Agent>> GetAgentsPagingAsync(int skip, int take) 
        {
            return await _context.Agents
                .Where(a => a.StatusTypeId != (long)StatusTypes.Reviewing)
                .OrderByDescending(a => a.CreationDate)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<IEnumerable<Agent>> GetNewRegisteredAgentsPagingAsync(int skip, int take) 
        {
            return await _context.Agents
                .Where(a => a.StatusTypeId == (long)StatusTypes.Reviewing)
                .OrderByDescending(a => a.CreationDate)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<IEnumerable<Agent>> GetActiveAgentsAsync()
        {
            return await _context.Agents.Where(a => a.IsDeleted == false).ToListAsync();
        }

        public async Task<IEnumerable<AgentCurrentStatus>> GetAgentsCurrentStatusesAsync()
        {
            return await _context.AgentCurrentStatuses.ToListAsync();
        }

        public async Task<IEnumerable<Agent>> GetAgentsByAsync(Expression<Func<Agent, bool>> predicate)
        {
            return await _context.Agents.Where(predicate).ToListAsync();
        }
        public IQueryable<Agent> GetByQuerable(Expression<Func<Agent, bool>> predicate)
        {
            return _context.Agents.Where(predicate);
        }

        public async Task<Agent?> GetAgentByIdAsync(string id)
        {
            return await _context.Agents.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Agent?> GetAgentByEmailAsync(string email) {
            return await _context.Agents.FirstOrDefaultAsync(a => a.Email == email);
        }



        public async Task<Agent> InsertAgentAsync(Agent agent)
        {
            agent.Id = Guid.NewGuid().ToString();
            agent.CreationDate = DateTime.Now;
            var result = await _context.Agents.AddAsync(agent);
            return result.Entity;
        }

        public async Task<AgentCurrentStatus> InsertAgentCurrentStatusAsync(AgentCurrentStatus agentCurrentStatus)
        {
            var agent = await _context.AgentCurrentStatuses.FirstOrDefaultAsync(a => a.AgentId == agentCurrentStatus.AgentId && a.IsCurrent == true);
            if (agent != null)
            {
                agent.IsCurrent = false;
                agent.ModifiedBy = agentCurrentStatus.ModifiedBy;
                agent.ModificationDate = DateTime.Now;
                _context.Entry<AgentCurrentStatus>(agent).State = EntityState.Modified;

            }

            agentCurrentStatus.IsCurrent = true;
            agentCurrentStatus.CreationDate = DateTime.Now;
            var result = await _context.AgentCurrentStatuses.AddAsync(agentCurrentStatus);
            return result.Entity;

        }

        public async Task<Agent> UpdateAgentAsync(Agent agent)
        {
            var oldAgent = await _context.Agents.FirstOrDefaultAsync(a => a.Id == agent.Id);
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
            oldAgent.Website = oldAgent.Website ?? oldAgent.Website;
            oldAgent.Image = agent.Image ?? oldAgent.Image;
            oldAgent.ModifiedBy = agent.ModifiedBy ?? oldAgent.ModifiedBy;
            oldAgent.CreatedBy = agent.CreatedBy ?? oldAgent.CreatedBy;
            oldAgent.CreationDate = agent.CreationDate ?? oldAgent.CreationDate;
            oldAgent.ModificationDate = DateTime.Now;
            _context.Entry<Agent>(oldAgent).State = EntityState.Modified;
            return oldAgent;
        }


        public async Task<Agent> UpdateAgentImageAsync(Agent agent) 
        {
            var oldAgent = await _context.Agents.FirstOrDefaultAsync(a => a.Id == agent.Id);
            if (oldAgent == null) return null;

            oldAgent.Image = agent.Image ?? oldAgent.Image;
            oldAgent.ModifiedBy = agent.ModifiedBy ?? oldAgent.ModifiedBy;
            oldAgent.ModificationDate = DateTime.Now;
            _context.Entry<Agent>(oldAgent).State = EntityState.Modified;
            return oldAgent;
        }


        public async Task<AgentCurrentStatus?> UpdateAgentCurrentStatusAsync(AgentCurrentStatus agentCurrentStatus)
        {
            var oldAgentCurrentStatus = await _context.AgentCurrentStatuses.FirstOrDefaultAsync(a => a.AgentId == agentCurrentStatus.AgentId);
            if (oldAgentCurrentStatus == null) return null;

            oldAgentCurrentStatus.StatusTypeId = agentCurrentStatus.StatusTypeId;
            oldAgentCurrentStatus.IsCurrent = agentCurrentStatus.IsCurrent;
            oldAgentCurrentStatus.ModifiedBy = agentCurrentStatus.ModifiedBy;
            oldAgentCurrentStatus.ModificationDate = DateTime.Now;
            _context.Entry<AgentCurrentStatus>(oldAgentCurrentStatus).State = EntityState.Modified;
            return oldAgentCurrentStatus;
        }

        public async Task<Agent?> UpdateAgentLocationAsync(Agent agent)
        {
            var oldAgent = await _context.Agents.FirstOrDefaultAsync(a => a.Id == agent.Id);
            if (oldAgent == null) return null;


            oldAgent.LocationLat = agent.LocationLat;
            oldAgent.LocationLong = agent.LocationLong;
            oldAgent.ModifiedBy = agent.ModifiedBy;
            oldAgent.ModificationDate = DateTime.Now;
            _context.Entry<Agent>(oldAgent).State = EntityState.Modified;
            return oldAgent;
        }

        public async Task<Agent?> UpdateAgentTokenAsync(Agent agent)
        {
            var oldAgent = await _context.Agents.FirstOrDefaultAsync(a => a.Id == agent.Id);
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

        public async Task<Coupon> InsertCouponAsync(Coupon coupon)
        {
            var result = await _context.Coupons.AddAsync(coupon);
            return result.Entity;

        }

        public async Task<bool> IsValidCouponAsync(string couponName, string? agentId, long? countryId)
        {
            var result = await _context.Coupons.FirstOrDefaultAsync(c => c.CouponName == couponName);
            var couponAssign = new CouponAssign();
            if(result != null)
			{
                 couponAssign = await _context.CouponAssigns.FirstOrDefaultAsync(c => c.CouponId == result.Id && c.AgentId == agentId
           || c.CouponId == result.Id && c.CouponId == countryId);
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

        public async Task<IEnumerable<Order>> GetAgentOrdersAsync(string agentId)
		{
            var orders = await  _context.Orders.Where(o => o.AgentId == agentId).ToListAsync();
            return orders;
		}

		public async Task<Order?> GetAgentOrderAsync(string agentId, long orderId)
		{
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId && o.AgentId == agentId);
            return order;
        }

		public async Task<Coupon?> GetCouponAsync(long ID)
		{
            var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.Id == ID);
            return coupon;
		}
        public async Task<Coupon?> GetCouponByCodeAsync(string couponName)
		{
             var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.CouponName == couponName);
           // var coupon = _context.CouponAssigns.Where(c => c.AgentId == agentId && c.CouponId == orderId).Select(c => c.Coupon).FirstOrDefault();
            return coupon;
        }
       
        
        public async Task<Coupon?> GetAssignedCoupon(string agentId, long orderId)
		{

			var couponAssign = await _context.CouponAssigns.FirstOrDefaultAsync(c => c.AgentId == agentId && c.CouponId == orderId);
            if (couponAssign == null) return null;

            var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.Id == couponAssign.CouponId);
			return coupon;
		}

		public async Task<object> AgentReportCountAsync()
		{
            return await Task.Run(() => { return getAgentReportCount(); });    
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


		public async Task<CouponUsage> InsertCouponUsageAsync(CouponUsage couponUsage)
		{
            var couponUsageResult = await _context.CouponUsages.AddAsync(couponUsage);
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


        public async Task<object> GetAgentsDeliveryPricesAsync()
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
            return await data.ToListAsync(); 
        }

        public async Task<object> GetAgentDeliveryPriceByIdAsync(long id)
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
            return await data.FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<AgentDeliveryPrice>> GetAgentDeliveryPriceByAsync(Expression<Func<AgentDeliveryPrice, bool>> predicate)
        {
            return await _context.AgentDeliveryPrices.Where(predicate).ToListAsync();
        }

        public async Task<AgentDeliveryPrice> InsertAgentDeliveryPriceAsync(AgentDeliveryPrice agentDeliveryPrice)
        {
            AgentDeliveryPrice oldAgentDeliveryPrice = await _context.AgentDeliveryPrices.FirstOrDefaultAsync(d => d.AgentId == agentDeliveryPrice.AgentId && d.IsCurrent == true);
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

        public async Task<AgentDeliveryPrice?> DeleteAgentDeliveryPriceAsync(long id)
        {
            AgentDeliveryPrice oldAgentDeliveryPrice = await _context.AgentDeliveryPrices.FirstOrDefaultAsync(d => d.Id == id);
            if (oldAgentDeliveryPrice == null) return null;
            
            oldAgentDeliveryPrice.ModificationDate = DateTime.Now;
            oldAgentDeliveryPrice.IsCurrent = false;
            oldAgentDeliveryPrice.IsDeleted = true;
            _context.Entry<AgentDeliveryPrice>(oldAgentDeliveryPrice).State = EntityState.Modified;
            return oldAgentDeliveryPrice;
        }

        public async Task<IEnumerable<AgentOrderDeliveryPrice>> GetAgentsOrdersDeliveryPricesAsync()
        {
            return await _context.AgentOrderDeliveryPrices.ToListAsync();
        }

        public async Task<AgentOrderDeliveryPrice?> GetAgentOrderDeliveryPriceByIdAsync(long id)
        {
            return await _context.AgentOrderDeliveryPrices.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<AgentOrderDeliveryPrice>> GetAgentOrderDeliveryPriceByAsync(Expression<Func<AgentOrderDeliveryPrice, bool>> predicate)
        {
            return await _context.AgentOrderDeliveryPrices.Where(predicate).ToListAsync();
        }

        public async Task<AgentOrderDeliveryPrice> InsertAgentOrderDeliveryPriceAsync(AgentOrderDeliveryPrice agentOrderDeliveryPrice)
        {
            agentOrderDeliveryPrice.CreationDate=DateTime.Now;
            var insertResult = await _context.AgentOrderDeliveryPrices.AddAsync(agentOrderDeliveryPrice);
            return insertResult.Entity;
        }

        public async Task<AgentOrderDeliveryPrice?> UpdateAgentOrderDeliveryPriceAsync(AgentOrderDeliveryPrice agentOrderDeliveryPrice)
        {
            var oldAgentOrderDeliveryPrice = await _context.AgentOrderDeliveryPrices
                .FirstOrDefaultAsync(o => o.Id == agentOrderDeliveryPrice.Id);
            if (oldAgentOrderDeliveryPrice == null) return null;

            oldAgentOrderDeliveryPrice.OrderId = agentOrderDeliveryPrice.OrderId;
            oldAgentOrderDeliveryPrice.AgentDeliveryPriceId = agentOrderDeliveryPrice.AgentDeliveryPriceId;
            oldAgentOrderDeliveryPrice.ModificationDate=DateTime.Now;
            _context.Entry<AgentOrderDeliveryPrice>(oldAgentOrderDeliveryPrice).State = EntityState.Modified;
            return oldAgentOrderDeliveryPrice;
        }

        public async Task<AgentOrderDeliveryPrice?> DeleteAgentOrderDeliveryPriceAsync(long id)
        {
            var oldAgentOrderDeliveryPrice = await _context.AgentOrderDeliveryPrices.FirstOrDefaultAsync(o => o.Id == id);
            if (oldAgentOrderDeliveryPrice == null) return null;

            _context.AgentOrderDeliveryPrices.Remove(oldAgentOrderDeliveryPrice);
            return oldAgentOrderDeliveryPrice;
        }

        public async Task<IEnumerable<AgentMessageHub>> GetAllAgentsMessageHubAsync()
        {
            return await _context.AgentMessageHubs.ToListAsync();
        }

        public async Task<AgentMessageHub?> GetAgentMessageHubByIdAsync(long id)
        {
            return await _context.AgentMessageHubs.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<AgentMessageHub>> GetAgentsMessageHubByAsync(Expression<Func<AgentMessageHub, bool>> predicate)
        {
            return await _context.AgentMessageHubs.Where(predicate).ToListAsync();
        }

        public async Task<AgentMessageHub> InsertAgentMessageHubAsync(AgentMessageHub agentMessageHub)
        {
            var oldUserHub = await _context.AgentMessageHubs.FirstOrDefaultAsync(a => a.AgentId == agentMessageHub.AgentId);
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
                var insertResult = await _context.AgentMessageHubs.AddAsync(agentMessageHub);
                return insertResult.Entity;
            }
        }

        
    }
}