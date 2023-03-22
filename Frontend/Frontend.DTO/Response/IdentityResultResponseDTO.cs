using Frontend.DTO.Response.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.DTO.Response
{
    public class IdentityResultResponseDTO
    {
        public IdentityResultResponseDTO()
        {
            Errors = new List<Error>();
        }
        public bool Succeeded { get; set; }
        public List<Error> Errors { get; set; }
    }
}
