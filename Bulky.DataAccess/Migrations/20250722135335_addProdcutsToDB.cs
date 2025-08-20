using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BulkyWeb.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addProdcutsToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListPrice = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Price50 = table.Column<double>(type: "float", nullable: false),
                    Price100 = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "Author", "Description", "ISBN", "ListPrice", "Price", "Price100", "Price50", "Title" },
                values: new object[,]
                {
                    { 1, "Billy Spark", "Ut porttitor nulla eros. Integer ac augue nibh. Phasellus molestie varius ante vel finibus. In hac habitasse platea dictumst. Suspendisse et sagittis velit, luctus venenatis felis. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Vestibulum eu tellus fringilla, pellentesque lorem vitae, sagittis tortor. Integer est eros, varius vel porttitor et, tempor nec nulla. Nulla et sapien ut lectus rhoncus facilisis. Ut maximus rhoncus diam quis bibendum. Maecenas vitae bibendum ligula, viverra aliquam neque. Interdum et malesuada fames ac ante ipsum primis in faucibus. Integer vel lacus mollis, malesuada nisl quis, commodo massa.\r\n\r\n", "SWD9999001", 99.0, 90.0, 80.0, 85.0, "Fortune of Time" },
                    { 2, "Lena Grey", "Aenean efficitur, nulla et bibendum fringilla, dolor purus convallis nisi, ac facilisis ipsum magna id turpis. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Curabitur luctus mauris vel nisl tincidunt, ac viverra tortor maximus. Morbi in arcu suscipit, cursus orci sed, volutpat sapien. Sed faucibus erat eget nisi tempor, eget sollicitudin dui posuere. Quisque ultricies lorem eget felis convallis, at suscipit metus rutrum. Pellentesque vehicula, tortor sit amet malesuada consectetur, nisi turpis interdum libero, a faucibus risus eros vel sapien. Duis vel ex nec odio elementum tincidunt ut ut sapien. Mauris vitae ante auctor, ultricies ex a, feugiat purus.\r\n\r\n", "SWD9999002", 120.0, 110.0, 100.0, 105.0, "Whispers of Eternity" },
                    { 3, "David Thorn", "Fusce vehicula fermentum tortor, a lobortis libero feugiat ac. Nunc gravida et augue at consequat. Vivamus eleifend dui id neque hendrerit, ut sollicitudin ligula porttitor. Etiam id sapien eget metus luctus auctor non non leo. Aliquam erat volutpat. Sed auctor dolor sit amet orci tincidunt, eu tempus orci maximus. Curabitur sollicitudin pharetra dolor, a suscipit magna sollicitudin in. Integer convallis est sit amet dui tempus, eu iaculis libero dictum. Nunc eget ipsum tincidunt, tempus justo sit amet, laoreet libero. In egestas dolor ac ante fringilla, vitae ullamcorper augue dictum.\r\n\r\n", "SWD9999003", 75.0, 70.0, 60.0, 65.0, "Shadows of the Past" },
                    { 4, "Katherine Blaze", "Sed auctor magna et orci convallis, sit amet pharetra libero egestas. Donec suscipit ipsum ac purus tristique, ut tincidunt orci rhoncus. Ut vitae felis lorem. Nulla hendrerit elit sit amet eros fermentum, in auctor ligula consequat. Nulla nec arcu ac purus tincidunt tristique. Proin sollicitudin tincidunt felis, vitae interdum justo convallis a. Integer a erat a nisi vestibulum mollis et et arcu. Vivamus scelerisque, lorem in tempor tempor, libero nunc cursus libero, eget bibendum enim arcu a ante. Phasellus sollicitudin risus quis nisi tincidunt, id fermentum sem vehicula.\r\n\r\n", "SWD9999004", 150.0, 140.0, 130.0, 135.0, "The Infinite Journey" },
                    { 5, "Alice Rivers", "Pellentesque viverra nisl et libero laoreet, et egestas ante tincidunt. Suspendisse potenti. Nulla convallis nec enim id sollicitudin. Sed fringilla malesuada nisl sit amet venenatis. Aliquam erat volutpat. Nam posuere tortor felis, id faucibus lectus pretium a. In dapibus scelerisque ante, ut dictum ex varius nec. Vivamus mollis tortor non purus fermentum, a tincidunt elit porttitor. Integer sed eros sit amet orci malesuada efficitur et non ligula. Ut tincidunt sapien vel mauris scelerisque, in faucibus erat euismod. Phasellus eget convallis justo.\r\n\r\n", "SWD9999005", 95.0, 85.0, 75.0, 80.0, "Echoes of Light" },
                    { 6, "Oliver Quinn", "Donec nec mauris sed eros ultricies pretium. Sed euismod neque vel nisi malesuada, nec elementum arcu cursus. Nullam non odio id orci tincidunt accumsan et eget dui. Curabitur non arcu vitae augue auctor euismod. In condimentum mauris ut sapien bibendum tempor. Maecenas convallis odio ut augue tristique, eu gravida neque tincidunt. Nulla nec metus id orci porttitor sodales ac vel ipsum. Integer ut urna id lectus vulputate pharetra sit amet nec erat. Donec non metus nec ante tincidunt tristique sit amet non leo.\r\n\r\n", "SWD9999006", 110.0, 100.0, 90.0, 95.0, "The Silent Storm" }
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
