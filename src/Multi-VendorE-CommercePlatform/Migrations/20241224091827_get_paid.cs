using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Multi_VendorE_CommercePlatform.Migrations
{
    /// <inheritdoc />
    public partial class get_paid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3972e673-d2f8-40b5-ba06-b05adeef0b6c"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c4361eb1-38eb-4956-b4ac-0b3c56b81bf0"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("de9d22d1-3a34-4e0e-b24d-020b37c4e270"));

            migrationBuilder.DropColumn(
                name: "DoesPaid",
                table: "CardItems");

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Cards",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Cards");

            migrationBuilder.AddColumn<bool>(
                name: "DoesPaid",
                table: "CardItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("3972e673-d2f8-40b5-ba06-b05adeef0b6c"), null, "Admin", "ADMIN" },
                    { new Guid("c4361eb1-38eb-4956-b4ac-0b3c56b81bf0"), null, "Customer", "CUSTOMER" },
                    { new Guid("de9d22d1-3a34-4e0e-b24d-020b37c4e270"), null, "Vendor", "VENDOR" }
                });
        }
    }
}
