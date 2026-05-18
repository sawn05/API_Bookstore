using API_Bookstore.Models.DTOs.User;
using API_Bookstore.Services;
using API_Bookstore.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService) {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            var data = await _userService.GetAllAsync();
            return Ok(new
            {
                success = true,
                message = "Lấy dữ liệu thành công",
                data
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"Không tìm thấy người dùng có id {id}"
                });
            }
            return Ok(new
            {
                success = true,
                message = $"Lấy dữ liệu với id {id} thành công",
                user
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDTO dto)
        {
            if (id <= 0)
                return BadRequest(new { success = false, message = "Id không tồn tại" });

            if (!ModelState.IsValid)
                return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ" });

            try
            {
                var updatedUser = await _userService.UpdateAsync(id, dto);

                if (updatedUser == null)
                {
                    return NotFound(new { success = false, message = $"Không tìm thấy người dùng với id {id}" });
                }

                return Ok(new
                {
                    success = true,
                    message = "Cập nhật người dùng thành công",
                    user = updatedUser
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Lỗi khi cập nhật thông tin người dùng", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ" });
            try
            {
                await _userService.DeleteAsync(id);
                return Ok(new { success = true, message = "Xóa thành công" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Lỗi khi xóa thông tin người dùng", error = ex.Message });
            }
        }
    }
}
