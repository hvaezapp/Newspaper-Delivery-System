using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NDS.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tbl_Province",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Province", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Staff",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Staff", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FkProvinceId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tbl_City_Tbl_Province_FkProvinceId",
                        column: x => x.FkProvinceId,
                        principalTable: "Tbl_Province",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Customer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    latitude = table.Column<double>(type: "float", nullable: false),
                    longitude = table.Column<double>(type: "float", nullable: false),
                    FkCityId = table.Column<int>(type: "int", nullable: false),
                    FkProvinceId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tbl_Customer_Tbl_City_FkCityId",
                        column: x => x.FkCityId,
                        principalTable: "Tbl_City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tbl_Customer_Tbl_Province_FkProvinceId",
                        column: x => x.FkProvinceId,
                        principalTable: "Tbl_Province",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Supplier",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FkCityId = table.Column<int>(type: "int", nullable: false),
                    FkProvinceId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Supplier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tbl_Supplier_Tbl_City_FkCityId",
                        column: x => x.FkCityId,
                        principalTable: "Tbl_City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tbl_Supplier_Tbl_Province_FkProvinceId",
                        column: x => x.FkProvinceId,
                        principalTable: "Tbl_Province",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Product",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductType = table.Column<int>(type: "int", nullable: false),
                    FkSupplierId = table.Column<long>(type: "bigint", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    IsExist = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tbl_Product_Tbl_Supplier_FkSupplierId",
                        column: x => x.FkSupplierId,
                        principalTable: "Tbl_Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_City_FkProvinceId",
                table: "Tbl_City",
                column: "FkProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Customer_FkCityId",
                table: "Tbl_Customer",
                column: "FkCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Customer_FkProvinceId",
                table: "Tbl_Customer",
                column: "FkProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Product_FkSupplierId",
                table: "Tbl_Product",
                column: "FkSupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Supplier_FkCityId",
                table: "Tbl_Supplier",
                column: "FkCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Supplier_FkProvinceId",
                table: "Tbl_Supplier",
                column: "FkProvinceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tbl_Customer");

            migrationBuilder.DropTable(
                name: "Tbl_Product");

            migrationBuilder.DropTable(
                name: "Tbl_Staff");

            migrationBuilder.DropTable(
                name: "Tbl_Supplier");

            migrationBuilder.DropTable(
                name: "Tbl_City");

            migrationBuilder.DropTable(
                name: "Tbl_Province");
        }
    }
}
