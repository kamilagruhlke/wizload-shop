using Basket.Api.Application.Models;
using MediatR;
using StackExchange.Redis;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Basket.Api.Application.Commands
{
    public class GetBasketCommandHandler : IRequestHandler<GetBasketCommand, BasketModel>
    {
        private readonly IDatabase _db;

        public GetBasketCommandHandler(IDatabase db)
        {
            _db = db;
        }

        public async Task<BasketModel> Handle(GetBasketCommand request, CancellationToken cancellationToken)
        {
            if (_db.KeyExists(request.BasketId.ToString()) == false)
            {
                return new BasketModel();
            }

            var basket = await _db.StringGetAsync(request.BasketId.ToString());
            return JsonSerializer.Deserialize<BasketModel>(basket);
        }
    }
}
