using HelpDeskAPI.Data;
using HelpDeskAPI.Models;
using HelpDeskAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskAPI.Repositories
{
    public class TicketRepository : Repository<Ticket> ,ITicketRepository
    {
        public TicketRepository(AppDbContext db) : base(db) {}

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync(int page, int pageSize, string? status, string? priority, string? category)
        {
            var query = _db.Tickets.AsQueryable();

            if (!string.IsNullOrEmpty(status))
                query = query.Where(t => t.TicketStatus.ToString() == status);

            if (!string.IsNullOrEmpty(priority))
                query = query.Where(t => t.TicketPriority.ToString() == priority);

            if (!string.IsNullOrEmpty(category))
                query = query.Where(t => t.Category.Name == category);

            return await query
                .Include(t => t.Category)
                .Include(t => t.AssignedTo)
                .Include(t => t.CreatedBy)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync(string? status, string? priority, string? category)
        {
            var query = _db.Tickets.AsQueryable();

            if (!string.IsNullOrEmpty(status))
                query = query.Where(t => t.TicketStatus.ToString() == status);

            if (!string.IsNullOrEmpty(priority))
                query = query.Where(t => t.TicketPriority.ToString() == priority);

            if (!string.IsNullOrEmpty(category))
                query = query.Where(t => t.Category.Name == category);

            return await query.CountAsync();
        }

        public async Task<Ticket?> GetByIdTicketAsync(int id) =>
            await _db.Tickets
                .Include(t => t.Category)
                .Include(t => t.AssignedTo)
                .Include(t => t.CreatedBy)
                .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<IEnumerable<Ticket>> GetTicketsForAgentAsync(int agentId) =>
            await _db.Tickets
                .Include(t => t.Category)
                .Include(t => t.CreatedBy)
                .Include(t => t.AssignedTo)
                .Where(t => t.AssignedToId == agentId)
                .ToListAsync();

        public async Task<IEnumerable<Ticket>> GetTicketsForCustomerAsync(int customerId) =>
            await _db.Tickets
                .Include(t => t.Category)
                .Include(t => t.AssignedTo)
                .Include(t => t.CreatedBy)
                .Where(t => t.CreatedById == customerId)
                .ToListAsync();
    }
}