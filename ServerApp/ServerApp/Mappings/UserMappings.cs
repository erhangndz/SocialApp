using AutoMapper;
using ServerApp.Dtos.UserDtos;
using ServerApp.Models;

namespace ServerApp.Mappings
{
    public class UserMappings: Profile
    {
        public UserMappings()
        {
            CreateMap<AppUser, ResultUserDto>().ReverseMap();
            CreateMap<AppUser, CreateUserDto>().ReverseMap();
            CreateMap<AppUser, LoginDto>().ReverseMap();
        }
    }
}
