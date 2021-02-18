using MediatR;
using Microsoft.AspNetCore.Mvc;
using Orders.Api.Application.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Orders.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand createOrderCommand,
            CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(createOrderCommand, cancellationToken));
        }

        [HttpPut]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderCommand updateOrderCommand,
            CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(updateOrderCommand, cancellationToken));
        }
    }
}
