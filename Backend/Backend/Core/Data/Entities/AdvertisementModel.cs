using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;

namespace Backend.Core.Data.Entities
{
    [Table("Advertisements")]
    public class AdvertisementModel
    {
        public AdvertisementModel()
        {
            Images = new List<ImageModel>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? AdvertisementId { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "Please Enter The Title")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Please Enter The Content")]
        public string Content { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("AspNetUsers")]
        public string UserId { get; set; }
        
        [ForeignKey("UserId")]
        public UserModel? User { get; set; }

        [NotMapped]
        public List<ImageModel>? Images { get; set; }
    }
}
