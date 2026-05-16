namespace API_Bookstore.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
        public DateTime CreatedAt { get; set; }

        // Navigation properties (1-N to Orders)
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
