namespace API_Bookstore.Models.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending"; // "Pending" | "Confirmed" | "Cancelled"

        // Navigation property (N-1 to Users)
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        // Navigation property (1-N to OrderDetails)
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
