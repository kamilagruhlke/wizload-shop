using Shop.Mvc.Application.Commands.Products;
using System.Collections.Generic;
using WizLoad.ApiClient;

namespace Shop.Mvc.Application.Models
{
    public class ProductEditModel
    {
        public ICollection<ProducerModel> Producers { get; set; }

        public ICollection<CategoryModel> Categories { get; set; }

        public EditProductCommand EditProductCommand { get; set; }
    }
}
