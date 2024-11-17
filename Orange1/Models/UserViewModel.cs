namespace Orange1.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public int NumberOfProducts { get; set; }


        public decimal TotalPrice { get; set; }

        public List<Product> products { get; set; }
  
    }
}
