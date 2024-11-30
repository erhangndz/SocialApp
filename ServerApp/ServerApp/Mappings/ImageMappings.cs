using AutoMapper;
using ServerApp.Dtos.ImageDtos;
using ServerApp.Models;

namespace ServerApp.Mappings
{
    public class ImageMappings: Profile
    {
        public ImageMappings()
        {
            CreateMap<Image,ResultImageDto>().ReverseMap();
        }
    }
}
