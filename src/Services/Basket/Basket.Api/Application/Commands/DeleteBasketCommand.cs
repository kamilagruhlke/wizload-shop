using MediatR;
using System;

namespace Basket.Api.Application.Commands
{
    public class DeleteBasketCommand : IRequest<bool>
    {
        public Guid BasketId { get; set; }
    }
}
