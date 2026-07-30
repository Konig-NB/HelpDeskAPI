using HelpDeskAPI.DTOs;
using HelpDeskAPI.Helpers;

namespace HelpDeskAPI.Services.Interfaces
{
    public interface ITicketService
    {
        Task<PagedResult<TicketDTO>> GetAllAsync(int page, int pageSize, string? status, string? priority, string? category);
        Task<TicketDTO?> GetByIdAsync(int id);
        Task<TicketDTO> CreateAsync(CreateTicketDTO dto);
        Task<TicketDTO?> UpdateAsync(int id, UpdateTicketDTO dto);
        Task<IEnumerable<TicketDTO>> GetTicketsForAgentAsync(int agentId);
        Task<IEnumerable<TicketDTO>> GetTicketsForCustomerAsync(int customerId);
        Task<bool> ExistsAsync(int id);
    }
}