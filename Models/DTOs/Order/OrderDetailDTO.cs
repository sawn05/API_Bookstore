namespace API_Bookstore.Models.DTOs.Order
{
    public class OrderDetailDTO
    {
        public Guid BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; set; } 
    }
}
