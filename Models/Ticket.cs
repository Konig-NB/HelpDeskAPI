using System.ComponentModel.DataAnnotations;

namespace HelpDeskAPI.Models
{
    public enum Status
    {
        Open,
        InProgress,
        Resolved,
        Closed
    }
    public enum Priority
    {
        Low,
        Medium,
        High
    }
    public class Ticket
    {
        public int Id {get; set;}

        [Required,StringLength(100,MinimumLength = 5)]
        public string Title {get; set;} = string.Empty;

        [Required,StringLength(1000,MinimumLength = 10)]
        public string Description {get; set;} = string.Empty;

        [Required]
        public int CategoryId {get; set;}
        public Category Category {get; set;} = null!;

        [Required]
        public Status TicketStatus {get; set;} = Status.Open;

        [Required]
        public Priority TicketPriority {get; set;} = Priority.Medium;

        [Required]
        public int CreatedById {get; set;}
        public User CreatedBy {get; set;} = null!;
        public int? AssignedToId {get; set;}
        public User? AssignedTo {get; set;} = null!;

        [Required]
        public DateTime CreatedAt {get; set;}
        public DateTime? UpdatedAt {get; set;}
    }
}