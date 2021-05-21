using Microsoft.EntityFrameworkCore.Migrations;

namespace ApniShop.Migrations
{
    public partial class migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductTitle",
                table: "Products",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "ProductRating",
                table: "Products",
                newName: "Rating");

            migrationBuilder.RenameColumn(
                name: "ProductImagePath",
                table: "Products",
                newName: "ImagePath");

            migrationBuilder.RenameColumn(
                name: "ProductDemand",
                table: "Products",
                newName: "Demand");

            migrationBuilder.RenameColumn(
                name: "ProductAvailability",
                table: "Products",
                newName: "Availability");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "Products",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Products",
                newName: "ProductTitle");

            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "Products",
                newName: "ProductRating");

            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Products",
                newName: "ProductImagePath");

            migrationBuilder.RenameColumn(
                name: "Demand",
                table: "Products",
                newName: "ProductDemand");

            migrationBuilder.RenameColumn(
                name: "Availability",
                table: "Products",
                newName: "ProductAvailability");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "ProductID");
        }
    }
}
