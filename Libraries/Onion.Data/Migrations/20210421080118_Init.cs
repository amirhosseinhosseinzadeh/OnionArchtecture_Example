using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Onion.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastModifiedOnUtc = table.Column<DateTime>(nullable: false),
                    LastModifierId = table.Column<string>(nullable: true),
                    IsUpdated = table.Column<bool>(nullable: false),
                    RoleName = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastModifiedOnUtc = table.Column<DateTime>(nullable: false),
                    LastModifierId = table.Column<string>(nullable: true),
                    IsUpdated = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(nullable: false),
                    EmailAddress = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User_Role_MiddleTable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastModifiedOnUtc = table.Column<DateTime>(nullable: false),
                    LastModifierId = table.Column<string>(nullable: true),
                    IsUpdated = table.Column<bool>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Role_MiddleTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Role_MiddleTable_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Role_MiddleTable_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_Role_MiddleTable_RoleId",
                table: "User_Role_MiddleTable",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Role_MiddleTable_UserId",
                table: "User_Role_MiddleTable",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User_Role_MiddleTable");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
