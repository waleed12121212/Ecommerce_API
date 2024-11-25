namespace Ecommerce.DTOs.Order
{
    public class OrderDetailsCreateDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
