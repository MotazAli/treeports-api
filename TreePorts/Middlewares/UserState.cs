using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using TreePorts.DTO;

namespace TreePorts.Utilities
{
    public class UserState
    {
        private readonly RequestDelegate _next;
        public UserState(RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(HttpContext context, IUnitOfWork unitOfWork, IMemoryCache cache)
        {


            try
            {

                //Reading the AuthHeader which is signed with JWT
                string authHeader = context.Request.Headers["Authorization"];


                if (!context.Request.Path.Value.ToLower().Contains("login") &&
                        !context.Request.Path.Value.ToLower().Contains("assets") &&
                        !context.Request.Path.Value.ToLower().Contains("messagehub") &&
                        !context.Request.Path.Value.ToLower().Contains("forgotpassword")&&
                        !context.Request.Path.Value.ToLower().Contains("api/v1/agents") &&
                        !context.Request.Path.Value.ToLower().Contains("maoun"))
                {
                    if (authHeader == null || authHeader == "")
                    {
                        context.Response.StatusCode = 401; //Unauthorized Request                
                        await context.Response.WriteAsync("Unauthorized request");
                        return;
                    }

                    string token = authHeader.Split(" ")[1];

                   


                    

                }




                await _next(context);

            }
            catch (Exception e)
            {
                context.Response.StatusCode = 999; //error authentication Request                
                await context.Response.WriteAsync(e.Message);
                return;
            }

        }




        private void getTokenInfo(string token, out string userType, out string userId)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var securityToken = handler.ReadToken(token) as JwtSecurityToken;

            userType = securityToken.Claims.FirstOrDefault(claim => claim.Type == "UserType").Value;
            userId = securityToken.Claims.FirstOrDefault(claim => claim.Type == "nameid").Value;
        }


        private async Task<bool> IsTokenValid(string token, IUnitOfWork unitOfWork, IMemoryCache cache)
        {

            string userId = "";
            string userType = "";
            getTokenInfo(token, out userType, out userId);
            string cacheKey = $"{userId}:{userType.ToLower()}:status";
            var isValidFromCache = GetStatusFromCache(token, cacheKey, cache);
            if (isValidFromCache)
            {
                // update expiration date in cache.
                cache.Set(cacheKey, token, TimeSpan.FromSeconds(10));
                return true;
            }
            else
            {
                var isValidFromDatabase = await GetStatusFromDatabase(token, userType, unitOfWork);
                if (isValidFromDatabase)
                {
                    cache.Set(cacheKey, token, TimeSpan.FromSeconds(10));
                    return true;
                }
            }

            return false;
        }


        private bool GetStatusFromCache(string token, string cacheKey, IMemoryCache cache)
        {
            long? cacheValue = null;
            if (cache.TryGetValue(cacheKey, out cacheValue))
            {
                Console.WriteLine("From Cache");
                if (isValidStatus(cacheValue)) return true;

                return false;
            }

            return false;
        }





        private async Task<bool> GetStatusFromDatabase(string token, string userType, IUnitOfWork unitOfWork)
        {
            Console.WriteLine("status From Database");
            if (userType == null || userType == "") return false;

            if (userType.ToString() == "Driver")
            {

                var users = await unitOfWork.CaptainRepository.GetUsersAccountsByAsync(u => u.Token == token);
                var user = users.FirstOrDefault();
                if (user != null && isValidStatus(user.StatusTypeId)) return true;
            }
            else if (userType.ToString() == "Agent")
            {
                var users = await unitOfWork.AgentRepository.GetAgentsByAsync(u => u.Token == token);
                var user = users.FirstOrDefault();
                if (user != null && isValidStatus(user.StatusTypeId)) return true;
            }
            else if (userType.ToString() == "Admin")
            {
                var users = await unitOfWork.AdminRepository.GetAdminUserAccountByAsync(u => u.Token == token);
                var user = users.FirstOrDefault();
                if (user != null && isValidStatus(user.StatusTypeId)) return true;
            }
            else if (userType.ToString() == "Support")
            {
                var users = await unitOfWork.SupportRepository.GetSupportUsersAccountsByAsync(u => u.Token == token);
                var user = users.FirstOrDefault();
                if (user != null && isValidStatus(user.StatusTypeId)) return true;
            }

            return false;
        }

        private bool isValidStatus(long? statusTypeId) 
        {
            if (statusTypeId == (long)StatusTypes.Stopped ||
                statusTypeId == (long)StatusTypes.Suspended) return false;

            return true;
        }




    }



    public static class MyUserStateMiddlewareExtensions
    {
        public static IApplicationBuilder UseUserStateMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserState>();
        }
    }


}
