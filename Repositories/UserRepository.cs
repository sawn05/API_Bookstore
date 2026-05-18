using API_Bookstore.Data;
using API_Bookstore.Models.DTOs.User;
using API_Bookstore.Models.Entities;
using API_Bookstore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API_Bookstore.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) {
            _context = context;
        }
        public async Task DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByEmailAsync(string email, int id)
        {
            return await _context.Users
                .AnyAsync(u => u.Email == email && u.Id != id);
        }

        public async Task<bool> ExistsByUsernameAsync(string username, int id)
        {
            return await _context.Users
                .AnyAsync(u => u.Username == username && u.Id != id);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
