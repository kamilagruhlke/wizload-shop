using MediatR;
using Shop.Mvc.Application.Models;
using System;

namespace Shop.Mvc.Application.Commands.Orders
{
    public class GetOrderByIdCommand : IRequest<OrderModel>
    {
        public Guid Id { get; set; }
    }
}
