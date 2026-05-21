using System.ComponentModel.DataAnnotations;

namespace API_Bookstore.Models.DTOs.Order
{
    public class CreateOrderDTO
    {
        [Required]
        [MinLength(1, ErrorMessage = "Đơn hàng phải có ít nhất 1 sản phẩm")]
        public List<OrderItemDTO> Items { get; set; } = new();
    }
}
