namespace Ecommerce.DTOs.Order
{
    public class OrderCreateDto
    {
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderDetailsCreateDto> details { get; set; }
    }
}
