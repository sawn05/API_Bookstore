using API_Bookstore.Configurations;
using API_Bookstore.Helpers;
using API_Bookstore.Models.DTOs.Auth;
using API_Bookstore.Models.Entities;
using API_Bookstore.Repositories;
using API_Bookstore.Repositories.Interfaces;
using API_Bookstore.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace API_Bookstore.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtHelper _jwtHelper;
        private readonly JwtSettings _jwtSettings;

        public AuthService(IUserRepository userRepository, JwtHelper jwtHelper, JwtSettings jwtSettings) {
            _userRepository = userRepository;
            _jwtHelper = jwtHelper;
            _jwtSettings = jwtSettings;
        }

        public async Task<AuthResponseDTO> RegisterAsync(RegisterDTO dto)
        {
            if (await _userRepository.ExistsByEmailAsync(dto.Email))
            {
                throw new Exception("Email đã tồn tại");
            }

            if (await _userRepository.ExistsByUsernameAsync(dto.Username))
            {
                throw new Exception("Tên tài khoản đã tồn tại");
            }

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = PasswordHelper.Hash(dto.Password),
                Role = "User" // mặc định register là User
            };

            var created = await _userRepository.CreateUserAsync(user);
            return BuildResponse(user);
        }

        public async Task<AuthResponseDTO> LoginAsync(LoginDTO dto)
        {
            var user = await _userRepository.GetUserByNameAsync(dto.Username);
            if (user == null || !PasswordHelper.Verify(dto.Password, user.PasswordHash))
            {
                throw new Exception("Tên tài khoản hoặc mật khẩu không chính xác");
            }

            return BuildResponse(user);
        }

        private AuthResponseDTO BuildResponse(User user)
        {
            var token = _jwtHelper.GenerateToken(user);
            return new AuthResponseDTO
            {
                Token = token,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                ExpiresAt = DateTime.UtcNow.AddHours(_jwtSettings.ExpiresInHours)
            };
        }
    }
}
