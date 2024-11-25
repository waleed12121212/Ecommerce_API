using Ecommerce.Models;

namespace Ecommerce.Repositories.ProductRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllWithCategoriesAsync( );
    }
}
