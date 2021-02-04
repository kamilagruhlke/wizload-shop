using Basket.Api.Application.Models;
using MediatR;
using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Basket.Api.Application.Commands
{
    public class SaveBasketCommandHandler : IRequestHandler<SaveBasketCommand, Guid>
    {
        private readonly IDatabase _db;

        public SaveBasketCommandHandler(IDatabase db)
        {
            _db = db;
        }

        public async Task<Guid> Handle(SaveBasketCommand request, CancellationToken cancellationToken)
        {
            var redisBasket = JsonSerializer.Serialize(new BasketModel
            {
                ProductIds = request.ProductIds
            });

            if (request.BasketId.HasValue)
            {
                await _db.StringSetAsync(request.BasketId.Value.ToString(), redisBasket);
                return request.BasketId.Value;
            }
            else
            {
                var newBasketId = Guid.NewGuid();

                await _db.StringSetAsync(newBasketId.ToString(), redisBasket);

                return newBasketId;
            }
        }
    }
}
