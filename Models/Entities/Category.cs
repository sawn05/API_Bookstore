namespace API_Bookstore.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        // Navigation property (1-N to Book)
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
