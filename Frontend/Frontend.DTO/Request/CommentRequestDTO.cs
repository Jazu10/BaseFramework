using Frontend.DTO.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.DTO.Request
{
    public class CommentRequestDTO
    {
        public int? CommentId { get; set; }
        public string PostId { get; set; }
        public string Comment { get; set; }
        public string UserId { get; set; }
        public UserResponseDTO? User { get; set; }
    }
}
