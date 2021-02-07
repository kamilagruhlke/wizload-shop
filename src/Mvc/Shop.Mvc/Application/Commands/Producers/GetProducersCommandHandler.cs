using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WizLoad.ApiClient;

namespace Shop.Mvc.Application.Commands.Producers
{
    public class GetProducersCommandHandler : IRequestHandler<GetProducersCommand, ICollection<ProducerModel>>
    {
        private readonly productsClient _productsClient;

        public GetProducersCommandHandler(productsClient productsClient) => _productsClient = productsClient;

        public async Task<ICollection<ProducerModel>> Handle(GetProducersCommand request, CancellationToken cancellationToken)
        {
            return await _productsClient.ProducersGetAllAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
