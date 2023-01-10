using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore.Migrations;
using WarehouseManagement.Auth;

#nullable disable

namespace WarehouseManagement.Migrations
{
    /// <inheritdoc />
    public partial class SeedingRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] {"Id","Name","NormalizedName","ConcurrencyStamp"},
                values:new object[] {Guid.NewGuid().ToString(),UserRoles.User,UserRoles.User.ToUpper(),Guid.NewGuid().ToString()}
                );

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { Guid.NewGuid().ToString(), UserRoles.Manager, UserRoles.Manager.ToUpper(), Guid.NewGuid().ToString() }
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [AspNetRoles]");
        }
    }
}
