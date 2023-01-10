using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WarehouseManagement.Migrations
{
    /// <inheritdoc />
    public partial class seedingBillDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BillDetails",
                columns: new[] { "Id", "Date", "TotalCost", "managerId", "type", "warehouseId" },
                values: new object[,]
                {
                    { new Guid("67ac8156-7c8a-44f3-aad8-8140a0f95e01"), new DateTime(2023, 1, 10, 13, 29, 22, 326, DateTimeKind.Local).AddTicks(894), 2000000.0, new Guid("702b0a47-e7cd-42ca-bed6-e08b66abb653"), 1, new Guid("1330cde5-971d-4727-989a-efff06576c86") },
                    { new Guid("c9dab83a-8739-4365-935d-20b6ad033ea0"), new DateTime(2023, 1, 10, 13, 29, 22, 326, DateTimeKind.Local).AddTicks(912), 1000000.0, new Guid("702b0a47-e7cd-42ca-bed6-e08b66abb653"), 0, new Guid("1330cde5-971d-4727-989a-efff06576c86") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BillDetails",
                keyColumn: "Id",
                keyValue: new Guid("67ac8156-7c8a-44f3-aad8-8140a0f95e01"));

            migrationBuilder.DeleteData(
                table: "BillDetails",
                keyColumn: "Id",
                keyValue: new Guid("c9dab83a-8739-4365-935d-20b6ad033ea0"));
        }
    }
}
