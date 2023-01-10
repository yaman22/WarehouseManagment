using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WarehouseManagement.Migrations
{
    /// <inheritdoc />
    public partial class seedingProductWarehouse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products_Warehouses",
                columns: new[] { "Id", "Amount", "ProductId", "WarehouseId" },
                values: new object[,]
                {
                    { new Guid("18c2c451-0e25-4ed4-b68d-ab95eb3e8820"), 5.0, new Guid("d558dc08-c5a8-4dcc-ad35-00c170bb0330"), new Guid("1330cde5-971d-4727-989a-efff06576c86") },
                    { new Guid("31280ecc-509a-40ce-8eee-e322f9d98238"), 100.0, new Guid("81b13ee0-2ca3-4f2a-b9d5-70f06a4f29cb"), new Guid("1330cde5-971d-4727-989a-efff06576c86") },
                    { new Guid("386163bf-fe58-4812-9fb7-46a7673febed"), 100.0, new Guid("87bb64c6-1eb0-4e26-b245-afc6359147d5"), new Guid("1330cde5-971d-4727-989a-efff06576c86") },
                    { new Guid("c88e1397-289a-4dd2-8500-cd2b6b283d2b"), 50.0, new Guid("9e9c1db5-afe3-4948-915f-34806db86462"), new Guid("1330cde5-971d-4727-989a-efff06576c86") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products_Warehouses",
                keyColumn: "Id",
                keyValue: new Guid("18c2c451-0e25-4ed4-b68d-ab95eb3e8820"));

            migrationBuilder.DeleteData(
                table: "Products_Warehouses",
                keyColumn: "Id",
                keyValue: new Guid("31280ecc-509a-40ce-8eee-e322f9d98238"));

            migrationBuilder.DeleteData(
                table: "Products_Warehouses",
                keyColumn: "Id",
                keyValue: new Guid("386163bf-fe58-4812-9fb7-46a7673febed"));

            migrationBuilder.DeleteData(
                table: "Products_Warehouses",
                keyColumn: "Id",
                keyValue: new Guid("c88e1397-289a-4dd2-8500-cd2b6b283d2b"));
        }
    }
}
