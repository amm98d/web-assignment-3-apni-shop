using Microsoft.EntityFrameworkCore.Migrations;

namespace ApniShop.Migrations
{
    public partial class migration5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_SellerId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Products",
                newName: "ProductTitle");

            migrationBuilder.RenameColumn(
                name: "SellerId",
                table: "Products",
                newName: "ProductSellerId");

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
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_SellerId",
                table: "Products",
                newName: "IX_Products_ProductSellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_ProductSellerId",
                table: "Products",
                column: "ProductSellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_ProductSellerId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ProductTitle",
                table: "Products",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "ProductSellerId",
                table: "Products",
                newName: "SellerId");

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
                name: "ProductId",
                table: "Products",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductSellerId",
                table: "Products",
                newName: "IX_Products_SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_SellerId",
                table: "Products",
                column: "SellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
