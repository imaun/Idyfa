using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Idyfa.EntityFrameworkCore.SqlServer.Migrations
{
    public partial class UserTwoFactiorProvider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TwoFactorProvider",
                table: "Idyfa.User",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TwoFactorProvider",
                table: "Idyfa.User");
        }
    }
}
