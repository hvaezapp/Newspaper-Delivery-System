using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NDS.Migrations
{
    public partial class mig9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tbl_AdminUser",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_AdminUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Payment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FkCustomerId = table.Column<long>(type: "bigint", nullable: false),
                    FkOrderId = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tbl_Payment_Tbl_Customer_FkCustomerId",
                        column: x => x.FkCustomerId,
                        principalTable: "Tbl_Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tbl_Payment_Tbl_Order_FkOrderId",
                        column: x => x.FkOrderId,
                        principalTable: "Tbl_Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Transaction",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Transaction", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Payment_FkCustomerId",
                table: "Tbl_Payment",
                column: "FkCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Payment_FkOrderId",
                table: "Tbl_Payment",
                column: "FkOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tbl_AdminUser");

            migrationBuilder.DropTable(
                name: "Tbl_Payment");

            migrationBuilder.DropTable(
                name: "Tbl_Transaction");
        }
    }
}
