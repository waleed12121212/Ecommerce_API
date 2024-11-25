using Ecommerce.Models;

namespace Ecommerce.Repositories.OrderRepository
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId);
        Task<Order> GetByIdAsync(int id);
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(int id);
        Task<bool> CustomerExistsAsync(int customerId);
        Task<bool> ProductExistsAsync(int productId);
    }
}
