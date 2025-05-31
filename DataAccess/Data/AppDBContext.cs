using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Zeiss_TakeHome.Domain.Entities;

namespace Zeiss_TakeHome.DataAccess.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
  : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasIndex(b => b.ProductId)
                .IsUnique();
        }
    }
}
