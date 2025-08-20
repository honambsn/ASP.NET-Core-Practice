using Bulky.Models;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // Define DbSets for your entities
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        // Add other DbSets as needed

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure entity properties and relationships here if needed


            modelBuilder.Entity<Category>().HasData(
                new Category { ID = 1, Name = "Action", DisplayOrder = 1 },
                new Category { ID = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { ID = 3, Name = "History", DisplayOrder = 3 }
                );


            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ID = 1,
                    Title = "Fortune of Time",
                    Author = "Billy Spark",
                    Description = "Ut porttitor nulla eros. Integer ac augue nibh. Phasellus molestie varius ante vel finibus. In hac habitasse platea dictumst. Suspendisse et sagittis velit, luctus venenatis felis. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Vestibulum eu tellus fringilla, pellentesque lorem vitae, sagittis tortor. Integer est eros, varius vel porttitor et, tempor nec nulla. Nulla et sapien ut lectus rhoncus facilisis. Ut maximus rhoncus diam quis bibendum. Maecenas vitae bibendum ligula, viverra aliquam neque. Interdum et malesuada fames ac ante ipsum primis in faucibus. Integer vel lacus mollis, malesuada nisl quis, commodo massa.\r\n\r\n",
                    ISBN = "SWD9999001",
                    ListPrice = 99,
                    Price = 90,
                    Price50 = 85,
                    Price100 = 80,
                    CategoryID = 1,
                    ImageUrl = "https://example.com/images/fortune_of_time.jpg",
                },
                new Product
                {
                    ID = 2,
                    Title = "Whispers of Eternity",
                    Author = "Lena Grey",
                    Description = "Aenean efficitur, nulla et bibendum fringilla, dolor purus convallis nisi, ac facilisis ipsum magna id turpis. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Curabitur luctus mauris vel nisl tincidunt, ac viverra tortor maximus. Morbi in arcu suscipit, cursus orci sed, volutpat sapien. Sed faucibus erat eget nisi tempor, eget sollicitudin dui posuere. Quisque ultricies lorem eget felis convallis, at suscipit metus rutrum. Pellentesque vehicula, tortor sit amet malesuada consectetur, nisi turpis interdum libero, a faucibus risus eros vel sapien. Duis vel ex nec odio elementum tincidunt ut ut sapien. Mauris vitae ante auctor, ultricies ex a, feugiat purus.\r\n\r\n",
                    ISBN = "SWD9999002",
                    ListPrice = 120,
                    Price = 110,
                    Price50 = 105,
                    Price100 = 100,
                    CategoryID = 1,
                    ImageUrl = "https://example.com/images/whispers_of_eternity.jpg",
                },
                new Product
                {
                    ID = 3,
                    Title = "Shadows of the Past",
                    Author = "David Thorn",
                    Description = "Fusce vehicula fermentum tortor, a lobortis libero feugiat ac. Nunc gravida et augue at consequat. Vivamus eleifend dui id neque hendrerit, ut sollicitudin ligula porttitor. Etiam id sapien eget metus luctus auctor non non leo. Aliquam erat volutpat. Sed auctor dolor sit amet orci tincidunt, eu tempus orci maximus. Curabitur sollicitudin pharetra dolor, a suscipit magna sollicitudin in. Integer convallis est sit amet dui tempus, eu iaculis libero dictum. Nunc eget ipsum tincidunt, tempus justo sit amet, laoreet libero. In egestas dolor ac ante fringilla, vitae ullamcorper augue dictum.\r\n\r\n",
                    ISBN = "SWD9999003",
                    ListPrice = 75,
                    Price = 70,
                    Price50 = 65,
                    Price100 = 60,
                    CategoryID = 1,
                    ImageUrl = "https://example.com/images/shadows_of_the_past.jpg",
                },
                new Product
                {
                    ID = 4,
                    Title = "The Infinite Journey",
                    Author = "Katherine Blaze",
                    Description = "Sed auctor magna et orci convallis, sit amet pharetra libero egestas. Donec suscipit ipsum ac purus tristique, ut tincidunt orci rhoncus. Ut vitae felis lorem. Nulla hendrerit elit sit amet eros fermentum, in auctor ligula consequat. Nulla nec arcu ac purus tincidunt tristique. Proin sollicitudin tincidunt felis, vitae interdum justo convallis a. Integer a erat a nisi vestibulum mollis et et arcu. Vivamus scelerisque, lorem in tempor tempor, libero nunc cursus libero, eget bibendum enim arcu a ante. Phasellus sollicitudin risus quis nisi tincidunt, id fermentum sem vehicula.\r\n\r\n",
                    ISBN = "SWD9999004",
                    ListPrice = 150,
                    Price = 140,
                    Price50 = 135,
                    Price100 = 130,
                    CategoryID = 1,
                    ImageUrl = "https://example.com/images/the_infinite_journey.jpg",
                },
                new Product
                {
                    ID = 5,
                    Title = "Echoes of Light",
                    Author = "Alice Rivers",
                    Description = "Pellentesque viverra nisl et libero laoreet, et egestas ante tincidunt. Suspendisse potenti. Nulla convallis nec enim id sollicitudin. Sed fringilla malesuada nisl sit amet venenatis. Aliquam erat volutpat. Nam posuere tortor felis, id faucibus lectus pretium a. In dapibus scelerisque ante, ut dictum ex varius nec. Vivamus mollis tortor non purus fermentum, a tincidunt elit porttitor. Integer sed eros sit amet orci malesuada efficitur et non ligula. Ut tincidunt sapien vel mauris scelerisque, in faucibus erat euismod. Phasellus eget convallis justo.\r\n\r\n",
                    ISBN = "SWD9999005",
                    ListPrice = 95,
                    Price = 85,
                    Price50 = 80,
                    Price100 = 75,
                    CategoryID = 2,
                    ImageUrl = "https://example.com/images/echoes_of_light.jpg",
                },
                new Product
                {
                    ID = 6,
                    Title = "The Silent Storm",
                    Author = "Oliver Quinn",
                    Description = "Donec nec mauris sed eros ultricies pretium. Sed euismod neque vel nisi malesuada, nec elementum arcu cursus. Nullam non odio id orci tincidunt accumsan et eget dui. Curabitur non arcu vitae augue auctor euismod. In condimentum mauris ut sapien bibendum tempor. Maecenas convallis odio ut augue tristique, eu gravida neque tincidunt. Nulla nec metus id orci porttitor sodales ac vel ipsum. Integer ut urna id lectus vulputate pharetra sit amet nec erat. Donec non metus nec ante tincidunt tristique sit amet non leo.\r\n\r\n",
                    ISBN = "SWD9999006",
                    ListPrice = 110,
                    Price = 100,
                    Price50 = 95,
                    Price100 = 90,
                    CategoryID = 2,
                    ImageUrl = "https://example.com/images/the_silent_storm.jpg",
                }
                );
        }

    }
}
