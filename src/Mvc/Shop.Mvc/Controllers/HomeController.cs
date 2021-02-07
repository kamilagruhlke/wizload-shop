using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shop.Mvc.Application.Helpers;
using Shop.Mvc.Models;
using WizLoad.ApiClient;

namespace Shop.Mvc.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly categoriesClient _categoriesClient;

        public HomeController(ILogger<HomeController> logger, categoriesClient categoriesClient)
        {
            _logger = logger;
            _categoriesClient = categoriesClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Products/{categoryName}")]
        public async Task<IActionResult> Products(string categoryName, CancellationToken cancellationToken)
        {
            var categories = await _categoriesClient.ActiveAsync(cancellationToken);
            var category = categories.FirstOrDefault(e => CategoryNameHelper.Normalize(e.Name) == categoryName);
            if (category is null)
            {
                return NotFound();
            }

            return View(category.Id);
        }

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
