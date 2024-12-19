using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSC.Data.Migrations
{
    public partial class IDFr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CalenderDateID",
                table: "Salaries",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalenderDateID",
                table: "Salaries");
        }
    }
}
