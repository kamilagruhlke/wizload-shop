using MediatR;
using Orders.Domain.AggregateModel.OrderAggregate;
using Orders.Domain.Utils.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Orders.Api.Application.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;

        private readonly IUserAccessor _userAccessor;


        public CreateOrderCommandHandler(IOrderRepository orderRepository, IUserAccessor userAccessor)
        {
            _orderRepository = orderRepository;
            _userAccessor = userAccessor;
        }

        public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order(request.ValueNet,
                request.ValueTax,
                request.Address,
                request.City,
                request.PostalCode,
                request.ClientFullName,
                request.Email,
                request.PhoneNumber,
                _userAccessor.GetCurrentUsername());

            foreach(var orderedProduct in request.OrderedProducts)
            {
                order.UpdateAddProductToList(new OrderedProduct(orderedProduct));
            }

            await _orderRepository.Add(order, cancellationToken)
                .ConfigureAwait(false);

            return await _orderRepository.UnitOfWork.SaveEntitiesAsync()
                .ConfigureAwait(false);
        }
    }
}
