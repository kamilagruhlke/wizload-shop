using Categories.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Categories.Domain.AggregateModel.CategoryAggregate
{
    interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> Add(Category category);

        Category Update(Category category);

        Task<Category> FindById(Guid id);

        Task<IList<Category>> FindByParentId(Guid parentId);
    }
}
