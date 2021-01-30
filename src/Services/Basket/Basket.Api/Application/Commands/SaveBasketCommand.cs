using MediatR;
using System;
using System.Collections.Generic;

namespace Basket.Api.Application.Commands
{
    public class SaveBasketCommand : IRequest<Guid>
    {
        public Guid? BasketId { get; set; }

        public IList<Guid> ProductIds { get; set; }
    }
}
