namespace API_Bookstore.Models.DTOs.Common
{
    public class PagedResultDTO<T>
    {
        public List<T> Items { get; set; } = new();
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
