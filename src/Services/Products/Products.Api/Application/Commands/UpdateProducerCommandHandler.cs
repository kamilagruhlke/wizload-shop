using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Products.Domain.AggregateModel.ProducerAggregate;
using Products.Domain.Exceptions;
using Products.Domain.Utils.Interfaces;

namespace Products.Api.Application.Commands
{
    public class UpdateProducerCommandHandler : IRequestHandler<UpdateProducerCommand, bool>
    {
        private readonly IProducerRepository _producerRepository;

        private readonly IUserAccessor _userAccessor;

        public UpdateProducerCommandHandler(IProducerRepository producerRepository, IUserAccessor userAccessor)
        {
            _producerRepository = producerRepository;
            _userAccessor = userAccessor;
        }

        public async Task<bool> Handle(UpdateProducerCommand request, CancellationToken cancellationToken)
        {
            var producer = await _producerRepository.FindById(request.Id, cancellationToken)
                .ConfigureAwait(false);

            if (producer is null)
            {
                throw new EntityNotFoundBusinessException($"Producer '{producer.Id}' not found");
            }

            producer.UpdateName(request.Name);
            producer.UpdateDescription(request.Description);
            producer.UpdateModificationDates(_userAccessor.GetCurrentUsername());

            _producerRepository.Update(producer);

            return await _producerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
