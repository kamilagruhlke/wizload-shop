using System;
using MediatR;

namespace Categories.Api.Application.Commands
{
    public class CreateCategoryCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public Guid? ParentId { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }
    }
}
