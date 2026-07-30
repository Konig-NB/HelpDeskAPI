using HelpDeskAPI.DTOs;
using HelpDeskAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.RateLimiting;

namespace HelpDeskAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TicketReplyController : ControllerBase
    {
        private readonly ITicketReplyService _service;

        public TicketReplyController(ITicketReplyService service) => _service = service;

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            if (page < 1 || pageSize < 1)
                return BadRequest(new { message = "Page and pageSize must be greater than 0." });

            var result = await _service.GetAllAsync(page, pageSize);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var ticketReply = await _service.GetByIdAsync(id);
            if (ticketReply is null)
                return NotFound(new { message = $"TicketReply with id {id} was not found." });

            return Ok(ticketReply);
        }

        [HttpGet("ticket/{ticketId:int}/replies")]
        [Authorize(Roles = "Admin,Customer,Agent")]
        public async Task<IActionResult> GetRepliesForTicket(int ticketId)
        {
            var replies = await _service.GetRepliesForTicketAsync(ticketId);
            if (!replies.Any())
                return NotFound(new { message = $"No replies found for ticket {ticketId}." });

            return Ok(replies);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Customer,Agent")]
        public async Task<IActionResult> Create([FromBody] CreateTicketReplyDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPatch("{id:int}")]
        [Authorize(Roles = "Admin,Agent")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTicketReplyDTO dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (updated is null)
                return NotFound(new { message = $"TicketReply with id {id} was not found." });

            return Ok(updated);
        }
    }
}