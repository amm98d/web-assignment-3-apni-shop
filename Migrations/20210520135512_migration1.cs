using Microsoft.EntityFrameworkCore.Migrations;

namespace ApniShop.Migrations
{
    public partial class migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApniShopUserProduct");

            migrationBuilder.CreateTable(
                name: "Wants_ProductApniShopUser",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    ApniShopUserID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wants_ProductApniShopUser", x => new { x.ApniShopUserID, x.ProductID });
                    table.ForeignKey(
                        name: "FK_Wants_ProductApniShopUser_AspNetUsers_ApniShopUserID",
                        column: x => x.ApniShopUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wants_ProductApniShopUser_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wants_ProductApniShopUser_ProductID",
                table: "Wants_ProductApniShopUser",
                column: "ProductID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wants_ProductApniShopUser");

            migrationBuilder.CreateTable(
                name: "ApniShopUserProduct",
                columns: table => new
                {
                    WantedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WantsProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApniShopUserProduct", x => new { x.WantedById, x.WantsProductID });
                    table.ForeignKey(
                        name: "FK_ApniShopUserProduct_AspNetUsers_WantedById",
                        column: x => x.WantedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApniShopUserProduct_Products_WantsProductID",
                        column: x => x.WantsProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApniShopUserProduct_WantsProductID",
                table: "ApniShopUserProduct",
                column: "WantsProductID");
        }
    }
}
