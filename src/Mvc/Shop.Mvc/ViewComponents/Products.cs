using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WizLoad.ApiClient;

namespace Shop.Mvc.ViewComponents
{
    public class Products : ViewComponent
    {
        private readonly productsClient _productsClient;

        public Products(productsClient productsClient) => _productsClient = productsClient;

        public async Task<IViewComponentResult> InvokeAsync(Guid? categoryId)
        {
            ICollection<ProductModel> products;

            if (categoryId.HasValue)
            {
                products = await _productsClient.ProductsGetByCategoryIdAsync(categoryId.Value)
                    .ConfigureAwait(false);
            }
            else
            {
                products = await _productsClient.ProductsGetLastCreatedProductsAsync(20)
                    .ConfigureAwait(false);
            }

            return View("Index", products);
        }
    }
}