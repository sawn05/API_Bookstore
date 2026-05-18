using API_Bookstore.Data;
using API_Bookstore.Models.DTOs.Book;
using API_Bookstore.Models.Entities;
using API_Bookstore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API_Bookstore.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _appDbContext;

        public BookRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Book> CreateBookAsync(Book book)
        {
            _appDbContext.Books.Add(book);
            await _appDbContext.SaveChangesAsync();
            return book;
        }

        public async Task DeleteBookAsync(Book book)
        {
            _appDbContext.Books.Remove(book);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _appDbContext.Books.Include(b => b.Category).ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(Guid id)
        {
            return await _appDbContext.Books.Include(b => b.Category).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<(List<Book> Items, int TotalCount)> GetPagedAsync(BookQueryDTO query)
        {
            // Bắt đầu IQueryable từ DbSet
            var queryable = _appDbContext.Books.Include(b => b.Category).AsQueryable();

            #region Search - Tìm kiếm theo tên hoặc tác giả
            // Search - Tìm kiếm theo tên hoặc tác giả
            if (!string.IsNullOrEmpty(query.Search)){
                var keyword = query.Search.Trim().ToLower();
                queryable = queryable.Where(b => b.Title.ToLower().Contains(keyword) || b.Author.ToLower().Contains(keyword));
            }
            #endregion

            #region Filter - Lọc theo nhiều điều kiện
            // Filter - Lọc theo danh mục
            if (query.CategoryId.HasValue)
            {
                queryable = queryable.Where(b => b.CategoryId == query.CategoryId.Value);
            }

            // Filter - Lọc theo giá
            if (query.Begin.HasValue)
            {
                queryable = queryable.Where(b => (double)b.Price >= query.Begin.Value);
            }
            if (query.End.HasValue)
            {
                queryable = queryable.Where(b => (double)b.Price <= query.End.Value);
            }
            #endregion

            #region Sort - Sắp xếp theo nhiều trường
            var sortBy = query.SortBy?.ToLower();
            var isDesc = query.SortOrder?.ToLower() == "desc";

            // switch expression
            queryable = sortBy switch
            {
                "price" => isDesc
                    ? queryable.OrderByDescending(b => b.Price)
                    : queryable.OrderBy(b => b.Price),

                "title" => isDesc
                    ? queryable.OrderByDescending(b => b.Title)
                    : queryable.OrderBy(b => b.Title),

                "author" => isDesc
                    ? queryable.OrderByDescending(b => b.Author)
                    : queryable.OrderBy(b => b.Author),

                "createddate" => isDesc
                    ? queryable.OrderByDescending(b => b.CreatedAt)
                    : queryable.OrderBy(b => b.CreatedAt),

                "stock" => isDesc
                    ? queryable.OrderByDescending(b => b.Stock)
                    : queryable.OrderBy(b => b.Stock),

                _ => queryable.OrderByDescending(b => b.CreatedAt)
            };
            #endregion

            #region Pagination - Phân trang
            var totalCount = queryable.Count();

            var pageSize = Math.Min(query.PageSize, 100);
            var page = Math.Max(query.PageNumber, 1);

            var items = await queryable
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            #endregion


            return (items, totalCount);
        }

        public async Task<bool> IsBookNameExistsAsync(string name)
        {
            return await _appDbContext.Books.AnyAsync(b => b.Title == name);
        }

        public async Task<Book?> UpdateBookAsync(Book book)
        {
            _appDbContext.Books.Update(book);
            await _appDbContext.SaveChangesAsync();
            return book;
        }
    }
}
