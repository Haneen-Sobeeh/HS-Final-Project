namespace Orange1.Models
{
    public class ProductInCartViewModel
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

		public int OrderProductId { get; set; }
        public string ProductName { get; set; }
        public string ImagePath { get; set; }
        public decimal Price { get; set; }
        public string Quantity { get; set; }
        public string ProductColor { get; set; } // Display color as text
        public string ProductSize { get; set; } // Display size as text
        public decimal TotalPrice => Int32.Parse(Quantity) * Price;
        public DateOnly Date { get; set; }
    }

    public class ProductsViewModel
    {
        public List<ProductInCartViewModel> CartItems { get; set; }
        public bool IsEmpty => CartItems == null || !CartItems.Any();
        public decimal GrandTotal => CartItems?.Sum(item => item.TotalPrice) ?? 0;
        public decimal TotalPrice {  get; set; }
    }
}
