using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulkyWeb.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addImageUrlToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://example.com/images/fortune_of_time.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://example.com/images/whispers_of_eternity.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://example.com/images/shadows_of_the_past.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 4,
                column: "ImageUrl",
                value: "https://example.com/images/the_infinite_journey.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 5,
                column: "ImageUrl",
                value: "https://example.com/images/echoes_of_light.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 6,
                column: "ImageUrl",
                value: "https://example.com/images/the_silent_storm.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Products");
        }
    }
}
