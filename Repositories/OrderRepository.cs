using API_Bookstore.Data;
using API_Bookstore.Models.Entities;
using API_Bookstore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace API_Bookstore.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context) {
            _context = context;
        }

        public async Task<int> CountAllAsync(string? status)
        {
            var query = _context.Orders.AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(o => o.Status == status);
            }

            return await query.CountAsync();
        }

        public async Task<int> CountOrderByUserIdAsync(int userId)
        {
            return await _context.Orders
                .CountAsync(o => o.UserId == userId);
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order; 
        }

        // Lấy tất cả — Admin, filter theo status nếu có
        public async Task<List<Order>> GetAllAsync(string? status, int page, int pageSize)
        {
            var query = _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Book) // Load được cả Book
                .AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(o => o.Status == status);
            }

            // result + pagination
            return await query
                .OrderByDescending(o => o.OrderDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        // Lấy tất cả đơn hàng theo UserId - Feature history order
        public async Task<List<Order>> GetOrderByUserIdAsync(int userId, int page, int pageSize)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Book)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        // Lấy chi tiết 1 đơn
        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            // Vì liên kết với user và orderdetails nên phải include 2 cái
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Book)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order> UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }
    }
}
