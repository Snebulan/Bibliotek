using Microsoft.EntityFrameworkCore.Migrations;

namespace Bibliotek.Migrations
{
    public partial class sixth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoanID",
                table: "Members");

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "ID", "FirstName", "LastName" },
                values: new object[] { 1, "Fredrik", "Gustafsson" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "LoanID",
                table: "Members",
                nullable: false,
                defaultValue: 0);
        }
    }
}
