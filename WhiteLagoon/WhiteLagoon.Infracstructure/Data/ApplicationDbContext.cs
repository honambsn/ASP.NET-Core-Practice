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
        public DbSet<VillaNumber> VillaNumbers { get; set; }

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
                },
                new Villa
                {
                    ID = 4,
                    Name = "Villa 4",
                    Description = "Villa 4 Description",
                    ImageUrl = "https://placehold.co/600x404",
                    Occupancy = 6,
                    Price = 500,
                    Sqft = 900,
                },
                new Villa
                {
                    ID = 5,
                    Name = "Villa 5",
                    Description = "Villa 5 Description",
                    ImageUrl = "https://placehold.co/600x405",
                    Occupancy = 5,
                    Price = 350,
                    Sqft = 800,
                },
                new Villa
                {
                    ID = 6,
                    Name = "Villa 6",
                    Description = "Villa 6 Description",
                    ImageUrl = "https://placehold.co/600x406",
                    Occupancy = 8,
                    Price = 600,
                    Sqft = 1000,
                },
                new Villa
                {
                    ID = 7,
                    Name = "Villa 7",
                    Description = "Villa 7 Description",
                    ImageUrl = "https://placehold.co/600x407",
                    Occupancy = 4,
                    Price = 450,
                    Sqft = 700,
                },
                new Villa
                {
                    ID = 8,
                    Name = "Villa 8",
                    Description = "Villa 8 Description",
                    ImageUrl = "https://placehold.co/600x408",
                    Occupancy = 6,
                    Price = 550,
                    Sqft = 850,
                },
                new Villa
                {
                    ID = 9,
                    Name = "Villa 9",
                    Description = "Villa 9 Description",
                    ImageUrl = "https://placehold.co/600x409",
                    Occupancy = 4,
                    Price = 350,
                    Sqft = 600,
                },
                new Villa
                {
                    ID = 10,
                    Name = "Villa 10",
                    Description = "Villa 10 Description",
                    ImageUrl = "https://placehold.co/600x410",
                    Occupancy = 5,
                    Price = 400,
                    Sqft = 700,
                },
                new Villa
                {
                    ID = 11,
                    Name = "Villa 11",
                    Description = "Villa 11 Description",
                    ImageUrl = "https://placehold.co/600x411",
                    Occupancy = 7,
                    Price = 650,
                    Sqft = 950,
                },
                new Villa
                {
                    ID = 12,
                    Name = "Villa 12",
                    Description = "Villa 12 Description",
                    ImageUrl = "https://placehold.co/600x412",
                    Occupancy = 6,
                    Price = 500,
                    Sqft = 800,
                },
                new Villa
                {
                    ID = 13,
                    Name = "Villa 13",
                    Description = "Villa 13 Description",
                    ImageUrl = "https://placehold.co/600x413",
                    Occupancy = 8,
                    Price = 700,
                    Sqft = 1200,
                }
            );

            // Additional configuration can go here if needed


            modelBuilder.Entity<VillaNumber>().HasData(
                new VillaNumber
                {
                    Villa_Number = 101,
                    VillaID = 1
                },
                new VillaNumber
                {
                    Villa_Number = 102,
                    VillaID = 1
                },
                new VillaNumber
                {
                    Villa_Number = 103,
                    VillaID = 2
                },
                new VillaNumber
                {
                    Villa_Number = 104,
                    VillaID = 2
                },
                new VillaNumber
                {
                    Villa_Number = 105,
                    VillaID = 3
                },
                new VillaNumber
                {
                    Villa_Number = 106,
                    VillaID = 3
                },
                new VillaNumber
                {
                    Villa_Number = 107,
                    VillaID = 3
                },
                new VillaNumber
                {
                    Villa_Number = 108,
                    VillaID = 3
                },
                new VillaNumber
                {
                    Villa_Number = 109,
                    VillaID = 3
                },
                new VillaNumber
                {
                    Villa_Number = 110,
                    VillaID = 3
                }
            );
        }
    }
}
