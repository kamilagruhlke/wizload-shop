using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shop.Mvc.Application.Helpers;
using WizLoad.ApiClient;

namespace Shop.Mvc.Application.Commands.Categories
{
    public class GetCategoryByNormalizedNameCommandHandler : IRequestHandler<GetCategoryByNormalizedNameCommand, CategoryModel>
    {
        private readonly categoriesClient _categoriesClient;

        public GetCategoryByNormalizedNameCommandHandler(categoriesClient categoriesClient) => _categoriesClient = categoriesClient;

        public async Task<CategoryModel> Handle(GetCategoryByNormalizedNameCommand request, CancellationToken cancellationToken)
        {
            var categories = await _categoriesClient.ActiveAsync(cancellationToken);
            var category = categories.FirstOrDefault(e => CategoryNameHelper.Normalize(e.Name) == request.NormalizedName);
            return category;
        }
    }
}
