using System.Threading;
using System.Threading.Tasks;
using Categories.Domain.AggregateModel.CategoryAggregate;
using Categories.Domain.SeedWork;
using Categories.Infrastructure.EnityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Categories.Infrastructure
{
    public class CategoriesDbContext : DbContext, IUnitOfWork
    {
        public CategoriesDbContext(DbContextOptions<CategoriesDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        public DbSet<Category> Categories { get; set; }
    }
}