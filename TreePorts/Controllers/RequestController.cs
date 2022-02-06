using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Timers;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TreePorts.DTO;
using TreePorts.DTO.ReturnDTO;
using TreePorts.Hubs;
using TreePorts.Models;
using TreePorts.Presentation;
using TreePorts.Utilities;

namespace TreePorts.Controllers
{
	 //[Route("/api/v1/[controller]")]
	// [Route("[controller]")]
	[Route("/api/v1/")]
	[ApiController]
	public class RequestController : ControllerBase
	{

		private IUnitOfWork _unitOfWork;
		private readonly IMapper mapper;
		private IWebHostEnvironment _hostingEnvironment;
		private IHubContext<MessageHub> _HubContext;
		private readonly INotifyService _notify;
		private readonly IOrderService _orderService;
		public RequestController(
			IUnitOfWork unitOfWork,
			IMapper mapper, 
			IWebHostEnvironment hostingEnvironment, 
			IHubContext<MessageHub> hubcontext, 
			INotifyService notify,
			IOrderService orderService
			)
		{
			_unitOfWork = unitOfWork;
			this.mapper = mapper;
			_hostingEnvironment = hostingEnvironment;
			_HubContext = hubcontext;
			_notify = notify;
			_orderService = orderService;
		}




		// GET: Request/CaptainCurrentLocation/id
		// [HttpGet("CaptainCurrentLocation/{id}")]
		//[HttpGet("Captain/Location/{id}")]
		[HttpGet("Orders/{id}/Location")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> GetCaptainCurrentLocationByOrderId(long id)
		{
			try
			{
				var userType = "";
				var userId = long.Parse("0");
				Utility.getRequestUserIdFromToken(HttpContext, out userId, out userType);

				var agentOrder = _unitOfWork.AgentRepository.GetAgentOrder(userId, id);
				if (userType.ToString() != "Agent" || agentOrder == null) return Unauthorized();
				
				var orderStatues = await _unitOfWork.OrderRepository.GetOrderCurrentStatusesByAsync(o => o.OrderId == id &&
					o.IsCurrent == true &&
					(o.StatusTypeId == (long)OrderStatusTypes.AssignedToCaptain || o.StatusTypeId == (long)OrderStatusTypes.Progress));

				var orderCurrentState = orderStatues.FirstOrDefault();
				if (orderCurrentState == null) NotFound("Driver not available, please try again");


				var orderAssigns = await _unitOfWork.OrderRepository.GetOrdersAssignmentsByAsync(a => a.OrderId == id);
				var orderAssign = orderAssigns.FirstOrDefault();
				if (orderAssign == null) NotFound("Driver not available, Please try again");

				var driverLocations = await _unitOfWork.CaptainRepository.GetUsersCurrentLocationsByAsync(u => u.UserId == orderAssign.UserId);
				var driverCurrentLoation = driverLocations.FirstOrDefault();
				if (driverCurrentLoation == null) NotFound("Driver not available, Please try again");

				return Ok(new { Latitude = double.Parse(driverCurrentLoation.Lat), longitude = double.Parse(driverCurrentLoation.Long) });

			}
			catch (Exception e)
			{
				return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
			}
		}



		// POST: Request/UpdateOrderLocations
		//[AllowAnonymous]
		// [HttpPost("UpdateOrderLocations")]
		//[HttpPost("Orders/UpdateLocations")]
		[HttpPut("Orders/{id}/Locations")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> UpdateOrderLocationsByOrderId(long id,[FromBody] Order order)
		{
			try
			{
				string userType = "";
				long userId = -1;
				Utility.getRequestUserIdFromToken(HttpContext, out userId, out userType);
				if (userType.ToLower() != "agent" || userId <= 0) return Unauthorized();

				if (order == null) return NoContent();//

				if (order.Id <= 0 && id > 0) order.Id = id;

				var agentOrder = _unitOfWork.AgentRepository.GetAgentOrder(userId, order.Id);
				if (agentOrder == null) return Unauthorized();
				

				var targetOrder = await _unitOfWork.OrderRepository.GetOrderDetailsByIdAsync(order.Id);
				if (targetOrder == null) return NotFound("Order Not Found");

				if (order?.PickupLocationLat?.ToString() != "")
					targetOrder.Order.PickupLocationLat = order.PickupLocationLat;

				if (order?.PickupLocationLong?.ToString() != "")
					targetOrder.Order.PickupLocationLong = order.PickupLocationLong;

				if (order?.DropLocationLat?.ToString() != "")
					targetOrder.Order.DropLocationLat = order.DropLocationLat;

				if (order?.DropLocationLong?.ToString() != "")
					targetOrder.Order.DropLocationLong = order.DropLocationLong;

				var insertResullt = await _unitOfWork.OrderRepository.UpdateOrderLocationAsync(targetOrder.Order);
				var result = await _unitOfWork.Save();
				if (result == 0) return new ObjectResult("Service Unavailable") {StatusCode = 503};

				return Ok(new { Result = true, Message = "Updated successfully" });
			}
			catch (Exception e)
			{
				return NoContent();// new ObjectResult(e.Message) {StatusCode = 666};
			}
		}



		// POST: Request/Registeration
		//[HttpPost("Registeration")]
		[HttpPost("Agents")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> AddAgent([FromBody] Agent agent)
		{
			try
			{
				var oldAgent = await _unitOfWork.AgentRepository.GetAgentByEmailAsync(agent.Email.ToLower());
				//var oldAgent = agents.FirstOrDefault();
				if (oldAgent != null)
					return Ok("User already registered");


				/*agents = await _unitOfWork.AgentRepository.GetBy(a => a.Email == agent.Email);
				oldAgent = agents.FirstOrDefault();
				if (oldAgent != null)
					return new ObjectResult("Email already registered") { StatusCode = 406 };*/


				//byte[] passwordHash, passwordSalt;
				//var password = Utility.GeneratePassword();
				//Utility.CreatePasswordHash(password, out passwordHash, out passwordSalt);
				//agent.PasswordHash = passwordHash;
				//agent.PasswordSalt = passwordSalt;
				string tempImage = "";
				if (!(bool)agent?.Image.ToLower().Contains(".jpeg")
					&& !(bool)agent?.Image.ToLower().Contains(".jpg")
					&& !(bool)agent?.Image.ToLower().Contains(".png"))
				{
					tempImage = agent?.Image;
					agent.Image = "";
				}

				agent.StatusTypeId = (long)StatusTypes.Reviewing;
				agent.Email = agent.Email.ToLower();
				var insertResult = await _unitOfWork.AgentRepository.InsertAgentAsync(agent);
				var result = await _unitOfWork.Save();

				if (result == 0)
					return new ObjectResult("Service Unavailable") { StatusCode = 503 };


				AgentCurrentStatus newAgentCurrentStatus = new AgentCurrentStatus()
				{
					AgentId = insertResult.Id,
					StatusId = (long)StatusTypes.New,
					IsCurrent = false,
					CreationDate = DateTime.Now
				};

				AgentCurrentStatus incompleteAgentCurrentStatus = new AgentCurrentStatus()
				{
					AgentId = insertResult.Id,
					StatusId = (long)StatusTypes.Reviewing,
					IsCurrent = true,
					CreationDate = DateTime.Now
				};

				if (tempImage != "")
				{
					insertResult = convertAndSaveAgentImages(insertResult);
					var updateAgentImageResult = await _unitOfWork.AgentRepository.UpdateAgentImageAsync(insertResult);
				}

				var newAgentStatusInsertedResult = await _unitOfWork.AgentRepository.InsertAgentCurrentStatusAsync(newAgentCurrentStatus);
				var incompleteAgentStatusInsertedResult = await _unitOfWork.AgentRepository.InsertAgentCurrentStatusAsync(incompleteAgentCurrentStatus);

				result = await _unitOfWork.Save();
				if (result == 0)
					return new ObjectResult("Service Unavailable") { StatusCode = 503 };

				var message = insertResult.Id.ToString();
				await _HubContext.Clients.All.SendAsync("NewAgentNotify", message);
				//var message2 = incompleteAgentInsertResult.Id.ToString();
				await _HubContext.Clients.All.SendAsync("NewInCompleteAgentNotify", message);



				//var token = Utility.GenerateToken(insertResult.Id, insertResult.Name, "Agent", null);
				//agent.Token = token;
				//var updateResult = await _unitOfWork.AgentRepository.UpdateToken(agent);
				//result = await _unitOfWork.Save();
				//if (result == 0) return new ObjectResult("Server not avalible") { StatusCode = 707 };

				return Ok("Registration success, and it's under reviewing");
			}
			catch (Exception e)
			{
				return NoContent(); // new ObjectResult(e.Message) { StatusCode = 666 };
			}
		}



		private Agent convertAndSaveAgentImages(Agent agent)
		{
			if (agent?.Image != null && agent?.Image != "" && !((bool)agent?.Image.Contains(".jpeg")))
			{

				var UserFolderPath = _hostingEnvironment.ContentRootPath + "/Assets/Images/Agents/" + agent.Id + "/PersonalPhotos";
				if (!Directory.Exists(UserFolderPath))
					Directory.CreateDirectory(UserFolderPath);

				string randum_numbers = Utility.GeneratePassword();
				string personalPhoto_name = "PP_" + randum_numbers.ToString() + ".jpeg";// PI is the first letter of PersonalImage
				bool result = Utility.SaveImage(agent?.Image, personalPhoto_name, UserFolderPath);
				if (result == true)
					agent.Image = personalPhoto_name;

			}
			return agent;

		}

		// [HttpPost("UpdateProfile")]
		[HttpPut("Agents/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> UpdateAgent(long? id ,[FromBody] Agent agent)
		{
			try
			{

				if (agent == null) return NoContent();
				if ((id == null || id <= 0)) return new ObjectResult("Agent {id} not provided in the request path") { StatusCode = 204 };

				if (id != null && id > 0)
					 agent.Id =(long) id;

				var userType = "";
				var userId = long.Parse("0");
				Utility.getRequestUserIdFromToken(HttpContext, out userId, out userType);
				if (userType.ToString() != "Agent" || id != userId || id != agent.Id) return Unauthorized();


				
				if (!(bool)agent?.Image.ToLower().Contains(".jpeg")
					&& !(bool)agent?.Image.ToLower().Contains(".jpg")
					&& !(bool)agent?.Image.ToLower().Contains(".png"))
				{
					agent = convertAndSaveAgentImages(agent);
				}


				var updateResult = await _unitOfWork.AgentRepository.UpdateAgentAsync(agent);
				var result = await _unitOfWork.Save();
				if (result == 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };

				return Ok(new { Result = true, Message = "User updated successfully" });
				
			}
			catch (Exception e)
			{
				return NoContent(); // new ObjectResult(e.Message) { StatusCode = 666 };
			}
		}

		
		
		// GET: Agent
		[HttpGet("Agents/Types")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AgentType>))]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> AgentTypes()
		{
			try
			{

				var userType = "";
				var userId = long.Parse("0");
				Utility.getRequestUserIdFromToken(HttpContext, out userId, out userType);
				if (userId <= 0 || userType.ToLower() != "agent" ) return Unauthorized();


				var types = await _unitOfWork.AgentRepository.GetAgentTypesAsync();

				return Ok(types);

			}
			catch (Exception e)
			{
				return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
			}
		}
		
		
		//  Request/GetAgentTypes/2
		[HttpGet("Agents/Types/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AgentType))]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> GetAgentTypes(long id)
		{
			try
			{

				var userType = "";
				var userId = long.Parse("0");
				Utility.getRequestUserIdFromToken(HttpContext, out userId, out userType);
				if (userType.ToString() != "Agent") return Unauthorized();


				var types = await _unitOfWork.AgentRepository.GetAgentTypeByIdAsync(id);
				return Ok(types);

			}
			catch (Exception e)
			{
				return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
			}
		}

		

		// GET: API/V1/Orders/{id}
		[HttpGet("Orders/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderDetails))]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> Order(long id)
		{
			try
			{
				long userId = -1;
				string userType = "";
				Utility.getRequestUserIdFromToken(HttpContext, out userId, out userType);
				
				if(userType.ToLower() != "agent" || userId <= 0) return Unauthorized();
				
				var result = await _unitOfWork.OrderRepository.GetOrderDetailsByIdAsync(id);

				if (result == null) return NotFound("No Order Found");
				if (result == null || result.Order.AgentId != userId) return Unauthorized();

				return Ok(result);

				// var statuses = await _unitOfWork.OrderRepository.GetOrderStatusBy(o => o.OrderId == resullt.Id);
				// var status = statuses.FirstOrDefault();
				//
				// var orderStatus = await _unitOfWork.OrderRepository.GetOrderStatusTypeByID((long)status.StatusTypeId);
				//
				// var invoices = await _unitOfWork.OrderRepository.GetOrderInvoiceBy(i => i.OrderId == resullt.Id);
				// var invoice = invoices.FirstOrDefault();
				//
				//
				// return Ok(new { Order = resullt, CurrentStatus = orderStatus, Invoice = invoice });
			}
			catch (Exception e)
			{
				return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
			}
		}


		// GET: API/V1/Orders
		[HttpGet("Orders")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Order>))]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> Orders() // its using the agent id from the token
		{
			try
			{
				long userId = -1;
				string userType = "";
				Utility.getRequestUserIdFromToken(HttpContext, out userId, out userType);
				if (userType.ToString() != "Agent" || userId <= 0) return Unauthorized();
				
				var result = await _unitOfWork.OrderRepository.GetOrdersByAsync(o => o.AgentId == userId);
				if (result == null ) return NotFound("No Orders Found");
				return Ok(result);
			}
			catch (Exception e)
			{
				return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
			}
		}



		// POST: Request/Captain
		//[AllowAnonymous]
		[HttpPost("Orders")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> AddOrder([FromBody] Order order, [FromQuery] string CouponCode)
		{
			try
			{

				long agentId = -1;
				string userType = "";
				Utility.getRequestUserIdFromToken(HttpContext, out agentId, out userType);
				if (userType.ToString() != "Agent" || agentId <= 0) return Unauthorized();


				if (order == null || order.PickupLocationLat == null || order.PickupLocationLong == null ||
					order.PickupLocationLat == "" || order.PickupLocationLong == "")
					return NoContent();


				if (order.AgentId == null || order.AgentId <= 0)
					order.AgentId = agentId;

				var orderInsertResult = await _unitOfWork.OrderRepository.InsertOrderAsync(order);
				var result = await _unitOfWork.Save();
				if (result <= 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };

				_ = _notify.ChangeOrderStatusAndNotify(OrderStatusTypes.New, orderInsertResult.Id,
					(long)orderInsertResult.AgentId);

				_ = _orderService.SearchForCaptainAndNotifyOrder(orderInsertResult);

				return Ok(new
				{
					OrderNumber = orderInsertResult.Id,
					OrderStatus = "New",
					Message = "Order saved and starting search for near captain"
				});


				
			}
			catch (Exception e)
			{
				return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
			}
		}



		




		/*// POST: Request/Captain
        //[AllowAnonymous]
        [HttpPost("Orders")]
        public async Task<IActionResult> Orders([FromBody] Order order, [FromQuery] string CouponCode)
        {
        	try
        	{

        		if (order == null || order.PickupLocationLat == null || order.PickupLocationLong == null ||
        			order.PickupLocationLat == "" || order.PickupLocationLong == "")
        			return new ObjectResult("Your request has no data") { StatusCode = 406 };


        		if (order.AgentId is null || order.AgentId <= 0)
        		{
        			long agentId = -1;
        			string userType = "";
        			Utility.getRequestUserIdFromToken(HttpContext, out agentId, out userType);
        			order.AgentId = agentId;
        		}

        		var orderInsertResult = await _unitOfWork.OrderRepository.InsertOrder(order);
        		var result = await _unitOfWork.Save();
        		if (result <= 0) return new ObjectResult("Server not available") { StatusCode = 503 };

        		_ = _notify.ChangeOrderStatusAndNotify(OrderStatusTypes.New, orderInsertResult.Id,
        			(long)orderInsertResult.AgentId);


        		var captain = await _unitOfWork.UserRepository.GetUserNearestLocation(order.PickupLocationLat, order.PickupLocationLong);
        		if (captain is null)
        		{
        			_ = _notify.ChangeOrderStatusAndNotify(OrderStatusTypes.NotAssignedToCaptain, orderInsertResult.Id,
        				(long)orderInsertResult.AgentId);

        			return Ok(new
        			{
        				OrderNumber = orderInsertResult.Id,
        				OrderStatus = "Not Assigned To captain",
        				Message = "No Captains available"
        			});
        		}





        		UserNewRequest driverRequest = new UserNewRequest()
        		{
        			OrderId = orderInsertResult.Id,
        			UserId = captain.Id,
        			AgentId = orderInsertResult.AgentId,
        			CreatedBy = 1,
        			CreationDate = DateTime.Now
        		};

        		var insertNewRequestResult = await _unitOfWork.UserRepository.InsertUserNewRequest(driverRequest);
        		result = await _unitOfWork.Save();
        		if (result <= 0) return new ObjectResult("Server not available") { StatusCode = 503 };

        		var usersMessageHub = await _unitOfWork.UserRepository.GetUserMessageHubBy(u => u.UserId == captain.Id);
        		var userMessageHub = usersMessageHub.FirstOrDefault();
        		if (userMessageHub != null && userMessageHub.Id > 0)
        		{
        			var notificationReuslt = Utility.SendFirebaseNotification(_hostingEnvironment, "newRequest", orderInsertResult.Id.ToString(), userMessageHub.ConnectionId);
        			if (notificationReuslt == "") return new ObjectResult("No Captains available, Plase try again") { StatusCode = 406 };
        		}


        		int time = 1000;
        		Timer timer1 = new Timer
        		{
        			Interval = 1000 // one second
        		};
        		timer1.Enabled = true;
        		timer1.Elapsed += new ElapsedEventHandler((sender, elapsedEventArgs) => { time = time + 1000; });



        		bool isDriverAcceptOrder = false;
        		bool isDriverRejectOrder = false;
        		bool isDriverIgnoredOrder = false;

        		while (isDriverAcceptOrder == false && isDriverRejectOrder == false && isDriverIgnoredOrder == false && time <= 50000)
        		{
        			var orderAssigns = await _unitOfWork.OrderRepository.GetOrderAssignmentBy(r => r.OrderId == orderInsertResult.Id);
        			var orderAssign = orderAssigns.FirstOrDefault();
        			if (orderAssign != null)
        				isDriverAcceptOrder = true;

        			var ordersReject = await _unitOfWork.UserRepository.GetUserRejectedRequestBy(r => r.OrderId == orderInsertResult.Id);
        			var rejectedOrder = ordersReject.FirstOrDefault();
        			if (rejectedOrder != null)
        				isDriverRejectOrder = true;


        			var ordersIgnored = await _unitOfWork.UserRepository.GetUserIgnoredRequestBy(r => r.OrderId == orderInsertResult.Id);
        			var ignoredOrder = ordersIgnored.FirstOrDefault();
        			if (ignoredOrder != null)
        				isDriverIgnoredOrder = true;

        		}
        		timer1.Enabled = false;


        		if (isDriverAcceptOrder) // case captain accept the request
        		{
        			*//* Create QrCode and Insert*//*
        			var qRCode = Utility.CreateQRCode(captain.Id, orderInsertResult.Id);
        			var qRCodeResult = await _unitOfWork.OrderRepository.InsertQrCode(qRCode);
        			*//* Create QrCode and Insert*//*
        			result = await _unitOfWork.Save();
        			if (result <= 0) return new ObjectResult("Server not available") { StatusCode = 503 };


        			_ = _notify.ChangeOrderStatusAndNotify(OrderStatusTypes.AssignedToCaptain,orderInsertResult.Id, (long)orderInsertResult.AgentId);
        			return Ok(new
        			{
        				OrderNumber = orderInsertResult.Id,
        				OrderStatus = "Assigned To Captain",
        				CaptainFullname = captain.FirstName + " " + captain.FamilyName,
        				CaptainMobile = captain.Mobile,
        				QRCodeInBytes = qRCodeResult.Code,
        				QRCodeUrl = qRCodeResult.QrCodeUrl
        			});
        		}

        		// case captain reject or ignored the request, or didn't received the request because of firebase failure and the timeout passed
        		_ = _notify.ChangeOrderStatusAndNotify(OrderStatusTypes.NotAssignedToCaptain, orderInsertResult.Id,
        			(long)orderInsertResult.AgentId);
        		return Ok(new
        		{
        			OrderNumber = orderInsertResult.Id,
        			OrderStatus = "Not Assigned To captain",
        			Message = "No Captains available"
        		});

        	}
        	catch (Exception e)
        	{
        		return new ObjectResult(e.Message) { StatusCode = 666 };
        	}
        }*/











		// // POST: Request/Captain
		// //[AllowAnonymous]
		// [HttpPost("Orders")]
		// public async Task<IActionResult> Orders([FromBody] Order order, [FromQuery] string CouponCode)
		// {
		//     try
		//     {
		//
		//
		//
		//         if (order == null || order.PickupLocationLat == null || order.PickupLocationLong == null ||
		//             order.PickupLocationLat == "" || order.PickupLocationLong == "")
		//             return new ObjectResult("Your request has no data") { StatusCode = 406 };
		//
		//
		//         var driver = await _unitOfWork.UserRepository.GetUserNearestLocation(order.PickupLocationLat, order.PickupLocationLong);
		//
		//         if (driver == null) return new ObjectResult("No Captains available, Plase try again") { StatusCode = 406 };
		//
		//
		//         if (order.AgentId == null || order.AgentId <= 0)
		//         {
		//             long agentId = -1;
		//             string userType = "";
		//             Utility.getRequestUserIdFromToken(HttpContext, out agentId, out userType);
		//             order.AgentId = agentId;
		//         }
		//
		//
		//         OrderCurrentStatus orderStatus = new OrderCurrentStatus()
		//         {
		//             StatusTypeId = (long)OrderStatusTypes.New,
		//
		//             IsCurrent = true,
		//             CreatedBy = 1,
		//             CreationDate = DateTime.Now
		//         };
		//
		//         var insertResullt = await _unitOfWork.OrderRepository.InsertOrder(order);
		//
		//         order.OrderCurrentStatus.Add(orderStatus);
		//         var result = await _unitOfWork.Save();
		//         if (result == 0) return new ObjectResult("Server not available") { StatusCode = 503 };
		//
		//
		//         UserNewRequest driverRequest = new UserNewRequest()
		//         {
		//             OrderId = insertResullt.Id,
		//             UserId = driver.Id,
		//             AgentId = insertResullt.AgentId,
		//             CreatedBy = 1,
		//             CreationDate = DateTime.Now
		//         };
		//
		//
		//         var insertNewRequestResult = await _unitOfWork.UserRepository.InsertUserNewRequest(driverRequest);
		//         result = await _unitOfWork.Save();
		//         if (result == 0) return new ObjectResult("Server not available") { StatusCode = 503 };
		//
		//         var usersMessageHub = await _unitOfWork.UserRepository.GetUserMessageHubBy(u => u.UserId == driver.Id);
		//         var userMessageHub = usersMessageHub.FirstOrDefault();
		//         if (userMessageHub != null && userMessageHub.Id > 0)
		//         {
		//             //OneSignalAPI.SendMessage("newRequest", insertResullt.Id.ToString(),new string[] { userMessageHub.ConnectionId });
		//             //PushyAPI.SendPushMessage(userMessageHub.ConnectionId, "newRequest", insertResullt.Id.ToString(), false);
		//             var notificationReuslt = Utility.SendFirebaseNotification(_hostingEnvironment, "newRequest", insertResullt.Id.ToString(), userMessageHub.ConnectionId);
		//             //await _HubContext.Clients.Client(userMessageHub.ConnectionId).SendAsync("newRequest", insertResullt.Id.ToString());
		//
		//             if (notificationReuslt == "") return new ObjectResult("No Captains available, Plase try again") { StatusCode = 406 };
		//
		//         }
		//
		//
		//         //await Task.Delay(TimeSpan.FromSeconds(30)).ConfigureAwait(false);
		//
		//         int time = 1000;
		//         Timer timer1 = new Timer
		//         {
		//             Interval = 1000 // one second
		//         };
		//         timer1.Enabled = true;
		//         timer1.Elapsed += new ElapsedEventHandler((sender, elapsedEventArgs) => { time = time + 1000; });
		//
		//
		//         OrderAssignment orderAssign = null;
		//         bool isDriverAcceptOrder = false;
		//         bool isDriverRejectOrder = false;
		//         bool isDriverIgnoredOrder = false;
		//
		//         while (isDriverAcceptOrder == false && isDriverRejectOrder == false && isDriverIgnoredOrder == false && time <= 50000)
		//         {
		//             var orderAssigns = await _unitOfWork.OrderRepository.GetOrderAssignmentBy(r => r.OrderId == insertResullt.Id);
		//             orderAssign = orderAssigns.FirstOrDefault();
		//             if (orderAssign != null)
		//                 isDriverAcceptOrder = true;
		//
		//             var ordersReject = await _unitOfWork.UserRepository.GetUserRejectedRequestBy(r => r.OrderId == insertResullt.Id);
		//             var rejectedOrder = ordersReject.FirstOrDefault();
		//             if (rejectedOrder != null)
		//                 isDriverRejectOrder = true;
		//
		//
		//             var ordersIgnored = await _unitOfWork.UserRepository.GetUserIgnoredRequestBy(r => r.OrderId == insertResullt.Id);
		//             var IgnoredOrder = ordersIgnored.FirstOrDefault();
		//             if (IgnoredOrder != null)
		//                 isDriverIgnoredOrder = true;
		//
		//         }
		//         timer1.Enabled = false;
		//
		//
		//
		//         //var orderAssigns = await _unitOfWork.OrderRepository.GetOrderAssignmentBy(r => r.OrderId == insertResullt.Id);
		//         //var orderAssign = orderAssigns.FirstOrDefault();
		//         //if (orderAssign == null)
		//         //{
		//         //	var deletedOrderStatuse = await _unitOfWork.OrderRepository.DeleteOrderStatus(order.OrderCurrentStatus.FirstOrDefault().Id);
		//         //	var deletedResullt = await _unitOfWork.OrderRepository.DeleteOrderPermenet(insertResullt.Id);
		//         //	//var deleteQRCode = await _unitOfWork.OrderRepository.DeleteQrCode(qRCodeResult.Id);
		//         //	result = await _unitOfWork.Save();
		//         //	if (result == 0) return new ObjectResult("Server not available") { StatusCode = 503 };
		//
		//         //	return new ObjectResult("No Captains available, Plase try again") { StatusCode = 406 };
		//         //}
		//
		//         if (isDriverAcceptOrder)
		//         {
		//             /* Create QrCode and Insert*/
		//             var qRCode = Utility.CreateQRCode(driver.Id, order.Id);
		//             var qRCodeResult = await _unitOfWork.OrderRepository.InsertQrCode(qRCode);
		//             /* Create QrCode and Insert*/
		//             result = await _unitOfWork.Save();
		//             if (result == 0) return new ObjectResult("Server not available") { StatusCode = 503 };
		//
		//
		//
		//
		//             //Notify notify = new Notify(_unitOfWork, _HubContext);
		//             _= _notify.NotifyOrderStatusChanged(OrderStatusTypes.New,insertResullt.Id, (long)insertResullt.AgentId);
		//             return Ok(new
		//             {
		//                 OrderNumber = insertResullt.Id,
		//                 CaptainFullname = driver.FirstName + " " + driver.FamilyName,
		//                 CaptainMobile = driver.Mobile,
		//                 QRCodeInBytes = qRCodeResult.Code,
		//                 QRCodeUrl = qRCodeResult.QrCodeUrl
		//             });
		//         }
		//
		//
		//         var deletedOrderStatuse = await _unitOfWork.OrderRepository.DeleteOrderStatus(insertResullt.OrderCurrentStatus.FirstOrDefault().Id);
		//         var deletedResullt = await _unitOfWork.OrderRepository.DeleteOrderPermenet(insertResullt.Id);
		//         result = await _unitOfWork.Save();
		//         if (result == 0) return new ObjectResult("Server not available") { StatusCode = 503 };
		//
		//         return new ObjectResult("No Captains available, Plase try again") { StatusCode = 406 };
		//
		//         //var statusType = await _unitOfWork.OrderRepository.GetOrderStatusTypeByID((long)OrderStatusTypes.New);
		//         //var notifyStatus = new
		//         //{
		//         //	order_status = new { Status = statusType.Status, ArabicStatus = statusType.ArabicStatus },
		//         //	order_id = order.Id
		//         //};
		//         //var isRegistered = await _unitOfWork.HookRepository.GetHookByAgent(order.AgentId, (long)WebHookTypes.OrderStatus);
		//         //if (isRegistered != null)
		//         //{
		//
		//         //	Utility.ExecuteWebHook(isRegistered.Url, Utility.ConvertToJson(notifyStatus));
		//
		//         //}
		//         //var message = insertResullt.Id.ToString();
		//         //await _HubContext.Clients.All.SendAsync("NewOrderNotify", message);
		//
		//
		//
		//
		//         //var agent = await _unitOfWork.AgentRepository.GetByID((long)insertResullt.AgentId);
		//         //var countryId = agent.CountryId;
		//         //var coupon = await _unitOfWork.AgentRepository.GetCouponByCode(CouponCode);
		//         //var isValid = await _unitOfWork.AgentRepository.IsValidCoupon(CouponCode, order.AgentId, countryId);
		//         //if (isValid == true)
		//         //{
		//
		//         //	var couponUsage = new CouponUsage
		//         //	{
		//         //		AgentId = order.AgentId,
		//         //		OrderId = insertResullt.Id,
		//         //		CouponId = coupon.Id,
		//         //		UsageDate = DateTime.Now
		//
		//         //	};
		//         //	var insertedAppliedCoupon = await _unitOfWork.AgentRepository.InsertCouponUsage(couponUsage);
		//         //	var appliedCoupon = await _unitOfWork.Save();
		//         //	if (appliedCoupon == 0) return new ObjectResult("Server not available") { StatusCode = 503 };
		//
		//
		//         //}
		//
		//
		//
		//
		//
		//
		//
		//
		//     }
		//     catch (Exception e)
		//     {
		//         return new ObjectResult(e.Message) { StatusCode = 666 };
		//     }
		// }


		//      [HttpGet("TestWebHooks")]
		//public async Task<IActionResult> TestWebHooks([FromQuery] long agentId,[FromQuery] long statusId)
		//{
		//	try
		//	{
		//		//Notify notify = new Notify(_unitOfWork, _HubContext);

		//		_= _notify.NotifyOrderStatusChanged((OrderStatusTypes)statusId,30, agentId);

		//		//_ =Task.Run(() =>
		//		//{
		//		//	_notify.notifyNewOrder(30, id);
		//		//});

		//		//var result = await _notify.notifyNewOrder(30, id);
		//		//if (result == false) return new ObjectResult("Test WebHook Failed") { StatusCode = 503 };

		//		return Ok("Test WebHook Succeeded");
		//	}
		//	catch (Exception e)
		//	{
		//		Console.Out.Write(e.Message);
		//		Console.Out.Write(e.InnerException);
		//		return new ObjectResult("Test WebHook Failed") { StatusCode = 503 };
		//	}
		//}


		[HttpGet("TestWebHooks/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> TestWebHooks(long id)
        {
            try
            {
                //Notify notify = new Notify(_unitOfWork, _HubContext);
                var result = await _notify.NotifyNewOrder(30, id);
                if (result == false) return Ok("Test WebHook Failed");

                return Ok("Test WebHook Succeeded");
            }
            catch (Exception e)
            {
                Console.Out.Write(e.Message);
                Console.Out.Write(e.InnerException);
				return NoContent();// new ObjectResult("Test WebHook Failed") { StatusCode = 503 };
            }
        }




        [HttpPut("Orders/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> UpdateOrder(long? id,[FromBody] Order order)
		{
			try
			{
				string userType = "";
				long userId = -1;
				Utility.getRequestUserIdFromToken(HttpContext, out userId, out userType);
				if (userType != "Agent" || userId <= 0 || userId != order.AgentId) return Unauthorized();

				if (order == null) return NoContent();

				if ((id == null || id <= 0) && order.Id <= 0) return NoContent();

				if (id != null && id > 0 && order.Id > 0 && id != order.Id)
					return NotFound("Conflict in order id, you provide two different id");


				if (id == null || id <= 0)
					id = order.Id;


				var targetOrder = await _unitOfWork.OrderRepository.GetOrderDetailsByIdAsync((long)id);
				if (targetOrder == null) return NotFound("No Order Found");


				targetOrder.Order = Utility.UpdateOrder(targetOrder.Order, order);
				var insertResullt = await _unitOfWork.OrderRepository.UpdateOrderAsync(targetOrder.Order);
				var result = await _unitOfWork.Save();
				if (result == 0) return new ObjectResult("Service Unavailable") { StatusCode = 503 };

				var orderAssigns = await _unitOfWork.OrderRepository.GetOrdersAssignmentsByAsync(r => r.OrderId == id);
				var orderAssign = orderAssigns.FirstOrDefault();

				var usersMessageHub = await _unitOfWork.CaptainRepository.GetUsersMessageHubsByAsync(u => u.UserId == orderAssign.UserId);
				var userMessageHub = usersMessageHub.FirstOrDefault();
				if (userMessageHub != null && userMessageHub.Id > 0)
				{

					var notificationReuslt = Utility.SendFirebaseNotification(_hostingEnvironment, "OrderUpdated", insertResullt.Id.ToString(), userMessageHub.ConnectionId);

					if (notificationReuslt == "") return NotFound("Captain not available, please try again");

				}


				return Ok(new { Result= true, Message = "Updated successfully"});

			}
			catch (Exception e)
			{
				return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
			}
		}



		// GET: API/v1/Countries
		//[AllowAnonymous]
		[HttpGet("Countries")]
		[ProducesResponseType(StatusCodes.Status200OK,Type = typeof(IEnumerable<Country>))]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> Countries()
		{
			try
			{

				string userType = "";
				long userId = -1;
				Utility.getRequestUserIdFromToken(HttpContext, out userId, out userType);
				if (userType != "Agent" || userId <= 0 ) return Unauthorized();

				var result = await _unitOfWork.CountryRepository.GetCountriesAsync();
				return Ok(result);
			}
			catch (Exception e)
			{
				return NoContent();
			}

		}


		// GET: API/v1/Countries/{id}
		//[AllowAnonymous]
		[HttpGet("Countries/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Country))]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> Countries(long id)
		{
			try
			{
				string userType = "";
				long userId = -1;
				Utility.getRequestUserIdFromToken(HttpContext, out userId, out userType);
				if (userType != "Agent" || userId <= 0) return Unauthorized();

				var result = await _unitOfWork.CountryRepository.GetCountryByIdAsync(id);
				return Ok(result);
			}
			catch (Exception e)
			{
				return NoContent(); // new ObjectResult(e.Message) { StatusCode = 666 };
			}

		}


		// GET: API/v1/Cities
		//[AllowAnonymous]
		[HttpGet("Countries/Cities")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<City>))]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> Cities()
		{
			try
			{
				string userType = "";
				long userId = -1;
				Utility.getRequestUserIdFromToken(HttpContext, out userId, out userType);
				if (userType != "Agent" || userId <= 0) return Unauthorized();


				var result = await _unitOfWork.CountryRepository.GetCitiesAsync();
				return Ok(result);
			}
			catch (Exception e)
			{
				return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
			}

		}


		// GET: API/v1/Cities/{id}
		//[AllowAnonymous]
		[HttpGet("Countries/Cities/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<City>))]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> GetCityById(long id)
		{
			try
			{

				string userType = "";
				long userId = -1;
				Utility.getRequestUserIdFromToken(HttpContext, out userId, out userType);
				if (userType != "Agent" || userId <= 0) return Unauthorized();

				var result = await _unitOfWork.CountryRepository.GetCityByIdAsync(id);
				return Ok(result);
			}
			catch (Exception e)
			{
				return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
			}

		}

		// GET: API/v1/Countries/{id}/Cities
		//[AllowAnonymous]
		[HttpGet("Countries/{id}/Cities")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<City>))]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> GetCitiesByCountryId(long id)
		{
			try
			{

				string userType = "";
				long userId = -1;
				Utility.getRequestUserIdFromToken(HttpContext, out userId, out userType);
				if (userType != "Agent" || userId <= 0) return Unauthorized();

				var result = await _unitOfWork.CountryRepository.GetCitiesByAsync(c => c.CountryId == id);
				return Ok(result);
			}
			catch (Exception e)
			{
				return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
			}
		}

		// GET: Request/ProductsTypes
		//[AllowAnonymous]
		[HttpGet("Products/Types")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductType>))]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> GetProductsTypes()
		{
			try
			{
				string userType = "";
				long userId = -1;
				Utility.getRequestUserIdFromToken(HttpContext, out userId, out userType);
				if (userType != "Agent" || userId <= 0) return Unauthorized();

				var result = await _unitOfWork.OrderRepository.GetProductTypesAsync();
				return Ok(result);
			}
			catch (Exception e)
			{
				return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
			}

		}


		// GET: Request/ProductsTypes/2
		//[AllowAnonymous]
		[HttpGet("Products/Types/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductType))]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> ProductsTypeById(long id)
		{
			try
			{

				string userType = "";
				long userId = -1;
				Utility.getRequestUserIdFromToken(HttpContext, out userId, out userType);
				if (userType != "Agent" || userId <= 0) return Unauthorized();

				var result = await _unitOfWork.OrderRepository.GetProductTypeByIdAsync(id);
				return Ok(result);
			}
			catch (Exception e)
			{
				return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
			}

		}


		// GET: Request/ProductTypes
		//[AllowAnonymous]
		[HttpGet("Payments/Types")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PaymentType>))]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> PaymentTypes()
		{
			try
			{

				string userType = "";
				long userId = -1;
				Utility.getRequestUserIdFromToken(HttpContext, out userId, out userType);
				if (userType != "Agent" || userId <= 0) return Unauthorized();

				var result = await _unitOfWork.OrderRepository.GetPaymentTypesAsync();
				return Ok(result);
			}
			catch (Exception e)
			{
				return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
			}

		}


		// GET: Request/ProductTypes/2
		//[AllowAnonymous]
		[HttpGet("Payments/Types/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaymentType))]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> PaymentTypes(long id)
		{
			try
			{

				string userType = "";
				long userId = -1;
				Utility.getRequestUserIdFromToken(HttpContext, out userId, out userType);
				if (userType != "Agent" || userId <= 0) return Unauthorized();

				var result = await _unitOfWork.OrderRepository.GetPaymentTypeByIdAsync(id);
				return Ok(result);
			}
			catch (Exception e)
			{
				return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
			}

		}



		[HttpGet("Report")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> Report([FromQuery] FilterParameters reportParameters)
		{
			try
			{
				// 1-Check Role and Id

				var userType = "";
				var userId = long.Parse("0");
				Utility.getRequestUserIdFromToken(HttpContext, out userId, out userType);
				if (userType != "Agent" || userId <= 0) return Unauthorized();

				IQueryable<Order> query;

				// 2- Get Querable data depends on Role and Id 

				/*if (userType == "Driver")
				{
					 query = _unitOfWork.OrderRepository.GetUserAcceptedRequestByQuerable(u => u.UserId == userId).Select(o => o.Order);

				}
				else if (userType == "Agent")
				{
					query = _unitOfWork.OrderRepository.GetByQuerable(o => o.AgentId == userId);
				}
				else
				{
					query = _unitOfWork.OrderRepository.GetAllOrdersQuerable();
				}*/
				// 3- Call generic filter method that take query data and filterparameters
				query = _unitOfWork.OrderRepository.GetByQuerable(o => o.AgentId == userId);
				var ordersResult = Utility.GetFilter(reportParameters, query);
				var orders = this.mapper.Map<List<OrderResponse>>(ordersResult);
				// 4- Pagination Result

				var total = orders.Count;

				return Ok(new { Orders = orders, Total = total });

			}
			catch (Exception e)
			{
				return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
			}


		}


		/* Get Orders Reports */
		[HttpGet("Search")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> Search([FromQuery] FilterParameters parameters)
		{
			try
			{

				var userType = "";
				var userId = long.Parse("0");
				Utility.getRequestUserIdFromToken(HttpContext, out userId, out userType);
				if (userType != "Agent" || userId <= 0) return Unauthorized();

				//var query = _unitOfWork.OrderRepository.GetAllOrdersQuerable();
				var query = _unitOfWork.OrderRepository.GetByQuerable(o => o.AgentId == userId);
				var skip = (parameters.NumberOfObjectsPerPage * (parameters.Page - 1));
				var take = parameters.NumberOfObjectsPerPage;
				var totalResult = 0;
				var ordersResult = Utility.GetFilter2<Order>(parameters, query, skip, take, out totalResult);
				var orders = this.mapper.Map<List<OrderResponse>>(ordersResult.ToList());
				var total = orders.Count();
				
				var totalPages = (int)Math.Ceiling(totalResult / (double)parameters.NumberOfObjectsPerPage);
				foreach (var order in orders)
				{
					if (order.Code != null)
					{
						order.QRCodeUrl = Utility.ConvertImgToString(order.Code);
					}

				}
				return Ok(new { Orders = orders, TotalResult = totalResult, Page = parameters.Page, TotalPages = totalPages });
			}
			catch (Exception e)
			{
				return NoContent();// //new ObjectResult(e.Message) { StatusCode = 666 };
			}
		}
		
		
		
		


		[HttpGet("Webhooks/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		/*Register Hook*/
		public async Task<IActionResult> GetWebhooksByAgentId(long id)
		{
			try
			{
				var userType = "";
				var userId = long.Parse("0");
				Utility.getRequestUserIdFromToken(HttpContext, out userId, out userType);
				if (userType != "Agent" || userId != id) return Unauthorized();

				if (id <= 0)
					return NoContent();

				var result = await _unitOfWork.HookRepository.GetWebhooksByAgentIdAsync(id);
				return Ok(result);

			}
			catch (Exception e)
			{
				return NoContent(); // new ObjectResult(e.Message) { StatusCode = 666 };
			}
		}
		
		
		[HttpPost("Webhooks")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		/*Register Hook*/
		public async Task<IActionResult> AddWebhook(WebHook webHook)
		{
			try
			{
				var userType = "";
				var userId = long.Parse("0");
				Utility.getRequestUserIdFromToken(HttpContext, out userId, out userType);
				if (userType != "Agent" || userId != webHook?.AgentId) return Unauthorized();

				
				if (webHook == null || webHook?.AgentId <= 0)
					return NoContent();

				var addedResult = await _unitOfWork.HookRepository.InsertWebhookAsync(webHook);
				var result = await _unitOfWork.Save();
				if (result == 0) return new ObjectResult("Service Unavailable") { StatusCode = 503};

				return Ok("Hook Registered Successfully");

			}
			catch (Exception e)
			{
				return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
			}
		}


		[HttpGet("Webhooks/Types")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<WebHookType>))]
		//[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		//[ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		/*Retrieve Hook Types*/
		public async Task<IActionResult> WebhookTypes()
		{
			try
			{
				var webHooksResult = await _unitOfWork.HookRepository.GetWebhooksTypesAsync();
				//var webHooks = this.mapper.Map<List<WebHookTypeResponse>>(webHooksResult);
				return Ok(webHooksResult);

			}
			catch (Exception e)
			{
				return NoContent();// new ObjectResult(e.Message) { StatusCode = 666 };
			}
		}
		// POST: Request/UpdateOrder
		
		
	}
}
