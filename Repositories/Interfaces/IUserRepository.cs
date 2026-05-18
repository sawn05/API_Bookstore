using API_Bookstore.Models.DTOs.User;
using API_Bookstore.Models.Entities;

namespace API_Bookstore.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);

        // Các phương thức khác
        Task<bool> ExistsByEmailAsync(string email, int id);
        Task<bool> ExistsByUsernameAsync(string username, int id);
    }
}
