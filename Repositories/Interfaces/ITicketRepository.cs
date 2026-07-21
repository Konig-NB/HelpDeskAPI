using HelpDeskAPI.Models;

namespace HelpDeskAPI.Repositories.Interfaces
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        Task<IEnumerable<Ticket>> GetAllTicketsAsync(int page, int pageSize);
        Task<int> GetTotalCountAsync();
        Task<Ticket?> GetByIdTicketAsync(int id);
        Task<IEnumerable<Ticket>> GetTicketsForAgentAsync(int agentId);
        Task<IEnumerable<Ticket>> GetTicketsForCustomerAsync(int customerId);
    }
}