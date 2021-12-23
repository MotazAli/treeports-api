using System;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using TreePorts.DTO;
using TreePorts.Hubs;
using TreePorts.Infrastructure;
using TreePorts.Infrastructure.Services;
using TreePorts.Models;
using TreePorts.Utilities;

namespace TreePorts.Presentation
{
	public class OrderService : IOrderService
	{

		private readonly IUnitOfWork _unitOfWork;
		private readonly INotifyService _notify;
		private readonly IWebHostEnvironment _hostingEnvironment;
		private readonly IServiceProvider _sp;
		public OrderService(IServiceProvider sp,IUnitOfWork unitOfWork, INotifyService notify, IWebHostEnvironment hostingEnvironment)//, IMapper mapper, IWebHostEnvironment hostingEnvironment, IHubContext<MessageHub> hubcontext)
		{
			_unitOfWork = unitOfWork;
			_notify = notify;
			_hostingEnvironment = hostingEnvironment;
			_sp = sp;
		}


		public async Task<bool> SearchForCaptainAndNotifyOrder(Order order)
		{
			try
			{


				using (var scope = _sp.CreateScope())
				{
					//var _unitOfWork = new UnitOfWork(dbContext);
					var services = scope.ServiceProvider;
					var _unitOfWork = services.GetRequiredService<IUnitOfWork>();
					var _notify = services.GetRequiredService<INotifyService>();
					if (_unitOfWork == null) return false;
					if (_notify == null) return false;

                    _ = await _notify.ChangeOrderStatusAndNotify(OrderStatusTypes.SearchingForCaptain, order.Id,
						(long)order.AgentId);

					var captain = await _unitOfWork.UserRepository.GetUserNearestLocationAsync(order.PickupLocationLat, order.PickupLocationLong);
					if (captain == null)
					{
						_ = await _notify.ChangeOrderStatusAndNotify(OrderStatusTypes.NotAssignedToCaptain, order.Id,
							(long)order.AgentId);

						return false;
					}


					CaptainUserNewRequest driverRequest = new CaptainUserNewRequest()
					{
						OrderId = order.Id,
						UserId = captain.Id,
						AgentId = order.AgentId,
						CreatedBy = 1,
						CreationDate = DateTime.Now
					};

					var insertNewRequestResult = await _unitOfWork.UserRepository.InsertUserNewRequestAsync(driverRequest);
					var result = await _unitOfWork.Save();
					if (result <= 0) return false;

					var usersMessageHub = await _unitOfWork.UserRepository.GetUsersMessageHubsByAsync(u => u.UserId == captain.Id);
					var userMessageHub = usersMessageHub.FirstOrDefault();
					if (userMessageHub != null && userMessageHub.Id > 0)
					{
						var notificationReuslt = Utility.SendFirebaseNotification(_hostingEnvironment, "newRequest", order.Id.ToString(), userMessageHub.ConnectionId);
						if (notificationReuslt == "") return false;
					}


					int time = 1000;
					Timer timer1 = new Timer
					{
						Interval = 1000 // one second
					};
					timer1.Enabled = true;
					timer1.Elapsed += new ElapsedEventHandler((sender, elapsedEventArgs) => { time = time + 1000; });



					bool isDriverAcceptOrder = false;
					bool isDriverRejectOrIgnoredOrder = false;
					//bool isDriverIgnoredOrder = false;


					do
					{
						var orderAssigns = await _unitOfWork.OrderRepository.GetOrdersAssignmentsByAsync(r => r.OrderId == order.Id);
						var orderAssign = orderAssigns.FirstOrDefault();
						if (orderAssign != null)
							isDriverAcceptOrder = true;

						var ordersReject = await _unitOfWork.UserRepository.GetUsersRejectedRequestsByAsync(r => r.OrderId == order.Id);
						var rejectedOrder = ordersReject.FirstOrDefault();
						if (rejectedOrder != null)
							isDriverRejectOrIgnoredOrder = true;


						var ordersIgnored = await _unitOfWork.UserRepository.GetUsersIgnoredRequestsByAsync(r => r.OrderId == order.Id);
						var ignoredOrder = ordersIgnored.FirstOrDefault();
						if (ignoredOrder != null)
							isDriverRejectOrIgnoredOrder = true;

					} while (isDriverAcceptOrder == false && isDriverRejectOrIgnoredOrder == false && time <= 50000);
					timer1.Enabled = false;


					if (isDriverAcceptOrder) // case captain accept the request
					{
						/* Create QrCode and Insert*/
						var qRCode = Utility.CreateQRCode(captain.Id, order.Id);
						var qRCodeResult = await _unitOfWork.OrderRepository.InsertQrCodeAsync(qRCode);
						/* Create QrCode and Insert*/
						result = await _unitOfWork.Save();
						if (result <= 0) return false;

						_ = await _notify.ChangeOrderStatusAndNotify(OrderStatusTypes.AssignedToCaptain, order.Id, (long)order.AgentId);
						return true;
					}

					// case captain reject or ignored the request, or didn't received the request because of firebase failure and the timeout passed
					_ = await _notify.ChangeOrderStatusAndNotify(OrderStatusTypes.NotAssignedToCaptain, order.Id,
						(long)order.AgentId);
					return false;
				}
			}
			catch (Exception e)
			{
				return false;
			}
		}



		/*public async Task<bool> ChangeOrderStatusAndNotify(OrderStatusTypes status, long orderId, long agentId)
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

					_ = _notify.NotifyOrderStatusChanged(status, orderId, agentId);

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

*/

		public async Task<object> AddNewOrder( Order order)
		{
			try
			{

				if (order == null || order.PickupLocationLat == null || order.PickupLocationLong == null ||
					order.PickupLocationLat == "" || order.PickupLocationLong == "")
					return null; // new ObjectResult("Your request has no data") { StatusCode = 406 };


				

				order.CurrentStatus = (long)OrderStatusTypes.New;
				var orderInsertResult = await _unitOfWork.OrderRepository.InsertOrderAsync(order);
				var result = await _unitOfWork.Save();
				if (result <= 0) return null;


				OrderCurrentStatus orderCurrentStatus = new OrderCurrentStatus()
				{
					OrderId = orderInsertResult.Id,
					StatusTypeId = (long)OrderStatusTypes.New,
					IsCurrent = true
				};
				var insertResult = await _unitOfWork.OrderRepository.InsertOrderStatusAsync(orderCurrentStatus);
				var resultSecondeOperation = await _unitOfWork.Save();
				if (resultSecondeOperation <= 0) return null;


				_ = await _notify.NotifyOrderStatusChanged(OrderStatusTypes.New, orderInsertResult.Id,
					(long)orderInsertResult.AgentId);


				_ = SearchForCaptainAndNotifyOrder(orderInsertResult);

				return new
				{
					OrderNumber = orderInsertResult.Id,
					OrderStatus = "New",
					Message = "Order saved and starting search for near captain"
				};


			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return null;// new ObjectResult(e.Message) { StatusCode = 666 };
			}
		}

       
    }
}