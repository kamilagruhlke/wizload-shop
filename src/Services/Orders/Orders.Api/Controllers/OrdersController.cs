using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orders.Api.Application.Commands;
using Orders.Api.Application.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Orders.Api.Controllers
{
    [Authorize]
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

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderModel), 200)]
        public async Task<IActionResult> GetOrderById(Guid id,
            CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetOrderCommand { 
                Id = id
            }, cancellationToken));
        }

        [HttpGet("Status/{status}")]
        [ProducesResponseType(typeof(List<OrderModel>), 200)]
        public async Task<IActionResult> GetOrdersByStatus(string status,
            CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetOrdersByStatusCommand
            {
                Status = status
            }, cancellationToken));
        }

        [HttpGet("Date")]
        [ProducesResponseType(typeof(List<OrderModel>), 200)]
        public async Task<IActionResult> GetOrderById(DateTime date,
            CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetCreatedOrdersCommand
            {
                Date = date
            }, cancellationToken));
        }
    }
}
