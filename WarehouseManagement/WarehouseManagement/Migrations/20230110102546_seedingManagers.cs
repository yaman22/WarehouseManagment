using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WarehouseManagement.Migrations
{
    /// <inheritdoc />
    public partial class seedingManagers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("702b0a47-e7cd-42ca-bed6-e08b66abb653"), "Ahmad" },
                    { new Guid("90515313-fb47-4ab4-bec1-9303c7661e48"), "Abdo" },
                    { new Guid("927ef47d-2231-4218-a8a8-f570b12c7fde"), "Ali" },
                    { new Guid("a90e41b6-be95-4382-97cc-4f6970b1978c"), "Mohannad" },
                    { new Guid("d0f1d718-05c8-48d6-a4d0-62a06f2d0d29"), "Yamen" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: new Guid("702b0a47-e7cd-42ca-bed6-e08b66abb653"));

            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: new Guid("90515313-fb47-4ab4-bec1-9303c7661e48"));

            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: new Guid("927ef47d-2231-4218-a8a8-f570b12c7fde"));

            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: new Guid("a90e41b6-be95-4382-97cc-4f6970b1978c"));

            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: new Guid("d0f1d718-05c8-48d6-a4d0-62a06f2d0d29"));
        }
    }
}
