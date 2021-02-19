using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Mvc.Application.Commands.Basket;

namespace Shop.Mvc.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class BasketController : Controller
    {
        private readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var basketId = await _mediator.Send(new GenerateBasketSessionIdCommand
            {
                Session = HttpContext.Session
            }, cancellationToken);

            var basketProducts = await _mediator.Send(new GetBasketProductsCommand
            {
                Id = basketId
            }, cancellationToken);

            return View(basketProducts);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromForm] Guid productId, CancellationToken cancellationToken)
        {
            var basketId = await _mediator.Send(new GenerateBasketSessionIdCommand
            {
                Session = HttpContext.Session
            }, cancellationToken);

            var basket = await _mediator.Send(new GetBasketCommand
            {
                Id = basketId
            }, cancellationToken);

            var products = basket.ProductIds?.ToList() ?? new List<Guid>();
            products.Add(productId);

            await _mediator.Send(new AddBasketProductCommand
            {
                BasketId = basketId,
                ProductIds = products
            }, cancellationToken);

            var basketProducts = await _mediator.Send(new GetBasketProductsCommand
            {
                Id = basketId
            }, cancellationToken);

            return View("Index", basketProducts);
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromForm] Guid productId, CancellationToken cancellationToken)
        {
            var basketId = await _mediator.Send(new GenerateBasketSessionIdCommand
            {
                Session = HttpContext.Session
            }, cancellationToken);

            var basket = await _mediator.Send(new GetBasketCommand
            {
                Id = basketId
            }, cancellationToken);

            var products = basket.ProductIds?.ToList() ?? new List<Guid>();
            products.RemoveAll(e => e == productId);

            await _mediator.Send(new AddBasketProductCommand
            {
                BasketId = basketId,
                ProductIds = products
            }, cancellationToken);

            var basketProducts = await _mediator.Send(new GetBasketProductsCommand
            {
                Id = basketId
            }, cancellationToken);

            return View("Index", basketProducts);
        }
    }
}
