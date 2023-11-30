using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Seed
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<ShopUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new ShopUser
                {
                    FirstName = "Tamara",
                    LastName = "Stanikevska",
                    Email = "tamara@test.com",
                    UserName = "tamara.stanikevska",
                    Address = "The street 100"
                };

                await userManager.CreateAsync(user, "Test123!");
            }
        }
    }
}
