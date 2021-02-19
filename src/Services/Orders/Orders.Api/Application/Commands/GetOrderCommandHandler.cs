using MediatR;
using Orders.Api.Application.Models;
using Orders.Domain.AggregateModel.OrderAggregate;
using Orders.Domain.Exceptions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Orders.Api.Application.Commands
{
    public class GetOrderCommandHandler : IRequestHandler<GetOrderCommand, OrderModel>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderModel> Handle(GetOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.FindById(request.Id, cancellationToken)
                .ConfigureAwait(false);

            if (order is null)
            {
                throw new EntityNotFoundBusinessException($"{request.Id}");
            }

            return new OrderModel
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
                ClientFullName = order.ClientFullName,
                Email = order.Email,
                PhoneNumber = order.PhoneNumber,
                CreatedAt = order.CreatedAt,
                CreatedBy = order.CreatedBy,
                UpdatedBy = order.UpdatedBy,
                UpdatedAt = order.UpdatedAt
            };
        }
    }
}
