using WizLoad.ApiClient;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Mvc.ViewComponents
{
    public class Categories : ViewComponent
    {
        private readonly categoriesClient _categoriesClient;

        public Categories(categoriesClient categoriesClient) => _categoriesClient = categoriesClient;

        public IViewComponentResult Invoke()
        {
            var categories = _categoriesClient.ActiveAsync()
                .GetAwaiter()
                .GetResult();

            return View("Index", categories);
        }
    }
}