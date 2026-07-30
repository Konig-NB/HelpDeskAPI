using HelpDeskAPI.Models;
using HelpDeskAPI.Repositories.Interfaces;
using HelpDeskAPI.DTOs;
using HelpDeskAPI.Services.Interfaces;
using HelpDeskAPI.Helpers;

namespace HelpDeskAPI.Services
{
    public class TicketReplyService : ITicketReplyService
    {
        private readonly ITicketReplyRepository _repo;
        private readonly ITicketRepository _ticketRepo;
        public TicketReplyService(ITicketReplyRepository repo, ITicketRepository ticketRepo)
        {
            _repo = repo;
            _ticketRepo = ticketRepo;
        } 

        public async Task<PagedResult<TicketReplyDTO>> GetAllAsync(int page, int pageSize)
        {
            var ticketReplies = await _repo.GetAllTicketRepliesAsync(page, pageSize);
            var totalCount = await _repo.GetTotalCountAsync();

            return new PagedResult<TicketReplyDTO>
            {
                Data = ticketReplies.Select(ToDto),
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }

        public async Task<TicketReplyDTO?> GetByIdAsync(int id)
        {
            var ticketReply = await _repo.GetByIdTicketReplyAsync(id);
            return ticketReply is null ? null : ToDto(ticketReply);
        }

        public async Task<TicketReplyDTO> CreateAsync(CreateTicketReplyDTO dto)
        {
            var ticket = await _ticketRepo.GetByIdTicketAsync(dto.TicketId);
            if (ticket is null)
                throw new Exception("Ticket not found.");

            if (ticket.CreatedById != dto.UserId && ticket.AssignedToId != dto.UserId)
                throw new UnauthorizedAccessException("User not authorized to reply to this ticket.");

            var ticketReply = new TicketReply
            {
                TicketId = dto.TicketId,
                UserId = dto.UserId,
                Message = dto.Message,
                CreatedAt = DateTime.UtcNow,
            };

            var created = await _repo.CreateAsync(ticketReply);
            return ToDto(created);
        }

        public async Task<TicketReplyDTO?> UpdateAsync(int id, UpdateTicketReplyDTO dto)
        {
            var ticketReply = await _repo.GetByIdTicketReplyAsync(id);
            if (ticketReply is null) return null;

            if (dto.Message is not null) ticketReply.Message = dto.Message;

            await _repo.UpdateAsync(ticketReply);
            var updated = await _repo.GetByIdTicketReplyAsync(id);
            return ToDto(updated!);
        }

        public async Task<bool> ExistsAsync(int id) =>
            await _repo.ExistsAsync(id);

        public async Task<IEnumerable<TicketReplyDTO>> GetRepliesForTicketAsync(int ticketId)
        {
            var replies = await _repo.GetRepliesByTicketIdAsync(ticketId);
            return replies.Select(ToDto);
        }


        private static TicketReplyDTO ToDto(TicketReply tr) => new TicketReplyDTO
        {
            Id = tr.Id,
            TicketId = tr.TicketId,
            TicketTitle = tr.Ticket?.Title ?? string.Empty,
            UserId = tr.UserId,
            UserName = $"{tr.User?.FirstName} {tr.User?.LastName}" ?? string.Empty,
            Message = tr.Message,
            CreatedAt = tr.CreatedAt,
        };
    }
}