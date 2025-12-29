using Mango.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductID = 1,
                    Name = "Prod1",
                    Price = 99.99,
                    Description = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit...",
                    ImageURL = "https://placehold.co/600x400/png",
                    CategoryName = "Test",
                },
                new Product
                {
                    ProductID = 2,
                    Name = "Prod2",
                    Price = 149.50,
                    Description = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium.",
                    ImageURL = "https://placehold.co/600x400/png",
                    CategoryName = "Test",
                },
                new Product
                {
                    ProductID = 3,
                    Name = "Prod3",
                    Price = 59.99,
                    Description = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium.",
                    ImageURL = "https://placehold.co/600x400/png",
                    CategoryName = "Test",
                },

                new Product
                {
                    ProductID = 4,
                    Name = "Prod4",
                    Price = 199.00,
                    Description = "Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus.",
                    ImageURL = "https://placehold.co/600x400/png",
                    CategoryName = "Test",
                },

                new Product
                {
                    ProductID = 5,
                    Name = "Prod5",
                    Price = 29.95,
                    Description = "Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet.",
                    ImageURL = "https://placehold.co/600x400/png",
                    CategoryName = "Test",
                },

                new Product
                {
                    ProductID = 6,
                    Name = "Prod6",
                    Price = 89.00,
                    Description = "Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae.",
                    ImageURL = "https://placehold.co/600x400/png",
                    CategoryName = "Test",
                }

            );

        }
    }
}
