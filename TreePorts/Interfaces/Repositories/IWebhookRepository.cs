using TreePorts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreePorts.Interfaces.Repositories
{
	public interface IWebhookRepository
	{
		Task<Webhook?> InsertOrUpdateAgentWebhookAsync(Webhook webhook);
		Task<List<WebhookType>> GetWebhooksTypesAsync();
		Task<Webhook?> GetWebhookByTypeIdAndAgentIdAsync(string? agentId, long? webhookTypeId);
		Task<object?> GetWebhooksByAgentIdAsync(string? agentId);
	}
}
