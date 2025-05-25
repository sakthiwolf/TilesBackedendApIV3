using Microsoft.EntityFrameworkCore;
using Tiles.Core.Domain.Entities;
using System;


namespace Tiles.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Subcategory> Subcategories => Set<Subcategory>();
        public DbSet<Seller> Sellers => Set<Seller>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Product list conversions
            modelBuilder.Entity<Product>()
                .Property(p => p.ProductSizes)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                );

            modelBuilder.Entity<Product>()
                .Property(p => p.Colors)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                );

            // Unique constraint on Category name
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();

            // Subcategory → Category relationship
            modelBuilder.Entity<Subcategory>()
                .HasOne(s => s.Category)
                .WithMany(c => c.Subcategories)
                .HasForeignKey(s => s.CategoryId);

            // Configure Seller timestamps with PostgreSQL NOW() as default
            modelBuilder.Entity<Seller>(entity =>
            {
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("NOW()");

                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("NOW()");
            });

            // Seed Admin User
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
                    PasswordHash = "$2a$11$yR3e3tvATjy4kQcP9Nlh.eFq3CWcXbgEnDghIxKaD2ZOHMGKhjE9K" // BCrypt hash
                }
            );

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                    Name = "Category One"
                },
                new Category
                {
                    Id = Guid.Parse("5b9a3c84-2d5f-4b8e-97c9-6f7d8b2e3a1f"),
                    Name = "Category Two"
                }
            );

            // Fixed UTC date for seeding to avoid EF Core PostgreSQL error
            var fixedDate = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            // Seed Sellers with fixed CreatedAt and UpdatedAt
            modelBuilder.Entity<Seller>().HasData(
                new Seller
                {
                    Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    SerialNumber = 1,
                    Name = "Ravi Traders",
                    Map = "https://maps.google.com/?q=ravi+traders",
                    WhatsappNumber = "9876543210",
                    Email = "ravi@example.com",
                    DealerName = "Ravi Kumar",
                    State = "Tamil Nadu",
                    City = "Chennai",
                    Address = "No. 10, Mount Road, Chennai",
                    CreatedAt = fixedDate,
                    UpdatedAt = fixedDate
                },
                new Seller
                {
                    Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                    SerialNumber = 2,
                    Name = "Suresh Distributors",
                    Map = "https://maps.google.com/?q=suresh+distributors",
                    WhatsappNumber = "9876543211",
                    Email = "suresh@example.com",
                    DealerName = "Suresh Babu",
                    State = "Kerala",
                    City = "Kochi",
                    Address = "Near MG Road, Kochi",
                    CreatedAt = fixedDate,
                    UpdatedAt = fixedDate
                },
                new Seller
                {
                    Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                    SerialNumber = 3,
                    Name = "Karthik Hardware",
                    Map = "https://maps.google.com/?q=karthik+hardware",
                    WhatsappNumber = "9876543212",
                    Email = "karthik@example.com",
                    DealerName = "Karthik",
                    State = "Karnataka",
                    City = "Bangalore",
                    Address = "Brigade Road, Bangalore",
                    CreatedAt = fixedDate,
                    UpdatedAt = fixedDate
                }
            );
        }
    }
}
