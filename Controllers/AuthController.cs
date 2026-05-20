using API_Bookstore.Models.DTOs.Auth;
using API_Bookstore.Services;
using API_Bookstore.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) 
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Invalid input" });
            }

            try
            {
                var data = _authService.RegisterAsync(dto);
                return Ok(new
                {
                    Sucess = true,
                    Message = "Đăng kí thành công",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Lỗi khi đăng kí tài khoản", error = ex.Message });
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Invalid input" });
            }

            try
            {
                var data = _authService.LoginAsync(dto);
                return Ok(new
                {
                    Sucess = true,
                    Message = "Đăng nhập thành công",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Lỗi khi đăng nhập tài khoản", error = ex.Message });
            }
        }
    }
}
