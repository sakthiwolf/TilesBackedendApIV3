using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiles.Core.Domain.Entities;
using Tiles.Core.Domain.RepositroyContracts;
using Tiles.Core.DTO.ProductDto.CategoryrequestandSubcategoryrequest;
using Tiles.Core.ServiceContracts;

namespace Tiles.Core.Services
{
    /// <summary>
    /// Service responsible for subcategory-related operations.
    /// </summary>
    public class SubcategoryService : ISubcategoryService
    {
        private readonly ISubcategoryRepository _repo;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubcategoryService"/> class.
        /// </summary>
        /// <param name="repo">The subcategory repository.</param>
        public SubcategoryService(ISubcategoryRepository repo) => _repo = repo;

        /// <summary>
        /// Creates a new subcategory.
        /// </summary>
        /// <param name="request">The subcategory creation request DTO.</param>
        /// <returns>The newly created <see cref="Subcategory"/>.</returns>
        public async Task<Subcategory> CreateSubcategoryAsync(SubcategoryRequest request)
        {
            var subcategory = new Subcategory
            {
                Name = request.Name,
                CategoryId = request.CategoryId
            };

            return await _repo.CreateAsync(subcategory);
        }

        /// <summary>
        /// Retrieves all subcategories for a given category ID.
        /// </summary>
        /// <param name="categoryId">The ID of the category.</param>
        /// <returns>A list of subcategories belonging to the specified category.</returns>
        public async Task<IEnumerable<Subcategory>> GetSubcategoriesByCategoryIdAsync(Guid categoryId)
        {
            return await _repo.GetByCategoryIdAsync(categoryId);
        }
    }
}
