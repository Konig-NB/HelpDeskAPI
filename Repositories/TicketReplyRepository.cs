using HelpDeskAPI.Data;
using HelpDeskAPI.Models;
using HelpDeskAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskAPI.Repositories
{
    public class TicketReplyRepository : Repository<TicketReply> ,ITicketReplyRepository
    {
        public TicketReplyRepository(AppDbContext db) : base(db) {}

        public async Task<IEnumerable<TicketReply>> GetAllTicketRepliesAsync(int page, int pagesize) =>
            await _db.TicketReplies
                .Include(t => t.Ticket)
                .Include(t => t.User)
                .Skip((page - 1) * pagesize)
                .Take(pagesize)
                .ToListAsync();

        public async Task<int> GetTotalCountAsync() =>
            await _db.TicketReplies.CountAsync();

        public async Task<TicketReply?> GetByIdTicketReplyAsync(int id) =>
            await _db.TicketReplies
                .Include(t => t.Ticket)
                .Include(t => t.User)
                .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<IEnumerable<TicketReply>> GetRepliesByTicketIdAsync(int ticketId)
        {
            return await _db.TicketReplies
                .Where(t => t.TicketId == ticketId)
                .Include(t => t.User)
                .Include(t => t.Ticket)
                .OrderBy(t => t.CreatedAt)
                .ToListAsync();
        }

    }
}