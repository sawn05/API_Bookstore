using API_Bookstore.Models.DTOs.Book;
using API_Bookstore.Models.DTOs.Common;
using API_Bookstore.Models.Entities;
using API_Bookstore.Repositories;
using API_Bookstore.Repositories.Interfaces;
using API_Bookstore.Services.Interfaces;

namespace API_Bookstore.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private ICategoryRepository _categoryRepository;

        public BookService(IBookRepository bookRepository, ICategoryRepository categoryRepository)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<BookDTO> CreateAsync(CreateBookDTO bookCreateDTO)
        {
            // Kiểm tra xem tên sách đã tồn tại hay chưa
            if (await _bookRepository.IsBookNameExistsAsync(bookCreateDTO.Title))
            {
                throw new InvalidOperationException($"Sách với tiêu đề '{bookCreateDTO.Title}' đã tồn tại.");
            }

            // Kiểm tra xem categoryId có tồn tại hay không
            var category = await _categoryRepository.GetCategoryByIdAsync(bookCreateDTO.CategoryId);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category với id {bookCreateDTO.CategoryId} không tồn tại.");
            }

            // Nếu không có lỗi, tạo sách mới
            var newBook = new Book
            {
                Title = bookCreateDTO.Title,
                Author = bookCreateDTO.Author,
                Description = bookCreateDTO.Description,
                Price = (decimal)(bookCreateDTO.Price ?? throw new InvalidOperationException("Giá không được trống.")),
                Stock = bookCreateDTO.Stock ?? throw new InvalidOperationException("Số lượng kho không dược trống."),
                CategoryId = bookCreateDTO.CategoryId
            };

            var created = await _bookRepository.CreateBookAsync(newBook);
            created.Category = category;
            return ToDTO(created);
        }

        public async Task DeleteAsync(Guid id)
        {
            // Lấy sách cần xóa
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                throw new KeyNotFoundException($"Sách với id {id} không tồn tại.");
            }

            await _bookRepository.DeleteBookAsync(book);
        }

        public async Task<List<BookDTO>> GetAllAsync()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            return books.Select(b => ToDTO(b)).ToList();
        }

        public async Task<BookDTO?> GetByIdAsync(Guid id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            return book == null ? null : ToDTO(book);
        }

        public async Task<PagedResultDTO<BookDTO>> GetPagedAsync(BookQueryDTO query)
        {
            // Validate đầu vào
            if (query.Begin.HasValue && query.End.HasValue
                && query.Begin > query.End)
            {
                throw new Exception("Giá trị không hợp lệ");
            }

            var (items, totalCount) = await _bookRepository.GetPagedAsync(query);

            var pageSize = Math.Min(query.PageSize, 100);
            var page = Math.Max(query.PageNumber, 1);

            return new PagedResultDTO<BookDTO>
            {
                Items = items.Select(b => ToDTO(b)).ToList(),
                TotalItems = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                PageSize = pageSize,
                CurrentPage = page
            };
        }

        public async Task<BookDTO?> UpdateAsync(Guid id, UpdateBookDTO bookUpdateDTO)
        {
            // Lấy sách cần cập nhật
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                throw new KeyNotFoundException($"Sách với id {id} không tồn tại.");
            }

            // Kiểm tra category 
            var category = await _categoryRepository.GetCategoryByIdAsync(bookUpdateDTO.CategoryId);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category với id {bookUpdateDTO.CategoryId} không tồn tại.");
            }

            // Kiểm tra trùng tên (bỏ qua sách hiện tại)
            if (bookUpdateDTO.Title != book.Title && 
                await _bookRepository.IsBookNameExistsAsync(bookUpdateDTO.Title))
            {
                throw new InvalidOperationException($"Sách với tiêu đề '{bookUpdateDTO.Title}' đã tồn tại.");
            }

            // Bước 4: Cập nhật thông tin sách
            book.Title = bookUpdateDTO.Title;
            book.Author = bookUpdateDTO.Author;
            book.Description = bookUpdateDTO.Description;
            book.Price = (decimal)(bookUpdateDTO.Price ?? throw new InvalidOperationException("Giá không được trống."));
            book.Stock = bookUpdateDTO.Stock ?? throw new InvalidOperationException("Số lượng kho không được trống.");
            book.CategoryId = bookUpdateDTO.CategoryId;

            // Bước 5: Lưu vào database và trả về
            var updated = await _bookRepository.UpdateBookAsync(book);
            updated.Category = category;
            return ToDTO(updated);
        }

        private BookDTO ToDTO(Book b) => new BookDTO
        {
            Id = b.Id,
            Title = b.Title,
            Author = b.Author,
            Description = b.Description,
            Price = b.Price,
            Stock = b.Stock,
            CategoryId = b.CategoryId,
            CategoryName = b.Category?.Name ?? string.Empty,
            CreatedAt = b.CreatedAt
        };
    }
}
