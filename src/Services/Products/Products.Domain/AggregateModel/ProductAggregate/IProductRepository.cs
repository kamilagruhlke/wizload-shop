using Products.Domain.SeedWork;
using System;
using System.Threading.Tasks;

namespace Products.Domain.AggregateModel.ProductAggregate
{
    interface IProductRepository : IRepository<Product>
    {
        Task<Product> Add(Product product);

        Product Update(Product product);

        Task<Product> FindById(Guid id);
    }
}
