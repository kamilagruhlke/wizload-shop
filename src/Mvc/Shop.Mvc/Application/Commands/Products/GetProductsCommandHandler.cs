using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WizLoad.ApiClient;

namespace Shop.Mvc.Application.Commands.Products
{
    public class GetProductsCommandHandler : IRequestHandler<GetProductsCommand, List<ProductModel>>
    {
        private readonly productsClient _productsClient;

        public GetProductsCommandHandler(productsClient productsClient)
        {
            _productsClient = productsClient;
        }

        public async Task<List<ProductModel>> Handle(GetProductsCommand request, CancellationToken cancellationToken)
        {
            return (await _productsClient.ProductsGetLastCreatedProductsAsync(100, cancellationToken)
                .ConfigureAwait(false))
                .ToList();
        }
    }
}
