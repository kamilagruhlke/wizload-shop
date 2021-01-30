using Microsoft.EntityFrameworkCore.Migrations;

namespace Products.Api.Migrations
{
    public partial class Added_missing_properties_translation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                schema: "products",
                table: "products",
                newName: "image");

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "products",
                table: "products",
                newName: "description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "image",
                schema: "products",
                table: "products",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "description",
                schema: "products",
                table: "products",
                newName: "Description");
        }
    }
}
