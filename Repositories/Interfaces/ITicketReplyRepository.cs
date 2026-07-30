using HelpDeskAPI.Models;

namespace HelpDeskAPI.Repositories.Interfaces
{
    public interface ITicketReplyRepository : IRepository<TicketReply>
    {
        Task<IEnumerable<TicketReply>> GetAllTicketRepliesAsync(int page, int pageSize);
        Task<int> GetTotalCountAsync();
        Task<TicketReply?> GetByIdTicketReplyAsync(int id);
        Task<IEnumerable<TicketReply>> GetRepliesByTicketIdAsync(int ticketId);
    }
}