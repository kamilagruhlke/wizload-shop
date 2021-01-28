using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Products.Domain.AggregateModel.ProducerAggregate;

namespace Products.Infrastructure.EntityTypeConfigurations
{
    public class ProducerEntityTypeConfiguration : IEntityTypeConfiguration<Producer>
    {
        public void Configure(EntityTypeBuilder<Producer> builder)
        {
            builder.ToTable("producers", "products");

            builder.HasKey(e => e.Id)
                .HasName("pk_producers_producer_id");

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .HasColumnName("name");

            builder.Property(e => e.Description)
                .HasColumnName("description");

            builder.Property(e => e.CreatedAt)
                .HasColumnName("created_at");

            builder.Property(e => e.CreatedBy)
                .HasColumnName("created_by");

            builder.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");

            builder.Property(e => e.UpdatedBy)
                .HasColumnName("updated_by");

            builder.HasIndex(e => e.Id)
                .HasDatabaseName("idx_producers_producer_id");
        }
    }
}
