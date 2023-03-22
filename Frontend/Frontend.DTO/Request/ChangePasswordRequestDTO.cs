using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Frontend.DTO.Request
{
    public class ChangePasswordRequestDTO
    {
        public string? UserId { get; set; }

        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please Enter Your Current password")]
        public string Password { get; set; }

        [DisplayName("New Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please Enter Your New Password")]
        public string NewPassword { get; set; }

        [DisplayName("Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords Doesn't Match")]
        public string ConfirmPassword { get; set; }
    }
}
