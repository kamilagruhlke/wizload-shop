using MediatR;
using Shop.Mvc.Application.Exceptions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WizLoad.ApiClient;

namespace Shop.Mvc.Application.Commands.Categories
{
    public class EditCategoryCommandHandler : IRequestHandler<EditCategoryCommand, bool>
    {
        private readonly categoriesClient _categoriesClient;

        public EditCategoryCommandHandler(categoriesClient categoriesClient)
        {
            _categoriesClient = categoriesClient;
        }

        public async Task<bool> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
        {
            if (request.ParentId.HasValue)
            {
                var categories = await _categoriesClient.CategoriesGetActiveCategoriesAsync(cancellationToken)
                    .ConfigureAwait(false);

                if (categories.All(e => e.ParentId != request.ParentId))
                {
                    throw new CategoryNotFoundValidationException(request.ParentId.Value);
                }
            }

            return await _categoriesClient.CategoriesUpdateCateogoryAsync(new UpdateCategoryCommand {
                Id = request.Id,
                ParentId = request.ParentId,
                Name = request.Name,
                IsDeleted = request.IsDeleted
            }, cancellationToken);
        }
    }
}
