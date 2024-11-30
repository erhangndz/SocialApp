using ServerApp.Dtos.UserDtos;
using ServerApp.Models;

namespace ServerApp.Dtos.ImageDtos
{
    public class ResultImageDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsProfile { get; set; }
       
       
    }
}
