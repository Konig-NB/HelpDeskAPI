using HelpDeskAPI.Models;

namespace HelpDeskAPI.Repositories.Interfaces
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        Task<IEnumerable<Ticket>> GetAllTicketsAsync(int page, int pageSize,string? status, string? priority, string? category);
        Task<int> GetTotalCountAsync(string? status, string? priority, string? category);
        Task<Ticket?> GetByIdTicketAsync(int id);
        Task<IEnumerable<Ticket>> GetTicketsForAgentAsync(int agentId);
        Task<IEnumerable<Ticket>> GetTicketsForCustomerAsync(int customerId);
    }
}