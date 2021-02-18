using MediatR;
using Orders.Domain.AggregateModel.OrderAggregate;
using Orders.Domain.Exceptions;
using Orders.Domain.Utils.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Orders.Api.Application.Commands
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;

        private readonly IUserAccessor _userAccessor;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IUserAccessor userAccessor)
        {
            _orderRepository = orderRepository;
            _userAccessor = userAccessor;
        }

        public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.FindById(request.Id, cancellationToken)
                            .ConfigureAwait(false);

            if (order is null)
            {
                throw new EntityNotFoundBusinessException($"Order '{order.Id}' not found");
            }
            
            order.UpdateStatus(request.Status, _userAccessor.GetCurrentUsername());
            order.UpdateValueNetto(request.ValueNet);
            order.UpdateValueTax(request.ValueTax);
            order.UpdateModificationDates(_userAccessor.GetCurrentUsername());

            _orderRepository.Update(order);

            return await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
