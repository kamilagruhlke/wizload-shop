using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Mvc.Application.Commands.Orders;
using Shop.Mvc.Application.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Mvc.Areas.Panel.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Panel")]
    [Route("Panel/[controller]")]
    public class OrdersController : Controller
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var ordersPending = await _mediator.Send(new GetOrdersByStatusCommand()
            {
                Status = "PENDING"
            }, cancellationToken);

            var ordersInProgress = await _mediator.Send(new GetOrdersByStatusCommand()
            {
                Status = "IN_PROGRESS"
            }, cancellationToken);

            var ordersFinished = await _mediator.Send(new GetOrdersByStatusCommand()
            {
                Status = "FINISHED"
            }, cancellationToken);

            var orders = new List<OrderModel>();
            orders.AddRange(ordersPending);
            orders.AddRange(ordersInProgress);
            orders.AddRange(ordersFinished);

            return View(orders);
        }

        [HttpGet("Preview/{id}")]
        public async Task<IActionResult> Preview(Guid id, CancellationToken cancellationToken)
        {
            var order = await _mediator.Send(new GetOrderByIdCommand
            {
                Id = id
            }, cancellationToken);

            return View(order);
        }

        [HttpGet("InProgress/{id}")]
        public async Task<IActionResult> InProgress(Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new InProgressOrderCommand
            {
                Id = id
            }, cancellationToken);

            return RedirectToAction("Index");
        }

        [HttpGet("Finish/{id}")]
        public async Task<IActionResult> Finish(Guid id, CancellationToken cancellationToken)
        {
            var order = await _mediator.Send(new FinishOrderCommand
            {
                Id = id
            }, cancellationToken);

            return RedirectToAction("Index");
        }
    }
}
