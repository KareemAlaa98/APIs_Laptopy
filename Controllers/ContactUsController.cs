using Laptopy_APIs.DTO;
using Laptopy_APIs.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Laptopy_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        IContactUsRepository contactUsRepository;
        public ContactUsController(IContactUsRepository _contactUsRepository)
        {
            this.contactUsRepository = _contactUsRepository;
        }

        [HttpPost("contact")]
        public IActionResult ContactUs(ContactUsDTO contactDTO)
        {
            if(ModelState.IsValid)
            {
                contactUsRepository.SaveMessage(contactDTO);
                return Ok("Your message has been recieved! We'll be in touch as soon as we can.");
            }
            return BadRequest(ModelState);
        }
    }
}


