using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Products.Api.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "products");

            migrationBuilder.CreateTable(
                name: "producers",
                schema: "products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_producers_producer_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                schema: "products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    specification = table.Column<string>(type: "text", nullable: true),
                    image = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    producer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    producer_code = table.Column<string>(type: "text", nullable: true),
                    net_price = table.Column<decimal>(type: "numeric", nullable: false),
                    tax = table.Column<decimal>(type: "numeric", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_products_product_id", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "idx_producers_producer_id",
                schema: "products",
                table: "producers",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "idx_producers_producer_name",
                schema: "products",
                table: "producers",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_products_category_id",
                schema: "products",
                table: "products",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "idx_products_producer_code_producer_id",
                schema: "products",
                table: "products",
                columns: new[] { "producer_code", "producer_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_products_producer_id",
                schema: "products",
                table: "products",
                column: "producer_id");

            migrationBuilder.CreateIndex(
                name: "idx_products_product_id",
                schema: "products",
                table: "products",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "producers",
                schema: "products");

            migrationBuilder.DropTable(
                name: "products",
                schema: "products");
        }
    }
}
