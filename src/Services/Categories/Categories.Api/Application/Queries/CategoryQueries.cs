using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Categories.Api.Application.Models;
using Categories.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Categories.Api.Application.Queries
{
    public class CategoryQueries : ICategoryQueries
    {
        private readonly CategoriesDbContext _categoriesDbContext;

        public CategoryQueries(CategoriesDbContext categoriesDbContext)
        {
            _categoriesDbContext = categoriesDbContext;
        }

        public async Task<IList<CategoryModel>> GetActiveCategories(CancellationToken cancellationToken)
        {
            return await _categoriesDbContext.Categories
                .Where(e => e.IsDeleted == false)
                .Select(e => new CategoryModel {
                    Id = e.Id,
                    ParentId = e.ParentId,
                    Name = e.Name
                })
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
