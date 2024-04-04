using System.Security.Claims;
using InfrastructureLayer.Interface;
using InfrastructureLayer.Services;
using Microsoft.AspNetCore.Http;
using DataLayer.Models;

namespace InfrastructureLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetMyName()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return result;
        }
        

        

        public void UpdateRegistration(Registration registration)
        {
            throw new NotImplementedException();
        }
    }
}