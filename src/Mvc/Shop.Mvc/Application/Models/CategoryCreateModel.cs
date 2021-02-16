using System.Collections.Generic;
using WizLoad.ApiClient;

namespace Shop.Mvc.Application.Models
{
    public class CategoryCreateModel
    {
        public ICollection<CategoryModel> Categories { get; set; }

        public Commands.Categories.CreateCategoryCommand CreateCategoryCommand { get; set; }
    }
}
