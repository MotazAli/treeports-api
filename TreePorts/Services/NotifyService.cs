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


		public NotifyService( IServiceProvider sp)//, IMapper mapper, IWebHostEnvironment hostingEnvironment, IHubContext<MessageHub> hubcontext)
		{
			_sp = sp;
			
		}


		/*public Notify()
        {
        }*/

		public async Task<bool> NotifyNewOrder(long orderId, long agentId)
		{

			try
			{
				//using (var scope = _sp.CreateScope())
				//{
					//var services = scope.ServiceProvider;
					//var _unitOfWork = services.GetRequiredService<IUnitOfWork>();
					var message = orderId.ToString();
					await _hubContext.Clients.All.SendAsync("NewOrderNotify", message);

					var statusType = await _unitOfWork.OrderRepository.GetOrderStatusTypeByIdAsync((long)OrderStatusTypes.New);
					var notifyStatus = new
					{
						order_status = new { Status = statusType.Status, ArabicStatus = statusType.ArabicStatus },
						order_id = orderId
					};
					var isRegistered = await _unitOfWork.HookRepository.GetWebhookByTypeIdAndAgentIdAsync(agentId, (long)WebHookTypes.OrderStatus);
					if (isRegistered is null) return false;

					Utility.ExecuteWebHook(isRegistered.Url, Utility.ConvertToJson(notifyStatus));
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




        public async Task<bool> ChangeOrderStatusAndNotify(OrderStatusTypes status, long orderId, long agentId)
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

                    OrderCurrentStatus orderCurrentStatus = new OrderCurrentStatus()
                    {
                        OrderId = orderId,
                        StatusTypeId = (long)status,
                        IsCurrent = true
                    };
                    var insertResult = await _unitOfWork.OrderRepository.InsertOrderStatusAsync(orderCurrentStatus);
                    var updateOrderResult =
                        await _unitOfWork.OrderRepository.UpdateOrderCurrentStatusAsync(orderId, (long)status);

                    var result = await _unitOfWork.Save();
                    if (result <= 0) return false;

                    var message = orderId.ToString();
                    var messageHubMethod = MessageHub.OrderStatusMessageHub(status);

					if(_hubContext == null)
						_hubContext = services.GetRequiredService<IHubContext<MessageHub>>(); 


				   await _hubContext.Clients.All.SendAsync(messageHubMethod, message);

                    var agentMessageHubs =
                        await _unitOfWork.AgentRepository.GetAgentsMessageHubByAsync(a => a.AgentId == agentId);
                    AgentMessageHub agentMessageHub = agentMessageHubs.FirstOrDefault();
                    if (agentMessageHub != null && agentMessageHub.Id > 0)
                        await _hubContext.Clients.Client(agentMessageHub.ConnectionId).SendAsync($"{messageHubMethod}_Agent", message);


                    var statusType = await _unitOfWork.OrderRepository.GetOrderStatusTypeByIdAsync((long)status);
                    var notifyStatus = new
                    {
                        order_status = new { Status = statusType.Status, ArabicStatus = statusType.ArabicStatus },
                        order_id = orderId
                    };
                    var isRegistered = await _unitOfWork.HookRepository.GetWebhookByTypeIdAndAgentIdAsync(agentId, (long)WebHookTypes.OrderStatus);
                    if (isRegistered != null)
                        Utility.ExecuteWebHook(isRegistered.Url, Utility.ConvertToJson(notifyStatus));


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



        public async Task<bool> NotifyOrderStatusChanged(OrderStatusTypes status, long orderId, long agentId)
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
						await _unitOfWork.AgentRepository.GetAgentsMessageHubByAsync(a => a.AgentId == agentId);
					AgentMessageHub agentMessageHub = agentMessageHubs.FirstOrDefault();
					if (agentMessageHub != null && agentMessageHub.Id > 0)
						await _hubContext.Clients.Client(agentMessageHub.ConnectionId).SendAsync($"{messageHubMethod}_Agent", message);


					var statusType = await _unitOfWork.OrderRepository.GetOrderStatusTypeByIdAsync((long)status);
					var notifyStatus = new
					{
						order_status = new { Status = statusType.Status, ArabicStatus = statusType.ArabicStatus },
						order_id = orderId
					};
					var isRegistered = await _unitOfWork.HookRepository.GetWebhookByTypeIdAndAgentIdAsync(agentId, (long)WebHookTypes.OrderStatus);
					if (isRegistered != null)
						Utility.ExecuteWebHook(isRegistered.Url, Utility.ConvertToJson(notifyStatus));


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


		public async Task<bool> SendGoogleCloudMessageToCaptain(long userId ,string title,string message) 
		{

			try
			{
				using (var scope = _sp.CreateScope())
				{
					
					var services = scope.ServiceProvider;
					var _unitOfWork = services.GetRequiredService<IUnitOfWork>();
					if (_unitOfWork == null) return false;

					var usersMessageHub = await _unitOfWork.CaptainRepository.GetUsersMessageHubsByAsync(u => u.UserId == userId);
					var userMessageHub = usersMessageHub.FirstOrDefault();
					if (userMessageHub != null && userMessageHub.Id > 0)
					{

						FirebaseNotificationResponse responseResult = null;
						var sendresult = FirebaseNotification.SendNotification(userMessageHub.ConnectionId, title, message);

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