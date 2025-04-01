using Catalog.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Data
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Products", "catalog");
            modelBuilder.Entity<Product>()
            .Property(p => p.SellingPrice)
            .HasPrecision(10, 2);

            modelBuilder.Entity<Product>()
            .Property(p => p.Sku)
            .HasMaxLength(50)
            .IsRequired();

            modelBuilder.Entity<Product>()
            .HasIndex(p => p.Sku)
            .IsUnique();//clave única para el SKU e indices

            modelBuilder.Entity<Category>().ToTable("Categories", "catalog");
            modelBuilder.Entity<Brand>().ToTable("Brands", "catalog");
            base.OnModelCreating(modelBuilder);
            // Aquí puedes configurar relaciones, restricciones, etc.
        }
    }
}
