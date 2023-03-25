using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frontend.DTO.Response.Common;

namespace Frontend.DTO.Response.Advertisement
{
    public class AdvertisementResponseDTO
    {
        public AdvertisementResponseDTO()
        {
            Images = new List<ImageModelDTO>();
        }

        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "Please Enter The Title")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Please Enter The Content")]
        public string Content { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string UserId { get; set; }
        public UserResponseDTO? User { get; set; }

        public List<ImageModelDTO>? Images { get; set; }
    }
}
