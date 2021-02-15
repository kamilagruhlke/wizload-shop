using Basket.Api.Application.Models;
using MediatR;
using System;

namespace Basket.Api.Application.Commands
{
    public class GetBasketCommand : IRequest<BasketModel>
    {
        public Guid BasketId { get; set; }

        public GetBasketCommand(Guid basketId)
        {
            BasketId = basketId;
        }
    }
}
