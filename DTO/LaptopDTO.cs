using Laptopy_APIs.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Laptopy_APIs.DTO
{
    public class LaptopDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Model { get; set; }
        public string? Description { get; set; }

        [Required]
        public int Price { get; set; }


        [Range(0, 99, ErrorMessage = "Discount percentage should be between 0 and 99")]
        [DefaultValue(0)]
        public int? DiscountPercentage { get; set; }


        [Range(0, 5, ErrorMessage ="Rate should be between 0 and 5")]
        [DefaultValue(0)]
        public double? Rating { get; set; }

        [Required]
        public int BrandId { get; set; }

        public string? BrandName { get; set; }
    }
}
