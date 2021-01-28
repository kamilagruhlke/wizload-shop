using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Products.Domain.AggregateModel.ProductAggregate;
using Products.Domain.SeedWork;

namespace Products.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductsDbContext _productsDbContext;

        public ProductRepository(ProductsDbContext productsDbContext) => _productsDbContext = productsDbContext;

        public IUnitOfWork UnitOfWork => _productsDbContext;

        public async Task<Product> Add(Product product, CancellationToken cancellationToken)
        {
            await _productsDbContext.Products.AddAsync(product, cancellationToken);

            return product;
        }

        public async Task<IList<Product>> FindByCategoryId(Guid id, CancellationToken cancellationToken)
        {
            return await _productsDbContext.Products.Where(e => e.Id == id)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public Product Update(Product product)
        {
            _productsDbContext.Products.Update(product);

            return product;
        }
    }
}
