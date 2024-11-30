using Microsoft.AspNetCore.Identity;
using ServerApp.Models;
using System.Text.Json;

namespace ServerApp.Data
{
    public static class SeedDatabase
    {

        public static async Task Seed(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var users = File.ReadAllText("Data/users.json");
                var listOfUsers= JsonSerializer.Deserialize<List<AppUser>>(users);
                foreach (var user in listOfUsers)
                {
                    await userManager.CreateAsync(user, "Password12*");
                }

            }
        }
    }
}
