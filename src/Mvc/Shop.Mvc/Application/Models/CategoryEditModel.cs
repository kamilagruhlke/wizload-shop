using System.Collections.Generic;
using WizLoad.ApiClient;

namespace Shop.Mvc.Application.Models
{
    public class CategoryEditModel
    {
        public ICollection<CategoryModel> Categories { get; set; }

        public Commands.Categories.EditCategoryCommand EditCategoryCommand { get; set; }
    }
}
