namespace API_Bookstore.Models.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // Navigation properties (N-1 to Orders)
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        // Navigation properties (N-1 to Books)
        public Guid BookId { get; set; }
        public Book Book { get; set; } = null!;
    }
}
