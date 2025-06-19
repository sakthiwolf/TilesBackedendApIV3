using Tiles.Core.DTO.ProductDto;

public interface IProductService
{
    Task<ProductResponse> CreateProductAsync(ProductRequest request);
    Task<List<ProductResponse>> GetProductsAsync(Guid? categoryId, Guid? subCategoryId, int page, int limit);
    Task<int> CountProductsAsync(Guid? categoryId, Guid? subCategoryId);
    Task<ProductResponse?> UpdateProductAsync(Guid id, ProductUpdateRequest request);
    Task<bool> DeleteProductAsync(Guid id);
}
