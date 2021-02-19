using Orders.Domain.Utils.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Orders.Api.Application.Utils
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUsername()
        {
            var username = _httpContextAccessor.HttpContext.User?.Identity?.Name;

            return username;
        }
    }
}
