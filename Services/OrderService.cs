using API_Bookstore.Data;
using API_Bookstore.Models.DTOs.Common;
using API_Bookstore.Models.DTOs.Order;
using API_Bookstore.Models.Entities;
using API_Bookstore.Repositories;
using API_Bookstore.Repositories.Interfaces;
using API_Bookstore.Services.Interfaces;
using Azure;

namespace API_Bookstore.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBookRepository _bookRepository;
        private readonly AppDbContext _context;

        // Inject AppDbContext để dùng transaction
        public OrderService(
            IOrderRepository orderRepository,
            IBookRepository bookRepository,
            AppDbContext context)
        {
            _orderRepository = orderRepository;
            _bookRepository = bookRepository;
            _context = context;
        }

        public async Task<OrderDTO> CreateOrderAsync(int currentUserId, CreateOrderDTO dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Validate item -> đổ dữ liệu book vào OrderDetail
                var orderDetails = new List<OrderDetail>();
                decimal totalAmount = 0;

                foreach (var item in dto.Items)
                {
                    var book = await _bookRepository.GetBookByIdAsync(item.BookId);

                    if (book == null)
                    {
                        throw new Exception($"Không tìm thấy sản phẩm có id {item.BookId}");
                    }

                    if (book.Stock < item.Quantity)
                    {
                        throw new Exception($"Sản phẩm {book.Title} không có đủ số lượng. Tồn kho: {book.Stock}");
                    }

                    // Tạo OrderDetail - lưu UnitPrice tại thời điểm đặt
                    orderDetails.Add(new OrderDetail
                    {
                        BookId = book.Id,
                        Quantity = item.Quantity,
                        UnitPrice = book.Price
                    });

                    totalAmount += item.Quantity * book.Price;

                    // Trừ stock
                    book.Stock -= item.Quantity;    
                    await _bookRepository.UpdateBookAsync(book);
                }

                // Tạo Order
                var order = new Order
                {
                    UserId = currentUserId,
                    TotalAmount = totalAmount,
                    Status = "Pending",
                    OrderDate = DateTime.UtcNow,
                    OrderDetails = orderDetails
                };

                var created = await _orderRepository.CreateOrderAsync(order);

                // Commit transaction
                await transaction.CommitAsync();

                // Load lại dữ liệu để map sang DTO
                var data = await _orderRepository.GetOrderByIdAsync(created.Id);

                return ToDTO(data);
            }
            catch // Có lỗi thì rollback toàn bộ dữ liệu
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        // Role : Admin
        public async Task<PagedResultDTO<OrderDTO>> GetAllOrdersAsync(string? status, int page, int pageSize)
        {
            var order = await _orderRepository.GetAllAsync(status, page, pageSize);
            var total = await _orderRepository.CountAllAsync(status);

            return new PagedResultDTO<OrderDTO> {
                Items = order.Select(o => ToDTO(o)).ToList(),
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = total
            };
        }

        // Map entity to DTO
        private OrderDTO ToDTO(Order o) => new OrderDTO
        {
            Id = o.Id,
            UserId = o.UserId,
            UserName = o.User?.Username ?? string.Empty,
            TotalAmount = o.TotalAmount,
            Status = o.Status,
            OrderDate = o.OrderDate,
            OrderDetails = o.OrderDetails.Select(od => new OrderDetailDTO
            {
                BookId = od.BookId,
                BookTitle = od.Book?.Title ?? string.Empty,
                Quantity = od.Quantity,
                UnitPrice = od.UnitPrice,
                SubTotal = od.Quantity * od.UnitPrice
            }).ToList(),
        };

        public async Task<OrderDTO?> GetByIdAsync(int orderId, int currentUserId, string currentUserRole)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);

            // User chỉ được xem những đơn hàng của mình
            if (currentUserRole != "Admin" && order.UserId != currentUserId)
            {
                throw new Exception("Bạn không được phép xem đơn hàng này");
            }

            return ToDTO(order);
        }

        public async Task<PagedResultDTO<OrderDTO>> GetOrderByUserIdAsync(int userId, int page, int pageSize)
        {
            var orders = await _orderRepository.GetOrderByUserIdAsync(userId, page, pageSize);
            var total = await _orderRepository.CountOrderByUserIdAsync(userId);

            return new PagedResultDTO<OrderDTO>
            {
                Items = orders.Select(o => ToDTO(o)).ToList(),
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = total
            };
        }

        public async Task<OrderDTO?> UpdateStatusOrderAsync(int orderId, UpdateOrderStatusDTO dto)
        {
            var allowedStatus = new[] { "Pending", "Cancelled", "Confirmed" };
            if (!allowedStatus.Contains(dto.Status))
            {
                throw new Exception($"Trạng thái đơn hàng không hợp lệ. Giá trị được cho phép: {string.Join(", ", allowedStatus)}");
            }

            // Kiểm tra đơn hàng đang cập nhật có tồn tại không
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null) return null;

            // Nếu từ Confirmed -> Cancelled thì hoàn trả stock
            if (dto.Status == "Cancelled" && order.Status == "Confirmed")
            {
                foreach (var detail in order.OrderDetails)
                {
                    var book = await _bookRepository.GetBookByIdAsync(detail.BookId);
                    if (book != null)
                    {
                        book.Stock += detail.Quantity;
                        await _bookRepository.UpdateBookAsync(book);
                    }
                }
            }

            order.Status = dto.Status;
            var updated = await _orderRepository.UpdateOrderAsync(order);
            return ToDTO(updated);
        }
    }
}
