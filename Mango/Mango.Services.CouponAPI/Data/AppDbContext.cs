using Mango.Services.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>().HasData(
                new Coupon
                {
                    CouponID = 1,
                    CouponCode = "Test Code",
                    DiscountAmount = 100,
                    MinAmount = 100000,
                },
                new Coupon
                {
                    CouponID = 2,
                    CouponCode = "WELCOME50",
                    DiscountAmount = 50,
                    MinAmount = 500,
                },
                new Coupon
                {
                    CouponID = 3,
                    CouponCode = "SUMMER2025",
                    DiscountAmount = 75,
                    MinAmount = 750,
                },
                new Coupon
                {
                    CouponID = 4,
                    CouponCode = "FREESHIP",
                    DiscountAmount = 25,
                    MinAmount = 300,
                },
                new Coupon
                {
                    CouponID = 5,
                    CouponCode = "VIP100",
                    DiscountAmount = 100,
                    MinAmount = 1000,
                },
                new Coupon
                {
                    CouponID = 6,
                    CouponCode = "BLACKFRIDAY",
                    DiscountAmount = 150,
                    MinAmount = 1500,
                }
            );

        }
    }
}
