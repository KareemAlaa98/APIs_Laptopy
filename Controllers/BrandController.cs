using Laptopy_APIs.DTO;
using Laptopy_APIs.IRepository;
using Laptopy_APIs.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Laptopy_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        IBrandRepository brandRepository;

        public BrandController(IBrandRepository brandRepository)
        {
            this.brandRepository = brandRepository;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var brands = brandRepository.GetAll();
            if (brands != null)
            {
                // Mapping
                List<BrandDTO> brandsDTO = new List<BrandDTO>();
                foreach (var item in brands)
                {
                    BrandDTO brandDto = new BrandDTO();
                    brandDto.Id = item.Id;
                    brandDto.Name = item.Name;

                    brandDto.LaptopsInThisBrand = brandRepository.GetLaptopsInBrand(brandDto);
                    
                    brandsDTO.Add(brandDto);
                }
                return Ok(brandsDTO);
            }
            return NotFound();
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var brand = brandRepository.GetById(id);
            if (brand != null)
            {
                // mapping
                BrandDTO brandDto = new BrandDTO();
                brandDto.Id = brand.Id;
                brandDto.Name = brand.Name;
                brandDto.LaptopsInThisBrand = brandRepository.GetLaptopsInBrand(brandDto);
                return Ok(brandDto);
            }
            return NotFound();
        }


        [HttpPost("create")]
        public IActionResult Create(BrandDTO brandDto)
        {
            if (ModelState.IsValid)
            {
                brandRepository.Create(brandDto);
                brandDto.LaptopsInThisBrand = brandRepository.GetLaptopsInBrand(brandDto);
                return Created($"https://localhost:7131/api/Brand/{brandDto.Id}", brandDto);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("update")]
        public IActionResult Update(BrandDTO brandDto)
        {
            if (ModelState.IsValid)
            {
                var brand = brandRepository.GetById(brandDto.Id);
                if (brand != null)
                {
                    brandRepository.Edit(brandDto);
                    brandDto.LaptopsInThisBrand = brandRepository.GetLaptopsInBrand(brandDto);
                    return Ok(brandDto);
                }
                else { return NotFound("The requested brand wasn't found"); }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (brandRepository.Delete(id))
            {
                return Ok("Deleted Successfully");
            };
            return BadRequest("Couldnt Find the requested brand");
        }
    }
}
