using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TreePorts.DTO;
using TreePorts.Hubs;
using TreePorts.Interfaces.Repositories;
using TreePorts.Interfaces.Services;
using TreePorts.Models;
using TreePorts.Repositories;
using TreePorts.Utilities;

namespace TreePorts.Presentation
{
    public class NotifyService : INotifyService
    {

		private  IUnitOfWork _unitOfWork;
		//private readonly IMapper mapper;
		//private IWebHostEnvironment _hostingEnvironment;
		private  IHubContext<MessageHub> _hubContext;
		private readonly IServiceProvider _sp;
		/*public Notify(IServiceProvider sp, IHubContext<MessageHub> hubcontext)//(IUnitOfWork unitOfWork, IHubContext<MessageHub> hubcontext)//, IMapper mapper, IWebHostEnvironment hostingEnvironment, IHubContext<MessageHub> hubcontext)
		{
			//_unitOfWork = unitOfWork;
			//this.mapper = mapper;
			//_hostingEnvironment = hostingEnvironment;
			_sp = sp;
			_hubContext = hubcontext;
		}*/


		public NotifyService(IUnitOfWork unitOfWork, IHubContext<MessageHub> hubcontext, IServiceProvider sp)//, IMapper mapper, IWebHostEnvironment hostingEnvironment, IHubContext<MessageHub> hubcontext)
		{
			_unitOfWork = unitOfWork;
			//this.mapper = mapper;
			//_hostingEnvironment = hostingEnvironment;
			_sp = sp;
			_hubContext = hubcontext;
		}


		/*public NotifyService( IServiceProvider sp)//, IMapper mapper, IWebHostEnvironment hostingEnvironment, IHubContext<MessageHub> hubcontext)
		{
			_sp = sp;
			
		}*/


		/*public Notify()
        {
        }*/

		public async Task<bool> NotifyNewOrder(long orderId, string agentId, CancellationToken cancellationToken)
		{

			try
			{
				//using (var scope = _sp.CreateScope())
				//{
					//var services = scope.ServiceProvider;
					//var _unitOfWork = services.GetRequiredService<IUnitOfWork>();
					var message = orderId.ToString();
					await _hubContext.Clients.All.SendAsync("NewOrderNotify", message,cancellationToken);

					var statusType = await _unitOfWork.OrderRepository.GetOrderStatusTypeByIdAsync((long)OrderStatusTypes.New,cancellationToken);
					var notifyStatus = new
					{
						order_status = new { Status = statusType?.Type, ArabicStatus = statusType?.ArabicType },
						order_id = orderId
					};
					var webhook = await _unitOfWork.HookRepository.GetWebhookByTypeIdAndAgentIdAsync(agentId, (long)WebHookTypes.OrderStatus,cancellationToken);
					if (webhook is null) return false;

					_ = Utility.ExecuteWebHook(webhook.Url, Utility.ConvertToJson(notifyStatus), cancellationToken);
					return true;
				//}
				
			}
			catch (Exception e)
			{
				Console.Out.Write(e.Message);
				Console.Out.Write(e.InnerException);
				return false;
			}

			
		}




        public async Task<bool> ChangeOrderStatusAndNotify(OrderStatusTypes status, long orderId, string agentId, CancellationToken cancellationToken)
        {

            try
            {

                //using (var dbContext = new SenderDBContext())
                using (var scope = _sp.CreateScope())
                {
                    //var _unitOfWork = new UnitOfWork(dbContext);
                    var services = scope.ServiceProvider;
					//var _unitOfWork = services.GetRequiredService<IUnitOfWork>();
					if (_unitOfWork == null) 
						_unitOfWork = services.GetRequiredService<IUnitOfWork>(); ;

                    OrderCurrentStatus orderCurrentStatus = new ()
                    {
                        OrderId = orderId,
                        OrderStatusTypeId = (long)status,
                        IsCurrent = true
                    };
                    var insertResult = await _unitOfWork.OrderRepository.InsertOrderStatusAsync(orderCurrentStatus,cancellationToken);
                    var updateOrderResult =
                        await _unitOfWork.OrderRepository.UpdateOrderCurrentStatusAsync(orderId, (long)status,cancellationToken);

                    var result = await _unitOfWork.Save(cancellationToken);
                    if (result <= 0) return false;

                    var message = orderId.ToString();
                    var messageHubMethod = MessageHub.OrderStatusMessageHub(status);

					if(_hubContext == null)
						_hubContext = services.GetRequiredService<IHubContext<MessageHub>>(); 


				   await _hubContext.Clients.All.SendAsync(messageHubMethod, message,cancellationToken);

                    var agentMessageHubs =
                        await _unitOfWork.AgentRepository.GetAgentsMessageHubByAsync(a => a.AgentId == agentId,cancellationToken);
                    var agentMessageHub = agentMessageHubs.FirstOrDefault();
                    if (agentMessageHub != null && agentMessageHub.Id > 0)
                        await _hubContext.Clients.Client(agentMessageHub.ConnectionId).SendAsync($"{messageHubMethod}_Agent", message, cancellationToken);


                    var statusType = await _unitOfWork.OrderRepository.GetOrderStatusTypeByIdAsync((long)status,cancellationToken);
                    var notifyStatus = new
                    {
                        order_status = new { Status = statusType.Type, ArabicStatus = statusType.ArabicType },
                        order_id = orderId
                    };
                    var webhook = await _unitOfWork.HookRepository.GetWebhookByTypeIdAndAgentIdAsync(agentId, (long)WebHookTypes.OrderStatus,cancellationToken);
                    if (webhook is null)
                        _ = Utility.ExecuteWebHook(webhook?.Url, Utility.ConvertToJson(notifyStatus), cancellationToken);


                    return true;
                }

            }
            catch (Exception e)
            {
                Console.Out.Write(e.Message);
                Console.Out.Write(e.InnerException);
                return false;
            }


        }



        public async Task<bool> NotifyOrderStatusChanged(OrderStatusTypes status, long orderId, string agentId, CancellationToken cancellationToken)
		{

			try
			{

				//using (var dbContext = new SenderDBContext())
				using (var scope = _sp.CreateScope())
				{
					//var _unitOfWork = new UnitOfWork(dbContext);
					var services = scope.ServiceProvider;
					var _unitOfWork = services.GetRequiredService<IUnitOfWork>();
					if (_unitOfWork == null) return false;	

					var message = orderId.ToString();
					var messageHubMethod = MessageHub.OrderStatusMessageHub(status);
					await _hubContext.Clients.All.SendAsync(messageHubMethod, message);

					var agentMessageHubs =
						await _unitOfWork.AgentRepository.GetAgentsMessageHubByAsync(a => a.AgentId == agentId,cancellationToken);
					var agentMessageHub = agentMessageHubs.FirstOrDefault();
					if (agentMessageHub != null && agentMessageHub.Id > 0)
						await _hubContext.Clients.Client(agentMessageHub.ConnectionId).SendAsync($"{messageHubMethod}_Agent", message,cancellationToken);


					var statusType = await _unitOfWork.OrderRepository.GetOrderStatusTypeByIdAsync((long)status,cancellationToken);
					var notifyStatus = new
					{
						order_status = new { Status = statusType?.Type, ArabicStatus = statusType?.ArabicType },
						order_id = orderId
					};
					var webhook = await _unitOfWork.HookRepository.GetWebhookByTypeIdAndAgentIdAsync(agentId, (long)WebHookTypes.OrderStatus, cancellationToken);
					if (webhook != null)
						_ = Utility.ExecuteWebHook(webhook?.Url, Utility.ConvertToJson(notifyStatus), cancellationToken);


					return true;
				}

			}
			catch (Exception e)
			{
				Console.Out.Write(e.Message);
				Console.Out.Write(e.InnerException);
				return false;
			}


		}


		public async Task<bool> SendGoogleCloudMessageToCaptain(string captainUserAccountId, string title,string message, CancellationToken cancellationToken) 
		{

			try
			{
				using (var scope = _sp.CreateScope())
				{
					
					var services = scope.ServiceProvider;
					var _unitOfWork = services.GetRequiredService<IUnitOfWork>();
					if (_unitOfWork == null) return false;

					var usersMessageHub = await _unitOfWork.CaptainRepository.GetCaptainUsersMessageHubsByAsync(u => u.CaptainUserAccountId == captainUserAccountId,cancellationToken);
					var userMessageHub = usersMessageHub.FirstOrDefault();
					if (userMessageHub != null && userMessageHub.Id > 0)
					{

						FirebaseNotificationResponse? responseResult = null;
						var sendresult = await FirebaseNotification.SendNotification(userMessageHub.ConnectionId, title, message,cancellationToken);

						if (sendresult != "")
							responseResult = JsonConvert.DeserializeObject<FirebaseNotificationResponse>(sendresult);

						if (responseResult == null || responseResult.messageId == "")
							return false; // Ok("Failed to send notification to captain");

					}
					return true;

				}
			}
			catch (Exception ex)
			{
				Console.Out.Write(ex.Message);
				Console.Out.Write(ex.InnerException);
				return false;
			}
	
		}

	}
}