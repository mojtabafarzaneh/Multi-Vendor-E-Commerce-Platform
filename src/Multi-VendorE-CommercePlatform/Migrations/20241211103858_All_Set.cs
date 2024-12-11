using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Multi_VendorE_CommercePlatform.Migrations
{
    /// <inheritdoc />
    public partial class All_Set : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("80974aef-1e60-4d29-be6d-4d2c68edabf9"), null, "Admin", "ADMIN" },
                    { new Guid("c95da3f1-8246-44b9-8f6e-c9e23de342fc"), null, "Customer", "CUSTOMER" },
                    { new Guid("fcfd6e15-777e-4de3-8f57-24a918875264"), null, "Vendor", "VENDOR" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("8c79dbc8-e040-42e4-86c3-ffc60b41fc61"), null, "Customer", "CUSTOMER" },
                    { new Guid("d2e90e3d-0b23-407a-90ad-553a6c0de0ff"), null, "Vendor", "VENDOR" },
                    { new Guid("d4f75b67-2b40-4849-881b-0af363e50c37"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
