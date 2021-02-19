using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Mvc.Application.Commands.Basket;
using Shop.Mvc.Application.Commands.Orders;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Mvc.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateOrderCommand createOrderCommand, CancellationToken cancellationToken)
        {
            var basketId = await _mediator.Send(new GenerateBasketSessionIdCommand {
                Session = HttpContext.Session
            }, cancellationToken);

            createOrderCommand.BaksetId = basketId;

            await _mediator.Send(createOrderCommand, cancellationToken);

            return View();
        }
    }
}