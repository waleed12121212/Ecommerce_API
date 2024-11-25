namespace Ecommerce.DTOs.Wishlist
{
    public class WishlistDto
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
    }
}
