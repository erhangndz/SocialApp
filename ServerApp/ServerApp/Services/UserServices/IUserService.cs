using ServerApp.Dtos.UserDtos;
using ServerApp.Models;

namespace ServerApp.Services.UserServices
{
    public interface IUserService
    {
        Task<ResultUserDto> GetUserByIdAsync(int id);
        Task<IEnumerable<ResultUserListDto>> GetAllUsersAsync();

        Task<bool> UpdateUserAsync(UpdateUserDto model);
    }
}
