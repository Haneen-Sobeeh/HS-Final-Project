using System.ComponentModel.DataAnnotations.Schema;

namespace Orange1.Models
{
    public enum Color
    {
        Black,
        White,
        Red,
        Blue,
        Green
        // Add other colors as needed
    }

    public enum Size
    {
        XS,
        S,
        L,
        M,
        XL
        // Add other sizes as needed
    }

    public class ProductInCard
    {
		public int Id { get; set; }
		public string UserId { get; set; }

		[ForeignKey("UserId")]
		public ApplicationUser User { get; set; }

		// Foreign key to Product
		[ForeignKey("Product")]
		public int ProductId { get; set; }
		public virtual Product Product { get; set; }

		// Quantity of the product in the order
		public int Quantity { get; set; } // Change from string to int

		public DateOnly Date { get; set; }

		// Color of the product
		public Color ProductColor { get; set; }

		// Size of the product
		public Size ProductSize { get; set; }

		public bool IsConfirm { get; set; } = false;

	}
}
