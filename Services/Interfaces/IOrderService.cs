using API_Bookstore.Models.DTOs.Common;
using API_Bookstore.Models.DTOs.Order;

namespace API_Bookstore.Services.Interfaces
{
    public interface IOrderService
    {
        Task<PagedResultDTO<OrderDTO>> GetAllOrdersAsync(string? status, int page, int pageSize);
        Task<PagedResultDTO<OrderDTO>> GetOrderByUserIdAsync(int userId, int page, int pageSize);
        Task<OrderDTO?> GetByIdAsync(int id, int currentUserId, string currentUserRole);
        Task<OrderDTO> CreateOrderAsync(int currentUserId, CreateOrderDTO dto);
        Task<OrderDTO?> UpdateStatusOrderAsync(int orderId, UpdateOrderStatusDTO dto);
    }
}
