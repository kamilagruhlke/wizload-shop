using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Mvc.Application.Commands.Producers;

namespace Shop.Mvc.Areas.Panel.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Panel")]
    [Route("Panel/[controller]")]
    public class ProducersController : Controller
    {
        private readonly IMediator _mediator;

        public ProducersController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [HttpGet("Index")]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            return View(await _mediator.Send(new GetProducersCommand(), cancellationToken));
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View(new CreateProducerCommand());
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(EditProducerCommand editeProducerCommand, CancellationToken cancellationToken)
        {
            await _mediator.Send(editeProducerCommand, cancellationToken);

            return View(editeProducerCommand);
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken)
        {
            var producers = await _mediator.Send(new GetProducersCommand(), cancellationToken);
            var producer = producers.FirstOrDefault(e => e.Id == id);

            return View(new EditProducerCommand { 
                Id = producer.Id,
                Name = producer.Name,
                Description = producer.Description
            });
        }

        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id, EditProducerCommand editeProducerCommand, CancellationToken cancellationToken)
        {
            await _mediator.Send(editeProducerCommand, cancellationToken);

            var producers = await _mediator.Send(new GetProducersCommand(), cancellationToken);
            var producer = producers.FirstOrDefault(e => e.Id == id);

            return View(new EditProducerCommand
            {
                Id = producer.Id,
                Name = producer.Name,
                Description = producer.Description
            });
        }
    }
}
