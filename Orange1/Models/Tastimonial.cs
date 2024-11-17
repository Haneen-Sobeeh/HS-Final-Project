using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orange1.Models
{
    public class Tastimonial
    {
        [Key]
        public int Id { get; set; }
        public string CommenUser { get; set; }

        public string CommentText { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }

        public ApplicationUser? User { get; set; }


    }
}
