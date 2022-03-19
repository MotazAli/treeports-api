using TreePorts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreePorts.Interfaces.Repositories
{
	public interface IWebhookRepository
	{
		Task<Webhook?> InsertOrUpdateAgentWebhookAsync(Webhook webhook,CancellationToken cancellationToken);
		Task<List<WebhookType>> GetWebhooksTypesAsync(CancellationToken cancellationToken);
		Task<Webhook?> GetWebhookByTypeIdAndAgentIdAsync(string? agentId, long? webhookTypeId,CancellationToken cancellationToken);
		Task<object?> GetWebhooksByAgentIdAsync(string? agentId,CancellationToken cancellationToken);
	}
}
