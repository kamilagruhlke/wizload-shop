using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Categories.Api.Application.Models;

namespace Categories.Api.Application.Queries
{
    public interface ICategoryQueries
    {
        public Task<IList<CategoryModel>> GetActiveCategories(CancellationToken cancellationToken);
    }
}
