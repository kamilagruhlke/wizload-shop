using Basket.Api.Application.Commands;
using Basket.Api.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Basket.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), 200)]
        public async Task<IActionResult> Post(SaveBasketCommand saveBasektCommand, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(saveBasektCommand, cancellationToken));
        }

        [HttpDelete]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> Delete(DeleteBasketCommand deleteBasketCommand, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(deleteBasketCommand, cancellationToken));
        }

        [HttpGet("{basketId}")]
        [ProducesResponseType(typeof(BasketModel), 200)]
        public async Task<IActionResult> Get(Guid basketId, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetBasketCommand(basketId), cancellationToken));
        }
    }
}
