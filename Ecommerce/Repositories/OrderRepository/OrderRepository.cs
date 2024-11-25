using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories.OrderRepository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
            return await context.Orders
               .Include(o => o.OrderDetails)
               .Where(o => o.CustomerId == customerId)
               .ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task AddAsync(Order order)
        {
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            context.Orders.Update(order);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var order = await context.Orders.FindAsync(id);
            if (order != null)
            {
                context.Orders.Remove(order);
                await context.SaveChangesAsync();
            }
        }
        public async Task<bool> CustomerExistsAsync(int customerId)
        {
            return await context.Customers.AnyAsync(c => c.Id == customerId);
        }

        public async Task<bool> ProductExistsAsync(int productId)
        {
            return await context.Products.AnyAsync(p => p.Id == productId);
        }
    }
}