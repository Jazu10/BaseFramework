using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace Frontend.DTO.Response
{
    public class PostResponseDTO
    {
        public string? PostId { get; set; }
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "Please Enter The Subject")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Please Enter The Content")]
        public string Content { get; set; }
        public string? Image { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string UserId { get; set; }
        public UserResponseDTO? User { get; set; }
        public bool IsLiked { get; set; }
        public int LikeCount { get; set; }
        [JsonIgnore]
        public IFormFile? ImgFiles { get; set; }
    }
}
