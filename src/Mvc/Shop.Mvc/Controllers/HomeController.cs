using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shop.Mvc.Models;

namespace Shop.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly categoriesClient _categoriesClient;

        public HomeController(ILogger<HomeController> logger, categoriesClient categoriesClient)
        {
            _logger = logger;
            _categoriesClient = categoriesClient;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var categories = await _categoriesClient.ActiveAsync(cancellationToken);
            return View(categories);
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
