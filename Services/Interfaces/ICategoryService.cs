using HelpDeskAPI.DTOs;
using HelpDeskAPI.Helpers;

namespace HelpDeskAPI.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<PagedResult<CategoryDTO>> GetAllAsync(int page, int pageSize);
        Task<CategoryDTO?> GetByIdAsync(int id);
        Task<CategoryDTO> CreateAsync(CreateCategoryDTO dto);
        Task<CategoryDTO?> UpdateAsync(int id, UpdateCategoryDTO dto);
        Task<bool> ExistsAsync(int id);
    }
}