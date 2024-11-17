namespace Orange1.Models
{
    public class OrderViewModel
    {
        public List<ProductInCartViewModel> CartItems { get; set; }
        public decimal GrandTotal { get; set; }
    }
}
