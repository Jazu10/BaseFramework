using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Core.Data.Entities
{
    [Table("Images")]
    public class ImageModel
    {
        [Key]
        public int ImageId { get; set; }

        [ForeignKey("Advertisements")]
        public string? AdvertisementId { get; set; }

        public string Image { get; set; }

        [ForeignKey("AdvertisementId")]
        public AdvertisementModel? Advertisement { get; set; }
    }
}
