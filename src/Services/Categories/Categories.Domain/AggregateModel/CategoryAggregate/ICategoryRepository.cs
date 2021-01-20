using System;
using System.Threading;
using System.Threading.Tasks;
using Categories.Domain.SeedWork;

namespace Categories.Domain.AggregateModel.CategoryAggregate
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> Add(Category category, CancellationToken cancellationToken);

        Category Update(Category category);

        Task<Category> FindById(Guid id, CancellationToken cancellationToken);
    }
}
