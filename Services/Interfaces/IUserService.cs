using HelpDeskAPI.DTOs;
using HelpDeskAPI.Helpers;

namespace HelpDeskAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<PagedResult<UserDTO>> GetAllAsync(int page, int pageSize);
        Task<UserDTO?> GetByIdAsync(int id);
        Task<UserDTO?> UpdateAsync(int id, UpdateUserDTO dto);
        Task<bool> ExistsAsync(int id);
    }
}