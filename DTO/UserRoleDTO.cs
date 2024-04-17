using System.ComponentModel.DataAnnotations;

namespace Laptopy_APIs.DTO
{
    public class UserRoleDTO
    {
        //public int Id { get; set; }

        [Required]
        public string Name { get; set; } 
    }
}
