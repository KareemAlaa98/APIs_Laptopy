using Laptopy_APIs.Data;
using Laptopy_APIs.DTO;
using Laptopy_APIs.IRepository;
using Laptopy_APIs.Models;
using Microsoft.EntityFrameworkCore;

namespace Laptopy_APIs.Repository
{
    public class ProductImageRepository : IProductImageRepository
    {
        ApplicationDbContext context;
        public ProductImageRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public string GetLaptopName(ProductImageDTO imgDto)
        {
            return context.Laptops.Where(e => e.Id == imgDto.LaptopId).Select(e => e.Name).FirstOrDefault();
        }

        public List<ProductImage> GetAll()
        {
            return context.ProductImages.Include("Laptop").ToList();
        }

        public ProductImage GetById(int id)
        {
            return context.ProductImages.FirstOrDefault(e=>e.Id == id);
        }

        public void Create(ProductImageDTO imageDTO)
        {
            if(imageDTO != null)
            {
                ProductImage newImg = new ProductImage()
                {
                    ImageUrl = imageDTO.ImageUrl,
                    LaptopId = imageDTO.LaptopId
                };
                context.ProductImages.Add(newImg);
                context.SaveChanges();
                imageDTO.Id = newImg.Id;
            }
        }
        public void Edit(ProductImageDTO imageDTO)
        {
            var img = GetById(imageDTO.Id);
            if(img != null)
            {
                img.ImageUrl = imageDTO.ImageUrl;
                img.LaptopId = imageDTO.LaptopId;
                
                context.SaveChanges();
            }
        }

        public bool Delete(int id)
        {
            var img = GetById(id);
            if (img != null)
            {
                context.ProductImages.Remove(img);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        
    }
}
