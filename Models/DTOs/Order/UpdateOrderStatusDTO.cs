using System.ComponentModel.DataAnnotations;

namespace API_Bookstore.Models.DTOs.Order
{
    public class UpdateOrderStatusDTO
    {
        [Required]
        public string Status { get; set; } = string.Empty;
    }
}
