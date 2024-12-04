using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using ServerApp.Dtos.UserDtos;
using ServerApp.Services.UserServices;
using System.Security.Claims;

namespace ServerApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService _userService) : ControllerBase
    {

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if(user == null)
            {
                return NotFound("User Not Found");
            }
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserDto model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if(model.Id!=int.Parse(userId))
            {
                return BadRequest("Unauthorized Update Proccess");
            }

            var succeed = await _userService.UpdateUserAsync(model);
            if (!succeed)
            {
                return BadRequest("User Update Fail");
                
            }
            return Ok("User Update Successful");

        }
    }
}

