using TreePorts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreePorts.Interfaces.Repositories
{
	public interface IHookRepository
	{
		Task<WebHook> InsertWebhookAsync(WebHook webhook);
		Task<List<WebHookType>> GetWebhooksTypesAsync();
		Task<WebHook> GetWebhookByTypeIdAndAgentIdAsync(long? id, long? webhookTypeId);
		Task<Object> GetWebhooksByAgentIdAsync(long? id);
	}
}
