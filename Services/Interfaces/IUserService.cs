using API_Bookstore.Models.DTOs.User;

namespace API_Bookstore.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllAsync();
        Task<UserDTO?> GetByIdAsync(int id);
        Task<UserDTO?> UpdateAsync(int id, UpdateUserDTO user);
        Task DeleteAsync(int id);
    }
}
