using System.ComponentModel.DataAnnotations.Schema;

namespace Orange1.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public bool IsConfirmed { get; set; } = false;

        public decimal TotalPrice { get; set; }

        // Collection of order items
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
