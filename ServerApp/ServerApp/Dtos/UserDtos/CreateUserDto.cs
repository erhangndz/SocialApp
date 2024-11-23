using System.ComponentModel.DataAnnotations;

namespace ServerApp.Dtos.UserDtos
{
    public class CreateUserDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        public DateTime CreatedDate { get; set; }= DateTime.Now;
        public DateTime LastActive { get; set; }= DateTime.Now;
    }
}
