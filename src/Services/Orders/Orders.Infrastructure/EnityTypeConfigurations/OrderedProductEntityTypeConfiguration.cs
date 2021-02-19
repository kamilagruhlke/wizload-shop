using Orders.Domain.AggregateModel.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Orders.Infrastructure.EnityTypeConfigurations
{
    public class OrderedProductEntityTypeConfiguration : IEntityTypeConfiguration<OrderedProduct>
    {
        public void Configure(EntityTypeBuilder<OrderedProduct> builder)
        {
            builder.ToTable("ordered_products", "orders");

            builder.HasKey(e => e.Id)
                .HasName("pk_orders_ordered_product_id");

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.ProductId)
                .HasColumnName("product_id");

            builder.HasIndex(e => e.Id)
                .HasDatabaseName("idx_orders_ordered_products_id");

            builder.HasIndex(e => e.ProductId)
                .HasDatabaseName("idx_orders_ordered_products_product_id");
        }
    }
}
