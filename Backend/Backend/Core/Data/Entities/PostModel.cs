using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Core.Data.Entities
{
    [Table("Posts")]
    public class PostModel
    {
        [Key]
        public string PostId { get; set; }
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "Please Enter The Subject")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Please Enter The Content")]
        public string Content { get; set; }
        public string? Image { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("Users")]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public UserModel? User { get; set; }
    }
}
