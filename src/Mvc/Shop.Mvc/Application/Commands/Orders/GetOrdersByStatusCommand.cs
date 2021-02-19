using MediatR;
using Shop.Mvc.Application.Models;
using System.Collections.Generic;

namespace Shop.Mvc.Application.Commands.Orders
{
    public class GetOrdersByStatusCommand : IRequest<List<OrderModel>>
    {
        public string Status { get; set; }
    }
}
