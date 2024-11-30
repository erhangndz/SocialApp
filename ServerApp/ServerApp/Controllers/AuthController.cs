using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServerApp.Dtos.UserDtos;
using ServerApp.Models;
using ServerApp.Services;
using System.Security.Claims;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager,ITokenService _tokenService,IMapper _mapper) : ControllerBase
    {



        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserDto model)
        {
            var user = _mapper.Map<AppUser>(model);
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
                return NotFound( "User not found" );
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (!result.Succeeded)
            {
                return BadRequest("Email or Password incorrect!" );
            }

            var tokenResponse =await _tokenService.GenerateJwtToken(user);
            return Ok(tokenResponse);
        }



        
    }
}
