namespace API_Bookstore.Models.DTOs.Book
{
    public class BookDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;

    }
}
