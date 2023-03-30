using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frontend.DTO.Response
{
    public class UserResponseDTO
    {
        [DisplayName("User Id")]
        public string userId { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "Please Enter Your First Name")]
        public string firstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Please Enter Your Last Name")]
        public string lastName { get; set; }

        [DisplayName("Image")]
        public string? image { get; set; }

        [DisplayName("DOB")]
        [Required(ErrorMessage = "Please Select Your DOB")]
        public DateTime dob { get; set; }

        [DisplayName("DOJ")]
        [Required(ErrorMessage = "Please Select Your DOJ")]
        public DateTime doj { get; set; }

        [DisplayName("Gender")]
        [Required(ErrorMessage = "Please Select Your Gender")]
        public string gender { get; set; }

        [DisplayName("Address")]
        [Required(ErrorMessage = "Please Enter Your Address")]
        public string address { get; set; }

        public User? user { get; set; }

        public IFormFile? File { get; set; }
    }

    public class User
    {
        public string id { get; set; }

        [DisplayName("User Name")]
        public string userName { get; set; }
        public string normalizedUserName { get; set; }

        [DisplayName("Email Id")]
        [Required(ErrorMessage = "Please Enter Your Email ID")]
        [EmailAddress(ErrorMessage = "Please Enter A Valid Email Address")]
        public string email { get; set; }
        public string normalizedEmail { get; set; }
        public bool emailConfirmed { get; set; }
        public string passwordHash { get; set; }
        public string securityStamp { get; set; }
        public string concurrencyStamp { get; set; }

        [Display(Name = "Mobile Number")]
        [Required(ErrorMessage = "Please Enter A valid Mobile Number")]
        [MinLength(10, ErrorMessage = "Mobile Number Should Be Of 10 Digits")]
        [MaxLength(10, ErrorMessage = "Mobile Number Should Be Of 10 Digits")]
        public string phoneNumber { get; set; }
        public bool phoneNumberConfirmed { get; set; }
        public bool twoFactorEnabled { get; set; }
        public object lockoutEnd { get; set; }
        public bool lockoutEnabled { get; set; }
        public int accessFailedCount { get; set; }
    }
}

