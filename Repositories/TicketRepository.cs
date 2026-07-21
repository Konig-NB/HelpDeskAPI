using HelpDeskAPI.Data;
using HelpDeskAPI.Models;
using HelpDeskAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskAPI.Repositories
{
    public class TicketRepository : Repository<Ticket> ,ITicketRepository
    {
        public TicketRepository(AppDbContext db) : base(db) {}

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync(int page, int pagesize) =>
            await _db.Tickets
                .Include(t => t.Category)
                .Include(t => t.AssignedTo)
                .Include(t => t.CreatedBy)
                .Skip((page - 1) * pagesize)
                .Take(pagesize)
                .ToListAsync();

        public async Task<int> GetTotalCountAsync() =>
            await _db.Tickets.CountAsync();

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