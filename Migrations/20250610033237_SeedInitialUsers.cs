using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace THEAI_BE.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 10, 3, 32, 37, 326, DateTimeKind.Utc).AddTicks(8634), "alice@example.com", "Alice", "Anderson" },
                    { 2, new DateTime(2025, 6, 10, 3, 32, 37, 326, DateTimeKind.Utc).AddTicks(8770), "bob@example.com", "Bob", "Brown" },
                    { 3, new DateTime(2025, 6, 10, 3, 32, 37, 326, DateTimeKind.Utc).AddTicks(8772), "charlie@example.com", "Charlie", "Clark" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
