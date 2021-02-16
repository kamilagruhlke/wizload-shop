using System;
using MediatR;
using Products.Api.Application.Models;

namespace Products.Api.Application.Commands
{
    public class GetProductByIdCommand : IRequest<ProductModel>
    {
        public Guid Id { get; set; }

        public GetProductByIdCommand(Guid id)
        {
            Id = id;
        }
    }
}
