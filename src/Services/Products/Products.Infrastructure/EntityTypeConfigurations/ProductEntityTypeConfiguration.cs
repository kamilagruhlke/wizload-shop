﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Products.Domain.AggregateModel.ProductAggregate;

namespace Products.Infrastructure.EntityTypeConfigurations
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("products", "products");

            builder.HasKey(e => e.Id)
                .HasName("pk_products_product_id");

            builder.Property(e => e.Id)
                .HasColumnName("id") 
                .ValueGeneratedOnAdd();

            builder.Property(e => e.ProducerCode)
                .HasColumnName("producer_code");

            builder.Property(e => e.ProducerId)
                .HasColumnName("producer_id");

            builder.Property(e => e.CategoryId)
                .HasColumnName("category_id");

            builder.Property(e => e.Specification)
                .HasColumnName("specification");

            builder.Property(e => e.Description)
                .HasColumnName("description");

            builder.Property(e => e.Image)
                .HasColumnName("image");

            builder.Property(e => e.IsDeleted)
                .HasColumnName("is_deleted");

            builder.Property(e => e.Name)
                .HasColumnName("name");

            builder.Property(e => e.NetPrice)
                .HasColumnName("net_price");

            builder.Property(e => e.Tax)
                .HasColumnName("tax");

            builder.Property(e => e.CreatedAt)
                .HasColumnName("created_at");

            builder.Property(e => e.CreatedBy)
                .HasColumnName("created_by");

            builder.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");

            builder.Property(e => e.UpdatedBy)
                .HasColumnName("updated_by");

            builder.HasIndex(e => e.Id)
                .HasDatabaseName("idx_products_product_id");

            builder.HasIndex(e => e.ProducerId)
                .HasDatabaseName("idx_products_producer_id");

            builder.HasIndex(e => e.CategoryId)
                .HasDatabaseName("idx_products_category_id");

            builder.HasIndex(e => new { e.ProducerCode, e.ProducerId })
                .HasDatabaseName("idx_products_producer_code_producer_id")
                .IsUnique(true);
        }
    }
}