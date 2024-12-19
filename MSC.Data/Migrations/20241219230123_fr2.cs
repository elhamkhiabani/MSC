using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSC.Data.Migrations
{
    public partial class fr2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Salaries_CalenderDateID",
                table: "Salaries",
                column: "CalenderDateID");

            migrationBuilder.AddForeignKey(
                name: "FK_Salaries_CalenderDates_CalenderDateID",
                table: "Salaries",
                column: "CalenderDateID",
                principalTable: "CalenderDates",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salaries_CalenderDates_CalenderDateID",
                table: "Salaries");

            migrationBuilder.DropIndex(
                name: "IX_Salaries_CalenderDateID",
                table: "Salaries");
        }
    }
}
