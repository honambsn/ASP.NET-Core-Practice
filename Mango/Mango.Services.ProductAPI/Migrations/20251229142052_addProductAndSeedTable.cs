using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mango.Services.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class addProductAndSeedTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "CategoryName", "Description", "ImageURL", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Test", "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit...", "https://placehold.co/600x400/png", "Prod1", 99.989999999999995 },
                    { 2, "Test", "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium.", "https://placehold.co/600x400/png", "Prod2", 149.5 },
                    { 3, "Test", "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium.", "https://placehold.co/600x400/png", "Prod3", 59.990000000000002 },
                    { 4, "Test", "Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus.", "https://placehold.co/600x400/png", "Prod4", 199.0 },
                    { 5, "Test", "Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet.", "https://placehold.co/600x400/png", "Prod5", 29.949999999999999 },
                    { 6, "Test", "Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae.", "https://placehold.co/600x400/png", "Prod6", 89.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
