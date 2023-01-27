using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NDS.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tbl_StaffSessionReady",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FkStaffId = table.Column<long>(type: "bigint", nullable: false),
                    SessionDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SessionReadyStatus = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_StaffSessionReady", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tbl_StaffSessionReady_Tbl_Staff_FkStaffId",
                        column: x => x.FkStaffId,
                        principalTable: "Tbl_Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_StaffSessionReady_FkStaffId",
                table: "Tbl_StaffSessionReady",
                column: "FkStaffId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tbl_StaffSessionReady");
        }
    }
}
