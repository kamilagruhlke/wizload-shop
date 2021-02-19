using Microsoft.EntityFrameworkCore.Migrations;

namespace Orders.Api.Migrations
{
    public partial class Added_client_field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "client_full_name",
                schema: "orders",
                table: "orders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "email",
                schema: "orders",
                table: "orders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phone_number",
                schema: "orders",
                table: "orders",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "client_full_name",
                schema: "orders",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "email",
                schema: "orders",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "phone_number",
                schema: "orders",
                table: "orders");
        }
    }
}
