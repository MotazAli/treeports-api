using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using TreePorts.Interfaces.Repositories;
using TreePorts.Models;
using TreePorts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TreePorts.DTO;

namespace TreePorts.Hubs
{
    public class MessageHub : Hub
    {
        

        private IServiceProvider _sp;
        public MessageHub(IServiceProvider sp)
        {
            _sp = sp;
        }

        public async Task<string> GetConnectionId(string userType, string userId)
        {
            try
            {

                string userConnectionID = Context.ConnectionId;
                using (var scope = _sp.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var _unitOfWork = services.GetRequiredService<IUnitOfWork>();
                    switch (userType.ToLower())
                    {
                        // case "driver":
                        //     {
                        //         var insertedUser = await _unitOfWork.UserRepository.InsertUserMessageHub(long.Parse(userId), userConnectionID);
                        //         var result = await _unitOfWork.Save();
                        //         if (result == 0) return "user not found";
                        //
                        //         return userConnectionID;
                        //     }

                        case "support":
                        {

                            SupportUserMessageHub supportUserMessageHub = new ()
                            {
                                SupportUserAccountId = userId,
                                ConnectionId = userConnectionID
                            };

                            var insertedUser =
                                await _unitOfWork.SupportRepository.InsertSupportUserMessageHubAsync(supportUserMessageHub,default);
                            var result = await _unitOfWork.Save();
                            if (result == 0) return "Support user not found in message hub";

                            return userConnectionID;
                        }
                        case "agent":
                        {

                            AgentMessageHub agentMessageHub = new ()
                            {
                                AgentId = userId,
                                ConnectionId = userConnectionID
                            };


                            var insertedUser =
                                await _unitOfWork.AgentRepository.InsertAgentMessageHubAsync(agentMessageHub,default);
                            var result = await _unitOfWork.Save();
                            if (result == 0) return "Agent user not found in message hub";

                            return userConnectionID;
                        }
                        case "admin":
                        {
                            AdminUserMessageHub admintUserMessageHub = new()
                            {
                                AdminUserAccountId = userId,
                                ConnectionId = userConnectionID
                            };

                            var insertedUser =
                                await _unitOfWork.AdminRepository.InsertAdminUserMessageHubAsync(admintUserMessageHub,default);
                            var result = await _unitOfWork.Save();
                            if (result == 0) return "Admin user not found in message hub";

                            return userConnectionID;
                        }
                        default:
                        {
                            return "user not found in message hub";
                        }

                    }


                    //return userConnectionID;
                }

            }
            catch (Exception e)
            {
                return e.Message;
            }

        }



       /* private async Task<String> GetConnectionIDFromDB(string userType, long userId) 
        {
            try
            {
                string userConnectionID = "";
                using (var scope = _sp.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var _unitOfWork = services.GetRequiredService<IUnitOfWork>();
                    switch (userType.ToLower())
                    {
                        case "driver":
                            {
                                var UserHubs = await _unitOfWork.UserRepository.GetUsersMessageHubsByAsync( h => h.UserId == userId);
                                var userHub = UserHubs.First();
                                if (userHub == null) return "";

                                break;
                            }

                        case "support":
                            {
                                var UserHubs = await _unitOfWork.UserRepository.GetUsersMessageHubsByAsync(h => h.UserId == userId);
                                var userHub = UserHubs.First();
                                if (userHub == null) return "";

                                break;
                            }
                        case "agent":
                            {
                                var UserHubs = await _unitOfWork.UserRepository.GetUsersMessageHubsByAsync(h => h.UserId == userId);
                                var userHub = UserHubs.First();
                                if (userHub == null) return "";

                                break;
                            }
                        case "admin":
                            {
                                var UserHubs = await _unitOfWork.UserRepository.GetUsersMessageHubsByAsync(h => h.UserId == userId);
                                var userHub = UserHubs.First();
                                if (userHub == null) return "";

                                break;
                            }
                        default:
                            {
                                return "";
                            }

                    }


                    return userConnectionID;
                }

            }
            catch (Exception e)
            {
                return e.Message;
            }
        }*/


        public async Task<bool> sendNewOrderRequest(string connectionId, string orderId)
        {
            try
            {
                await Clients.Client(connectionId).SendAsync("newRequest", orderId);
                return true;

            } catch (Exception e)
            {
                Console.Write(e.InnerException);
                return false;
            }

            
        }
       
        public override Task OnDisconnectedAsync(Exception exception)
        {
            if(Context.UserIdentifier !=null)
                Console.Write(Context.UserIdentifier + "has been disconnected");

            if (exception?.InnerException != null) {
                Console.Write(exception.InnerException);
                return base.OnDisconnectedAsync(exception);
            }

            return base.OnDisconnectedAsync(null);

        }

        public override Task OnConnectedAsync()
        {
            if (Context.UserIdentifier != null)
                Console.Write(Context.UserIdentifier +"has been connected");

            return base.OnConnectedAsync();
        }




        public static string OrderStatusMessageHub(OrderStatusTypes type)
        {
            switch (type)
            {
                case OrderStatusTypes.New:
                    return "NewOrderNotify";
                case OrderStatusTypes.AssignedToCaptain:
                    return "AssignedToCaptainOrderNotify";
                case OrderStatusTypes.PickedUp:
                    return "PickedUpOrderNotify";
                case OrderStatusTypes.Progress:
                    return "ProgressOrderNotify";
                case OrderStatusTypes.Dropped:
                    return "DroppedOrderNotify";
                case OrderStatusTypes.Delivered:
                    return "DeliveredOrderNotify";
                case OrderStatusTypes.Canceled:
                    return "CanceledOrderNotify";
                case OrderStatusTypes.End:
                    return "EndOrderNotify";
                case OrderStatusTypes.NotAssignedToCaptain:
                    return "NotAssignedToCaptainOrderNotify";
                case OrderStatusTypes.SearchingForCaptain:
                    return "SearchingForCaptainOrderNotify";
                default: 
                    return "";

            }
        }







    }





    



}
