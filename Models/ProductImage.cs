using System.ComponentModel.DataAnnotations;

namespace Laptopy_APIs.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        public int LaptopId { get; set; }
        public Laptop Laptop { get; set; }
    }
}
