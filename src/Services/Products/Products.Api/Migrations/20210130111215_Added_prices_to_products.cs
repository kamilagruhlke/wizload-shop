using Microsoft.EntityFrameworkCore.Migrations;

namespace Products.Api.Migrations
{
    public partial class Added_prices_to_products : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "gross_price",
                schema: "products",
                table: "products",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "tax",
                schema: "products",
                table: "products",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "idx_products_producer_code_producer_id",
                schema: "products",
                table: "products",
                columns: new[] { "producer_code", "producer_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_producers_producer_name",
                schema: "products",
                table: "producers",
                column: "name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "idx_products_producer_code_producer_id",
                schema: "products",
                table: "products");

            migrationBuilder.DropIndex(
                name: "idx_producers_producer_name",
                schema: "products",
                table: "producers");

            migrationBuilder.DropColumn(
                name: "gross_price",
                schema: "products",
                table: "products");

            migrationBuilder.DropColumn(
                name: "tax",
                schema: "products",
                table: "products");
        }
    }
}
