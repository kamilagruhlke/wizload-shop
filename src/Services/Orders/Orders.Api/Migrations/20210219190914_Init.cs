using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orders.Api.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "orders");

            migrationBuilder.CreateTable(
                name: "orders",
                schema: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<string>(type: "text", nullable: true),
                    value_net = table.Column<decimal>(type: "numeric", nullable: false),
                    value_tax = table.Column<decimal>(type: "numeric", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders_order_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ordered_products",
                schema: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders_ordered_product_id", x => x.id);
                    table.ForeignKey(
                        name: "FK_ordered_products_orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "orders",
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "idx_orders_ordered_products_id",
                schema: "orders",
                table: "ordered_products",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "idx_orders_ordered_products_product_id",
                schema: "orders",
                table: "ordered_products",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_ordered_products_OrderId",
                schema: "orders",
                table: "ordered_products",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "idx_orders_orders_id",
                schema: "orders",
                table: "orders",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "idx_orders_orders_user_id",
                schema: "orders",
                table: "orders",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ordered_products",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "orders",
                schema: "orders");
        }
    }
}
