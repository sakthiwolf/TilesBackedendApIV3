using Tiles.Core.Domain.Entites;
using Tiles.Core.Domain.RepositroyContracts;
using Tiles.Core.DTO.ProductDto;
using Tiles.Core.ServiceContracts;

namespace Tiles.Core.Services
{
    /// <summary>
    /// Service class responsible for handling product-related operations.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="repo">The product repository interface.</param>
        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Adds a new product to the repository.
        /// </summary>
        /// <param name="dto">Product data transfer object containing product details.</param>
        /// <returns>The unique identifier of the newly created product.</returns>
        public async Task<Guid> AddProduct(ProductRequest dto)
        {
            var product = new Product
            {
                Category = dto.Category,
                SubCategory = dto.SubCategory,
                ProductName = dto.ProductName,
                ProductImage = dto.ProductImage,
                ProductSizes = dto.ProductSizes ?? new List<string>(),
                Description = dto.Description,
                Colors = dto.Colors ?? new List<string>(),
                Disclaimer = dto.Disclaimer,
                Stock = dto.Stock
            };

            var created = await _repo.CreateAsync(product);
            return created.Id;
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">The unique identifier of the product to update.</param>
        /// <param name="dto">Product update request DTO with new values.</param>
        /// <exception cref="Exception">Thrown when the product is not found.</exception>
        public async Task UpdateProduct(Guid id, ProductUpdateRequest dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                throw new Exception("Product not found");

            existing.ProductName = dto.ProductName ?? existing.ProductName;
            existing.ProductImage = dto.ProductImage ?? existing.ProductImage;
            existing.ProductSizes = dto.ProductSizes ?? existing.ProductSizes;
            existing.Description = dto.Description ?? existing.Description;
            existing.Colors = dto.Colors ?? existing.Colors;
            existing.Disclaimer = dto.Disclaimer ?? existing.Disclaimer;
            existing.Stock = dto.Stock ?? existing.Stock;
            existing.Category = dto.Category ?? existing.Category;
            existing.SubCategory = dto.SubCategory ?? existing.SubCategory;

            await _repo.UpdateAsync(existing);
        }

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A collection of all products as <see cref="ProductResponse"/> DTOs.</returns>
        public async Task<IEnumerable<ProductResponse>> GetAllProducts()
        {
            var products = await _repo.GetAllAsync();
            return products.Select(MapToDto);
        }

        /// <summary>
        /// Retrieves products filtered by category and/or subcategory.
        /// </summary>
        /// <param name="categoryId">Optional category ID.</param>
        /// <param name="subCategoryId">Optional subcategory ID.</param>
        /// <returns>A filtered collection of products as <see cref="ProductResponse"/> DTOs.</returns>
        public async Task<IEnumerable<ProductResponse>> GetProductsByFilter(Guid? categoryId, Guid? subCategoryId)
        {
            var products = await _repo.GetByFilterAsync(categoryId, subCategoryId);
            return products.Select(MapToDto);
        }

        /// <summary>
        /// Retrieves a product by its unique identifier.
        /// </summary>
        /// <param name="id">The product ID.</param>
        /// <returns>A <see cref="ProductResponse"/> if found; otherwise, null.</returns>
        public async Task<ProductResponse?> GetProductById(Guid id)
        {
            var p = await _repo.GetByIdAsync(id);
            return p == null ? null : MapToDto(p);
        }

        /// <summary>
        /// Deletes a product by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <exception cref="Exception">Thrown if the product is not found or cannot be deleted.</exception>
        public async Task DeleteProduct(Guid id)
        {
            var success = await _repo.DeleteAsync(id);
            if (!success)
                throw new Exception("Product not found or could not be deleted");
        }

        /// <summary>
        /// Maps a <see cref="Product"/> entity to a <see cref="ProductResponse"/> DTO.
        /// </summary>
        /// <param name="p">The product entity.</param>
        /// <returns>The mapped product response DTO.</returns>
        private ProductResponse MapToDto(Product p)
        {
            return new ProductResponse
            {
                Id = p.Id,
                Category = p.Category,
                SubCategory = p.SubCategory,
                ProductName = p.ProductName,
                ProductImage = p.ProductImage,
                ProductSizes = p.ProductSizes,
                Description = p.Description,
                Colors = p.Colors,
                Disclaimer = p.Disclaimer,
                Stock = p.Stock
            };
        }
    }
}
