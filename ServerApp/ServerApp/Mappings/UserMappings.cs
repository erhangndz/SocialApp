using AutoMapper;
using ServerApp.Dtos.UserDtos;
using ServerApp.Helpers;
using ServerApp.Models;

namespace ServerApp.Mappings
{
    public class UserMappings: Profile
    {
        public UserMappings()
        {
            CreateMap<AppUser, ResultUserDto>().ForMember(dest => dest.Age, opt =>
                                                    opt.MapFrom(src => src.BirthDate.CalculateAge()));
            CreateMap<AppUser, ResultUserListDto>()
                                                    .ForMember(dest=>dest.Image,opt=>
                                                    opt.MapFrom(src=>src.Images.FirstOrDefault(x=>x.IsProfile)))
                                                    .ForMember(dest=> dest.Age,opt=>
                                                    opt.MapFrom(src=>src.BirthDate.CalculateAge()));
            CreateMap<AppUser, CreateUserDto>().ReverseMap();
            CreateMap<AppUser, LoginDto>().ReverseMap();
        }
    }
}
