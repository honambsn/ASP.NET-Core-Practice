using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteLagoon.Domain.Entities;
using static System.Net.WebRequestMethods;

namespace WhiteLagoon.Infracstructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    ID = 1,
                    Name = "Villa 1",
                    Description = "Villa 1 Description",
                    ImageUrl = "https://placehold.co/600x401",
                    Occupancy = 4,
                    Price = 200,
                    Sqft = 550,
                },
                new Villa
                {
                    ID = 2,
                    Name = "Villa 2",
                    Description = "Villa 2 Description",
                    ImageUrl = "https://placehold.co/600x402",
                    Occupancy = 4,
                    Price = 300,
                    Sqft = 550,
                },
                new Villa
                {
                    ID = 3,
                    Name = "Villa 3",
                    Description = "Villa 3 Description",
                    ImageUrl = "https://placehold.co/600x403",
                    Occupancy = 4,
                    Price = 400,
                    Sqft = 750,
                }
            );

            // Additional configuration can go here if needed
        }
    }
}
