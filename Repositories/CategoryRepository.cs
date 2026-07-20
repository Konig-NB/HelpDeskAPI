using HelpDeskAPI.Data;
using HelpDeskAPI.Models;
using HelpDeskAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskAPI.Repositories
{
    public class CategoryRepository : Repository<Category> ,ICategoryRepository
    {
        public CategoryRepository(AppDbContext db) : base(db) {}

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(int page, int pagesize) =>
            await _db.Categories
                .Skip((page - 1) * pagesize)
                .Take(pagesize)
                .ToListAsync();

        public async Task<int> GetTotalCountAsync() =>
            await _db.Categories.CountAsync();

        public async Task<Category?> GetByIdCategoryAsync(int id) =>
            await _db.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
            
        public async Task<bool> IsNameTakenAsync(string name, int? excludeId = null)
        {
            var query = _db.Categories.Where(c => c.Name == name);

            if(excludeId.HasValue)
                query = query.Where(c => c.Id != excludeId.Value);

            return await query.AnyAsync();
        }
    }
}