using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Products.Domain.AggregateModel.ProducerAggregate;
using Products.Domain.SeedWork;

namespace Products.Infrastructure.Repositories
{
    public class ProducerRepository : IProducerRepository
    {
        private readonly ProductsDbContext _productsDbContext;

        public ProducerRepository(ProductsDbContext productsDbContext) => _productsDbContext = productsDbContext;

        public IUnitOfWork UnitOfWork => _productsDbContext;

        public async Task<Producer> Add(Producer producer, CancellationToken cancellationToken)
        {
            await _productsDbContext.Producers.AddAsync(producer, cancellationToken);

            return producer;
        }

        public async Task<Producer> FindById(Guid id, CancellationToken cancellationToken)
        {
            return await _productsDbContext.Producers.FirstOrDefaultAsync(e => e.Id == id, cancellationToken)
                .ConfigureAwait(false);
        }

        public Producer Update(Producer producer)
        {
            _productsDbContext.Producers.Update(producer);

            return producer;
        }
    }
}
