using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WarehouseManagement.Migrations
{
    /// <inheritdoc />
    public partial class seedingWarehouses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Warehouses",
                columns: new[] { "Id", "Address", "MainWarehouseId", "ManagerId", "Name" },
                values: new object[,]
                {
                    { new Guid("1330cde5-971d-4727-989a-efff06576c86"), "Alzahraa", null, new Guid("702b0a47-e7cd-42ca-bed6-e08b66abb653"), "Alzahraa" },
                    { new Guid("5af29802-9a62-42e9-bb5e-b3459cb81f40"), "Hamdania", null, new Guid("90515313-fb47-4ab4-bec1-9303c7661e48"), "Hamdania" },
                    { new Guid("734f2c3c-0a2a-4860-a6c6-27c013b6aa21"), "Mohafaza", null, new Guid("927ef47d-2231-4218-a8a8-f570b12c7fde"), "Mohafaza" },
                    { new Guid("86bb2b95-d023-4390-8dc1-2d27da8a8a15"), "Mogambo", null, new Guid("d0f1d718-05c8-48d6-a4d0-62a06f2d0d29"), "Mogambo" },
                    { new Guid("c22d0d3e-4e42-4a3b-a930-f628c14b874d"), "Andalos", null, new Guid("a90e41b6-be95-4382-97cc-4f6970b1978c"), "Andalos" },
                    { new Guid("11ec5b78-7e2b-41c2-ae9f-5634a1105887"), "Andalos", new Guid("c22d0d3e-4e42-4a3b-a930-f628c14b874d"), new Guid("a90e41b6-be95-4382-97cc-4f6970b1978c"), "Andalos.1" },
                    { new Guid("3daf5dd6-dbb4-44d8-88ac-15c65a6741d4"), "Mohafaza", new Guid("734f2c3c-0a2a-4860-a6c6-27c013b6aa21"), new Guid("927ef47d-2231-4218-a8a8-f570b12c7fde"), "Mohafaza.1" },
                    { new Guid("4dd38fd3-8fcc-4de5-b56c-169785e1586f"), "Mogambo", new Guid("86bb2b95-d023-4390-8dc1-2d27da8a8a15"), new Guid("d0f1d718-05c8-48d6-a4d0-62a06f2d0d29"), "Mogambo.1" },
                    { new Guid("53965e59-5ad4-4d1b-bf98-b743fb1d8681"), "Andalos", new Guid("c22d0d3e-4e42-4a3b-a930-f628c14b874d"), new Guid("a90e41b6-be95-4382-97cc-4f6970b1978c"), "Andalos.3" },
                    { new Guid("879eb9b1-1bfb-4fd5-96e2-9a4bbc500c29"), "Alzahraa", new Guid("1330cde5-971d-4727-989a-efff06576c86"), new Guid("702b0a47-e7cd-42ca-bed6-e08b66abb653"), "Alzahraa.2" },
                    { new Guid("e24e4c78-aa68-4f6e-9749-56da8bb6f67a"), "Hamdania", new Guid("5af29802-9a62-42e9-bb5e-b3459cb81f40"), new Guid("90515313-fb47-4ab4-bec1-9303c7661e48"), "Hamdania.1" },
                    { new Guid("f27a7dd1-b44a-423f-9a84-2462a0f2ad48"), "Andalos", new Guid("c22d0d3e-4e42-4a3b-a930-f628c14b874d"), new Guid("a90e41b6-be95-4382-97cc-4f6970b1978c"), "Andalos.2" },
                    { new Guid("fcc58645-f4cd-4700-a667-fec39323fded"), "Alzahraa", new Guid("1330cde5-971d-4727-989a-efff06576c86"), new Guid("702b0a47-e7cd-42ca-bed6-e08b66abb653"), "Alzahraa.1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Warehouses",
                keyColumn: "Id",
                keyValue: new Guid("11ec5b78-7e2b-41c2-ae9f-5634a1105887"));

            migrationBuilder.DeleteData(
                table: "Warehouses",
                keyColumn: "Id",
                keyValue: new Guid("3daf5dd6-dbb4-44d8-88ac-15c65a6741d4"));

            migrationBuilder.DeleteData(
                table: "Warehouses",
                keyColumn: "Id",
                keyValue: new Guid("4dd38fd3-8fcc-4de5-b56c-169785e1586f"));

            migrationBuilder.DeleteData(
                table: "Warehouses",
                keyColumn: "Id",
                keyValue: new Guid("53965e59-5ad4-4d1b-bf98-b743fb1d8681"));

            migrationBuilder.DeleteData(
                table: "Warehouses",
                keyColumn: "Id",
                keyValue: new Guid("879eb9b1-1bfb-4fd5-96e2-9a4bbc500c29"));

            migrationBuilder.DeleteData(
                table: "Warehouses",
                keyColumn: "Id",
                keyValue: new Guid("e24e4c78-aa68-4f6e-9749-56da8bb6f67a"));

            migrationBuilder.DeleteData(
                table: "Warehouses",
                keyColumn: "Id",
                keyValue: new Guid("f27a7dd1-b44a-423f-9a84-2462a0f2ad48"));

            migrationBuilder.DeleteData(
                table: "Warehouses",
                keyColumn: "Id",
                keyValue: new Guid("fcc58645-f4cd-4700-a667-fec39323fded"));

            migrationBuilder.DeleteData(
                table: "Warehouses",
                keyColumn: "Id",
                keyValue: new Guid("1330cde5-971d-4727-989a-efff06576c86"));

            migrationBuilder.DeleteData(
                table: "Warehouses",
                keyColumn: "Id",
                keyValue: new Guid("5af29802-9a62-42e9-bb5e-b3459cb81f40"));

            migrationBuilder.DeleteData(
                table: "Warehouses",
                keyColumn: "Id",
                keyValue: new Guid("734f2c3c-0a2a-4860-a6c6-27c013b6aa21"));

            migrationBuilder.DeleteData(
                table: "Warehouses",
                keyColumn: "Id",
                keyValue: new Guid("86bb2b95-d023-4390-8dc1-2d27da8a8a15"));

            migrationBuilder.DeleteData(
                table: "Warehouses",
                keyColumn: "Id",
                keyValue: new Guid("c22d0d3e-4e42-4a3b-a930-f628c14b874d"));
        }
    }
}
