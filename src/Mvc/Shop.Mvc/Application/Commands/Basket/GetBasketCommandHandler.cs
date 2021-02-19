using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WizLoad.ApiClient;

namespace Shop.Mvc.Application.Commands.Basket
{
    public class GetBasketCommandHandler : IRequestHandler<GetBasketCommand, BasketModel>
    {
        private readonly basketClient _basketClient;

        public GetBasketCommandHandler(basketClient basketClient)
        {
            _basketClient = basketClient;
        }

        public async Task<BasketModel> Handle(GetBasketCommand request, CancellationToken cancellationToken)
        {
            return await _basketClient.Basket3Async(request.Id, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
