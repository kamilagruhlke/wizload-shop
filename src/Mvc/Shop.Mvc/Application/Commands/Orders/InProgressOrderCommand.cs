using MediatR;
using System;

namespace Shop.Mvc.Application.Commands.Orders
{
    public class InProgressOrderCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
