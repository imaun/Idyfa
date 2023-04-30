using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Idyfa.EntityFrameworkCore.SQLite.Migrations
{
    public partial class UserTwoFactorProvider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastTwoFactorCode",
                table: "Idyfa.User",
                type: "TEXT",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "TwoFactorProvider",
                table: "Idyfa.User",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TwoFactorProvider",
                table: "Idyfa.User");

            migrationBuilder.AlterColumn<string>(
                name: "LastTwoFactorCode",
                table: "Idyfa.User",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 20,
                oldNullable: true);
        }
    }
}
