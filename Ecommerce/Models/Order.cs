namespace Ecommerce.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        // علاقة 1 إلى عدة مع تفاصيل الطلب
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
