using System;
using MediatR;
using WizLoad.ApiClient;

namespace Shop.Mvc.Application.Commands.Products
{
    public class GetProductByIdCommand : IRequest<ProductModel>
    {
        public Guid ProductId { get; set; }

        public GetProductByIdCommand(Guid produtId) => ProductId = produtId;
    }
}
