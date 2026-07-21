using HelpDeskAPI.DTOs;
using HelpDeskAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HelpDeskAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _service;

        public TicketController(ITicketService service) => _service = service;

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
            var ticket = await _service.GetByIdAsync(id);
            if (ticket is null)
                return NotFound(new { message = $"Ticket with id {id} was not found." });

            return Ok(ticket);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> Create([FromBody] CreateTicketDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPatch("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTicketDTO dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (updated is null)
                return NotFound(new { message = $"Ticket with id {id} was not found." });

            return Ok(updated);
        }

        [HttpGet("tickets")]
        public async Task<IActionResult> GetTickets()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var roleClaim = User.FindFirst(ClaimTypes.Role);

            if (userIdClaim == null || roleClaim == null)
            {
                return Unauthorized("Required claims are missing.");
            }

            int userId = int.Parse(userIdClaim.Value);
            string role = roleClaim.Value;

            if (role == "Agent")
                return Ok(await _service.GetTicketsForAgentAsync(userId));

            if (role == "Customer")
                return Ok(await _service.GetTicketsForCustomerAsync(userId));

            if (role == "Admin")
                return Ok(await _service.GetAllAsync(1, 100));

            return Forbid();
        }
    }
}