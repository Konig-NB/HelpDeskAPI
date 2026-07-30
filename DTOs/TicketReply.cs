using System.ComponentModel.DataAnnotations;

namespace HelpDeskAPI.DTOs
{
    public class CreateTicketReplyDTO
    {
        [Required]
        public int TicketId {get; set;}

        [Required]
        public int UserId {get; set;}

        [Required,StringLength(1000)]
        public string Message {get; set;} = string.Empty;
    }

    public class UpdateTicketReplyDTO
    {
        [Required,StringLength(1000)]
        public string Message {get; set;} = string.Empty;
    }

    public class TicketReplyDTO
    {
        public int Id {get; set;}
        public int TicketId {get; set;}
        public string TicketTitle {get; set;} = string.Empty;
        public int UserId {get; set;}
        public string UserName {get; set;} = string.Empty;
        public string Message {get; set;} = string.Empty;
        public DateTime CreatedAt {get; set;}
    }
}