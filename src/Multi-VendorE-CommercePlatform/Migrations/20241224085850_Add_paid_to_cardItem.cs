using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Multi_VendorE_CommercePlatform.Migrations
{
    /// <inheritdoc />
    public partial class Add_paid_to_cardItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("80974aef-1e60-4d29-be6d-4d2c68edabf9"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c95da3f1-8246-44b9-8f6e-c9e23de342fc"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fcfd6e15-777e-4de3-8f57-24a918875264"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("80974aef-1e60-4d29-be6d-4d2c68edabf9"), null, "Admin", "ADMIN" },
                    { new Guid("c95da3f1-8246-44b9-8f6e-c9e23de342fc"), null, "Customer", "CUSTOMER" },
                    { new Guid("fcfd6e15-777e-4de3-8f57-24a918875264"), null, "Vendor", "VENDOR" }
                });
        }
    }
}
