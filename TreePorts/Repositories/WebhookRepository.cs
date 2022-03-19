
namespace TreePorts.Repositories
{
	public class WebhookRepository : IWebhookRepository
	{
		private readonly TreePortsDBContext _context;
		public WebhookRepository(TreePortsDBContext context)
		{
			_context = context;
		}

		public async Task<Webhook?> GetWebhookByTypeIdAndAgentIdAsync(string? agentId, long? webhookTypeId, CancellationToken cancellationToken)
		{
			return await _context.Webhooks.FirstOrDefaultAsync(wh => wh.AgentId == agentId && wh.WebhookTypeId == webhookTypeId,cancellationToken);
		}

		public async Task<List<WebhookType>> GetWebhooksTypesAsync(CancellationToken cancellationToken)
		{
			return await _context.WebhookTypes.ToListAsync(cancellationToken);
		}

        public async Task<object?> GetWebhooksByAgentIdAsync(string? agentId, CancellationToken cancellationToken)
        {
			return await (from agentWebhooks in _context.Webhooks
						  join webhooks in _context.WebhookTypes
						  on agentWebhooks.WebhookTypeId equals webhooks.Id
						  where agentWebhooks.AgentId == agentId
						  select new { agent_id = agentWebhooks.AgentId, url = agentWebhooks.Url, type = webhooks.Type }).ToListAsync(cancellationToken);

			
		}

        public async Task<Webhook?> InsertOrUpdateAgentWebhookAsync(Webhook webhook, CancellationToken cancellationToken)
		{
			var oldWebhook = await _context.Webhooks.FirstOrDefaultAsync(wh => wh.AgentId == webhook.AgentId && wh.WebhookTypeId == webhook.WebhookTypeId,cancellationToken);
			if(oldWebhook != null)
			{
				oldWebhook.Url = webhook.Url;
				_context.Entry<Webhook>(oldWebhook).State = EntityState.Modified;
				return oldWebhook;
			}
			var result = await _context.Webhooks.AddAsync(webhook,cancellationToken);
			return result.Entity;
		}

		
	}
}
