using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace netcore_server.Migrations
{
    /// <inheritdoc />
    public partial class AddUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "Role", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 8, 9, 25, 9, 814, DateTimeKind.Utc).AddTicks(5048), "$2a$11$iyH8/cGggdn5lL5Tb51Oge7NgfKpQ1xhDDniYJJ84X1T1cZNw9Nfu", "USER", new DateTime(2026, 3, 8, 9, 25, 9, 814, DateTimeKind.Utc).AddTicks(5050) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 8, 15, 53, 22, 83, DateTimeKind.Local).AddTicks(6884), "$2a$11$pSVIMb0o5VnCEvt.0pmLzOrtiF9asXPeJnRKPIb/vlP55kZpgihCq", null });
        }
    }
}
