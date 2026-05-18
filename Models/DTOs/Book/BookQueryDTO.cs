namespace API_Bookstore.Models.DTOs.Book
{
    public class BookQueryDTO
    {
        // Search - Tìm kiếm theo tên sách, tác giả
        public string? Search { get; set; }

        // Filter - Lọc theo giá sách
        public double? Begin { get; set; }
        public double? End { get; set; }

        // Filter - Lọc theo category
        public int? CategoryId { get; set; }

        // Sort - Sắp xếp theo giá sách, tên sách, tác giả, ngày tạo, số lượng kho v.v.
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; } // "asc" hoặc "desc"

        // Pagination - Phân trang
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
}
