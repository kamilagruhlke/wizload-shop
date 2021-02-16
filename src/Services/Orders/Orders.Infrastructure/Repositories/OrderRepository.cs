using Microsoft.EntityFrameworkCore;
using Orders.Domain.AggregateModel.OrderAggregate;
using Orders.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Orders.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrdersDbContext _ordersDbContext;

        public OrderRepository(OrdersDbContext ordersDbContext) => _ordersDbContext = ordersDbContext;

        public IUnitOfWork UnitOfWork => _ordersDbContext;

        public async Task<Order> Add(Order order, CancellationToken cancellationToken)
        {
            await _ordersDbContext.Orders.AddAsync(order, cancellationToken);

            return order;
        }

        public void Delete(Order order)
        {
            _ordersDbContext.Orders.Remove(order);
        }

        public async Task<Order> FindById(Guid id, CancellationToken cancellationToken)
        {
            return await _ordersDbContext.Orders
                .Include(e => e.OrderedProducts)
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<List<Order>> GetCreatedOrders(DateTime date, CancellationToken cancellationToken)
        {
            return await _ordersDbContext.Orders
                .Include(e => e.OrderedProducts)
                .OrderByDescending(e => e.CreatedAt.Date == date)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<List<Order>> GetOrdersByStatus(string status, CancellationToken cancellationToken)
        {
            return await _ordersDbContext.Orders
                .Include(e => e.OrderedProducts)
                .Where(e => e.Status == status)
                .ToListAsync(cancellationToken);
        }

        public Order Update(Order order)
        {
            _ordersDbContext.Orders.Update(order);

            return order;
        }
    }
}
