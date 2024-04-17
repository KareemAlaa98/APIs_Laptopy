using Laptopy_APIs.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Laptopy_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        RoleManager<IdentityRole> roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager) 
        {
            this.roleManager = roleManager;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(UserRoleDTO userRoleDto)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole(userRoleDto.Name);
                var result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return Ok($"The role {role.Name} has been created");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                        return BadRequest(ModelState);
                    }
                }
            }
            return BadRequest(ModelState);
        }
    }
}
