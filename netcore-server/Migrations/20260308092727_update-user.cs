using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace netcore_server.Migrations
{
    /// <inheritdoc />
    public partial class updateuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 8, 9, 27, 26, 608, DateTimeKind.Utc).AddTicks(9551), "$2a$11$E3uPNc.F8v1Ir/O0MLjVIuiqTzt.bc2cX5AisAHMFhZpzuWkz2ReS", new DateTime(2026, 3, 8, 9, 27, 26, 608, DateTimeKind.Utc).AddTicks(9553) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 8, 9, 25, 9, 814, DateTimeKind.Utc).AddTicks(5048), "$2a$11$iyH8/cGggdn5lL5Tb51Oge7NgfKpQ1xhDDniYJJ84X1T1cZNw9Nfu", new DateTime(2026, 3, 8, 9, 25, 9, 814, DateTimeKind.Utc).AddTicks(5050) });
        }
    }
}
