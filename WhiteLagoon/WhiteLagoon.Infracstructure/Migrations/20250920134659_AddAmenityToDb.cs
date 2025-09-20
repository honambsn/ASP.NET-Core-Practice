using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WhiteLagoon.Infracstructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAmenityToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Amenities",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VillaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenities", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Amenities_Villas_VillaID",
                        column: x => x.VillaID,
                        principalTable: "Villas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Amenities",
                columns: new[] { "ID", "Description", "Name", "VillaID" },
                values: new object[,]
                {
                    { 1, null, "Private Pool", 1 },
                    { 2, null, "Microwave", 1 },
                    { 3, null, "Private Balcony", 1 },
                    { 4, null, "1 king bed and 1 sofa bed", 1 },
                    { 5, null, "Private Plunge Pool", 2 },
                    { 6, null, "Microwave and Mini Refrigerator", 2 },
                    { 7, null, "Private Balcony", 2 },
                    { 8, null, "king bed or 2 double beds", 2 },
                    { 9, null, "Private Pool", 3 },
                    { 10, null, "Jacuzzi", 3 },
                    { 11, null, "Private Balcony", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Amenities_VillaID",
                table: "Amenities",
                column: "VillaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Amenities");
        }
    }
}
