using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Frontend.DTO.Request
{
    public class LoginRequestDTO
    {
        [Required(ErrorMessage = "Please Enter Your User Name")]
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter Your Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
