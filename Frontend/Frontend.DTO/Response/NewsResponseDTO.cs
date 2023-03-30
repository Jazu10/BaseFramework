using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;



namespace Frontend.DTO.Response
{
    public class NewsResponseDTO
    {
        [Display(Name = "News Id")]
        public string? NewsId { get; set; }





        [Display(Name = "Created Date")]
        public DateTime CreatedAt { get; set; }





        [Required]
        [Display(Name = "News Title")]
        public string Subject { get; set; }





        [Required]
        [Display(Name = "News Content")]
        public string Content { get; set; }





        [Display(Name = "Image")]
        public string? Image { get; set; }




        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }






        public string? UserId { get; set; }



        [JsonIgnore]
        public IFormFile? ImgFiles { get; set; }
        public UserResponseDTO? User { get; set; }
    }
}