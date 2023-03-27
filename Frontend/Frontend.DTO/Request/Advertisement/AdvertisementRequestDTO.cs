using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frontend.DTO.Response.Advertisement;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace Frontend.DTO.Request.Advertisement
{
    public class AdvertisementRequestDTO : AdvertisementResponseDTO
    {
        [JsonIgnore]
        public List<IFormFile>? ImgFiles { get; set; }

    }
}
