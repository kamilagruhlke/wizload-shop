using System.Collections.Generic;
using MediatR;
using Products.Api.Application.Models;

namespace Products.Api.Application.Commands
{
    public class GetLastCreatedProductsCommand : IRequest<IList<ProductModel>>
    {
        public int NumberOfItems { get; set; }

        public GetLastCreatedProductsCommand(int numberOfItems)
        {
            NumberOfItems = numberOfItems;
        }
    }
}
