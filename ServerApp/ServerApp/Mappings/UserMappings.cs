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
                                                opt.MapFrom(src => src.BirthDate.CalculateAge()))
                                               .ForMember(dest=>dest.ProfileImage,
                                                opt=>opt.MapFrom(src=>src.Images.FirstOrDefault(x=>x.IsProfile).Name))
                                               .ForMember(x=>x.Images,opt=>opt.MapFrom(src=>src.Images.Where(x=>x.IsProfile==false)));
            CreateMap<AppUser, ResultUserListDto>()
                                                   .ForMember(dest=>dest.ProfileImage,opt=>
                                                    opt.MapFrom(src=>src.Images.FirstOrDefault(x=>x.IsProfile).Name))
                                                    .ForMember(dest=> dest.Age,opt=>
                                                    opt.MapFrom(src=>src.BirthDate.CalculateAge()));
            CreateMap<AppUser, CreateUserDto>().ReverseMap();
            CreateMap<AppUser, LoginDto>().ReverseMap();
            CreateMap<AppUser, UpdateUserDto>().ReverseMap();
        }
    }
}
