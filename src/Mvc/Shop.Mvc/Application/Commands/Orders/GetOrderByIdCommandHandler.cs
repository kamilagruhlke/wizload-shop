using MediatR;
using Shop.Mvc.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Mvc.Application.Commands.Orders
{
    public class GetOrderByIdCommandHandler : IRequestHandler<GetOrderByIdCommand, OrderModel>
    {
        private readonly WizLoad.ApiClient.ordersClient _ordersClient;

        private readonly WizLoad.ApiClient.productsClient _productsClient;

        public GetOrderByIdCommandHandler(WizLoad.ApiClient.ordersClient ordersClient,
            WizLoad.ApiClient.productsClient productsClient)
        {
            _ordersClient = ordersClient;
            _productsClient = productsClient;
        }

        public async Task<OrderModel> Handle(GetOrderByIdCommand request, CancellationToken cancellationToken)
        {
            var order = await _ordersClient.OrdersGetOrderByIdAsync(request.Id, cancellationToken);

            return new OrderModel
            {
                Id = order.Id,
                Address = order.Address,
                City = order.City,
                PostalCode = order.PostalCode,
                Status = order.Status,
                ValueNet = (decimal)order.ValueNet,
                ValueTax = (decimal)order.ValueTax,
                ClientFullName = order.ClientFullName,
                Email = order.Email,
                PhoneNumber = order.PhoneNumber,
                CreatedAt = order.CreatedAt.DateTime,
                CreatedBy = order.CreatedBy,
                UpdatedAt = order.UpdatedAt?.DateTime,
                UpdatedBy = order.UpdatedBy,
                OrderedProducts = GetProducts(order.OrderedProducts.Select(e => e.ProductId).ToList(), cancellationToken)
                    .GetAwaiter()
                    .GetResult()
            };
        }

        private async Task<List<WizLoad.ApiClient.ProductModel>> GetProducts(List<Guid> productIds, CancellationToken cancellationToken)
        {
            var productList = new List<WizLoad.ApiClient.ProductModel>();
            foreach (var productId in productIds)
            {
                var product = await _productsClient.ProductsGetByIdAsync(productId, cancellationToken);
                productList.Add(product);
            }

            return productList;
        }
    }
}
