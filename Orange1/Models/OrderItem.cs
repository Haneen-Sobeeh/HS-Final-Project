using System.ComponentModel.DataAnnotations.Schema;

namespace Orange1.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public Color ProductColor { get; set; }

        public Size ProductSize { get; set; }
    }
}
