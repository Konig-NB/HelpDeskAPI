using System.ComponentModel.DataAnnotations;

namespace HelpDeskAPI.DTOs
{
    public class CreateCategoryDTO
    {
        [Required,StringLength(100)]
        public string Name {get; set;} = string.Empty;
    }

    public class UpdateCategoryDTO
    {
        [StringLength(100)]
        public string? Name {get; set;} = string.Empty;
    }

    public class CategoryDTO
    {
        public int Id {get; set;}
        public string Name {get; set;} = string.Empty;
    }
}