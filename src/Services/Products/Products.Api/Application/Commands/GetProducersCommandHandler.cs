using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Products.Api.Application.Models;
using Products.Domain.AggregateModel.ProducerAggregate;

namespace Products.Api.Application.Commands
{
    public class GetProducersCommandHandler : IRequestHandler<GetProducersCommand, List<ProducerModel>>
    {
        private readonly IProducerRepository _producerRepository;

        public GetProducersCommandHandler(IProducerRepository producerRepository)
        {
            _producerRepository = producerRepository;
        }

        public async Task<List<ProducerModel>> Handle(GetProducersCommand request, CancellationToken cancellationToken)
        {
            var producers = await _producerRepository.GetAll(cancellationToken);
            return producers.Select(e => new ProducerModel
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description
            }).ToList();
        }
    }
}
