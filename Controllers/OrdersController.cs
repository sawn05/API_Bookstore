using API_Bookstore.Models.DTOs.Order;
using API_Bookstore.Models.Entities;
using API_Bookstore.Services;
using API_Bookstore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API_Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService) {
            _orderService = orderService;
        }

        // Helper: Lấy UserId từ JWT Token
        private int GetCurrentUserId()
        {
            return int.Parse(User.FindFirstValue("Id")!);
        }

        // Helper: lấy Role từ JWT token
        private string GetCurrentUserRole()
            => User.FindFirstValue(ClaimTypes.Role)!;

        // GET ALL ORDER - GET /api/orders?status=Pending&page=1&pageSize=10
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOrder(
            [FromQuery] string? status,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            if (page < 1 || pageSize < 1)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Page và pageSize phải >= 1"
                });
            }

            var data = await _orderService.GetAllOrdersAsync(status, page, pageSize);

            return Ok(new
            {
                success = true,
                messgae = "Lấy dữ liệu thành công",
                data = data.Items,
                pagination = new
                {
                    data.CurrentPage,
                    data.PageSize,
                    data.TotalItems,
                    data.TotalPages
                }
            });
        }

        // GET my-orders - GET /api/orders/my-orders?page=1&pageSize=4
        [HttpGet("my-orders")]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> GetMyOrders(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 4)
        {
            if (page < 1 || pageSize < 1)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Page và pageSize phải >= 1"
                });
            }

            var userId = GetCurrentUserId();
            var data = await _orderService.GetOrderByUserIdAsync(userId, page, pageSize);

            return Ok(new
            {
                success = true,
                messgae = "Lấy dữ liệu thành công",
                data = data.Items,
                pagination = new
                {
                    data.CurrentPage,
                    data.PageSize,
                    data.TotalItems,
                    data.TotalPages
                }
            });
        }

        // GET BY ID - GET /api/orders/{orderId}
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetById(int orderId)
        {
            try
            {
                var data = await _orderService.GetByIdAsync(orderId, GetCurrentUserId(), GetCurrentUserRole());

                if (data == null) return NotFound(new
                {
                    sucess = false,
                    message = $"Không tìm thấy đơn hàng có id là {orderId}"
                });

                return Ok(new
                {
                    sucess = true,
                    message = "Lấy dữ liệu thành công",
                    data = data
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, new { success = false, message = ex.Message });
            }
        }

        // CREATE ORDER - User - POST /api/orders
        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(new
            {
                success = false,
                message = "Dữ liệu đầu vào không hợp lệ"
            });

            try
            {
                var userId = GetCurrentUserId();
                var data = await _orderService.CreateOrderAsync(userId, dto);

                return CreatedAtAction(
                    nameof(GetById), 
                    new { id = data.Id },
                    new
                    {
                        success = true,
                        message = "Tạo đơn hàng thành công",
                        data
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Lỗi khi tạo đơn hàng",
                    error = ex.Message
                });
            }
        }


        // UPDATE STATUS ORDER - Admin - PUT /api/orders/{orderId}/status
        [HttpPut("{orderId}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStatus(int orderId, [FromBody] UpdateOrderStatusDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(new
            {
                success = false,
                message = "Dữ liệu đầu vào không hợp lệ"
            });

            try
            {
                var data = await _orderService.UpdateStatusOrderAsync(orderId, dto);
                if (data == null) return NotFound(new
                {
                    success = false,
                    message = $"Không tìm thấy đơn hàng với id là {orderId}"
                });

                return Ok(new
                {
                    success = true,
                    message = "Cập nhật thành công",
                    data = data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Lỗi khi cập nhật đơn hàng",
                    error = ex.Message
                });
            }
        }
    }
}
