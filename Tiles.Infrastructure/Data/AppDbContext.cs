using Microsoft.EntityFrameworkCore;
using Tiles.Core.Domain.Entities;
using Tiles.Core.Domain.Entites;

namespace Tiles.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Seller> Sellers => Set<Seller>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Subcategory> Subcategories => Set<Subcategory>();

        public DbSet<EcatalogCategory> EcatalogCategories => Set<EcatalogCategory>();

        public DbSet<Ecatalog> Ecatalogs => Set<Ecatalog>();

        public DbSet<Instagram> Instagrams => Set<Instagram>();





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ========== CONVERSIONS ==========
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

            // ========== UNIQUE INDEX ==========
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();

            // ========== RELATIONSHIPS ==========
            modelBuilder.Entity<Subcategory>()
                .HasOne(s => s.Category)
                .WithMany(c => c.Subcategories)
                .HasForeignKey(s => s.CategoryId);

            // ========== TIMESTAMPS ==========
            modelBuilder.Entity<Seller>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("NOW()");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("NOW()");
            });

            var fixedDate = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            // ========== SEED GUIDS ==========
            var category1Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            var category2Id = Guid.Parse("5b9a3c84-2d5f-4b8e-97c9-6f7d8b2e3a1f");

            var subcategory1Id = Guid.Parse("11111111-2222-3333-4444-555555555555");
            var subcategory2Id = Guid.Parse("66666666-7777-8888-9999-000000000000");
            var subcategory3Id = Guid.Parse("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee");
            var subcategory4Id = Guid.Parse("99999999-aaaa-bbbb-cccc-dddddddddddd");

            // ========== SEED USERS ==========
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
                    PasswordHash = "$2a$11$yR3e3tvATjy4kQcP9Nlh.eFq3CWcXbgEnDghIxKaD2ZOHMGKhjE9K"
                }
            );

            // ========== SEED CATEGORIES ==========
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = category1Id, Name = "Wall Tiles" },
                new Category { Id = category2Id, Name = "Floor Tiles" }
            );

            // ========== SEED SUBCATEGORIES ==========
            modelBuilder.Entity<Subcategory>().HasData(
                new Subcategory { Id = subcategory1Id, Name = "Glossy Finish", CategoryId = category1Id },
                new Subcategory { Id = subcategory2Id, Name = "Matt Finish", CategoryId = category1Id },
                new Subcategory { Id = subcategory3Id, Name = "Anti-Skid", CategoryId = category2Id },
                new Subcategory { Id = subcategory4Id, Name = "Granite Look", CategoryId = category2Id }
            );

            // ========== SEED SELLERS ==========
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

            // ========== SEED PRODUCTS ==========
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = Guid.Parse("d1111111-d222-4e33-b444-d55555555555"),
                    SerialNumber = "P001",
                    CategoryId = category1Id,
                    SubCategoryId = subcategory1Id,
                    ProductName = "Glossy White Tile",
                    ProductImage = "https://example.com/images/glossy-white.jpg",
                    ProductSizes = new List<string> { "12x18", "12x24" },
                    Description = "Elegant glossy white tile ideal for walls.",
                    Colors = new List<string> { "White", "Ivory" },
                    Disclaimer = "Color tone may vary due to lighting.",
                    Stock = 120,
                    Link360 = "https://example.com/360/glossy-white"
                },
                new Product
                {
                    Id = Guid.Parse("e1111111-e222-4e33-f444-e55555555555"),
                    SerialNumber = "P002",
                    CategoryId = category1Id,
                    SubCategoryId = subcategory2Id,
                    ProductName = "Matt Beige Tile",
                    ProductImage = "https://example.com/images/matt-beige.jpg",
                    ProductSizes = new List<string> { "12x24" },
                    Description = "A soft matt beige wall tile.",
                    Colors = new List<string> { "Beige", "Cream" },
                    Disclaimer = "Shade may differ slightly between batches.",
                    Stock = 80,
                    Link360 = null
                },
                new Product
                {
                    Id = Guid.Parse("f1111111-f222-4e33-f444-f55555555555"),
                    SerialNumber = "P003",
                    CategoryId = category2Id,
                    SubCategoryId = subcategory3Id,
                    ProductName = "Anti-Skid Grey Tile",
                    ProductImage = "https://example.com/images/anti-skid-grey.jpg",
                    ProductSizes = new List<string> { "16x16", "24x24" },
                    Description = "Perfect for wet areas with anti-skid surface.",
                    Colors = new List<string> { "Grey" },
                    Disclaimer = "Surface texture may feel different after use.",
                    Stock = 140,
                    Link360 = "https://example.com/360/anti-skid-grey"
                },
                new Product
                {
                    Id = Guid.Parse("a1111111-a222-4e33-a444-a55555555555"),
                    SerialNumber = "P004",
                    CategoryId = category2Id,
                    SubCategoryId = subcategory4Id,
                    ProductName = "Granite Look Black Tile",
                    ProductImage = "https://example.com/images/granite-black.jpg",
                    ProductSizes = new List<string> { "24x24" },
                    Description = "High-end tile with a granite stone look.",
                    Colors = new List<string> { "Black", "Charcoal" },
                    Disclaimer = "Granite texture varies by lot.",
                    Stock = 60,
                    Link360 = null
                },
                new Product
                {
                    Id = Guid.Parse("b1111111-b222-4e33-b444-b55555555555"),
                    SerialNumber = "P005",
                    CategoryId = category2Id,
                    SubCategoryId = subcategory4Id,
                    ProductName = "Granite Look White Tile",
                    ProductImage = "https://example.com/images/granite-white.jpg",
                    ProductSizes = new List<string> { "24x24", "32x32" },
                    Description = "Shiny granite-style tile for modern floors.",
                    Colors = new List<string> { "White", "Grey" },
                    Disclaimer = "Inspect each box for consistency before install.",
                    Stock = 100,
                    Link360 = "https://example.com/360/granite-white"
                }
            );
        }
    }
}
