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
    [Authorize]
    [Route("api/[controller]")]
    public class ProducersController : Controller
    {
        private readonly IMediator _mediator;

        public ProducersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> Post(CreateProducerCommand createProducerCommand, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(createProducerCommand, cancellationToken));
        }

        [HttpPut]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> Put(UpdateProducerCommand updateProducerCommand, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(updateProducerCommand, cancellationToken));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProducerModel), 200)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetProducerByIdCommand(id), cancellationToken));
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ProducerModel>), 200)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetProducersCommand(), cancellationToken));
        }
    }
}