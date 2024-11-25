using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // إعداد العلاقة عدة إلى عدة بين Customers و Products
            modelBuilder.Entity<Wishlist>()
                .HasKey(w => new { w.CustomerId , w.ProductId });

            modelBuilder.Entity<Wishlist>()
                .HasOne(w => w.Customer)
                .WithMany(c => c.Wishlists)
                .HasForeignKey(w => w.CustomerId);

            modelBuilder.Entity<Wishlist>()
                .HasOne(w => w.Product)
                .WithMany(p => p.Wishlists)
                .HasForeignKey(w => w.ProductId);

            modelBuilder.Entity<Order>()
               .HasMany(o => o.OrderDetails)
               .WithOne(od => od.Order)
               .HasForeignKey(od => od.OrderId);

            modelBuilder.Entity<OrderDetails>()
                .HasOne(od => od.Product)
                .WithMany()
                .HasForeignKey(od => od.ProductId);
        }
    }
}
