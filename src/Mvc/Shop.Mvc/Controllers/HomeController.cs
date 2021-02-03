using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shop.Mvc.Models;
using WizLoad.ApiClient;

namespace Shop.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly productsClient _productsClient;

        public HomeController(ILogger<HomeController> logger, productsClient productsClient)
        {
            _logger = logger;
            _productsClient = productsClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Products(Guid categoryId, CancellationToken cancellationToken)
        {
            var products = await _productsClient.ProductsAllAsync(categoryId, cancellationToken);

            return View(products);
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
