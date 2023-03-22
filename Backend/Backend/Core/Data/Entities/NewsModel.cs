using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Core.Data.Entities
{
    [Table("News")]
    public class NewsModel
    {
        [Key]
        public string NewsId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string? Image { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("AspNetUsers")]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public UserModel? User { get; set; }
    }
}
