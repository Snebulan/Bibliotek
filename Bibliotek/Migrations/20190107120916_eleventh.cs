using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bibliotek.Migrations
{
    public partial class eleventh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Members_MemberID",
                table: "Loans");

            migrationBuilder.DeleteData(
                table: "Loans",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "MemberID",
                table: "Loans",
                nullable: true,
                oldClrType: typeof(int));

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
                name: "FK_Loans_Books_BookID",
                table: "Loans",
                column: "BookID",
                principalTable: "Books",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Members_MemberID",
                table: "Loans",
                column: "MemberID",
                principalTable: "Members",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Books_BookID",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Members_MemberID",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_BookID",
                table: "Loans");

            migrationBuilder.AlterColumn<int>(
                name: "MemberID",
                table: "Loans",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BookID",
                table: "Loans",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Loans",
                columns: new[] { "ID", "BookID", "DateLoan", "DateReturn", "MemberID" },
                values: new object[] { 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Members_MemberID",
                table: "Loans",
                column: "MemberID",
                principalTable: "Members",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
