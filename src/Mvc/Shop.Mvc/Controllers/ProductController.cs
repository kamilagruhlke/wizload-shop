using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WizLoad.ApiClient;

namespace Shop.Mvc.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly productsClient _productsClient;

        public ProductController(productsClient productsClient) => _productsClient = productsClient;

        [HttpGet("{productId}")]
        public async Task<IActionResult> Index(Guid productId, CancellationToken cancellationToken)
        {
            var productModel = await _productsClient.ProductsGetByIdAsync(productId, cancellationToken)
                .ConfigureAwait(false);

            return View(productModel);
        }
    }
}
