using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServerApp.Dtos.UserDtos;
using ServerApp.Models;
using ServerApp.Services;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager,TokenService _tokenService) : ControllerBase
    {



        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserDto model)
        {
            var user = new AppUser
            {
                Name = model.Name,
                Email = model.Email,
                BirthDate = model.BirthDate,
                Gender = model.Gender,
                UserName = model.UserName
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if(!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(model);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return NotFound("User not found");
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (!result.Succeeded)
            {
                return BadRequest("Email or Password incorrect!");
            }


            return Ok(new
            {
                token = _tokenService.GenerateJwtToken(user),
                userName= user.UserName,
                id= user.Id,
                email = user.Email,
              
            });
        }
    }
}
