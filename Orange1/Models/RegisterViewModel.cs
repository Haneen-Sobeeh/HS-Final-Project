using System.ComponentModel.DataAnnotations;
namespace Orange1.Models
{
    public class RegisterViewModel
    {
        [Required]
        [MinLength(5, ErrorMessage = "Name Must be at Least 5 Character")]
        public string FullName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        public string PhoneNumber {  get; set; }

       
    }
}
