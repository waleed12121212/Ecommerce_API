using AutoMapper;
using Ecommerce.DTOs.Order;
using Ecommerce.Models;
using Ecommerce.Repositories.OrderRepository;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository repository;
        private readonly IMapper mapper;

        public OrderController(IOrderRepository repository , IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        // GET: api/orders/customer/{customerId}
        [HttpGet("customer")]
        public async Task<IActionResult> GetOrdersByCustomerId(int customerId)
        {
            var orders = await repository.GetOrdersByCustomerIdAsync(customerId);
            if (orders == null || !orders.Any()) return NotFound("No orders found for this customer.");

            var orderDtos = mapper.Map<IEnumerable<OrderDto>>(orders);
            return Ok(orderDtos);
        }

        // GET: api/orders/{id}
        [HttpGet("getById")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await repository.GetByIdAsync(id);
            if (order == null) return NotFound("Order not found.");

            var orderDto = mapper.Map<OrderDto>(order);
            return Ok(orderDto);
        }

        // POST: api/orders
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderCreateDto dto)
        {
            // تحقق من صحة البيانات المرسلة
            if (dto == null || dto.details == null || !dto.details.Any())
                return BadRequest("Invalid order data.");

            // تحقق من وجود CustomerId
            var customerExists = await repository.CustomerExistsAsync(dto.CustomerId);
            if (!customerExists)
                return NotFound($"Customer with ID {dto.CustomerId} not found.");

            // تحقق من وجود كل المنتجات
            foreach (var detail in dto.details)
            {
                var productExists = await repository.ProductExistsAsync(detail.ProductId);
                if (!productExists)
                    return NotFound($"Product with ID {detail.ProductId} not found.");
            }

            // إنشاء الطلب
            var order = mapper.Map<Order>(dto);
            await repository.AddAsync(order);

            var orderDto = mapper.Map<OrderDto>(order);
            return CreatedAtAction(nameof(GetOrderById) , new { id = order.Id } , orderDto);
        }
    }
}
