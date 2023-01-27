using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NDS.Migrations
{
    public partial class mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Count",
                table: "Tbl_Product",
                newName: "Quantity");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Tbl_Product",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Price",
                table: "Tbl_Product",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Tbl_Order",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FkCustomerId = table.Column<long>(type: "bigint", nullable: false),
                    TotalPrice = table.Column<long>(type: "bigint", nullable: false),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false),
                    CompleteDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tbl_Order_Tbl_Customer_FkCustomerId",
                        column: x => x.FkCustomerId,
                        principalTable: "Tbl_Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_OrderDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FkOrderId = table.Column<long>(type: "bigint", nullable: false),
                    FkProductId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tbl_OrderDetails_Tbl_Order_FkOrderId",
                        column: x => x.FkOrderId,
                        principalTable: "Tbl_Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tbl_OrderDetails_Tbl_Product_FkProductId",
                        column: x => x.FkProductId,
                        principalTable: "Tbl_Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Order_FkCustomerId",
                table: "Tbl_Order",
                column: "FkCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_OrderDetails_FkOrderId",
                table: "Tbl_OrderDetails",
                column: "FkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_OrderDetails_FkProductId",
                table: "Tbl_OrderDetails",
                column: "FkProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tbl_OrderDetails");

            migrationBuilder.DropTable(
                name: "Tbl_Order");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Tbl_Product");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Tbl_Product");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Tbl_Product",
                newName: "Count");
        }
    }
}
