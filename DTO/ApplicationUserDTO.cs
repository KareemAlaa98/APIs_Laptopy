using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace Laptopy_APIs.DTO
{
    public class ApplicationUserDTO
    {
        //public int Id { get; set; }

        [Required]
        [RegularExpression(@"^\S*$", ErrorMessage = "Username cannot contain spaces.")]
        public string UserName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters (lowercase or uppercase) are allowed.")]
        public string FirstName { get; set; }


        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters (lowercase or uppercase) are allowed.")]
        public string LastName { get; set; }


        [Required]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$", ErrorMessage = "Please enter a valid email address. Example: john.doe@example.com")]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).*$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
