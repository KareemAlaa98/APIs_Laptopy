using System.ComponentModel.DataAnnotations;

namespace Laptopy_APIs.DTO
{
    public class BrandDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<LaptopDTO>? LaptopsInThisBrand { get; set; }
    }
}
