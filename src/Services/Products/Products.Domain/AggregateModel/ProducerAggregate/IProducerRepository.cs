using Products.Domain.SeedWork;
using System;
using System.Threading.Tasks;

namespace Products.Domain.AggregateModel.ProducerAggregate
{
    interface IProducerRepository : IRepository<Producer>
    {
        Task<Producer> Add(Producer producer);

        Producer Update(Producer producer);

        Task<Producer> FindById(Guid id);
    }
}