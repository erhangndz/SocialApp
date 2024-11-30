using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServerApp.Models;

namespace ServerApp.Context
{
    public class SocialContext : IdentityDbContext<AppUser,AppRole,int>
    {
        public SocialContext(DbContextOptions options):base(options){}

        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
