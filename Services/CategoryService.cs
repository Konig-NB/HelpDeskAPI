using HelpDeskAPI.Models;
using HelpDeskAPI.Repositories.Interfaces;
using HelpDeskAPI.DTOs;
using HelpDeskAPI.Services.Interfaces;
using HelpDeskAPI.Helpers;

namespace HelpDeskAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;
        public CategoryService(ICategoryRepository repo) => _repo = repo;

        public async Task<PagedResult<CategoryDTO>> GetAllAsync(int page, int pageSize)
        {
            var categories = await _repo.GetAllCategoriesAsync(page, pageSize);
            var totalCount = await _repo.GetTotalCountAsync();

            return new PagedResult<CategoryDTO>
            {
                Data = categories.Select(ToDto),
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }

        public async Task<CategoryDTO?> GetByIdAsync(int id)
        {
            var category = await _repo.GetByIdCategoryAsync(id);
            return category is null ? null : ToDto(category);
        }

        public async Task<CategoryDTO> CreateAsync(CreateCategoryDTO dto)
        {
            var category = new Category
            {
                Name = dto.Name
            };

            var created = await _repo.CreateAsync(category);
            return ToDto(created);
        }

        public async Task<CategoryDTO?> UpdateAsync(int id, UpdateCategoryDTO dto)
        {
            var category = await _repo.GetByIdCategoryAsync(id);
            if (category is null) return null;

            if (dto.Name is not null) category.Name = dto.Name;

            await _repo.UpdateAsync(category);
            var updated = await _repo.GetByIdCategoryAsync(id);
            return ToDto(updated!);
        }

        public async Task<bool> ExistsAsync(int id) =>
            await _repo.ExistsAsync(id);

        private static CategoryDTO ToDto(Category c) => new CategoryDTO
        {
            Id = c.Id,
            Name = c.Name
        };
    }
}