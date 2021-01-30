using System;
using System.Collections.Generic;
using MediatR;
using Products.Api.Application.Models;

namespace Products.Api.Application.Commands
{
    public class GetProductByCategoryIdCommand : IRequest<IList<ProductModel>>
    {
        public Guid CategoryId { get; set; }

        public GetProductByCategoryIdCommand(Guid categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
