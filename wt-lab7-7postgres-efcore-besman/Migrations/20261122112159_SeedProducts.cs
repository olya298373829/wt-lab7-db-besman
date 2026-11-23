using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace wt_lab6_6minimalapi_besman.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BankAccounts",
                columns: new[] { "Id", "AccountNumber", "Balance", "CreatedAt", "IsBlocked", "OwnerName" },
                values: new object[,]
                {
                    { 1, "1234567890", 2500m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, "Иван Иванов" },
                    { 2, "0987654321", 80m, new DateTime(2026, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), true, "Петр Петров" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BankAccounts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BankAccounts",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
