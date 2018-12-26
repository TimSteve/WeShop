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
                name: "Catalog",
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
                    table.PrimaryKey("PK_Catalog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Catalog_ProductBrand_ProductBrandId",
                        column: x => x.ProductBrandId,
                        principalTable: "ProductBrand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Catalog_ProductBrand_ProductBrandId1",
                        column: x => x.ProductBrandId1,
                        principalTable: "ProductBrand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Catalog_ProductType_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Catalog_ProductType_ProductTypeId1",
                        column: x => x.ProductTypeId1,
                        principalTable: "ProductType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Catalog_ProductBrandId",
                table: "Catalog",
                column: "ProductBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Catalog_ProductBrandId1",
                table: "Catalog",
                column: "ProductBrandId1");

            migrationBuilder.CreateIndex(
                name: "IX_Catalog_ProductTypeId",
                table: "Catalog",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Catalog_ProductTypeId1",
                table: "Catalog",
                column: "ProductTypeId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Catalog");

            migrationBuilder.DropTable(
                name: "ProductBrand");

            migrationBuilder.DropTable(
                name: "ProductType");
        }
    }
}
