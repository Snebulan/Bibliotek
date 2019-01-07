using Microsoft.EntityFrameworkCore.Migrations;

namespace Bibliotek.Migrations
{
    public partial class somenumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_BookCopies_BookID",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_BookID",
                table: "Loans");

            migrationBuilder.AlterColumn<int>(
                name: "BookID",
                table: "Loans",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Loans",
                columns: new[] { "ID", "BookID", "MemberID" },
                values: new object[] { 1, 1, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Loans",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "BookID",
                table: "Loans",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Loans_BookID",
                table: "Loans",
                column: "BookID");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_BookCopies_BookID",
                table: "Loans",
                column: "BookID",
                principalTable: "BookCopies",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
