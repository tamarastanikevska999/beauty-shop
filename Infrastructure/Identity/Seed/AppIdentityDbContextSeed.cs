using Core.Entities;
using Core.Entities.Identity;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Seed
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<ShopUser> userManager, StoreContext context)
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

                var basket = new CustomerBasket
                {
                    UserEmail = user.Email,
                };
                context.CustomerBaskets.Add(basket);

                await userManager.CreateAsync(user, "Test123!");
            }
        }
    }
}
