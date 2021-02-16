using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WizLoad.ApiClient;

namespace Shop.Mvc.Application.Commands.Producers
{
    public class EditProducerCommandHandler : IRequestHandler<EditProducerCommand, bool>
    {
        private readonly productsClient _productsClient;

        public EditProducerCommandHandler(productsClient productsClient)
        {
            _productsClient = productsClient;
        }

        public async Task<bool> Handle(EditProducerCommand request, CancellationToken cancellationToken)
        {
            await _productsClient.ProducersPutAsync(request.Id,
                request.Name,
                request.Description,
                cancellationToken);

            return true;
        }
    }
}
