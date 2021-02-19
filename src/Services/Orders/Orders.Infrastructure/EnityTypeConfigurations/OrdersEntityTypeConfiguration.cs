using Orders.Domain.AggregateModel.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Orders.Infrastructure.EnityTypeConfigurations
{
    public class OrdersEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("orders", "orders");

            builder.HasKey(e => e.Id)
                .HasName("pk_orders_order_id");

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Status)
                .HasColumnName("status");

            builder.Property(e => e.ValueNet)
                .HasColumnName("value_net");

            builder.Property(e => e.ValueTax)
                .HasColumnName("value_tax");

            builder.Property(e => e.UserId)
                .HasColumnName("user_id");

            builder.Property(e => e.CreatedBy)
                .HasColumnName("created_by");

            builder.Property(e => e.CreatedAt)
                .HasColumnName("created_at");

            builder.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");

            builder.Property(e => e.UpdatedBy)
                .HasColumnName("updated_by");

            builder.HasIndex(e => e.Id)
                .HasDatabaseName("idx_orders_orders_id");

            builder.HasIndex(e => e.UserId)
                .HasDatabaseName("idx_orders_orders_user_id");
        }
    }
}
