namespace Orange1.Models
{
    public class Categories
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Property to store the image path for the category
        public string? ImagePath { get; set; }
    }
}
