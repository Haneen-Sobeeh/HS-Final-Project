using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Orange1.Models
{
    public class OrderProduct
    {
        [Key]
        public int OrderProductId { get; set; }

        // Foreign key to Order
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        // Foreign key to Product
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        // Quantity of the product in the order
        public int Quantity { get; set; }

        // Additional properties can be added here if needed
    }
}
