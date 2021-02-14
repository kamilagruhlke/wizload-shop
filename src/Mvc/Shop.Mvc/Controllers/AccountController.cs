using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Mvc.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccesor;

        public AccountController(IHttpContextAccessor httpContextAccesor)
        {
            _httpContextAccesor = httpContextAccesor;
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            return Redirect(Url.Content("~/"));
        }

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect(Url.Content("~/"));
        }

        [AllowAnonymous]
        [HttpGet("AccessDenied")]
        public IActionResult AccessDenied(string returnUrl)
        {
            return View();
        }

        [HttpGet("Details")]
        public IActionResult Details()
        {
            return View(_httpContextAccesor);
        }
    }
}
