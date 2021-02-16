using System.Collections.Generic;
using MediatR;
using WizLoad.ApiClient;

namespace Shop.Mvc.Application.Commands.Categories
{
    public class GetCategoriesCommand : IRequest<ICollection<CategoryModel>>
    {
    }
}
