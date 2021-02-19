using Orders.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Orders.Domain.AggregateModel.OrderAggregate
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> Add(Order order, CancellationToken cancellationToken);

        Order Update(Order order);

        void Delete(Order order);

        Task<Order> FindById(Guid id, CancellationToken cancellationToken);

        Task<List<Order>> GetOrdersByStatus(string status, CancellationToken cancellationToken);

        Task<List<Order>> GetCreatedOrders(DateTime date, CancellationToken cancellationToken);
    }
}
