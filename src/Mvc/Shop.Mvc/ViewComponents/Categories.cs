using WizLoad.ApiClient;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Shop.Mvc.ViewComponents
{
    public class Categories : ViewComponent
    {
        private readonly categoriesClient _categoriesClient;

        public Categories(categoriesClient categoriesClient) => _categoriesClient = categoriesClient;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoriesClient.ActiveAsync()
                .ConfigureAwait(false);

            return View("Index", categories);
        }
    }
}