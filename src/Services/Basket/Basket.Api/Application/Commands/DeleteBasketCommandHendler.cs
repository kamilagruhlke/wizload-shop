using MediatR;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Basket.Api.Application.Commands
{
    public class DeleteBasketCommandHendler : IRequestHandler<DeleteBasketCommand, bool>
    {
        private readonly IDatabase _db;

        public DeleteBasketCommandHendler(IDatabase db)
        {
            _db = db;
        }

        public async Task<bool> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _db.KeyDeleteAsync(request.BasketId.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
