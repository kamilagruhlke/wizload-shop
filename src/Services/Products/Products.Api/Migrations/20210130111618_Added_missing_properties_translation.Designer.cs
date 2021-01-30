﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Products.Infrastructure;

namespace Products.Api.Migrations
{
    [DbContext(typeof(ProductsDbContext))]
    [Migration("20210130111618_Added_missing_properties_translation")]
    partial class Added_missing_properties_translation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Products.Domain.AggregateModel.ProducerAggregate.Producer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text")
                        .HasColumnName("updated_by");

                    b.HasKey("Id")
                        .HasName("pk_producers_producer_id");

                    b.HasIndex("Id")
                        .HasDatabaseName("idx_producers_producer_id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("idx_producers_producer_name");

                    b.ToTable("producers", "products");
                });

            modelBuilder.Entity("Products.Domain.AggregateModel.ProductAggregate.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid")
                        .HasColumnName("category_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<decimal>("GrossPrice")
                        .HasColumnType("numeric")
                        .HasColumnName("gross_price");

                    b.Property<string>("Image")
                        .HasColumnType("text")
                        .HasColumnName("image");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("ProducerCode")
                        .HasColumnType("text")
                        .HasColumnName("producer_code");

                    b.Property<Guid>("ProducerId")
                        .HasColumnType("uuid")
                        .HasColumnName("producer_id");

                    b.Property<string>("Specification")
                        .HasColumnType("text")
                        .HasColumnName("specification");

                    b.Property<decimal>("Tax")
                        .HasColumnType("numeric")
                        .HasColumnName("tax");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text")
                        .HasColumnName("updated_by");

                    b.HasKey("Id")
                        .HasName("pk_products_product_id");

                    b.HasIndex("CategoryId")
                        .HasDatabaseName("idx_products_category_id");

                    b.HasIndex("Id")
                        .HasDatabaseName("idx_products_product_id");

                    b.HasIndex("ProducerId")
                        .HasDatabaseName("idx_products_producer_id");

                    b.HasIndex("ProducerCode", "ProducerId")
                        .IsUnique()
                        .HasDatabaseName("idx_products_producer_code_producer_id");

                    b.ToTable("products", "products");
                });
#pragma warning restore 612, 618
        }
    }
}
