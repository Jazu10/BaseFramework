using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frontend.DTO.Request
{
    public class RegistrationRequestDTO
    {
        public string? UserId { get; set; }

        [DisplayName("User Name")]
        [Required(ErrorMessage = "Please Enter Your User Name")]
        public string UserName { get; set; }

        [DisplayName("Email Id")]
        [Required(ErrorMessage = "Please Enter Your Email ID")]
        [EmailAddress(ErrorMessage = "Please Enter A Valid Email Address")]
        public string EmailId { get; set; }

        [Display(Name = "Mobile Number")]
        [Required(ErrorMessage = "Please Enter A valid Mobile Number")]
        [MinLength(10, ErrorMessage = "Mobile Number Should Be Of 10 Digits")]
        [MaxLength(10, ErrorMessage = "Mobile Number Should Be Of 10 Digits")]
        public string PhoneNumber { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "Please Enter Your First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Please Enter Your Last Name")]
        public string LastName { get; set; }
        public string ? Image { get; set; }

        [DisplayName("Date of Birth")]
        [Required(ErrorMessage = "Please Select Your DOB")]
        public DateTime DOB { get; set; }

        [DisplayName("Date of Joining")]
        [Required(ErrorMessage = "Please Select Your DOJ")]
        public DateTime DOJ { get; set; }

        [DisplayName("Gender")]
        [Required(ErrorMessage = "Please Select Your Gender")]
        public string Gender { get; set; }

        [DisplayName("Address")]
        [Required(ErrorMessage = "Please Enter Your Address")]
        public string Address { get; set; }

        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please Enter Your password")]
        public string Password { get; set; }

        [DisplayName("Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords Doesn't Match")]
        public string ConfirmPassword { get; set; }

        public IFormFile ? File { get; set; }
    }
}
