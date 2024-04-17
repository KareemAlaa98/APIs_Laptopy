using Laptopy_APIs.Configuration;
using Laptopy_APIs.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Laptopy_APIs.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Laptop> Laptops { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        public ApplicationDbContext(){}

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new LaptopEntityTypeConfiguration().Configure(modelBuilder.Entity<Laptop>());

        }

    }
}
