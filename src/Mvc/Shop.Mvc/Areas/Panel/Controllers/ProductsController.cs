using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Mvc.Application.Commands.Categories;
using Shop.Mvc.Application.Commands.Producers;
using Shop.Mvc.Application.Commands.Products;
using Shop.Mvc.Application.Models;

namespace Shop.Mvc.Areas.Panel.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Panel")]
    [Route("Panel/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [HttpGet("Index")]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            return View(await _mediator.Send(new GetProductsCommand(), cancellationToken));
        }

        [HttpGet("Create")]
        public async Task<IActionResult> Create(CancellationToken cancellationToken)
        {
            return View(new ProductCreateModel {
                Categories = await _mediator.Send(new GetCategoriesCommand(), cancellationToken),
                Producers = await _mediator.Send(new GetProducersCommand(), cancellationToken),
                CreateProductCommand = new CreateProductCommand()
            });
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateProductCommand createProductCommand, CancellationToken cancellationToken)
        {
            await _mediator.Send(createProductCommand, cancellationToken);

            return View(new ProductCreateModel
            {
                Categories = await _mediator.Send(new GetCategoriesCommand(), cancellationToken),
                Producers = await _mediator.Send(new GetProducersCommand(), cancellationToken),
                CreateProductCommand = createProductCommand
            });
        }
    }
}
