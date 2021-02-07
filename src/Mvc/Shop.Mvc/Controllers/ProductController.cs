using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Mvc.Application.Commands.Products;

namespace Shop.Mvc.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator) => _mediator = mediator;

        [HttpGet("{productId}")]
        public async Task<IActionResult> Index(Guid productId, CancellationToken cancellationToken)
        {
            return View(await _mediator.Send(new GetProductByIdCommand(productId), cancellationToken));
        }
    }
}
