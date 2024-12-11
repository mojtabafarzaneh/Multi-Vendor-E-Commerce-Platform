using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Multi_VendorE_CommercePlatform.Migrations
{
    /// <inheritdoc />
    public partial class Done_With_Categories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3f2a6ec7-e264-4a9f-9d2e-32660721f693"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4fcaed7a-425f-4b28-bb25-bb98cd27c58c"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("eab5d07d-5b0a-4fbf-811b-7bdd83bdae3f"));

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Categories");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("8c79dbc8-e040-42e4-86c3-ffc60b41fc61"), null, "Customer", "CUSTOMER" },
                    { new Guid("d2e90e3d-0b23-407a-90ad-553a6c0de0ff"), null, "Vendor", "VENDOR" },
                    { new Guid("d4f75b67-2b40-4849-881b-0af363e50c37"), null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8c79dbc8-e040-42e4-86c3-ffc60b41fc61"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d2e90e3d-0b23-407a-90ad-553a6c0de0ff"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d4f75b67-2b40-4849-881b-0af363e50c37"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Categories",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Categories",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("3f2a6ec7-e264-4a9f-9d2e-32660721f693"), null, "Vendor", "VENDOR" },
                    { new Guid("4fcaed7a-425f-4b28-bb25-bb98cd27c58c"), null, "Admin", "ADMIN" },
                    { new Guid("eab5d07d-5b0a-4fbf-811b-7bdd83bdae3f"), null, "Customer", "CUSTOMER" }
                });
        }
    }
}
