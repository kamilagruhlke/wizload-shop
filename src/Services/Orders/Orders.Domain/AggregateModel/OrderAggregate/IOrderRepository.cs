using Orders.Domain.SeedWork;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Orders.Domain.AggregateModel.OrderAggregate
{
    interface IOrderRepository : IRepository<Order>
    {
        Task<Order> Add(Order order, CancellationToken cancellationToken);

        Order Update(Order order);

        void Delete(Order order);

        Task<Order> FindById(Guid id, CancellationToken cancellationToken);
    }
}
