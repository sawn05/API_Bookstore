using API_Bookstore.Models.DTOs.User;
using API_Bookstore.Models.Entities;

namespace API_Bookstore.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByNameAsync(string userName);
        Task<User> CreateUserAsync(User user);
        Task<User?> UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);

        // Các phương thức khác
        Task<bool> ExistsByEmailAsync(string email, int excludeId = 0);
        Task<bool> ExistsByUsernameAsync(string username, int excludeId = 0);
    }
}
