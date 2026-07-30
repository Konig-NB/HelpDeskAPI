using System.ComponentModel.DataAnnotations;

namespace HelpDeskAPI.Models
{
    public class TicketReply
    {
        public int Id {get; set;}

        [Required]
        public int TicketId {get; set;}
        public Ticket Ticket {get; set;} = null!;

        [Required]
        public int UserId {get; set;}
        public User User {get; set;} = null!;

        [Required,StringLength(1000)]
        public string Message {get; set;} = string.Empty;
        public DateTime CreatedAt {get; set;}
    }
}