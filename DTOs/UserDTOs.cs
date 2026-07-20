using System.ComponentModel.DataAnnotations;
using HelpDeskAPI.Models;

namespace HelpDeskAPI.DTOs
{
    public class UserDTO
    {
        public int Id {get; set;}
        public string FullName {get; set;} = string.Empty;
        public string Email {get; set;} = string.Empty;
        public UserRole Role {get; set;} = UserRole.Customer;
        public DateTime CreatedAt {get; set;}
    }

    public class UpdateUserDTO
    {
        [StringLength(100)]
        public string? FirstName {get; set;}

        [StringLength(100)]
        public string? LastName {get; set;}
    }
}