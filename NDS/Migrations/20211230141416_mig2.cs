using Microsoft.EntityFrameworkCore.Migrations;

namespace NDS.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Tbl_Staff",
                newName: "Mobile");

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Tbl_Staff",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Tbl_Staff");

            migrationBuilder.RenameColumn(
                name: "Mobile",
                table: "Tbl_Staff",
                newName: "Phone");
        }
    }
}
