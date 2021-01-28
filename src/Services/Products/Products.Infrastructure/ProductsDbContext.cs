using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Products.Domain.AggregateModel.ProducerAggregate;
using Products.Domain.AggregateModel.ProductAggregate;
using Products.Domain.SeedWork;
using Products.Infrastructure.EntityTypeConfigurations;

namespace Products.Infrastructure
{
    public class ProductsDbContext : DbContext, IUnitOfWork
    {
        public ProductsDbContext(DbContextOptions<ProductsDbContext> options)
                 : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProducerEntityTypeConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Producer> Producers { get; set; }
    }
}
