
namespace Laptopy_APIs.Models
{
    public class Laptop
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Model { get; set; }
        public int Price { get; set; }
        public int? DiscountPercentage { get; set; }
        public string? Description { get; set; }
        public double? Rating { get; set; }

        public List<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

        public int BrandId { get; set; }  
        public Brand Brand {  get; set; } 
    }
}
