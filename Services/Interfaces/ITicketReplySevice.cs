using HelpDeskAPI.DTOs;
using HelpDeskAPI.Helpers;

namespace HelpDeskAPI.Services.Interfaces
{
    public interface ITicketReplyService
    {
        Task<PagedResult<TicketReplyDTO>> GetAllAsync(int page, int pageSize);
        Task<TicketReplyDTO?> GetByIdAsync(int id);
        Task<TicketReplyDTO> CreateAsync(CreateTicketReplyDTO dto);
        Task<TicketReplyDTO?> UpdateAsync(int id, UpdateTicketReplyDTO dto);
        Task<IEnumerable<TicketReplyDTO>> GetRepliesForTicketAsync(int ticketId);
        Task<bool> ExistsAsync(int id);
    }
}