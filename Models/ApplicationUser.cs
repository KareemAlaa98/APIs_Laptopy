using Microsoft.AspNetCore.Identity;

namespace Laptopy_APIs.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName {get;set;}
        public string LastName {get;set;}
    }
}
