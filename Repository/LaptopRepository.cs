using Laptopy_APIs.Data;
using Laptopy_APIs.DTO;
using Laptopy_APIs.IRepository;
using Laptopy_APIs.Models;
using Microsoft.EntityFrameworkCore;

namespace Laptopy_APIs.Repository
{
    public class LaptopRepository : ILaptopRepository
    {
        ApplicationDbContext context;
        public LaptopRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<Laptop> ReadAll()
        {
            var laptops = context.Laptops.Include("Brand").ToList();

            if (laptops == null)
            {
                throw new Exception(" No Laptops found.");
            }
            return laptops;
        }

        public Laptop ReadById(int id)
        {
            var laptop = context.Laptops.Include("Brand").FirstOrDefault(e => e.Id == id);
            if (laptop == null)
            {
                throw new Exception("Laptop not found.");
            }
            return laptop;
        }

        public void Create(LaptopDTO lapDto)
        {
            if (lapDto != null)
            {
                var laptop = new Laptop()
                {
                    Name = lapDto.Name,
                    Model = lapDto.Model,
                    Price = lapDto.Price,
                    DiscountPercentage = lapDto.DiscountPercentage,
                    Description = lapDto.Description,
                    Rating = lapDto.Rating,
                    BrandId = lapDto.BrandId
                };

                context.Laptops.Add(laptop);
                context.SaveChanges();
                lapDto.Id = laptop.Id;
            }
        }

        public void Update(LaptopDTO lapDto)
        {
            if (lapDto != null)
            {
                Laptop? laptop = context.Laptops.Find(lapDto.Id);

                if (laptop == null)
                {
                    throw new Exception("Laptop not found for update.");
                }

                laptop.Name = lapDto.Name;
                laptop.Model = lapDto.Model;
                laptop.Price = lapDto.Price;
                laptop.DiscountPercentage = lapDto.DiscountPercentage;
                laptop.Description = lapDto.Description;
                laptop.Rating = lapDto.Rating;
                laptop.BrandId = lapDto.BrandId;

                context.SaveChanges();
            }
        }

        public bool Delete(int id)
        {
            Laptop? laptop = context.Laptops.Find(id);
            if (laptop != null)
            {
                context.Laptops.Remove(laptop);
                context.SaveChanges();
                return true;
            }
            return false;

        }

        public List<Laptop> Search(string search)
        {
            var results = context.Laptops.Include(e => e.Brand).Where(e => e.Name.Contains(search)).ToList();
            if (results != null)
            {
                return (results);
            }
            else
            {
                throw new Exception("No Results Found");
            }
        }

        public string GetLaptopBrandName(LaptopDTO dto)
        {
            return context.Brands.Where(e => e.Id == dto.BrandId).Select(e => e.Name).FirstOrDefault();
        }

    }
}
