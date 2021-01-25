using System;

namespace Categories.Api.Application.Models
{
    public class CategoryModel
    {
        public Guid Id { get; set; }

        public Guid? ParentId { get; set; }

        public string Name { get; set; }
    }
}
