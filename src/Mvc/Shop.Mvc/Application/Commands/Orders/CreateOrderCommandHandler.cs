using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WizLoad.ApiClient;

namespace Shop.Mvc.Application.Commands.Orders
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly ordersClient _ordersClient;

        private readonly basketClient _basketClient;

        private readonly productsClient _productsClient;

        public CreateOrderCommandHandler(ordersClient ordersClient, basketClient basketClient, productsClient productsClient)
        {
            _ordersClient = ordersClient;
            _basketClient = basketClient;
            _productsClient = productsClient;
        }

        public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var basket = await _basketClient.Basket3Async(request.BaksetId, cancellationToken);

            var productList = new List<ProductModel>();
            foreach(var product in basket.ProductIds)
            {
                productList.Add(await _productsClient.ProductsGetByIdAsync(product, cancellationToken));
            }

            await _ordersClient.OrdersCreateOrderAsync(new WizLoad.ApiClient.CreateOrderCommand
            {
                City = request.City,
                Address = request.Address,
                PostalCode = request.PostalCode,
                ClientFullName = request.ClientFullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                ValueNet = productList.Sum(e => e.NetPrice),
                ValueTax = productList.Sum(e => Math.Abs(e.NetPrice - e.GrossPrice)),
                OrderedProducts = basket.ProductIds,
            }, cancellationToken);

            return true;
        }
    }
}
