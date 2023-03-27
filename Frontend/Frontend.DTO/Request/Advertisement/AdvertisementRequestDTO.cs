<<<<<<< HEAD
﻿using System;
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
=======
﻿using System;
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
>>>>>>> da45c59eb01f2ebd994edd0f15a3fad5fd26b7f9
