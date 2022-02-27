using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TreePorts.DTO;
using TreePorts.Interfaces.Repositories;
using TreePorts.Models;
using TreePorts.Utilities;

namespace TreePorts.Utilities
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(HttpContext context, IUnitOfWork unitOfWork, IMemoryCache cache)
        {


            try
            {
                string urlEndpoint = context.Request.Path.Value.ToLower();
                string method = context.Request.Method.ToLower();

                if (!isContainIgnoreURLs(urlEndpoint,method))
                {

                    //Reading the AuthHeader which is signed with JWT
                    string authHeader = context.Request.Headers["Authorization"];

                    if (authHeader == null || authHeader == "")
                    {
                        context.Response.StatusCode = 401; //Unauthorized Request                
                        await context.Response.WriteAsync("Unauthorized request");
                        return;
                    }

                    string token = authHeader.Split(" ")[1];
                    var isValid = await IsTokenValid(token, unitOfWork, cache);
                    if (!isValid) 
                    {
                        context.Response.StatusCode = 401; //Unauthorized Request                
                        await context.Response.WriteAsync("Unauthorized request");
                        return;
                    }


                    

                }
                //Pass to the next middleware
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


        private async Task<bool> IsTokenValid( string token , IUnitOfWork unitOfWork, IMemoryCache cache) 
        {

            string userId = "", userType = "";
            long? statusCacheValue = null;
            getTokenInfo(token, out userType, out userId);
            string tokenCacheKey = $"{userId}:{userType.ToLower()}:token";
            string statusCacheKey = $"{userId}:{userType.ToLower()}:status";

            var isTokenValidFromCache = GetTokenFromCache( token, tokenCacheKey, cache);
            var isStatusValidFromCache = GetStatusFromCache(out statusCacheValue, statusCacheKey,cache);

            if (isTokenValidFromCache)
            {
                // update expiration date in cache.
                //cache.Set(tokenCacheKey, token, TimeSpan.FromSeconds(10));
                Utility.SetCacheForAuth(tokenCacheKey, token, cache);
                if (!isStatusValidFromCache) return false;

                //cache.Set(statusCacheKey, statusCacheValue, TimeSpan.FromSeconds(10));
                Utility.SetCacheForAuth(statusCacheKey, statusCacheValue, cache);
                return true;
            }
            else {
                var isValidFromDatabase = await GetTokenFromDatabase( token, userType, tokenCacheKey, statusCacheKey, unitOfWork,cache);
                if (isValidFromDatabase) return true;
                
            }

            return false;
        }





        private bool GetTokenFromCache(string token, string cacheKey, IMemoryCache cache) 
        {
            string cacheValue = "";
            if (cache.TryGetValue(cacheKey, out cacheValue))
            {
                Console.WriteLine("Token from cache");
                if (cacheValue == token) return true;

                return false;
            }

            return false;
        }


        private bool GetStatusFromCache(out long? cacheValue, string cacheKey, IMemoryCache cache)
        {
            
            if (cache.TryGetValue(cacheKey, out cacheValue))
            {
                Console.WriteLine("status from cache");
                if (isValidStatus(cacheValue)) return true;

                return false;
            }

            return false;
        }





        private async Task<bool> GetTokenFromDatabase(string token,string userType, string tokenCacheKey, string statusCacheKey ,IUnitOfWork unitOfWork, IMemoryCache cache) 
        {
            Console.WriteLine("From Database");
            if (userType == null || userType == "") return false;

            if (userType.ToLower() == "driver")
            {

                var users = await unitOfWork.CaptainRepository.GetCaptainUsersAccountsByAsync(u => u.Token == token);
                var user = users.FirstOrDefault();
                if (user != null && user.Id != "" && isValidStatus(user.StatusTypeId)) 
                {
                    Utility.SetCacheForAuth(tokenCacheKey, token, cache);
                    Utility.SetCacheForAuth(statusCacheKey, user.StatusTypeId, cache);
                    return true; 
                }
            }
            else if (userType.ToLower() == "agent")
            {
                var users = await unitOfWork.AgentRepository.GetAgentsByAsync(u => u.Token == token);
                var user = users.FirstOrDefault();
                if (user != null && user.Id != "" && isValidStatus(user.StatusTypeId))
                {
                    Utility.SetCacheForAuth(tokenCacheKey, token, cache);
                    Utility.SetCacheForAuth(statusCacheKey, user.StatusTypeId, cache);
                    return true;
                }
            }
            else if (userType.ToLower() == "admin")
            {
                var users = await unitOfWork.AdminRepository.GetAdminUserAccountByAsync(u => u.Token == token);
                var user = users.FirstOrDefault();
                if (user != null && user.Id != "" && isValidStatus(user.StatusTypeId))
                {
                    Utility.SetCacheForAuth(tokenCacheKey, token, cache);
                    Utility.SetCacheForAuth(statusCacheKey, user.StatusTypeId, cache);
                    return true;
                }
            }
            else if (userType.ToLower() == "support")
            {
                var users = await unitOfWork.SupportRepository.GetSupportUsersAccountsByAsync(u => u.Token == token);
                var user = users.FirstOrDefault();
                if (user != null && user.Id != "" && isValidStatus(user.StatusTypeId))
                {
                    Utility.SetCacheForAuth(tokenCacheKey, token, cache);
                    Utility.SetCacheForAuth(statusCacheKey, user.StatusTypeId, cache);
                    return true;
                }
            }

            return false;
        }

        private bool isValidStatus(long? statusTypeId)
        {
            if (statusTypeId == (long)StatusTypes.Stopped ||
                statusTypeId == (long)StatusTypes.Suspended) return false;

            return true;
        }



        private bool isContainIgnoreURLs(string url,string method) 
        {
            bool isContain = false;
            string[] igonreURLs = { "login", "assets" , 
                "changepassword", "api/v1/agents", "messagehub", "swagger" };

            //to allow registeration for agent and captain from sender profile
            //if (( url == "/agents/" || url == "/captains/" ) && method == "post") return true;

            for (int i = 0; i < igonreURLs.Length; i++) 
            {
                if (url.Contains(igonreURLs[i])) 
                {
                    isContain = true;
                    break;
                }
            }

            return isContain;
        }


    }


    public static class MyMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyAuthMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>();
        }
    }


}
