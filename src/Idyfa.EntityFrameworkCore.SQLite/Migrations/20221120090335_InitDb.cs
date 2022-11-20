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
                name: "IdyfaPermission",
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
                    table.PrimaryKey("PK_IdyfaPermission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdyfaRole",
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
                    table.PrimaryKey("PK_IdyfaRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdyfaUserCategory",
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
                    table.PrimaryKey("PK_IdyfaUserCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdyfaUserClaim",
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
                    table.PrimaryKey("PK_IdyfaUserClaim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdyfaRolePermission",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    PermissionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdyfaRolePermission", x => new { x.PermissionId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_IdyfaRolePermission_IdyfaRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "IdyfaRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
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
                    table.PrimaryKey("PK_RoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaim_IdyfaRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "IdyfaRole",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IdyfaUser",
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
                    table.PrimaryKey("PK_IdyfaUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdyfaUser_IdyfaUserCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "IdyfaUserCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "IdyfaUserLogin",
                columns: table => new
                {
                    ProviderKey = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdyfaUserLogin", x => new { x.UserId, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_IdyfaUserLogin_IdyfaUser_UserId",
                        column: x => x.UserId,
                        principalTable: "IdyfaUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdyfaUserLoginRecord",
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
                    table.PrimaryKey("PK_IdyfaUserLoginRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdyfaUserLoginRecord_IdyfaUser_UserId",
                        column: x => x.UserId,
                        principalTable: "IdyfaUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdyfaUserPermission",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    PermissionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdyfaUserPermission", x => new { x.UserId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_IdyfaUserPermission_IdyfaUser_UserId",
                        column: x => x.UserId,
                        principalTable: "IdyfaUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdyfaUserRole",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdyfaUserRole", x => new { x.RoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_IdyfaUserRole_IdyfaRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "IdyfaRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IdyfaUserRole_IdyfaUser_UserId",
                        column: x => x.UserId,
                        principalTable: "IdyfaUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdyfaUserToken",
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
                    table.PrimaryKey("PK_IdyfaUserToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdyfaUserToken_IdyfaUser_UserId",
                        column: x => x.UserId,
                        principalTable: "IdyfaUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdyfaUserUsedPassword",
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
                    table.PrimaryKey("PK_IdyfaUserUsedPassword", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdyfaUserUsedPassword_IdyfaUser_UserId",
                        column: x => x.UserId,
                        principalTable: "IdyfaUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IdyfaRolePermission_RoleId",
                table: "IdyfaRolePermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_IdyfaUser_CategoryId",
                table: "IdyfaUser",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_IdyfaUserLoginRecord_UserId",
                table: "IdyfaUserLoginRecord",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IdyfaUserRole_UserId",
                table: "IdyfaUserRole",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IdyfaUserToken_UserId",
                table: "IdyfaUserToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IdyfaUserUsedPassword_UserId",
                table: "IdyfaUserUsedPassword",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_RoleId",
                table: "RoleClaim",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdyfaPermission");

            migrationBuilder.DropTable(
                name: "IdyfaRolePermission");

            migrationBuilder.DropTable(
                name: "IdyfaUserClaim");

            migrationBuilder.DropTable(
                name: "IdyfaUserLogin");

            migrationBuilder.DropTable(
                name: "IdyfaUserLoginRecord");

            migrationBuilder.DropTable(
                name: "IdyfaUserPermission");

            migrationBuilder.DropTable(
                name: "IdyfaUserRole");

            migrationBuilder.DropTable(
                name: "IdyfaUserToken");

            migrationBuilder.DropTable(
                name: "IdyfaUserUsedPassword");

            migrationBuilder.DropTable(
                name: "RoleClaim");

            migrationBuilder.DropTable(
                name: "IdyfaUser");

            migrationBuilder.DropTable(
                name: "IdyfaRole");

            migrationBuilder.DropTable(
                name: "IdyfaUserCategory");
        }
    }
}
