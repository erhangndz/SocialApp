using Microsoft.AspNetCore.Identity;

namespace ServerApp.Models
{
    public class AppUser: IdentityUser<int>
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastActive { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? About { get; set; }
        public string? Hobby { get; set; }

    }
}
