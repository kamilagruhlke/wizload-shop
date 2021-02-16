using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Products.Domain.SeedWork;

namespace Products.Domain.AggregateModel.ProducerAggregate
{
    public interface IProducerRepository : IRepository<Producer>
    {
        Task<Producer> Add(Producer producer, CancellationToken cancellationToken);

        Producer Update(Producer producer);

        Task<Producer> FindById(Guid id, CancellationToken cancellationToken);

        Task<List<Producer>> GetAll(CancellationToken cancellationToken);
    }
}