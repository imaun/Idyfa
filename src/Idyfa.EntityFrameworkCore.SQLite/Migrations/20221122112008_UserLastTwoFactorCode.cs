using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Idyfa.EntityFrameworkCore.SQLite.Migrations
{
    public partial class UserLastTwoFactorCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastTwoFactorCode",
                table: "Idyfa.User",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastTwoFactorCodeTime",
                table: "Idyfa.User",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastTwoFactorCode",
                table: "Idyfa.User");

            migrationBuilder.DropColumn(
                name: "LastTwoFactorCodeTime",
                table: "Idyfa.User");
        }
    }
}
