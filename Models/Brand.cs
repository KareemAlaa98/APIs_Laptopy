namespace Laptopy_APIs.Models
{
    public class Brand
    {
        public int Id { get; set; } 
        public string Name { get; set; } 
        public List<Laptop> Laptops { get; set; } 
    }
}
