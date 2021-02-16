using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shop.Mvc.Application.Exceptions;
using WizLoad.ApiClient;

namespace Shop.Mvc.Application.Commands.Categories
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, bool>
    {
        private readonly categoriesClient _categoriesClient;

        public CreateCategoryCommandHandler(categoriesClient categoriesClient)
        {
            _categoriesClient = categoriesClient;
        }

        public async Task<bool> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
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

            return await _categoriesClient.CategoriesCreateCategoryAsync(new WizLoad.ApiClient.CreateCategoryCommand {
                ParentId = request.ParentId,
                Name = request.Name,
                IsDeleted = false
            }, cancellationToken);
        }
    }
}
