using API_Bookstore.Models.Entities;

namespace API_Bookstore.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        // Lấy tất cả đơn hàng - Admin (filter + pagination)
        Task<List<Order>> GetAllAsync(string? status, int page, int pageSize);
        Task<int> CountAllAsync(string? status);

        // Lấy đơn hàng của 1 user (pagination)
        Task<List<Order>> GetOrderByUserIdAsync(int userId, int page, int pageSize);
        Task<int> CountOrderByUserIdAsync(int userId);

        // Lấy chi tiết 1 đơn hàng
        Task<Order?> GetOrderByIdAsync(int id);

        // Tạo đơn hàng
        Task<Order> CreateOrderAsync(Order order);

        // Cập nhật đơn hàng (status, hoàn trả stock)
        Task<Order> UpdateOrderAsync(Order order);
    }
}
