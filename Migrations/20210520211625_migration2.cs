using Microsoft.EntityFrameworkCore.Migrations;

namespace ApniShop.Migrations
{
    public partial class migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "ProductAvailability", "ProductDemand", "ProductImagePath", "ProductRating", "ProductTitle" },
                values: new object[,]
                {
                    { 1, 10, 3, "/images/image-1.jpg", 0, "Mango" },
                    { 2, 3, 1, "/images/image-2.jpg", 1, "Sofa" },
                    { 3, 3, 1, "/images/image-2.jpg", 1, "Earphones" },
                    { 4, 3, 1, "/images/image-2.jpg", 1, "Xbox" },
                    { 5, 3, 1, "/images/image-2.jpg", 1, "Laptop" },
                    { 6, 3, 1, "/images/image-2.jpg", 1, "Office chair" },
                    { 7, 3, 1, "/images/image-2.jpg", 1, "Sofa" },
                    { 8, 3, 1, "/images/image-2.jpg", 3, "Sofa" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 8);
        }
    }
}
