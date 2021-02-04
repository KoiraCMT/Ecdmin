using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecdmin.Database.Migrations.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "administrator",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_administrator", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "permission_group",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permission_group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "permission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PermissionGroupId = table.Column<int>(type: "int", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_permission_permission_group_PermissionGroupId",
                        column: x => x.PermissionGroupId,
                        principalTable: "permission_group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "administrator_role",
                columns: table => new
                {
                    AdministratorId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_administrator_role", x => new { x.AdministratorId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_administrator_role_administrator_AdministratorId",
                        column: x => x.AdministratorId,
                        principalTable: "administrator",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_administrator_role_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "role_permission",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_permission", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_role_permission_permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_role_permission_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "administrator",
                columns: new[] { "Id", "Avatar", "CreatedTime", "IsDeleted", "Name", "Password", "UpdatedTime", "Username" },
                values: new object[] { 1, null, new DateTimeOffset(new DateTime(2021, 2, 3, 13, 50, 55, 776, DateTimeKind.Unspecified).AddTicks(7600), new TimeSpan(0, 8, 0, 0, 0)), false, "echo", "AQAAAAEAACcQAAAAEJR9eS0Xh/0/kfXScR0qrO/xUTjWo5Qk+VdIN/CBMNpSJsKzTEFNbMVUcJIPCKldwA==", null, "echo" });

            migrationBuilder.InsertData(
                table: "permission_group",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "用户管理" },
                    { 2, "权限管理" },
                    { 3, "角色管理" }
                });

            migrationBuilder.InsertData(
                table: "permission",
                columns: new[] { "Id", "DisplayName", "Name", "PermissionGroupId" },
                values: new object[,]
                {
                    { 1, "首页", "administrator.index", 1 },
                    { 2, "新增", "administrator.add", 1 },
                    { 3, "修改", "administrator.update", 1 },
                    { 4, "删除", "administrator.delete", 1 },
                    { 5, "首页", "permission.index", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_administrator_Username",
                table: "administrator",
                column: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_administrator_role_RoleId",
                table: "administrator_role",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_permission_PermissionGroupId",
                table: "permission",
                column: "PermissionGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_role_permission_PermissionId",
                table: "role_permission",
                column: "PermissionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "administrator_role");

            migrationBuilder.DropTable(
                name: "role_permission");

            migrationBuilder.DropTable(
                name: "administrator");

            migrationBuilder.DropTable(
                name: "permission");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "permission_group");
        }
    }
}
