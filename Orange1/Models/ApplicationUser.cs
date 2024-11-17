using Microsoft.AspNetCore.Identity;

namespace Orange1.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }

        public string PhoneNumber {  get; set; }
    }
}
