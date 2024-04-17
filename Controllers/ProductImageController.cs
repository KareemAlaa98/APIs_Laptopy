using Laptopy_APIs.DTO;
using Laptopy_APIs.IRepository;
using Laptopy_APIs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Laptopy_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        IProductImageRepository productImageRepository;

        public ProductImageController(IProductImageRepository productImageRepository)
        {
            this.productImageRepository = productImageRepository;   
        }


        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var imgs = productImageRepository.GetAll();
            if (imgs != null)
            {
                // Mapping
                List<ProductImageDTO> imgsDTO = new List<ProductImageDTO>();
                foreach (var item in imgs)
                {
                    ProductImageDTO imgDto = new ProductImageDTO();
                    imgDto.Id = item.Id;
                    imgDto.ImageUrl = item.ImageUrl;
                    imgDto.LaptopId = item.LaptopId;
                    imgDto.LaptopName = productImageRepository.GetLaptopName(imgDto);

                    imgsDTO.Add(imgDto);
                }
                return Ok(imgsDTO);

            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var img = productImageRepository.GetById(id);

            if (img != null)
            {
                // mapping
                ProductImageDTO imgDto = new ProductImageDTO();
                imgDto.Id = img.Id;
                imgDto.ImageUrl = img.ImageUrl;
                imgDto.LaptopId = imgDto.LaptopId;
                imgDto.LaptopName = productImageRepository.GetLaptopName(imgDto);
                return Ok(imgDto);
            }
            return NotFound();
        }

        [HttpPost("create")]
        public IActionResult Create(ProductImageDTO imgDto)
        {
            if (ModelState.IsValid)
            {
                productImageRepository.Create(imgDto);
                imgDto.LaptopName = productImageRepository.GetLaptopName(imgDto);
                return Created($"https://localhost:7131/api/ProductImage/{imgDto.Id}", imgDto);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("update")]
        public IActionResult Update(ProductImageDTO imgDto)
        {
            if (ModelState.IsValid)
            {
                var img = productImageRepository.GetById(imgDto.Id);
                if(img != null)
                {
                    productImageRepository.Edit(imgDto);
                    imgDto.LaptopName = productImageRepository.GetLaptopName(imgDto);
                    return Ok(imgDto);
                }
                else { return NotFound("The requested image wasn't found"); }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (productImageRepository.Delete(id))
            {
                return Ok("Deleted Successfully");
            };
            return BadRequest("Couldn't find the requested image");
        }
    }
}
