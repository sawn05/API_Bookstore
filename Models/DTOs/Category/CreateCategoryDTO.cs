using System.ComponentModel.DataAnnotations;

namespace API_Bookstore.Models.DTOs.Category
{
    public class CreateCategoryDTO
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(255, ErrorMessage = "Name cannot exceed 255 characters.")]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
