using MediatR;
using WizLoad.ApiClient;

namespace Shop.Mvc.Application.Commands.Categories
{
    public class GetCategoryByNormalizedNameCommand : IRequest<CategoryModel>
    {
        public string NormalizedName { get; set; }

        public GetCategoryByNormalizedNameCommand(string normalizedName)
        {
            NormalizedName = normalizedName;
        }
    }
}
