using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WarehouseManagement.Migrations
{
    /// <inheritdoc />
    public partial class seedingProductBills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BillDetails",
                keyColumn: "Id",
                keyValue: new Guid("67ac8156-7c8a-44f3-aad8-8140a0f95e01"),
                column: "Date",
                value: new DateTime(2023, 1, 10, 13, 29, 54, 278, DateTimeKind.Local).AddTicks(6727));

            migrationBuilder.UpdateData(
                table: "BillDetails",
                keyColumn: "Id",
                keyValue: new Guid("c9dab83a-8739-4365-935d-20b6ad033ea0"),
                column: "Date",
                value: new DateTime(2023, 1, 10, 13, 29, 54, 278, DateTimeKind.Local).AddTicks(6744));

            migrationBuilder.InsertData(
                table: "Products_Bills",
                columns: new[] { "BillDetailsId", "Product_Warehouse_Id", "Amount", "Cost", "Date", "Id", "Type" },
                values: new object[,]
                {
                    { new Guid("67ac8156-7c8a-44f3-aad8-8140a0f95e01"), new Guid("18c2c451-0e25-4ed4-b68d-ab95eb3e8820"), 10.0, 600000.0, new DateTime(2023, 1, 10, 13, 29, 54, 278, DateTimeKind.Local).AddTicks(6826), new Guid("ad3711b7-67aa-47b0-b73d-f5ac22a91e3a"), 1 },
                    { new Guid("c9dab83a-8739-4365-935d-20b6ad033ea0"), new Guid("18c2c451-0e25-4ed4-b68d-ab95eb3e8820"), 5.0, 300000.0, new DateTime(2023, 1, 10, 13, 29, 54, 278, DateTimeKind.Local).AddTicks(6841), new Guid("eebb2de5-1b4b-4026-9028-fc22dc718653"), 0 },
                    { new Guid("67ac8156-7c8a-44f3-aad8-8140a0f95e01"), new Guid("31280ecc-509a-40ce-8eee-e322f9d98238"), 200.0, 300000.0, new DateTime(2023, 1, 10, 13, 29, 54, 278, DateTimeKind.Local).AddTicks(6830), new Guid("94157a7c-964a-40d0-9bcd-37a608e08c07"), 1 },
                    { new Guid("c9dab83a-8739-4365-935d-20b6ad033ea0"), new Guid("31280ecc-509a-40ce-8eee-e322f9d98238"), 100.0, 150000.0, new DateTime(2023, 1, 10, 13, 29, 54, 278, DateTimeKind.Local).AddTicks(6847), new Guid("d4c64d8e-4ce3-4368-9685-c23190c71fe8"), 0 },
                    { new Guid("67ac8156-7c8a-44f3-aad8-8140a0f95e01"), new Guid("386163bf-fe58-4812-9fb7-46a7673febed"), 200.0, 1000000.0, new DateTime(2023, 1, 10, 13, 29, 54, 278, DateTimeKind.Local).AddTicks(6822), new Guid("834554e1-aea5-4ab1-bbc7-d79a8dd90527"), 1 },
                    { new Guid("c9dab83a-8739-4365-935d-20b6ad033ea0"), new Guid("386163bf-fe58-4812-9fb7-46a7673febed"), 100.0, 500000.0, new DateTime(2023, 1, 10, 13, 29, 54, 278, DateTimeKind.Local).AddTicks(6838), new Guid("3fb50473-7c2e-46ff-b6b1-75a2b31e7ff6"), 0 },
                    { new Guid("67ac8156-7c8a-44f3-aad8-8140a0f95e01"), new Guid("c88e1397-289a-4dd2-8500-cd2b6b283d2b"), 100.0, 100000.0, new DateTime(2023, 1, 10, 13, 29, 54, 278, DateTimeKind.Local).AddTicks(6834), new Guid("0a06c1f7-7b44-4e5b-b3df-9a0e24a705ec"), 1 },
                    { new Guid("c9dab83a-8739-4365-935d-20b6ad033ea0"), new Guid("c88e1397-289a-4dd2-8500-cd2b6b283d2b"), 50.0, 50000.0, new DateTime(2023, 1, 10, 13, 29, 54, 278, DateTimeKind.Local).AddTicks(6850), new Guid("eae46677-a94d-406f-a3d8-504357c910eb"), 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products_Bills",
                keyColumns: new[] { "BillDetailsId", "Product_Warehouse_Id" },
                keyValues: new object[] { new Guid("67ac8156-7c8a-44f3-aad8-8140a0f95e01"), new Guid("18c2c451-0e25-4ed4-b68d-ab95eb3e8820") });

            migrationBuilder.DeleteData(
                table: "Products_Bills",
                keyColumns: new[] { "BillDetailsId", "Product_Warehouse_Id" },
                keyValues: new object[] { new Guid("c9dab83a-8739-4365-935d-20b6ad033ea0"), new Guid("18c2c451-0e25-4ed4-b68d-ab95eb3e8820") });

            migrationBuilder.DeleteData(
                table: "Products_Bills",
                keyColumns: new[] { "BillDetailsId", "Product_Warehouse_Id" },
                keyValues: new object[] { new Guid("67ac8156-7c8a-44f3-aad8-8140a0f95e01"), new Guid("31280ecc-509a-40ce-8eee-e322f9d98238") });

            migrationBuilder.DeleteData(
                table: "Products_Bills",
                keyColumns: new[] { "BillDetailsId", "Product_Warehouse_Id" },
                keyValues: new object[] { new Guid("c9dab83a-8739-4365-935d-20b6ad033ea0"), new Guid("31280ecc-509a-40ce-8eee-e322f9d98238") });

            migrationBuilder.DeleteData(
                table: "Products_Bills",
                keyColumns: new[] { "BillDetailsId", "Product_Warehouse_Id" },
                keyValues: new object[] { new Guid("67ac8156-7c8a-44f3-aad8-8140a0f95e01"), new Guid("386163bf-fe58-4812-9fb7-46a7673febed") });

            migrationBuilder.DeleteData(
                table: "Products_Bills",
                keyColumns: new[] { "BillDetailsId", "Product_Warehouse_Id" },
                keyValues: new object[] { new Guid("c9dab83a-8739-4365-935d-20b6ad033ea0"), new Guid("386163bf-fe58-4812-9fb7-46a7673febed") });

            migrationBuilder.DeleteData(
                table: "Products_Bills",
                keyColumns: new[] { "BillDetailsId", "Product_Warehouse_Id" },
                keyValues: new object[] { new Guid("67ac8156-7c8a-44f3-aad8-8140a0f95e01"), new Guid("c88e1397-289a-4dd2-8500-cd2b6b283d2b") });

            migrationBuilder.DeleteData(
                table: "Products_Bills",
                keyColumns: new[] { "BillDetailsId", "Product_Warehouse_Id" },
                keyValues: new object[] { new Guid("c9dab83a-8739-4365-935d-20b6ad033ea0"), new Guid("c88e1397-289a-4dd2-8500-cd2b6b283d2b") });

            migrationBuilder.UpdateData(
                table: "BillDetails",
                keyColumn: "Id",
                keyValue: new Guid("67ac8156-7c8a-44f3-aad8-8140a0f95e01"),
                column: "Date",
                value: new DateTime(2023, 1, 10, 13, 29, 22, 326, DateTimeKind.Local).AddTicks(894));

            migrationBuilder.UpdateData(
                table: "BillDetails",
                keyColumn: "Id",
                keyValue: new Guid("c9dab83a-8739-4365-935d-20b6ad033ea0"),
                column: "Date",
                value: new DateTime(2023, 1, 10, 13, 29, 22, 326, DateTimeKind.Local).AddTicks(912));
        }
    }
}
