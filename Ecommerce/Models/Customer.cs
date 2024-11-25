namespace Ecommerce.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // علاقة 1 إلى عدة مع الطلبات
        public ICollection<Order> Orders { get; set; }

        // علاقة عدة إلى عدة مع Wishlist
        public ICollection<Wishlist> Wishlists { get; set; }
    }
}
