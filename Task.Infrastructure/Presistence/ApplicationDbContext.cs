using Microsoft.EntityFrameworkCore;
using Task.Core.Entities;


namespace Task.Infrastructure.Presistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Brand> Brands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>()
                        .HasOne(p => p.Brand)        
                        .WithMany(e => e.Products)   
                        .HasForeignKey(p => p.BrandId);

            modelBuilder.Entity<Brand>()
                        .Property(b => b.Field)
                        .HasConversion<string>();

            modelBuilder.Entity<Brand>()
                        .Property(b => b.Country)
                        .HasConversion<string>();
        }
    }
}
