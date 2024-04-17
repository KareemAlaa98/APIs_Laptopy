using System.ComponentModel.DataAnnotations;

namespace Laptopy_APIs.DTO
{
    public class UserLoginDTO
    {
        [Required]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$", ErrorMessage = "Please enter a valid email address. Example: john.doe@example.com")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
