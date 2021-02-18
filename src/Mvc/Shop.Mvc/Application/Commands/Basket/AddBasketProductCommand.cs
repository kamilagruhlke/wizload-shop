using System;
using System.Collections.Generic;
using MediatR;

namespace Shop.Mvc.Application.Commands.Basket
{
    public class AddBasketProductCommand : IRequest<bool>
    {
        public List<Guid> ProductIds { get; set; }

        public Guid BasketId { get; set; }
    }
}
