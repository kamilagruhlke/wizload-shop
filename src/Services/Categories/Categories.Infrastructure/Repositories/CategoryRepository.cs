using System;
using System.Threading;
using System.Threading.Tasks;
using Categories.Domain.AggregateModel.CategoryAggregate;
using Categories.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Categories.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategoriesDbContext _categoriesDbContext;

        public CategoryRepository(CategoriesDbContext categoriesDbContext)
        {
            _categoriesDbContext = categoriesDbContext;
        }

        public IUnitOfWork UnitOfWork => _categoriesDbContext;

        public async Task<Category> Add(Category category, CancellationToken cancellationToken)
        {
            await _categoriesDbContext.Categories.AddAsync(category, cancellationToken);
            return category;
        }

        public async Task<Category> FindById(Guid id, CancellationToken cancellationToken)
        {
            return await _categoriesDbContext.Categories
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public Category Update(Category category)
        {
            _categoriesDbContext.Categories.Update(category);
            return category;
        }
    }
}
