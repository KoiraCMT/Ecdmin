using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecdmin.Database.Migrations.Migrations
{
    public partial class adddisplayNametoPermissionGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "permission_group",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "administrator",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "Password" },
                values: new object[] { new DateTimeOffset(new DateTime(2021, 2, 5, 10, 3, 49, 153, DateTimeKind.Unspecified).AddTicks(3070), new TimeSpan(0, 8, 0, 0, 0)), "AQAAAAEAACcQAAAAELVd73DIDUHSsJ2nIe6KN/OtHCkTeU72hTxixsainCkt5hnUMfgTStyC6AhlH3+nmA==" });

            migrationBuilder.UpdateData(
                table: "permission_group",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DisplayName", "Name" },
                values: new object[] { "用户管理", "administrator" });

            migrationBuilder.UpdateData(
                table: "permission_group",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DisplayName", "Name" },
                values: new object[] { "权限管理", "permission" });

            migrationBuilder.UpdateData(
                table: "permission_group",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DisplayName", "Name" },
                values: new object[] { "角色管理", "role" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "permission_group");

            migrationBuilder.UpdateData(
                table: "administrator",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "Password" },
                values: new object[] { new DateTimeOffset(new DateTime(2021, 2, 3, 13, 50, 55, 776, DateTimeKind.Unspecified).AddTicks(7600), new TimeSpan(0, 8, 0, 0, 0)), "AQAAAAEAACcQAAAAEJR9eS0Xh/0/kfXScR0qrO/xUTjWo5Qk+VdIN/CBMNpSJsKzTEFNbMVUcJIPCKldwA==" });

            migrationBuilder.UpdateData(
                table: "permission_group",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "用户管理");

            migrationBuilder.UpdateData(
                table: "permission_group",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "权限管理");

            migrationBuilder.UpdateData(
                table: "permission_group",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "角色管理");
        }
    }
}
