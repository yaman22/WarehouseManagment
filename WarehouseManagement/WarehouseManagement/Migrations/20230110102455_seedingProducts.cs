using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WarehouseManagement.Migrations
{
    /// <inheritdoc />
    public partial class seedingProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("81b13ee0-2ca3-4f2a-b9d5-70f06a4f29cb"), "Indomi", 1500.0 },
                    { new Guid("87bb64c6-1eb0-4e26-b245-afc6359147d5"), "Sugar", 5000.0 },
                    { new Guid("9a66da83-ec29-4f75-8b8d-a36d543c17b5"), "Coffe", 55000.0 },
                    { new Guid("9e9c1db5-afe3-4948-915f-34806db86462"), "Salt", 1000.0 },
                    { new Guid("d558dc08-c5a8-4dcc-ad35-00c170bb0330"), "Tee", 60000.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("81b13ee0-2ca3-4f2a-b9d5-70f06a4f29cb"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("87bb64c6-1eb0-4e26-b245-afc6359147d5"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9a66da83-ec29-4f75-8b8d-a36d543c17b5"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9e9c1db5-afe3-4948-915f-34806db86462"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d558dc08-c5a8-4dcc-ad35-00c170bb0330"));
        }
    }
}
