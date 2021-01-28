using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Categories.Api.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "categories");

            migrationBuilder.CreateTable(
                name: "categories",
                schema: "categories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    parent_id = table.Column<Guid>(type: "uuid", nullable: true),
                    name = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categories_category_id", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "idx_categories_categories_id",
                schema: "categories",
                table: "categories",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "idx_categories_categories_parent_id",
                schema: "categories",
                table: "categories",
                column: "parent_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "categories",
                schema: "categories");
        }
    }
}
