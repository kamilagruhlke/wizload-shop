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
    [Authorize(Roles = "Administrator")]
    [Route("[controller]")]
    public class PanelController : Controller
    {
        private readonly IMediator _mediator;

        public PanelController(IMediator mediator) => _mediator = mediator;

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Producers")]
        public async Task<IActionResult> GetProducers(CancellationToken cancellationToken)
        {
            return View("Producers", await _mediator.Send(new GetProducersCommand(), cancellationToken));
        }

        [HttpGet("Producer/Create")]
        public IActionResult ProducerCreate()
        {
            return View("ProducerCreate", new CreateProducerCommand());
        }

        [HttpPost("Producer/Create")]
        public async Task<IActionResult> ProducerCreate(CreateProducerCommand createProducerCommand, CancellationToken cancellationToken)
        {
            await _mediator.Send(createProducerCommand, cancellationToken);

            return View("ProducerCreate", createProducerCommand);
        }

        [HttpGet("Products")]
        public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
        {
            return View("Products", await _mediator.Send(new GetProductsCommand(), cancellationToken));
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

        [HttpGet("Categories")]
        public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
        {
            return View("Categories", await _mediator.Send(new GetCategoriesCommand(), cancellationToken));
        }

        [HttpGet("Category/Create")]
        public async Task<IActionResult> CategoryCreate(CancellationToken cancellationToken)
        {
            return View("CategoryCreate", new CategoryCreateModel
            {
                Categories = await _mediator.Send(new GetCategoriesCommand(), cancellationToken),
                CreateCategoryCommand = new CreateCategoryCommand()
            });
        }

        [HttpPost("Category/Create")]
        public async Task<IActionResult> CategoryCreate(CreateCategoryCommand createCategoryCommand, CancellationToken cancellationToken)
        {
            await _mediator.Send(createCategoryCommand, cancellationToken);

            return View("CategoryCreate", new CategoryCreateModel
            {
                Categories = await _mediator.Send(new GetCategoriesCommand(), cancellationToken),
                CreateCategoryCommand = createCategoryCommand
            });
        }
    }
}
