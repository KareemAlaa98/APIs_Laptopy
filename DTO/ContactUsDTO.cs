using System.ComponentModel.DataAnnotations;

namespace Laptopy_APIs.DTO
{
    public class ContactUsDTO
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only letters (lowercase or uppercase) and spaces are allowed.")]
        public string Name { get; set; }
        
        [Required]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$", ErrorMessage = "Please enter a valid email address. Example: john.doe@example.com")]
        public string Email { get; set; }
        
        [Required]
        public string Subject { get; set; }
        
        [Required]
        public string Message { get; set; }
    }
}
