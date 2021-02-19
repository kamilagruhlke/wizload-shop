using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WizLoad.ApiClient;

namespace Shop.Mvc.Application.Commands.Basket
{
    public class AddBasketProductCommandHandler : IRequestHandler<AddBasketProductCommand, bool>
    {
        private readonly basketClient _basketClient;

        public AddBasketProductCommandHandler(basketClient basketClient)
        {
            _basketClient = basketClient;
        }

        public async Task<bool> Handle(AddBasketProductCommand request, CancellationToken cancellationToken)
        {
            await _basketClient.BasketAsync(new SaveBasketCommand {
                BasketId = request.BasketId,
                ProductIds = request.ProductIds
            }, cancellationToken);

            return true;
        }
    }
}
