using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Backend.Core.Data.Entities
{
    [Table("Users")]
    public class UserModel
    {
        [Key]
        [ForeignKey("AspNetUsers")]
        public string? UserId { get; set; }

        [Required(ErrorMessage = "Please Enter Your First Name")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Please Enter Your Last Name")]
        public string LastName { get; set; }

        public string? Image { get; set; }

        //[Required]
        //[Range(1, int.MaxValue, ErrorMessage = "Please select a Position")]
        //[DisplayName("Position")]
        //public int PositionId { get; set; }


        [Required(ErrorMessage = "Please Select Your DOB")]
        public DateTime DOB { get; set; }

        public DateTime? DOJ { get; set; }

        [Required(ErrorMessage = "Please Select Your Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Please Enter Your Address")]
        public string? Address { get; set; }




        [NotMapped]
        public string UserName { get; set; }

        [NotMapped]
        public string EmailId { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Please Enter A Valid Mobile Number")]
        [MinLength(10, ErrorMessage = "Mobile Number Should Be Of 10 Digits")]
        [MaxLength(10, ErrorMessage = "Mobile Number Should Be Of 10 Digits")]
        public string PhoneNumber { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please Enter Your Password")]
        public string Password { get; set; }

        [NotMapped]
        [DisplayName("Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords Doesn't Match")]
        public string ConfirmPassword { get; set; }

        [ForeignKey("UserId")]
        public IdentityUser? User { get; set; }

        [NotMapped]
        [JsonIgnore]
        public IdentityRole? Role { get; set; }
    }
}
