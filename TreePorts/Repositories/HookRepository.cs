using Microsoft.EntityFrameworkCore;
using TreePorts.Interfaces.Repositories;
using TreePorts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreePorts.Repositories
{
	public class HookRepository : IHookRepository
	{
		private readonly TreePortsDBContext _context;
		public HookRepository(TreePortsDBContext context)
		{
			_context = context;
		}

		public async Task<WebHook> GetWebhookByTypeIdAndAgentIdAsync(long? id, long? webhookTypeId)
		{
			return await _context.WebHooks.FirstOrDefaultAsync(wh => wh.AgentId == id && wh.TypeId == webhookTypeId);
		}

		public async Task<List<WebHookType>> GetWebhooksTypesAsync()
		{
			return await _context.WebHookTypes.ToListAsync();
		}

        public async Task<Object> GetWebhooksByAgentIdAsync(long? id)
        {
			return await (from agentWebhooks in _context.WebHooks
						  join webhooks in _context.WebHookTypes
						  on agentWebhooks.TypeId equals webhooks.Id
						  where agentWebhooks.AgentId == id
						  select new { agent_id = agentWebhooks.AgentId, url = agentWebhooks.Url, type = webhooks.HookType }).ToListAsync();

			
		}

        public async Task<WebHook> InsertWebhookAsync(WebHook webhook)
		{
			var oldWebhook = await _context.WebHooks.FirstOrDefaultAsync(wh => wh.AgentId == webhook.AgentId && wh.TypeId == webhook.TypeId);
			if(oldWebhook != null)
			{
				oldWebhook.Url = webhook.Url;
				_context.Entry<WebHook>(oldWebhook).State = EntityState.Modified;
				return oldWebhook;
			}
			var result = await _context.WebHooks.AddAsync(webhook);
			return result.Entity;
		}

		
	}
}
