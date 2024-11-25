using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories.ProductRepository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<Product> GetByIdAsync(int id)
        {
            return await context.Products
                .Include(p => p.Category) // تضمين العلاقات إذا كنت بحاجة
                .FirstOrDefaultAsync(p => p.Id == id); // شرط على المعرف
        }
        public async Task<IEnumerable<Product>> GetAllWithCategoriesAsync( )
        {
            return await context.Products
                .Include(p => p.Category) // تضمين بيانات الفئات
                .ToListAsync();
        }
    }
}
