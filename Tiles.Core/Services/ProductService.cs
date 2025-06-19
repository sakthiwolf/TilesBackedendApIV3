using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tiles.Core.Domain.Entites;
using Tiles.Core.Domain.Entities;
using Tiles.Core.Domain.RepositroyContracts;
using Tiles.Core.DTO.ProductDto;
using Tiles.Core.ServiceContracts;

namespace Tiles.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly ISubcategoryRepository _subCategoryRepo;

        public ProductService(
            IProductRepository repo,
            ICategoryRepository categoryRepo,
            ISubcategoryRepository subCategoryRepo)
        {
            _repo = repo;
            _categoryRepo = categoryRepo;
            _subCategoryRepo = subCategoryRepo;
        }

        public async Task<ProductResponse> CreateProductAsync(ProductRequest request)
        {
            // Ensure Category exists
            var category = await _categoryRepo.GetByIdAsync(request.CategoryId);
            if (category == null)
                throw new Exception($"Category ID '{request.CategoryId}' does not exist.");

            // Ensure Subcategory exists
            var subCategory = await _subCategoryRepo.GetByIdAsync(request.SubCategoryId);
            if (subCategory == null)
                throw new Exception($"SubCategory ID '{request.SubCategoryId}' does not exist.");

            var product = new Product
            {
                Id = Guid.NewGuid(),
                SerialNumber = Guid.NewGuid().ToString(),
                CategoryId = request.CategoryId,
                SubCategoryId = request.SubCategoryId,
                ProductName = request.ProductName,
                ProductImage = request.ProductImage,
                ProductSizes = request.ProductSizes,
                Description = request.Description,
                Colors = request.Colors,
                Disclaimer = request.Disclaimer,
                Stock = request.Stock,
                Link360 = request.Link360,
            
            };

            var created = await _repo.CreateAsync(product);
            return MapToResponse(created);
        }

        public async Task<List<ProductResponse>> GetProductsAsync(Guid? categoryId, Guid? subCategoryId, int page, int limit)
        {
            var products = await _repo.GetAllAsync(categoryId, subCategoryId, page, limit);
            return products.Select(MapToResponse).ToList();
        }

        public async Task<int> CountProductsAsync(Guid? categoryId, Guid? subCategoryId)
        {
            return await _repo.CountAsync(categoryId, subCategoryId);
        }

        public async Task<ProductResponse?> UpdateProductAsync(Guid id, ProductUpdateRequest request)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null) return null;

            if (request.CategoryId.HasValue)
            {
                var category = await _categoryRepo.GetByIdAsync(request.CategoryId.Value);
                if (category == null)
                    throw new Exception($"Category ID '{request.CategoryId}' does not exist.");

                product.CategoryId = request.CategoryId.Value;
            }

            if (request.SubCategoryId.HasValue)
            {
                var subCategory = await _subCategoryRepo.GetByIdAsync(request.SubCategoryId.Value);
                if (subCategory == null)
                    throw new Exception($"SubCategory ID '{request.SubCategoryId}' does not exist.");

                product.SubCategoryId = request.SubCategoryId.Value;
            }

            product.ProductName = request.ProductName ?? product.ProductName;
            product.ProductImage = request.ProductImage ?? product.ProductImage;
            product.ProductSizes = request.ProductSizes ?? product.ProductSizes;
            product.Description = request.Description ?? product.Description;
            product.Colors = request.Colors ?? product.Colors;
            product.Disclaimer = request.Disclaimer ?? product.Disclaimer;
            product.Stock = request.Stock ?? product.Stock;
            product.Link360 = request.Link360 ?? product.Link360;

            var updated = await _repo.UpdateAsync(product);
            return MapToResponse(updated);
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            return await _repo.DeleteAsync(id);
        }

       private ProductResponse MapToResponse(Product product)
{
    return new ProductResponse
    {
        Id = product.Id,
        SerialNumber = product.SerialNumber,
        ProductName = product.ProductName,
        ProductImage = product.ProductImage,
        ProductSizes = product.ProductSizes,
        Description = product.Description,
        Colors = product.Colors,
        Disclaimer = product.Disclaimer,
        Stock = product.Stock switch
        {
            <= 0 => "out_of_stock",
            <= 10 => "low_stock",
            _ => "in_stock"
        },
        Link360 = product.Link360,
        Category = new NestedCategory
        {
            Id = product.Category.Id,
            Name = product.Category.Name
        },
        SubCategory = new NestedCategory
        {
            Id = product.SubCategory.Id,
            Name = product.SubCategory.Name
        }
    };
}

    }
}
