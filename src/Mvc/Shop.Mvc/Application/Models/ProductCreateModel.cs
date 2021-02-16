using System.Collections.Generic;
using Shop.Mvc.Application.Commands.Products;
using WizLoad.ApiClient;

namespace Shop.Mvc.Application.Models
{
    public class ProductCreateModel
    {
        public ICollection<ProducerModel> Producers { get; set; }

        public ICollection<CategoryModel> Categories { get; set; }

        public CreateProductCommand CreateProductCommand { get; set; }
    }
}
