using Laptopy_APIs.Models;
using System.ComponentModel.DataAnnotations;

namespace Laptopy_APIs.DTO
{
    public class ProductImageDTO
    {
        public int Id { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public int LaptopId { get; set; }
        public string LaptopName { get; set; }
    }
}
