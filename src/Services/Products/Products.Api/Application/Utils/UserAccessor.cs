using Microsoft.AspNetCore.Http;
using Products.Domain.Utils.Interfaces;

namespace Products.Api.Application.Utils
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
