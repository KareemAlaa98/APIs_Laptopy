using Laptopy_APIs.Data;
using Laptopy_APIs.DTO;
using Laptopy_APIs.IRepository;
using Laptopy_APIs.Models;
using Microsoft.EntityFrameworkCore;

namespace Laptopy_APIs.Repository
{
    public class BrandRepository : IBrandRepository
    {
        ApplicationDbContext context;
        ILaptopRepository laptopRepository;
        public BrandRepository(ApplicationDbContext context, ILaptopRepository laptopRepository)
        {
            this.context = context;
            this.laptopRepository = laptopRepository;
        }

        public List<Brand> GetAll()
        {
            return context.Brands.Include("Laptops").ToList();
        }

        public Brand GetById(int id)
        {
            return context.Brands.Find(id);
        }

        public void Create(BrandDTO brandDto)
        {
            if (brandDto != null)
            {
                Brand newBrand = new Brand()
                {
                    Name = brandDto.Name,
                };
                context.Brands.Add(newBrand);
                context.SaveChanges();
                brandDto.Id = newBrand.Id;
            }
        }

        public void Edit(BrandDTO brandDto)
        {
            var brand = GetById(brandDto.Id);
            if (brand != null)
            {
                brand.Name = brandDto.Name;
                context.SaveChanges();
            }
        }

        public bool Delete(int id)
        {
            var brand = GetById(id);
            if (brand != null)
            {
                context.Brands.Remove(brand);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<LaptopDTO> GetLaptopsInBrand(BrandDTO brandDto)
        {
            return context.Laptops
               .Where(e => e.BrandId == brandDto.Id)
               .Select(e => new LaptopDTO { 
                   Id = e.Id, 
                   Name = e.Name, 
                   Model = e.Model,
                   Description = e.Description,
                   Price = e.Price,
                   DiscountPercentage = e.DiscountPercentage,
                   Rating  = e.Rating,
                   BrandId = e.BrandId,
                   BrandName = brandDto.Name,
               })
               .ToList();
        }
    }
}
