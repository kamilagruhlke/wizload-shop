using MediatR;
using Orders.Api.Application.Models;
using System.Collections.Generic;

namespace Orders.Api.Application.Commands
{
    public class GetOrdersByStatusCommand : IRequest<List<OrderModel>>
    {
        public string Status { get; set; }
    }
}
