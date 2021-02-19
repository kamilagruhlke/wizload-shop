using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Mvc.Application.Commands.Orders
{
    public class InProgresshOrderCommandHandler : IRequestHandler<InProgressOrderCommand, bool>
    {
        private readonly WizLoad.ApiClient.ordersClient _ordersClient;

        public InProgresshOrderCommandHandler(WizLoad.ApiClient.ordersClient ordersClient)
        {
            _ordersClient = ordersClient;
        }

        public async Task<bool> Handle(InProgressOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _ordersClient.OrdersGetOrderByIdAsync(request.Id, cancellationToken);

            await _ordersClient.OrdersUpdateOrderAsync(new WizLoad.ApiClient.UpdateOrderCommand
            {
                Id = order.Id,
                Status = "IN_PROGRESS",
                Address = order.Address,
                City = order.City,
                OrderedProducts = order.OrderedProducts.Select(e => e.ProductId).ToList(),
                PostalCode = order.PostalCode,
                ValueNet = order.ValueNet,
                ValueTax = order.ValueTax
            });

            return true;
        }
    }
}
