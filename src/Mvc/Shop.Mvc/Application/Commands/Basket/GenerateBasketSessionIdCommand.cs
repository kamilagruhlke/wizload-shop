using System;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Shop.Mvc.Application.Commands.Basket
{
    public class GenerateBasketSessionIdCommand : IRequest<Guid>
    {
        public ISession Session { get; set; }
    }
}
