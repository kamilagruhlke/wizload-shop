using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Mvc.Application.Commands.Categories;
using Shop.Mvc.Application.Commands.Producers;
using Shop.Mvc.Application.Commands.Products;
using Shop.Mvc.Application.Models;

namespace Shop.Mvc.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class PanelController : Controller
    {
        private readonly IMediator _mediator;

        public PanelController(IMediator mediator) => _mediator = mediator;

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Product/Create")]
        public async Task<IActionResult> ProductCreate(CancellationToken cancellationToken)
        {
            return View("ProductCreate", new ProductCreateModel {
                Categories = await _mediator.Send(new GetCategoriesCommand(), cancellationToken),
                Producers = await _mediator.Send(new GetProducersCommand(), cancellationToken),
                CreateProductCommand = new CreateProductCommand()
            });
        }

        [HttpPost("Product/Create")]
        public async Task<IActionResult> ProductCreate(CreateProductCommand createProductCommand, CancellationToken cancellationToken)
        {
            await _mediator.Send(createProductCommand, cancellationToken);

            return View("ProductCreate", new ProductCreateModel
            {
                Categories = await _mediator.Send(new GetCategoriesCommand(), cancellationToken),
                Producers = await _mediator.Send(new GetProducersCommand(), cancellationToken),
                CreateProductCommand = createProductCommand
            });
        }
    }
}
