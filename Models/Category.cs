using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskAPI.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Category
    {
        public int Id {get; set;}

        [Required,StringLength(100)]
        public string Name {get; set;} = string.Empty;

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}