using Orange1.Models;
using System.ComponentModel.DataAnnotations.Schema;
namespace Orange1.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Categories? Category { get; set; }    
        public string ProductName { get; set; }
        public string Condition { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public ApplicationUser? User { get; set; }

        [ForeignKey("User")]
        public string? UserId { get; set; }

        // New property for image path
        public string? ImagePath { get; set; }
     
    }
}