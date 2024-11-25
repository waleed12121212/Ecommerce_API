using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories.WishlistRepository
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly ApplicationDbContext context;

        public WishlistRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        // إضافة عنصر إلى قائمة التفضيلات
        public async Task AddAsync(Wishlist wishlist)
        {
            await context.Wishlists.AddAsync(wishlist);
            await context.SaveChangesAsync();
        }

        // استرجاع قائمة التفضيلات بناءً على معرف العميل
        public async Task<IEnumerable<Wishlist>> GetWishlistByCustomerIdAsync(int customerId)
        {
            return await context.Wishlists
                .Include(w => w.Product) // تضمين بيانات المنتج
                .Where(w => w.CustomerId == customerId)
                .ToListAsync();
        }

        // استرجاع عنصر من قائمة التفضيلات بناءً على customerId و productId
        public async Task<Wishlist?> GetWishlistItemAsync(int customerId , int productId)
        {
            return await context.Wishlists
                .FirstOrDefaultAsync(w => w.CustomerId == customerId && w.ProductId == productId);
        }

        // حذف عنصر بناءً على معرف العنصر (Id)
        public async Task RemoveAsync(int id)
        {
            var wishlistItem = await context.Wishlists.FindAsync(id);
            if (wishlistItem != null)
            {
                context.Wishlists.Remove(wishlistItem);
                await context.SaveChangesAsync();
            }
        }

        // حذف عنصر بناءً على customerId و productId
        public async Task RemoveAsync(int customerId , int productId)
        {
            // جلب العنصر بناءً على المفتاح المركب
            var wishlistItem = await context.Wishlists
                .FirstOrDefaultAsync(w => w.CustomerId == customerId && w.ProductId == productId);

            // التحقق من وجود العنصر
            if (wishlistItem != null)
            {
                context.Wishlists.Remove(wishlistItem);
                await context.SaveChangesAsync();
            }
        }
    }
}
