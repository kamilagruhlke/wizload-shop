using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WizLoad.ApiClient;

namespace Shop.Mvc.Application.Commands.Categories
{
    public class GetCategoriesCommandHandler : IRequestHandler<GetCategoriesCommand, ICollection<CategoryModel>>
    {
        private readonly categoriesClient _categoriesClient;

        public GetCategoriesCommandHandler(categoriesClient categoriesClient) => _categoriesClient = categoriesClient;

        public async Task<ICollection<CategoryModel>> Handle(GetCategoriesCommand request, CancellationToken cancellationToken)
        {
            return await _categoriesClient.CategoriesGetActiveCategoriesAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
