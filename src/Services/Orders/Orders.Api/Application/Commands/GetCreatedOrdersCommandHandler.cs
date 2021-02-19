﻿using MediatR;
using Orders.Api.Application.Models;
using Orders.Domain.AggregateModel.OrderAggregate;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Orders.Api.Application.Commands
{
    public class GetCreatedOrdersCommandHandler : IRequestHandler<GetCreatedOrdersCommand, List<OrderModel>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetCreatedOrdersCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<OrderModel>> Handle(GetCreatedOrdersCommand request, CancellationToken cancellationToken)
        {
            var ordersByStatus = await _orderRepository.GetCreatedOrders(request.Date, cancellationToken)
                            .ConfigureAwait(false);

            return ordersByStatus.Select(order => new OrderModel
            {
                Id = order.Id,
                OrderedProducts = order.OrderedProducts.Select(e => new OrderedProductModel
                {
                    Id = e.Id,
                    ProductId = e.ProductId
                }).ToList(),
                Status = order.Status,
                ValueTax = order.ValueTax,
                ValueNet = order.ValueNet,
                City = order.City,
                Address = order.Address,
                PostalCode = order.PostalCode,
                CreatedAt = order.CreatedAt,
                CreatedBy = order.CreatedBy,
                UpdatedBy = order.UpdatedBy,
                UpdatedAt = order.UpdatedAt
            }).ToList();
        }
    }
}
