﻿namespace Ecommerce.DTOs.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderDetailsDto> OrderDetails { get; set; }
    }
}
