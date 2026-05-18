
using API_Bookstore.Models.DTOs.Book;
using API_Bookstore.Models.DTOs.Common;

namespace API_Bookstore.Services.Interfaces
{
    public interface IBookService
    {
        Task<List<BookDTO>> GetAllAsync();
        Task<BookDTO?> GetByIdAsync(Guid id);
        Task<BookDTO> CreateAsync(CreateBookDTO bookCreateDTO);
        Task<BookDTO?> UpdateAsync(Guid id, UpdateBookDTO bookUpdateDTO);
        Task DeleteAsync(Guid id);

        // Các phương thức khác nếu cần thiết
        Task<PagedResultDTO<BookDTO>> GetPagedAsync(BookQueryDTO query);
    }
}
