using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WizLoad.ApiClient;

namespace Shop.Mvc.Application.Commands.Producers
{
    public class CreateProducerCommandHandler : IRequestHandler<CreateProducerCommand, bool>
    {
        private readonly productsClient _productsClient;

        public CreateProducerCommandHandler(productsClient productsClient)
        {
            _productsClient = productsClient;
        }

        public async Task<bool> Handle(CreateProducerCommand request, CancellationToken cancellationToken)
        {
            await _productsClient.ProducersPostAsync(request.Name,
                request.Description,
                cancellationToken);

            return true;
        }
    }
}
