using System.ComponentModel.DataAnnotations;

namespace API_Bookstore.Models.DTOs.Category
{
    public class CreateCategoryDTO
    {
        [Required(ErrorMessage = "Tên là bắt buộc.")]
        [MaxLength(255, ErrorMessage = "Tên không vượt quá 255 kí tự.")]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
