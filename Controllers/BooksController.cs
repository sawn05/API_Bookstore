using API_Bookstore.Models.DTOs.Book;
using API_Bookstore.Services;
using API_Bookstore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks(
            [FromQuery] BookQueryDTO queryDTO)
        {
            try
            {
                var data = await _bookService.GetPagedAsync(queryDTO);
                return Ok(new
                {
                    success = true,
                    message = "Lấy danh sách sách thành công",
                    data
                });
            } 
            catch(Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Lỗi khi lấy danh sách sách",
                    error = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"Không tìm thấy sách với id {id} "
                });
            }
            return Ok(new
            {
                success = true,
                message = $"Lấy dữ liệu với id {id} thành công",
                book
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateBookDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { success = false, message = "Đầu vào không hợp lệ" });
            try
            {
                var createdBook = await _bookService.CreateAsync(dto);
                return Ok(new
                {
                    success = true,
                    message = "Book created successfully",
                    createdBook
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Lỗi khi tạo mới sách", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await _bookService.DeleteAsync(id);
                return Ok(new
                {
                    success = true,
                    message = $"Sách với id {id} đã được xóa thành công"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Lỗi khi xóa sách",
                    error = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateBookDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { success = false, message = "Đầu vào không hợp lệ" });
            try
            {
                var updatedBook = await _bookService.UpdateAsync(id, dto);
                return Ok(new
                {
                    success = true,
                    message = "Cập nhật thành công",
                    updatedBook
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Lỗi khi cập nhật sách",
                    error = ex.Message
                });
            }
        }
    }
}
