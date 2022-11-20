using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Idyfa.EntityFrameworkCore.SQLite.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Idyfa.Permission",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    SystemName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Category = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Idyfa.Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Idyfa.Role",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    AltTitle = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 1),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastStatusChanged = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Idyfa.Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Idyfa.UserCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: true),
                    ParentId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Idyfa.UserCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Idyfa.UserClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimType = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    ClaimValue = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Idyfa.UserClaim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Idyfa.RoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimType = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    ClaimValue = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Idyfa.RoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Idyfa.RoleClaim_Idyfa.Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Idyfa.Role",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Idyfa.RolePermission",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    PermissionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Idyfa.RolePermission", x => new { x.PermissionId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_Idyfa.RolePermission_Idyfa.Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Idyfa.Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Idyfa.User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NationalCode = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    DisplayName = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    BirthDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastStatusChanged = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastVisitDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ApiKey = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ReferralCode = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    CategoryId = table.Column<Guid>(type: "TEXT", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Idyfa.User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Idyfa.User_Idyfa.UserCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Idyfa.UserCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Idyfa.UserLogin",
                columns: table => new
                {
                    ProviderKey = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Idyfa.UserLogin", x => new { x.UserId, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_Idyfa.UserLogin_Idyfa.User_UserId",
                        column: x => x.UserId,
                        principalTable: "Idyfa.User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Idyfa.UserLoginRecord",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    LoginUrl = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: true),
                    IpAddress = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    HostName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    UserAgent = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    OsName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Country = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    City = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ExtraInfo = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Idyfa.UserLoginRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Idyfa.UserLoginRecord_Idyfa.User_UserId",
                        column: x => x.UserId,
                        principalTable: "Idyfa.User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Idyfa.UserPermission",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    PermissionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Idyfa.UserPermission", x => new { x.UserId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_Idyfa.UserPermission_Idyfa.User_UserId",
                        column: x => x.UserId,
                        principalTable: "Idyfa.User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Idyfa.UserRole",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Idyfa.UserRole", x => new { x.RoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Idyfa.UserRole_Idyfa.Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Idyfa.Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Idyfa.UserRole_Idyfa.User_UserId",
                        column: x => x.UserId,
                        principalTable: "Idyfa.User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Idyfa.UserToken",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Value = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Idyfa.UserToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Idyfa.UserToken_Idyfa.User_UserId",
                        column: x => x.UserId,
                        principalTable: "Idyfa.User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Idyfa.UserUsedPassword",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    HashedPassword = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Idyfa.UserUsedPassword", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Idyfa.UserUsedPassword_Idyfa.User_UserId",
                        column: x => x.UserId,
                        principalTable: "Idyfa.User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Idyfa.RoleClaim_RoleId",
                table: "Idyfa.RoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Idyfa.RolePermission_RoleId",
                table: "Idyfa.RolePermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Idyfa.User_CategoryId",
                table: "Idyfa.User",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Idyfa.UserLoginRecord_UserId",
                table: "Idyfa.UserLoginRecord",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Idyfa.UserRole_UserId",
                table: "Idyfa.UserRole",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Idyfa.UserToken_UserId",
                table: "Idyfa.UserToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Idyfa.UserUsedPassword_UserId",
                table: "Idyfa.UserUsedPassword",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Idyfa.Permission");

            migrationBuilder.DropTable(
                name: "Idyfa.RoleClaim");

            migrationBuilder.DropTable(
                name: "Idyfa.RolePermission");

            migrationBuilder.DropTable(
                name: "Idyfa.UserClaim");

            migrationBuilder.DropTable(
                name: "Idyfa.UserLogin");

            migrationBuilder.DropTable(
                name: "Idyfa.UserLoginRecord");

            migrationBuilder.DropTable(
                name: "Idyfa.UserPermission");

            migrationBuilder.DropTable(
                name: "Idyfa.UserRole");

            migrationBuilder.DropTable(
                name: "Idyfa.UserToken");

            migrationBuilder.DropTable(
                name: "Idyfa.UserUsedPassword");

            migrationBuilder.DropTable(
                name: "Idyfa.Role");

            migrationBuilder.DropTable(
                name: "Idyfa.User");

            migrationBuilder.DropTable(
                name: "Idyfa.UserCategory");
        }
    }
}
