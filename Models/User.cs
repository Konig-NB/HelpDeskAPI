using System.ComponentModel.DataAnnotations;

namespace HelpDeskAPI.Models
{
    public enum UserRole
    {
        Admin,
        Agent,
        Customer
    }
    public class User
    {
        public int Id {get; set;}

        [Required,StringLength(100)]
        public string FirstName {get; set;} = string.Empty;

        [Required,StringLength(100)]
        public string LastName {get; set;} = string.Empty;

        [Required,EmailAddress,StringLength(200)]
        public string Email {get; set;} = string.Empty;
        
        [Required]
        public string PasswordHash {get; set;} = string.Empty;
        public UserRole Role {get; set;} = UserRole.Customer;
        public DateTime CreatedAt {get; set;} = DateTime.UtcNow;

        public ICollection<Ticket> CreatedTickets { get; set; } = new List<Ticket>();
        public ICollection<Ticket> AssignedTickets { get; set; } = new List<Ticket>();
    }
}