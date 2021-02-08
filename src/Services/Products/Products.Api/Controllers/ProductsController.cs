using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products.Api.Application.Commands;
using Products.Api.Application.Models;

namespace Products.Api.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> Post(CreateProductCommand createProductCommand, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(createProductCommand, cancellationToken));
        }

        [HttpPut]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> Put(UpdateProductCommand updateProductCommand, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(updateProductCommand, cancellationToken));
        }

        [AllowAnonymous]
        [HttpGet("ByCategoryId/{categoryId}")]
        [ProducesResponseType(typeof(IList<ProductModel>), 200)]
        public async Task<IActionResult> GetByCategoryId(Guid categoryId, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetProductByCategoryIdCommand(categoryId), cancellationToken));
        }

        [AllowAnonymous]
        [HttpGet("Last/{numberOfItems}")]
        [ProducesResponseType(typeof(IList<ProductModel>), 200)]
        public async Task<IActionResult> GetLastCreatedProducts(int numberOfItems, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetLastCreatedProductsCommand(numberOfItems), cancellationToken));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductModel), 200)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetProductByIdCommand(id), cancellationToken));
        }
    }
}
