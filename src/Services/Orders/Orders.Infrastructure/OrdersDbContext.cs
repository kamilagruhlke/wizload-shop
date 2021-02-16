using Microsoft.EntityFrameworkCore;
using Orders.Domain.AggregateModel.OrderAggregate;
using Orders.Domain.SeedWork;
using System.Threading;
using System.Threading.Tasks;

namespace Orders.Infrastructure
{
    public class OrdersDbContext : DbContext, IUnitOfWork
    {
        public OrdersDbContext(DbContextOptions<OrdersDbContext> options)
            : base(options) 
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderedProduct> OrderedProducts { get; set; }
    }
}
