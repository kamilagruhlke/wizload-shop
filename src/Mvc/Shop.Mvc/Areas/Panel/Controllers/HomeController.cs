using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Mvc.Areas.Panel.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Panel")]
    [Route("Panel/[controller]")]
    public class HomeController : Controller
    {
        [HttpGet]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
