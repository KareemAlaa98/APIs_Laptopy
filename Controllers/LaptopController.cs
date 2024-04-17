using Laptopy_APIs.Data;
using Laptopy_APIs.DTO;
using Laptopy_APIs.IRepository;
using Laptopy_APIs.Models;
using Laptopy_APIs.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Laptopy_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaptopController : ControllerBase
    {
        ILaptopRepository laptopRepo;
        ApplicationDbContext context;
        public LaptopController(ILaptopRepository laptopRepo, ApplicationDbContext context)
        {
            this.laptopRepo = laptopRepo;
            this.context = context;
        }


        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var laptops = laptopRepo.ReadAll();
            if(laptops != null)
            {
                // Mapping
                List<LaptopDTO> laptopDTOs = new List<LaptopDTO>();
                foreach(var item in laptops)
                {
                    LaptopDTO dto = new LaptopDTO();
                    dto.Id = item.Id;
                    dto.Name = item.Name;
                    dto.Model = item.Model;
                    dto.Price = item.Price;
                    dto.DiscountPercentage = item.DiscountPercentage;
                    dto.Description = item.Description;
                    dto.Rating = item.Rating;
                    dto.BrandId = item.BrandId;
                    dto.BrandName = laptopRepo.GetLaptopBrandName(dto);

                    laptopDTOs.Add(dto);
                }
                return Ok(laptopDTOs);
            }
            return NotFound();
        }

        [HttpGet("get/{id}")]
        public IActionResult GetById(int id)
        {
            var laptop = laptopRepo.ReadById(id);
            if (laptop != null)
            {
                LaptopDTO dto = new LaptopDTO();
                dto.Id = laptop.Id;
                dto.Name = laptop.Name;
                dto.Model = laptop.Model;
                dto.Price = laptop.Price;
                dto.DiscountPercentage = laptop.DiscountPercentage;
                dto.Description = laptop.Description;
                dto.Rating = laptop.Rating;
                dto.BrandId = laptop.BrandId;
                dto.BrandName = laptopRepo.GetLaptopBrandName(dto);

                return Ok(dto);
            }
            return NotFound();
        }

        [HttpPost("create")]
        public IActionResult Create(LaptopDTO laptopDTO)
        {
            if (ModelState.IsValid)
            {
                laptopRepo.Create(laptopDTO);
                laptopDTO.BrandName = laptopRepo.GetLaptopBrandName(laptopDTO);
                return Created($"https://localhost:7131/api/Laptop/{laptopDTO.Id}", laptopDTO);
            }
            return BadRequest();
        }

        [HttpPut("update")]
        public IActionResult Update(LaptopDTO laptopDTO)
        {
            if (ModelState.IsValid)
            {
                laptopRepo.Update(laptopDTO);
                laptopDTO.BrandName = laptopRepo.GetLaptopBrandName(laptopDTO);
                return Ok(laptopDTO);
            }
            return BadRequest();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (laptopRepo.Delete(id))
            {
                return Ok("Laptop Deleted");
            }
            return BadRequest("Couldn't Find a macthing laptop ");
        }

        [HttpGet("search/{search}")]
        public IActionResult Search(string search)
        {
            if(string.IsNullOrEmpty(search))
            {
                return BadRequest("Search query is required");
            }
            
            List<Laptop> searchResults = laptopRepo.Search(search);

            if (searchResults.Count > 0)
            {
                var searchResultsDTOs = new List<LaptopDTO>();
                foreach (var item in searchResults)
                {
                    LaptopDTO dto = new LaptopDTO();
                    dto.Id = item.Id;
                    dto.Name = item.Name;
                    dto.Model = item.Model;
                    dto.Price = item.Price;
                    dto.DiscountPercentage = item.DiscountPercentage;
                    dto.Description = item.Description;
                    dto.Rating = item.Rating;
                    dto.BrandId = item.BrandId;
                    dto.BrandName = laptopRepo.GetLaptopBrandName(dto);

                    searchResultsDTOs.Add(dto);
                }
                return Ok(searchResultsDTOs);
            }
            return NotFound();
        }

        [HttpGet("filter")]
        public IActionResult Filter(string? brandName = null, double? minPrice = null, double? maxPrice = null, int? rating = null)
        {
            try
            {
                var query = context.Laptops.AsQueryable();

                // // Brand Filter
                if (!string.IsNullOrEmpty(brandName))
                {
                    query = query.Where(e => e.Brand.Name == brandName);
                }

                // // Price Filter
                if (minPrice.HasValue && maxPrice.HasValue)
                {
                    if (minPrice >= maxPrice)
                    {
                        throw new Exception("Error: Min price must be less than Max price");
                    }
                    query = query.Where(e => e.Price >= minPrice && e.Price <= maxPrice);
                }
                else if (minPrice.HasValue)
                {
                    query = query.Where(e => e.Price >= minPrice);
                }
                else if (maxPrice.HasValue)
                {
                    query = query.Where(e => e.Price <= maxPrice);
                }


                // // Rating Filter
                if (rating.HasValue)
                {
                    if (rating > 5 || rating <=0)
                    {
                        throw new Exception("Rating should be between 1 and 5");
                    }
                    query = query.Where(e => e.Rating == rating);
                }

                
                List<Laptop> FilteredLaptops = query.ToList();
                
                return Ok(FilteredLaptops);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request: " + ex.Message);
            }
        }
    }   
}
