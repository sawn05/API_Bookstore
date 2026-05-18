using System.ComponentModel.DataAnnotations;

namespace API_Bookstore.Models.DTOs.User
{
    public class UpdateUserDTO
    {
        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
    }
}
