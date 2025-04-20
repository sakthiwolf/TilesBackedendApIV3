using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tiles.Core.Domain.Entites;

namespace Tiles.Infrastructure.data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            // Seed sample products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Price = 1200.00m },
                new Product { Id = 2, Name = "Smartphone", Price = 800.00m },
                new Product { Id = 3, Name = "Headphones", Price = 150.00m }
            );

            // Example User seed data
            modelBuilder.Entity<User>().HasData(
        new User
        {
            Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            SerialNumber = 1,
            Name = "Admin",
            Email = "admin@example.com",
            Phone = "1234567890",
            Designation = "Administrator",
            IsActive = true,
            PasswordHash = "$2a$11$yR3e3tvATjy4kQcP9Nlh.eFq3CWcXbgEnDghIxKaD2ZOHMGKhjE9K" // <- hashed "Admin@123"
        }
    );

        }

    }
}
