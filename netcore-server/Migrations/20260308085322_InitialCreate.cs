using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace netcore_server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 3, 8, 15, 53, 22, 83, DateTimeKind.Utc).AddTicks(6884), "$2a$11$pSVIMb0o5VnCEvt.0pmLzOrtiF9asXPeJnRKPIb/vlP55kZpgihCq" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 3, 8, 15, 52, 0, 159, DateTimeKind.Utc).AddTicks(5925), "$2a$11$BXoNm2Tc5ptJTfuWUc70ou9937Te77cXwH/S0zCJ/FV5kLXQapzua" });
        }
    }
}
