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

        public async Task<IList<Product>> FindByCategoryId(Guid categoryId, CancellationToken cancellationToken)
        {
            return await _productsDbContext.Products.Where(e => e.CategoryId == categoryId)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<Product> FindById(Guid id, CancellationToken cancellationToken)
        {
            return await _productsDbContext.Products.FirstOrDefaultAsync(e => e.Id == id, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IList<Product>> GetLast(int numberOfItems, CancellationToken cancellationToken)
        {
            return await _productsDbContext.Products.OrderByDescending(e => e.CreatedAt)
                .Take(numberOfItems)
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
