using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Products.Domain.SeedWork;

namespace Products.Domain.AggregateModel.ProductAggregate
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> Add(Product product, CancellationToken cancellationToken);

        Product Update(Product product);

        Task<IList<Product>> FindByCategoryId(Guid categoryId, CancellationToken cancellationToken);
    }
}