using Microsoft.EntityFrameworkCore;
using TreePorts.Interfaces.Repositories;
using TreePorts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreePorts.Repositories
{
	public class WebhookRepository : IWebhookRepository
	{
		private readonly TreePortsDBContext _context;
		public WebhookRepository(TreePortsDBContext context)
		{
			_context = context;
		}

		public async Task<Webhook?> GetWebhookByTypeIdAndAgentIdAsync(string? agentId, long? webhookTypeId)
		{
			return await _context.Webhooks.FirstOrDefaultAsync(wh => wh.AgentId == agentId && wh.WebhookTypeId == webhookTypeId);
		}

		public async Task<List<WebhookType>> GetWebhooksTypesAsync()
		{
			return await _context.WebhookTypes.ToListAsync();
		}

        public async Task<object?> GetWebhooksByAgentIdAsync(string? agentId)
        {
			return await (from agentWebhooks in _context.Webhooks
						  join webhooks in _context.WebhookTypes
						  on agentWebhooks.WebhookTypeId equals webhooks.Id
						  where agentWebhooks.AgentId == agentId
						  select new { agent_id = agentWebhooks.AgentId, url = agentWebhooks.Url, type = webhooks.Type }).ToListAsync();

			
		}

        public async Task<Webhook?> InsertOrUpdateAgentWebhookAsync(Webhook webhook)
		{
			var oldWebhook = await _context.Webhooks.FirstOrDefaultAsync(wh => wh.AgentId == webhook.AgentId && wh.WebhookTypeId == webhook.WebhookTypeId);
			if(oldWebhook != null)
			{
				oldWebhook.Url = webhook.Url;
				_context.Entry<Webhook>(oldWebhook).State = EntityState.Modified;
				return oldWebhook;
			}
			var result = await _context.Webhooks.AddAsync(webhook);
			return result.Entity;
		}

		
	}
}
