using API_Bookstore.Models.DTOs.Book;
using API_Bookstore.Models.Entities;

namespace API_Bookstore.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(Guid id);
        Task<bool> IsBookNameExistsAsync(string name);
        Task<Book> CreateBookAsync(Book book);
        Task<Book?> UpdateBookAsync(Book book);
        Task DeleteBookAsync(Book book);

        // Các phương thức khác nếu cần thiết
        Task<(List<Book> Items, int TotalCount)> GetPagedAsync(BookQueryDTO query);
    }
}
