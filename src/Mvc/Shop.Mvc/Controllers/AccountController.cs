using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Mvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccesor;

        public AccountController(IHttpContextAccessor httpContextAccesor)
        {
            _httpContextAccesor = httpContextAccesor;
        }

        public IActionResult AccessDenied(string returnUrl)
        {
            return View();
        }

        [Authorize]
        public IActionResult Details()
        {
            return View(_httpContextAccesor);
        }
    }
}
