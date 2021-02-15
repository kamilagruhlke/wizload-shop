using MediatR;
using System;

namespace Shop.Mvc.Application.Commands.Categories
{
    public class EditCategoryCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public Guid? ParentId { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }
    }
}
