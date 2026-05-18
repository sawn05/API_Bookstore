using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Bookstore.Models.Entities
{
    public class Book
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string? Description { get; set; }

        public decimal Price { get; set; }
        public int Stock { get; set; }  
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property (N-1 to Categories)
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        // Navigation property (1-N to OrderDetails)
        public ICollection<OrderDetail>? OrderDetails { get; set; } = new List<OrderDetail>();

    }
}
