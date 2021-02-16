using System;
using MediatR;

namespace Shop.Mvc.Application.Commands.Categories
{
    public class CreateCategoryCommand : IRequest<bool>
    {
        public Guid? ParentId { get; set; }

        public string Name { get; set; }
    }
}
