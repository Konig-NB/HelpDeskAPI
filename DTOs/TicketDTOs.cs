using System.ComponentModel.DataAnnotations;
using HelpDeskAPI.Models;

namespace HelpDeskAPI.DTOs
{
    public class CreateTicketDTO
    {
        [Required,StringLength(100,MinimumLength = 5)]
        public string Title {get; set;} = string.Empty;

        [Required,StringLength(1000,MinimumLength = 10)]
        public string Description {get; set;} = string.Empty;

        [Required]
        public int CategoryId {get; set;}

        [Required]
        public Status TicketStatus {get; set;} = Status.Open;

        [Required]
        public Priority TicketPriority {get; set;} = Priority.Medium;

        [Required]
        public int CreatedById {get; set;}
    }

    public class UpdateTicketDTO
    {
        [StringLength(100,MinimumLength = 5)]
        public string? Title {get; set;}

        [StringLength(1000,MinimumLength = 10)]
        public string? Description {get; set;}
        public int? CategoryId {get; set;}
        public Status? TicketStatus {get; set;}
        public Priority? TicketPriority {get; set;}
        public int? CreatedById {get; set;}
        public int? AssignedToId {get; set;}
    }

    public class TicketDTO
    {
        public int Id {get; set;}
        public string Title {get; set;} = string.Empty;
        public string Description {get; set;} = string.Empty;
        public int CategoryId {get; set;}
        public string CategoryName {get; set;} = string.Empty;
        public Status TicketStatus {get; set;} = Status.Open;
        public Priority TicketPriority {get; set;} = Priority.Medium;
        public int CreatedById {get; set;}
        public string CustomerName {get; set;} = string.Empty;
        public int? AssignedToId {get; set;}
        public string? AgentName {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime? UpdatedAt {get; set;}
    }
}