using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orders.Api.Migrations
{
    public partial class Added_address_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "idx_orders_orders_user_id",
                schema: "orders",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "user_id",
                schema: "orders",
                table: "orders");

            migrationBuilder.AddColumn<string>(
                name: "address",
                schema: "orders",
                table: "orders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "city",
                schema: "orders",
                table: "orders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "postal_code",
                schema: "orders",
                table: "orders",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address",
                schema: "orders",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "city",
                schema: "orders",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "postal_code",
                schema: "orders",
                table: "orders");

            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                schema: "orders",
                table: "orders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "idx_orders_orders_user_id",
                schema: "orders",
                table: "orders",
                column: "user_id");
        }
    }
}
