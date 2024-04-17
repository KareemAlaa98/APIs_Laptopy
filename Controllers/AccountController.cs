using Laptopy_APIs.DTO;
using Laptopy_APIs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Laptopy_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        UserManager<ApplicationUser> userManager;
        SignInManager<ApplicationUser> signInManager;
        public AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager)
        {
            this.userManager = _userManager;
            this.signInManager = _signInManager;
        }
        
        
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(ApplicationUserDTO userDto)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = userDto.UserName,
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    Email = userDto.Email,
                    PasswordHash = userDto.Password,
                };

                var result = await userManager.CreateAsync(user, userDto.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
                    return Ok($"Successfully Signed Up {userDto.UserName}");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return BadRequest(ModelState);
                }
            }
            return BadRequest(ModelState);
        }


        [HttpPost("signin")]
        public async Task <IActionResult> SignIn(UserLoginDTO userLoginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(userLoginDto.Email);
                if(user != null)
                {
                    var checkpassword = await userManager.CheckPasswordAsync(user, userLoginDto.Password);
                    if (checkpassword)
                    {
                        await signInManager.SignInAsync(user, userLoginDto.RememberMe);
                        return Ok($"Welcome {user.UserName}");
                    }
                    return BadRequest("Incorrect Password");
                }
                return NotFound($"User not found");
            }
            return BadRequest(ModelState);
        }
    }
}
