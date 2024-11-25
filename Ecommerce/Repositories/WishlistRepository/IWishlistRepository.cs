using Ecommerce.Models;

namespace Ecommerce.Repositories.WishlistRepository
{
    public interface IWishlistRepository
    {
        Task AddAsync(Wishlist wishlist);
        Task<IEnumerable<Wishlist>> GetWishlistByCustomerIdAsync(int customerId);
        Task<Wishlist?> GetWishlistItemAsync(int customerId , int productId); // أضف هذه الطريقة
        Task RemoveAsync(int id);
        Task RemoveAsync(int customerId , int productId);
    }
}
