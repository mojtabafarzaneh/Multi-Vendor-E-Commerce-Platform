using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Multi_VendorE_CommercePlatform.Migrations
{
    /// <inheritdoc />
    public partial class _categoryConfig_Completed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2bf1360f-afa7-44b9-a0e2-11da14e82a82"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4d7c0489-6f04-4eba-8ee6-6c1628aa2d06"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d883e94-f10b-4b2a-9448-c3ecb9098560"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { new Guid("2bf1360f-afa7-44b9-a0e2-11da14e82a82"), null, "Vendor", "VENDOR" },
                    { new Guid("4d7c0489-6f04-4eba-8ee6-6c1628aa2d06"), null, "Admin", "ADMIN" },
                    { new Guid("8d883e94-f10b-4b2a-9448-c3ecb9098560"), null, "Customer", "CUSTOMER" }
                });
        }
    }
}
