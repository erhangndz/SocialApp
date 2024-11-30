using ServerApp.Dtos.ImageDtos;
using System.Reflection.PortableExecutable;

namespace ServerApp.Dtos.UserDtos
{
    public class ResultUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }

        public int Age => DateTime.Now.Year - BirthDate.Year; 
        public DateTime CreatedDate { get; set; }
        public DateTime LastActive { get; set; }
        public string? City { get; set; } 
        public string? Country { get; set; }
        public string? About { get; set; }
        public string? Hobby { get; set; }

        public List<ResultImageDto> Images { get; set; }
    }
}
