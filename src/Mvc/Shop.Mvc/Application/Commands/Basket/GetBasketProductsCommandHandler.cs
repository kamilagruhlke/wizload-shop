using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shop.Mvc.Application.Models;
using WizLoad.ApiClient;

namespace Shop.Mvc.Application.Commands.Basket
{
    public class GetBasketProductsCommandHandler : IRequestHandler<GetBasketProductsCommand, BasketProductsModel>
    {
        private readonly basketClient _basketClient;

        private readonly productsClient _productsClient;

        public GetBasketProductsCommandHandler(basketClient basketClient,
            productsClient productsClient)
        {
            _basketClient = basketClient;
            _productsClient = productsClient;
        }

        public async Task<BasketProductsModel> Handle(GetBasketProductsCommand request, CancellationToken cancellationToken)
        {
            var basket = await _basketClient.Basket3Async(request.Id, cancellationToken)
                .ConfigureAwait(false);

            if (basket.ProductIds == null || basket?.ProductIds?.Count <= 0)
            {
                return new BasketProductsModel
                {
                    Products = new List<ProductModel>(),
                    GrossSum = 0.0m
                };
            }

            var products = new List<ProductModel>();
            foreach (var productId in basket.ProductIds)
            {
                var product = await _productsClient.ProductsGetByIdAsync(productId,
                    cancellationToken);

                products.Add(product);
            }

            return new BasketProductsModel
            {
                Products = products,
                GrossSum = (decimal)products.Sum(e => e.GrossPrice)
            };
        }
    }
}
