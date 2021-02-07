using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Mvc.Application.Commands.Categories;
using Shop.Mvc.Models;

namespace Shop.Mvc.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Products/{categoryName}")]
        public async Task<IActionResult> Products(string categoryName, CancellationToken cancellationToken)
        {
            var category = await _mediator.Send(new GetCategoryByNormalizedNameCommand(categoryName), cancellationToken);
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
            return View(new ErrorViewModel {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
