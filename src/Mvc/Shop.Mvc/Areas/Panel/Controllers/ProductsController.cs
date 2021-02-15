using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Mvc.Application.Commands.Categories;
using Shop.Mvc.Application.Commands.Producers;
using Shop.Mvc.Application.Commands.Products;
using Shop.Mvc.Application.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

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

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken)
        {
            var product = await _mediator.Send(new GetProductByIdCommand(id), cancellationToken);

            return View(new ProductEditModel
            {
                Categories = await _mediator.Send(new GetCategoriesCommand(), cancellationToken),
                Producers = await _mediator.Send(new GetProducersCommand(), cancellationToken),
                EditProductCommand = new EditProductCommand {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Specification = product.Specification,
                    ProducerCode = product.ProducerCode,
                    ProducerId = product.ProducerId,
                    NetPrice = product.NetPrice,
                    Tax = product.Tax,
                    CategoryId = product.CategoryId
                }
            });
        }

        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id, EditProductCommand editProductCommand, CancellationToken cancellationToken)
        {
            await _mediator.Send(editProductCommand, cancellationToken);

            var product = await _mediator.Send(new GetProductByIdCommand(id), cancellationToken);

            return View(new ProductEditModel
            {
                Categories = await _mediator.Send(new GetCategoriesCommand(), cancellationToken),
                Producers = await _mediator.Send(new GetProducersCommand(), cancellationToken),
                EditProductCommand = new EditProductCommand
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Specification = product.Specification,
                    ProducerCode = product.ProducerCode,
                    ProducerId = product.ProducerId,
                    NetPrice = product.NetPrice,
                    Tax = product.Tax,
                    CategoryId = product.CategoryId
                }
            });
        }
    }
}
