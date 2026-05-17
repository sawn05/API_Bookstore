using System.ComponentModel.DataAnnotations;

namespace API_Bookstore.Models.DTOs.Category
{
    public class UpdateCategoryDTO
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
