using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Products.Api.Application.Models;
using Products.Domain.AggregateModel.ProducerAggregate;

namespace Products.Api.Application.Commands
{
    public class GetProducerByIdCommandHandler : IRequestHandler<GetProducerByIdCommand, ProducerModel>
    {
        private readonly IProducerRepository _producerRepository;

        public GetProducerByIdCommandHandler(IProducerRepository producerRepository)
        {
            _producerRepository = producerRepository;
        }

        public async Task<ProducerModel> Handle(GetProducerByIdCommand request, CancellationToken cancellationToken)
        {
            var producer = await _producerRepository.FindById(request.Id, cancellationToken)
                .ConfigureAwait(false);

            return new ProducerModel
            {
                Id = producer.Id,
                Name = producer.Name,
                Description = producer.Description
            };
        }
    }
}
