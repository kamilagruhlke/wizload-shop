using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Shop.Mvc.Application.Commands.Basket
{
    public class GenerateBasketSessionIdCommandHandler : IRequestHandler<GenerateBasketSessionIdCommand, Guid>
    {
        public async Task<Guid> Handle(GenerateBasketSessionIdCommand request, CancellationToken cancellationToken)
        {
            if (request.Session.TryGetValue("basket_id", out var basketId) == false)
            {
                basketId = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());
                request.Session.Set("basket_id", basketId);
            }

            return Guid.Parse(Encoding.UTF8.GetString(basketId));
        }
    }
}