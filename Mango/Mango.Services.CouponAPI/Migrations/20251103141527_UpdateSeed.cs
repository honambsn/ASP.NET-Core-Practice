using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mango.Services.CouponAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponID", "CouponCode", "DiscountAmount", "LastUpdated", "MinAmount" },
                values: new object[,]
                {
                    { 1, "Test Code", 100.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100000 },
                    { 2, "WELCOME50", 50.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 500 },
                    { 3, "SUMMER2025", 75.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 750 },
                    { 4, "FREESHIP", 25.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 300 },
                    { 5, "VIP100", 100.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1000 },
                    { 6, "BLACKFRIDAY", 150.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1500 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponID",
                keyValue: 6);
        }
    }
}
