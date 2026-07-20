using HelpDeskAPI.Data;
using HelpDeskAPI.Models;
using HelpDeskAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskAPI.Repositories
{
    public class UserRepository : Repository<User> ,IUserRepository
    {
        public UserRepository(AppDbContext db) : base(db) {}

        public async Task<IEnumerable<User>> GetAllUsersAsync(int page, int pagesize) =>
            await _db.Users
                .Skip((page - 1) * pagesize)
                .Take(pagesize)
                .ToListAsync();

        public async Task<int> GetTotalCountAsync() =>
            await _db.Users.CountAsync();

        public async Task<User?> GetByIdUserAsync(int id) =>
            await _db.Users
                .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<bool> IsEmailTakenAsync(string email, int? excludeId = null)
        {
            var query = _db.Users.Where(b => b.Email == email);

            if(excludeId.HasValue)
                query = query.Where(b => b.Id != excludeId.Value);

            return await query.AnyAsync();
        }
    }
}