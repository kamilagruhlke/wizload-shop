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

            builder.Property(e => e.Address)
                .HasColumnName("address");

            builder.Property(e => e.City)
                .HasColumnName("city");

            builder.Property(e => e.PostalCode)
                .HasColumnName("postal_code");

            builder.Property(e => e.ClientFullName)
                .HasColumnName("client_full_name");

            builder.Property(e => e.Email)
                .HasColumnName("email");

            builder.Property(e => e.PhoneNumber)
                .HasColumnName("phone_number");

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
        }
    }
}
