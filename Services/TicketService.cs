using HelpDeskAPI.Models;
using HelpDeskAPI.Repositories.Interfaces;
using HelpDeskAPI.DTOs;
using HelpDeskAPI.Services.Interfaces;
using HelpDeskAPI.Helpers;

namespace HelpDeskAPI.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _repo;
        public TicketService(ITicketRepository repo) => _repo = repo;

        public async Task<PagedResult<TicketDTO>> GetAllAsync(int page, int pageSize)
        {
            var tickets = await _repo.GetAllTicketsAsync(page, pageSize);
            var totalCount = await _repo.GetTotalCountAsync();

            return new PagedResult<TicketDTO>
            {
                Data = tickets.Select(ToDto),
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }

        public async Task<TicketDTO?> GetByIdAsync(int id)
        {
            var ticket = await _repo.GetByIdTicketAsync(id);
            return ticket is null ? null : ToDto(ticket);
        }

        public async Task<TicketDTO> CreateAsync(CreateTicketDTO dto)
        {
            var ticket = new Ticket
            {
                Title = dto.Title,
                Description = dto.Description,
                CategoryId = dto.CategoryId,
                TicketStatus = dto.TicketStatus,
                TicketPriority = dto.TicketPriority,
                CreatedById = dto.CreatedById,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            };

            var created = await _repo.CreateAsync(ticket);
            return ToDto(created);
        }

        public async Task<TicketDTO?> UpdateAsync(int id, UpdateTicketDTO dto)
        {
            var ticket = await _repo.GetByIdTicketAsync(id);
            if (ticket is null) return null;

            if (dto.Title is not null) ticket.Title = dto.Title;
            if (dto.Description is not null) ticket.Description = dto.Description;
            if (dto.CategoryId is not null) ticket.CategoryId = dto.CategoryId.Value;
            if (dto.TicketStatus is not null) ticket.TicketStatus = dto.TicketStatus.Value;
            if (dto.TicketPriority is not null) ticket.TicketPriority = dto.TicketPriority.Value;
            if (dto.CreatedById is not null) ticket.CreatedById = dto.CreatedById.Value;
            if (dto.AssignedToId is not null) ticket.AssignedToId = dto.AssignedToId.Value;
            ticket.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(ticket);
            var updated = await _repo.GetByIdTicketAsync(id);
            return ToDto(updated!);
        }

        public async Task<bool> ExistsAsync(int id) =>
            await _repo.ExistsAsync(id);

        public async Task<IEnumerable<TicketDTO>> GetTicketsForAgentAsync(int agentId)
        {
            var tickets = await _repo.GetTicketsForAgentAsync(agentId);
            return tickets.Select(ToDto);
        }

        public async Task<IEnumerable<TicketDTO>> GetTicketsForCustomerAsync(int customerId)
        {
            var tickets = await _repo.GetTicketsForCustomerAsync(customerId);
            return tickets.Select(ToDto);
        }

        private static TicketDTO ToDto(Ticket t) => new TicketDTO
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            CategoryId = t.CategoryId,
            CategoryName = t.Category?.Name ?? string.Empty,
            TicketStatus = t.TicketStatus,
            TicketPriority = t.TicketPriority,
            CreatedById = t.CreatedById,
            CustomerName = $"{t.CreatedBy?.FirstName} {t.CreatedBy?.LastName}" ?? string.Empty,
            AssignedToId = t.AssignedToId,
            AgentName = $"{t.AssignedTo?.FirstName} {t.AssignedTo?.LastName}" ?? string.Empty,
            CreatedAt = t.CreatedAt,
            UpdatedAt = t.UpdatedAt
        };
    }
}