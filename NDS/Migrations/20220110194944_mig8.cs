using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NDS.Migrations
{
    public partial class mig8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tbl_PublishPlan",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FkCustomerId = table.Column<long>(type: "bigint", nullable: false),
                    FkStaffId = table.Column<long>(type: "bigint", nullable: false),
                    FkProductId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    DeliveryDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_PublishPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tbl_PublishPlan_Tbl_Customer_FkCustomerId",
                        column: x => x.FkCustomerId,
                        principalTable: "Tbl_Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tbl_PublishPlan_Tbl_Product_FkProductId",
                        column: x => x.FkProductId,
                        principalTable: "Tbl_Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Tbl_PublishPlan_Tbl_Staff_FkStaffId",
                        column: x => x.FkStaffId,
                        principalTable: "Tbl_Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_PublishPlan_FkCustomerId",
                table: "Tbl_PublishPlan",
                column: "FkCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_PublishPlan_FkProductId",
                table: "Tbl_PublishPlan",
                column: "FkProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_PublishPlan_FkStaffId",
                table: "Tbl_PublishPlan",
                column: "FkStaffId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tbl_PublishPlan");
        }
    }
}
