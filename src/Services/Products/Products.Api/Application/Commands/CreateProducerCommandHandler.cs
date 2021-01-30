using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Products.Domain.AggregateModel.ProducerAggregate;
using Products.Domain.Utils.Interfaces;

namespace Products.Api.Application.Commands
{
    public class CreateProducerCommandHandler : IRequestHandler<CreateProducerCommand, bool>
    {
        private readonly IProducerRepository _producerRepository;

        private readonly IUserAccessor _userAccessor;

        public CreateProducerCommandHandler(IProducerRepository producerRepository, IUserAccessor userAccessor)
        {
            _producerRepository = producerRepository;
            _userAccessor = userAccessor;
        }

        public async Task<bool> Handle(CreateProducerCommand request, CancellationToken cancellationToken)
        {
            var producer = new Producer(request.Name,
                request.Description,
                _userAccessor.GetCurrentUsername());

            await _producerRepository.Add(producer, cancellationToken)
                .ConfigureAwait(false);

            return await _producerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
