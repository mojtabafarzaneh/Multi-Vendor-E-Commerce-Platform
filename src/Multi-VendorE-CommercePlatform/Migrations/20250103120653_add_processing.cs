using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Multi_VendorE_CommercePlatform.Migrations
{
    /// <inheritdoc />
    public partial class add_processing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15f7a8b1-70da-4c3c-8531-359c7256ab67"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7f71c6c6-05a5-4141-aa66-2e8cd4cd7270"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9ba560c1-500c-4f2c-9890-36ac2971fab5"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("3c8e4e3f-bae2-4371-b527-974d99ca0117"), null, "Customer", "CUSTOMER" },
                    { new Guid("664ab43e-6790-4ddc-9093-7dc4ba170969"), null, "Vendor", "VENDOR" },
                    { new Guid("bec5d129-7eea-40ab-a31d-6a4910c588da"), null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3c8e4e3f-bae2-4371-b527-974d99ca0117"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("664ab43e-6790-4ddc-9093-7dc4ba170969"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("bec5d129-7eea-40ab-a31d-6a4910c588da"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("15f7a8b1-70da-4c3c-8531-359c7256ab67"), null, "Vendor", "VENDOR" },
                    { new Guid("7f71c6c6-05a5-4141-aa66-2e8cd4cd7270"), null, "Admin", "ADMIN" },
                    { new Guid("9ba560c1-500c-4f2c-9890-36ac2971fab5"), null, "Customer", "CUSTOMER" }
                });
        }
    }
}
