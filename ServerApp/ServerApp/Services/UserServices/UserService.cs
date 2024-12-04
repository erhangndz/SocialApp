using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServerApp.Context;
using ServerApp.Dtos.UserDtos;
using ServerApp.Models;

namespace ServerApp.Services.UserServices
{
    public class UserService(SocialContext _context,IMapper _mapper) : IUserService
    {
        public async Task<IEnumerable<ResultUserListDto>> GetAllUsersAsync()
        {
         var users =  await _context.Users.Include(x=>x.Images).ToListAsync();
            return _mapper.Map<List<ResultUserListDto>>(users);
        }

        public async Task<ResultUserDto> GetUserByIdAsync(int id)
        {
           var user =  await _context.Users.Include(x => x.Images).FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<ResultUserDto>(user);
        }

        public async Task<bool> UpdateUserAsync(UpdateUserDto model)
        {
            var updateModel = _mapper.Map<AppUser>(model);
            _context.Users.Update(updateModel);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
