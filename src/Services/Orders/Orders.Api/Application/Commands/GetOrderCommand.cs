using MediatR;
using Orders.Api.Application.Models;
using System;

namespace Orders.Api.Application.Commands
{
    public class GetOrderCommand : IRequest<OrderModel>
    {
        public Guid Id { get; set; }
    }
}
