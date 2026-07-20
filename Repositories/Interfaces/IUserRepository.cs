using HelpDeskAPI.Models;

namespace HelpDeskAPI.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetAllUsersAsync(int page, int pageSize);
        Task<int> GetTotalCountAsync();
        Task<User?> GetByIdUserAsync(int id);
        Task<bool> IsEmailTakenAsync(string email, int? excludeId = null);
    }
}