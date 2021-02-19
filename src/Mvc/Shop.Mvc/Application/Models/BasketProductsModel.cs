using System.Collections.Generic;
using WizLoad.ApiClient;

namespace Shop.Mvc.Application.Models
{
    public class BasketProductsModel
    {
        public List<ProductModel> Products { get; set; }

        public decimal GrossSum { get; set; }
    }
}
