namespace Ecommerce.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // علاقة 1 إلى عدة مع المنتجات
        public ICollection<Product> Products { get; set; }
    }
}
