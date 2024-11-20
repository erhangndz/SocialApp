using Microsoft.EntityFrameworkCore;
using ServerApp.Models;

namespace ServerApp.Context
{
    public class SocialContext : DbContext
    {
        public SocialContext(DbContextOptions options):base(options){}

        public DbSet<Product> Products { get; set; }
    }
}
