using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp13_Backend.Migrations
{
    public partial class Cart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "ProductDTO");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductDTO",
                table: "ProductDTO",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductDTO",
                table: "ProductDTO");

            migrationBuilder.RenameTable(
                name: "ProductDTO",
                newName: "Products");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");
        }
    }
}
