namespace Ecommerce.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        // علاقة 1 إلى عدة مع التصنيفات
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        // علاقة عدة إلى عدة مع Wishlist
        public ICollection<Wishlist> Wishlists { get; set; }
    }
}
