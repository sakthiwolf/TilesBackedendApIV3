using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiles.Core.Domain.Entities;
using Tiles.Core.Domain.RepositroyContracts;
using Tiles.Core.DTO.ProductDto.CategoryrequestandSubcategoryrequest;
using Tiles.Core.ServiceContracts;

namespace Tiles.Core.Services
{
    /// <summary>
    /// Service class for handling category-related operations.
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryService"/> class.
        /// </summary>
        /// <param name="repo">The repository for managing categories.</param>
        public CategoryService(ICategoryRepository repo) => _repo = repo;

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="request">The DTO containing category creation data.</param>
        /// <returns>The newly created <see cref="Category"/> entity.</returns>
        public async Task<Category> CreateCategoryAsync(CategoryRequest request)
        {
            var category = new Category { Name = request.Name };
            return await _repo.CreateAsync(category);
        }

        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        /// <returns>A list of all <see cref="Category"/> entities.</returns>
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _repo.GetAllAsync();
        }
    }
}
