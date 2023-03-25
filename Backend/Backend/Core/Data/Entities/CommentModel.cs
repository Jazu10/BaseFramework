using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Core.Data.Entities
{
    [Table("Comments")]
    public class CommentModel
    {
        [Key]
        public int? CommentId { get; set; }
        public string PostId { get; set; }
        public string Comment { get; set; }

        [ForeignKey("Users")]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public UserModel? User { get; set; }

        [ForeignKey("PostId")]
        public PostModel Post { get; set; }
    }
}
