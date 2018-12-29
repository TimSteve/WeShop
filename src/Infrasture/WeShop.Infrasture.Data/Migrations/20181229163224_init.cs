using Microsoft.EntityFrameworkCore.Migrations;

namespace WeShop.Infrasture.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductBrand",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBrand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductType",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    PictureFileName = table.Column<string>(nullable: true),
                    ProductTypeId = table.Column<long>(nullable: false),
                    ProductBrandId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsPublished = table.Column<bool>(nullable: false),
                    ProductBrandId1 = table.Column<long>(nullable: true),
                    ProductTypeId1 = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_ProductBrand_ProductBrandId",
                        column: x => x.ProductBrandId,
                        principalTable: "ProductBrand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_ProductBrand_ProductBrandId1",
                        column: x => x.ProductBrandId1,
                        principalTable: "ProductBrand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_ProductType_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_ProductType_ProductTypeId1",
                        column: x => x.ProductTypeId1,
                        principalTable: "ProductType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductBrandId",
                table: "Product",
                column: "ProductBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductBrandId1",
                table: "Product",
                column: "ProductBrandId1");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductTypeId",
                table: "Product",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductTypeId1",
                table: "Product",
                column: "ProductTypeId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ProductBrand");

            migrationBuilder.DropTable(
                name: "ProductType");
        }
    }
}
