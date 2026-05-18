using API_Bookstore.Models.DTOs.User;
using API_Bookstore.Models.Entities;
using API_Bookstore.Repositories;
using API_Bookstore.Repositories.Interfaces;
using API_Bookstore.Services.Interfaces;

namespace API_Bookstore.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) {
            _userRepository = userRepository;
        }
        public async Task DeleteAsync(int id)
        {
            // Kiểm tra user có tồn tại không
            var existsUser = await _userRepository.GetUserByIdAsync(id);
            if (existsUser == null)
                throw new KeyNotFoundException($"Không tìm thấy người dùng với id {id}");

            await _userRepository.DeleteUserAsync(existsUser);
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            var allUser = await _userRepository.GetAllUsersAsync();
            return allUser.Select(u => ToDTO(u)).ToList();
        }

        public async Task<UserDTO?> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return user == null ? null : ToDTO(user);
        }

        public async Task<UserDTO?> UpdateAsync(int id, UpdateUserDTO user)
        {
            // Kiểm tra user có tồn tại không
            var existsUser = await _userRepository.GetUserByIdAsync(id);
            if (existsUser == null)
                return null;

            // Kiểm tra email và username mới không được trùng
            if (await _userRepository.ExistsByEmailAsync(user.Email, id))
                throw new Exception("Email đã tồn tại");
            if (await _userRepository.ExistsByUsernameAsync(user.Username, id))
                throw new Exception("Username đã tồn tại");

            // Cập nhật
            existsUser.Username = user.Username;
            existsUser.Email = user.Email;

            var updatedUser = await _userRepository.UpdateUserAsync(existsUser);
            return ToDTO(updatedUser);
        }

        private UserDTO ToDTO(User u) => new UserDTO
        {
            Id = u.Id,
            Username = u.Username,
            Email = u.Email,
            Role = u.Role,
            CreatedAt = u.CreatedAt
        };
    }
}
