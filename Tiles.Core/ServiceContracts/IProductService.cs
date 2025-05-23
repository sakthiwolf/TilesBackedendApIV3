using Tiles.Core.DTO.ProductDto;

namespace Tiles.Core.ServiceContracts
{
    public interface IProductService
    {
        Task<Guid> AddProduct(ProductRequest dto);
        Task UpdateProduct(Guid id, ProductUpdateRequest dto);
        Task<IEnumerable<ProductResponse>> GetAllProducts();
        Task<IEnumerable<ProductResponse>> GetProductsByFilter(Guid? categoryId, Guid? subCategoryId);
        Task<ProductResponse?> GetProductById(Guid id);
        Task DeleteProduct(Guid id);
    }
}
