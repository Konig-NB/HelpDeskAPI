using HelpDeskAPI.Models;
using HelpDeskAPI.Repositories.Interfaces;
using HelpDeskAPI.DTOs;
using HelpDeskAPI.Services.Interfaces;
using HelpDeskAPI.Helpers;

namespace HelpDeskAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        public UserService(IUserRepository repo) => _repo = repo;

        public async Task<PagedResult<UserDTO>> GetAllAsync(int page, int pageSize)
        {
            var users = await _repo.GetAllUsersAsync(page, pageSize);
            var totalCount = await _repo.GetTotalCountAsync();

            return new PagedResult<UserDTO>
            {
                Data = users.Select(ToDto),
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }

        public async Task<UserDTO?> GetByIdAsync(int id)
        {
            var user = await _repo.GetByIdUserAsync(id);
            return user is null ? null : ToDto(user);
        }

        public async Task<UserDTO?> UpdateAsync(int id, UpdateUserDTO dto)
        {
            var user = await _repo.GetByIdUserAsync(id);
            if (user is null) return null;

            if (dto.FirstName is not null) user.FirstName = dto.FirstName;
            if (dto.LastName is not null) user.LastName = dto.LastName;

            await _repo.UpdateAsync(user);
            var updated = await _repo.GetByIdUserAsync(id);
            return ToDto(updated!);
        }

        public async Task<bool> ExistsAsync(int id) =>
            await _repo.ExistsAsync(id);

        private static UserDTO ToDto(User u) => new UserDTO
        {
            Id = u.Id,
            FullName = $"{u.FirstName} {u.LastName}",
            Email = u.Email,
            Role = u.Role,
            CreatedAt = u.CreatedAt
        };
    }
}