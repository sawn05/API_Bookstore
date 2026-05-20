using System.ComponentModel.DataAnnotations;

namespace API_Bookstore.Models.DTOs.Order
{
    public class OrderItemDTO
    {
        [Required]
        public Guid BookId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải ít nhất là 1")]
        public int Quantity { get; set; }
    }
}
