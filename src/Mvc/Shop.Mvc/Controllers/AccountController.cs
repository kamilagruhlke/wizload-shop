using Microsoft.AspNetCore.Mvc;

namespace Shop.Mvc.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult AccessDenied(string returnUrl)
        {
            return View();
        }
    }
}
