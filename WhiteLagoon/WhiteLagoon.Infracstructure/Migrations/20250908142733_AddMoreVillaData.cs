using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WhiteLagoon.Infracstructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreVillaData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "ID", "Created_Date", "Description", "ImageUrl", "Name", "Occupancy", "Price", "Sqft", "Updated_Date" },
                values: new object[,]
                {
                    { 4, null, "Villa 4 Description", "https://placehold.co/600x404", "Villa 4", 6, 500.0, 900, null },
                    { 5, null, "Villa 5 Description", "https://placehold.co/600x405", "Villa 5", 5, 350.0, 800, null },
                    { 6, null, "Villa 6 Description", "https://placehold.co/600x406", "Villa 6", 8, 600.0, 1000, null },
                    { 7, null, "Villa 7 Description", "https://placehold.co/600x407", "Villa 7", 4, 450.0, 700, null },
                    { 8, null, "Villa 8 Description", "https://placehold.co/600x408", "Villa 8", 6, 550.0, 850, null },
                    { 9, null, "Villa 9 Description", "https://placehold.co/600x409", "Villa 9", 4, 350.0, 600, null },
                    { 10, null, "Villa 10 Description", "https://placehold.co/600x410", "Villa 10", 5, 400.0, 700, null },
                    { 11, null, "Villa 11 Description", "https://placehold.co/600x411", "Villa 11", 7, 650.0, 950, null },
                    { 12, null, "Villa 12 Description", "https://placehold.co/600x412", "Villa 12", 6, 500.0, 800, null },
                    { 13, null, "Villa 13 Description", "https://placehold.co/600x413", "Villa 13", 8, 700.0, 1200, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "ID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "ID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "ID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "ID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "ID",
                keyValue: 13);
        }
    }
}
