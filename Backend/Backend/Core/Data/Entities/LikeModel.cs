using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Core.Data.Entities
{
    [Table("Likes")]
    public class LikeModel
    {
        [ForeignKey("AspNetUsers")]
        public string UserId { get; set; }
        public string ContentId { get; set; }
    }
}
