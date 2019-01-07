using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bibliotek.Migrations
{
    public partial class ten : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateLoan",
                table: "Loans",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateReturn",
                table: "Loans",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateLoan",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "DateReturn",
                table: "Loans");
        }
    }
}
