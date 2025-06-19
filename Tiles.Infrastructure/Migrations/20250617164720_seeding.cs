using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tiles.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                column: "Name",
                value: "Wall Tiles");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5b9a3c84-2d5f-4b8e-97c9-6f7d8b2e3a1f"),
                column: "Name",
                value: "Floor Tiles");

            migrationBuilder.InsertData(
                table: "Subcategories",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-2222-3333-4444-555555555555"), new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), "Glossy Finish" },
                    { new Guid("66666666-7777-8888-9999-000000000000"), new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), "Matt Finish" },
                    { new Guid("99999999-aaaa-bbbb-cccc-dddddddddddd"), new Guid("5b9a3c84-2d5f-4b8e-97c9-6f7d8b2e3a1f"), "Granite Look" },
                    { new Guid("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee"), new Guid("5b9a3c84-2d5f-4b8e-97c9-6f7d8b2e3a1f"), "Anti-Skid" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Colors", "Description", "Disclaimer", "Link360", "ProductImage", "ProductName", "ProductSizes", "SerialNumber", "Stock", "SubCategoryId" },
                values: new object[,]
                {
                    { new Guid("a1111111-a222-4e33-a444-a55555555555"), new Guid("5b9a3c84-2d5f-4b8e-97c9-6f7d8b2e3a1f"), "Black,Charcoal", "High-end tile with a granite stone look.", "Granite texture varies by lot.", null, "https://example.com/images/granite-black.jpg", "Granite Look Black Tile", "24x24", "P004", 60, new Guid("99999999-aaaa-bbbb-cccc-dddddddddddd") },
                    { new Guid("b1111111-b222-4e33-b444-b55555555555"), new Guid("5b9a3c84-2d5f-4b8e-97c9-6f7d8b2e3a1f"), "White,Grey", "Shiny granite-style tile for modern floors.", "Inspect each box for consistency before install.", "https://example.com/360/granite-white", "https://example.com/images/granite-white.jpg", "Granite Look White Tile", "24x24,32x32", "P005", 100, new Guid("99999999-aaaa-bbbb-cccc-dddddddddddd") },
                    { new Guid("d1111111-d222-4e33-b444-d55555555555"), new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), "White,Ivory", "Elegant glossy white tile ideal for walls.", "Color tone may vary due to lighting.", "https://example.com/360/glossy-white", "https://example.com/images/glossy-white.jpg", "Glossy White Tile", "12x18,12x24", "P001", 120, new Guid("11111111-2222-3333-4444-555555555555") },
                    { new Guid("e1111111-e222-4e33-f444-e55555555555"), new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), "Beige,Cream", "A soft matt beige wall tile.", "Shade may differ slightly between batches.", null, "https://example.com/images/matt-beige.jpg", "Matt Beige Tile", "12x24", "P002", 80, new Guid("66666666-7777-8888-9999-000000000000") },
                    { new Guid("f1111111-f222-4e33-f444-f55555555555"), new Guid("5b9a3c84-2d5f-4b8e-97c9-6f7d8b2e3a1f"), "Grey", "Perfect for wet areas with anti-skid surface.", "Surface texture may feel different after use.", "https://example.com/360/anti-skid-grey", "https://example.com/images/anti-skid-grey.jpg", "Anti-Skid Grey Tile", "16x16,24x24", "P003", 140, new Guid("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-a222-4e33-a444-a55555555555"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b1111111-b222-4e33-b444-b55555555555"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d1111111-d222-4e33-b444-d55555555555"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e1111111-e222-4e33-f444-e55555555555"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f1111111-f222-4e33-f444-f55555555555"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-3333-4444-555555555555"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("66666666-7777-8888-9999-000000000000"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("99999999-aaaa-bbbb-cccc-dddddddddddd"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee"));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                column: "Name",
                value: "Category One");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5b9a3c84-2d5f-4b8e-97c9-6f7d8b2e3a1f"),
                column: "Name",
                value: "Category Two");
        }
    }
}
