using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WizLoad.ApiClient;

namespace Shop.Mvc.Application.Commands.Products
{
    public class GetProductByIdCommandHandler : IRequestHandler<GetProductByIdCommand, ProductModel>
    {
        private readonly productsClient _productsClient;

        public GetProductByIdCommandHandler(productsClient productsClient) => _productsClient = productsClient;

        public async Task<ProductModel> Handle(GetProductByIdCommand request, CancellationToken cancellationToken)
        {
            var productModel = await _productsClient.ProductsGetByIdAsync(request.ProductId, cancellationToken)
                .ConfigureAwait(false);

            return productModel;
        }
    }
}
